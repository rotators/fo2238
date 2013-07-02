// Author: Wipe/WHINE Team
//
//	VTDB v0.3, savefile v2
//

#include <stdio.h>
#include "online_stats.h"
#include "../parameters/parameters.cpp"

#ifdef _MSC_VER
#define sprintf	sprintf_s
#endif

//
// c/p fest, parameters.fos
//
/*#define MAX(a,b)			(((a)>(b))?(a):(b))
#define LBS_TO_GRAMM(lbs)	((lbs)*453)
#define StatBase			Params
#define Stat				Params
#define Trait				Params*/

/*
 * save block of params in file as: @key = [save];
 *	if value == 0 or 0x80000000 - skip
 *	if value == 1 - save as: [param_number]
 *	anything else - save as: [param_number]:[param_value]
 */
void SaveParamBlock( FILE *f, char *key, Critter& player, uint begin, uint end )
{
	bool found = false;
	for( uint param = begin; param <= end; param++ )
	{
		if( player.Params[param] != 0 &&
			player.Params[param] != 0x80000000 )
		{
			if( !found )
			{
				found = true;
				fprintf( f, "@%s =", key );
			}
			fprintf( f, " %d", param );
			if( player.Params[param] != 1 )
				fprintf( f, ":%d", player.Params[param] );
		};
	};
	if( found )
		fprintf( f, ";\n" );
};

EXPORT void OnlineStats_CharDelete( Critter& player, ScriptString& directory )
{
	FILE *f;
	char filename[50];
	int n;

	// v1 //
	n = sprintf( filename, "%s/%d.php", directory.c_str(), player.Id );
#ifdef _MSC_VER
	fopen_s(&f, filename, "r");
#else
	f=fopen( filename, "r" )
#endif
	if(f)
	{
		fclose( f );
		if( remove( filename ) == -1 )
		{
			// TODO?
		};
	};

	// v2+ //
	n = sprintf( filename, "%s/%d.vtdb", directory.c_str(), player.Id );
#ifdef _MSC_VER
	fopen_s(&f, filename, "r");
#else
	f=fopen( filename, "r" )
#endif
	if(f)
	{
		fclose( f );
		if( remove( filename ) == -1 )
		{
			// TODO?
		};
	};
};

EXPORT void OnlineStats_CharSave( Critter& player, int settings, ScriptString& directory )
{
	if( !settings )
		return;

	char filename[50];
	int n = sprintf( filename, "%s/%d.vtdb", directory.c_str(), player.Id );

	FILE *f;
#ifdef _MSC_VER
	fopen_s(&f, filename, "w");
#else
	f=fopen( filename, "w" )
#endif
	if(f)
	{
		/*** v2 ***/
		fprintf( f, "/* Generated automagically, DO NOT EDIT */\n\n"
			"@version = %d;\n"
			"@name = %s;\n@gender = %d;\n@age = %d;\n\n",
			ONLINE_STATS_CHARVER,

			player.NameStr.c_str(),
			player.Params[ST_GENDER],
			player.Params[ST_AGE] );

		if( FLAG( settings,OS_SPECIAL ) || FLAG( settings,OS_ALL ))
		{
			fprintf( f, "@special = %d %d %d %d %d %d %d;\n",
				player.Params[ST_STRENGTH],
				player.Params[ST_PERCEPTION],
				player.Params[ST_ENDURANCE],
				player.Params[ST_CHARISMA],
				player.Params[ST_INTELLECT],
				player.Params[ST_AGILITY],
				player.Params[ST_LUCK] );
		};

		if( FLAG( settings,OS_XP ) || FLAG( settings,OS_ALL ))
		{
			fprintf( f, "@xp = %d %d %d;\n",
				player.Params[ST_LEVEL],
				player.Params[ST_EXPERIENCE],
				player.Params[ST_LEVEL]*(player.Params[ST_LEVEL]+1)*500 );
		};

		if( FLAG( settings,OS_LIFE ) || FLAG( settings,OS_ALL ))
		{
			fprintf( f, "@life = %d %d %d %d %d %d %d %d %d;\n", 
				player.Params[ST_CURRENT_HP],
				getParam_MaxLife( player, -1 ), //player.Params[ST_MAX_LIFE],
				player.Params[DAMAGE_POISONED],
				player.Params[DAMAGE_RADIATED],
				player.Params[DAMAGE_EYE],
				player.Params[DAMAGE_RIGHT_ARM],
				player.Params[DAMAGE_LEFT_ARM],
				player.Params[DAMAGE_RIGHT_LEG],
				player.Params[DAMAGE_LEFT_LEG] );
		};

		if( FLAG( settings,OS_STATS ) || FLAG( settings,OS_ALL ))
		{
			fprintf( f, "@stats = %d %d %d %d %d %d %d %d %d %d;\n",
				GetRunningAc( player, false ),
				getParam_MaxAp( player, -1 ),
				getParam_MaxWeight( player, -1 ),
				getParam_MeleeDmg( player, -1 ),
				player.Params[ST_NORMAL_RESIST],
				getParam_PoisonResist( player, -1 ),
				getParam_RadiationResist( player, -1 ),
				getParam_Sequence( player, -1 ),
				getParam_HealingRate( player, -1 ),
				getParam_CriticalChance( player, -1 ));
		};

		// don't use SaveParamBlock here, we need tagged skills marked
		if( FLAG( settings,OS_SKILLS ) || FLAG( settings,OS_ALL ))
		{
			fprintf( f, "@skills = " );
			for( uint skill = SKILL_BEGIN; skill <= SKILL_END; skill++ )
			{
				bool tagged = false;
				for( uint tag = TAG_BEGIN; tag <= TAG_END; tag++ )
				{
					if( player.Params[tag] == skill )
					{
						tagged = true;
						break;
					};
				};
				fprintf( f, "%s%d ", tagged ? "+" : "", player.Params[skill] );
			};
			fprintf( f, "%d;\n", player.Params[ST_UNSPENT_SKILL_POINTS] );
		};

		/*** v2 ***/
		if( FLAG( settings,OS_TRAITS) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "traits", player, TRAIT_BEGIN, TRAIT_END );

		if( FLAG( settings,OS_PERKS ) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "perks", player, PERK_BEGIN, PERK_END );

		if( FLAG( settings,OS_KARMA ) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "karma", player, KARMA_BEGIN, KARMA_END );

		if( FLAG( settings,OS_ADDICTIONS ) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "addictions", player, ADDICTION_BEGIN, ADDICTION_END );

		if( FLAG( settings,OS_REPUTATION ) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "reputation", player, REPUTATION_BEGIN, REPUTATION_END );

		if( FLAG( settings,OS_KILLS ) || FLAG( settings,OS_ALL) )
			SaveParamBlock( f, "kills", player, KILL_BEGIN, KILL_END );

		if( FLAG( settings,OS_POSITION ))
			fprintf( f, "@position = %d %d;\n", player.WorldX, player.WorldY );

		// TODO: Log()

		fclose( f );
	};
};
