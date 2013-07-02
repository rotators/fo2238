#include <cmath>
#include "Move.h"
#include "WallDist.h"
#include "../../scripts/_animation.fos"
#include <sstream>
#include <string>
#include <vector>

using namespace std;

// TODO: multihex bonus to look distance (once multihex works properly)

bool isCompiler=false;
bool Init=false;

extern uint8* Lookup;
extern void InitLookup();
extern void FinishLookup();

extern void InitDists();
extern WallDist* GetProtoDists(Map& map);
extern void FinishDists();
extern bool IsMoving(Critter& cr);

void InitLook() // generation
{
	InitLookup();
	InitDists();
	Init=true;
}

int __stdcall DllMain(void* module, unsigned long reason, void* reserved)
{
	switch(reason)
	{
	case 1: // Process attach
		break;
	case 2: // Thread attach
		break;
	case 3: // Thread detach
		break;
	case 0: // Process detach
		{
			if(!isCompiler)
			{
				FinishLookup();
				FinishDists();
			}
		}
		break;
	}
	return 1;
}

FONLINE_DLL_ENTRY(compiler)
{
	isCompiler=compiler;
}

int CheckOccluder(uint16 hx, uint16 hy, uint16 tx, uint16 ty, Map& map)
{
    int dx = (int)tx - (int)hx;
    int dy = (int)ty - (int)hy;
    if ((ABS(dx)>=MAX_TRACE) || (ABS(dy)>=MAX_TRACE)) return 0;
    int idx = (SIZE_LIN*(dy+MAX_TRACE)+dx+MAX_TRACE)*MAX_ARR;
    uint16 maxX = map.Proto->Header.MaxHexX;
    uint16 maxY = map.Proto->Header.MaxHexY;
    for (uint i=0;i<OCCLUDER_DIST;i++)
    {
        Move(hx,hy,Lookup[idx++]);
        if ((hx>=maxX) || (hy>=maxY)) return 0; // this include uint16(-1), it's not nice, but practical

		// you can shoot thru
		if(map.IsHexRaked(hx, hy))
		{
			// but not pass - that means occluder
			if(!map.IsHexPassed(hx, hy))
                return BONUS_OCCLUDER;// optimize it that way for now
			else
				continue; // clean
		} // you can't shoot through it
		else
			return 100; // shouldn't even occur (trace check first)
	}
    return 0;
}

int TraceWall(uint16 hx, uint16 hy, uint16 tx, uint16 ty, Map& map, int dist)
{
    // if (dist==0) dist=GetDistantion(hx,hy,tx,ty); // not needed here; enable if and when needed
    int dx = (int)tx - (int)hx;
    int dy = (int)ty - (int)hy;
    if ((ABS(dx)>=MAX_TRACE) || (ABS(dy)>=MAX_TRACE)) return 0;
    int idx = (SIZE_LIN*(dy+MAX_TRACE)+dx+MAX_TRACE)*MAX_ARR;
    uint16 maxX = map.Proto->Header.MaxHexX;
    uint16 maxY = map.Proto->Header.MaxHexY;
    for (int i=0;i<dist;i++)
    {
        Move(hx,hy,Lookup[idx++]);
        if ((hx>=maxX) || (hy>=maxY)) return i; // this include uint16(-1), it's not nice, but practical
        if(!map.IsHexRaked(hx, hy)) return i;
	}
    return dist;
}

EXPORT bool check_look(Map& map, Critter& cr, Critter& opponent)
{
	if(!Init) InitLook();
	if(_CritHasExtMode(opponent,MODE_EXT_GOD)) return false;

	if(!cr.CritterIsNpc)
	{
		if(_CritHasExtMode(cr,MODE_EXT_LOOK_ADMIN)) return true;
		if(cr.MapId != map.Data.MapId && map.Data.UserData[MAP_DATA_ACTIVE_COUNTDOWN]!=0) return false;
	}
	if(_CritHasExtMode(opponent,MODE_EXT_LOOK_INVISIBLE) && _CritHasMode(opponent, MODE_HIDE))
        return false; // 100% invis for admins

	if(_CritHasExtMode(opponent,MODE_EXT_LOOK_ALWAYS_VISIBLE) && !_CritHasMode(opponent, MODE_HIDE))
        return true;

	uint16 cx = cr.HexX;
	uint16 cy = cr.HexY;
	uint16 ox = opponent.HexX;
	uint16 oy = opponent.HexY;

	int dist = GetDistantion(cx, cy, ox, oy);
	if(dist>60) return false;

	if(dist<=cr.ItemSlotExt->Proto->MagicPower || dist<=cr.ItemSlotMain->Proto->MagicPower) return true;

	// min range - always visible
	if(dist <= (int)(FOnline->LookMinimum)) return true;

	// dead/unconcious/neg hp - only minimum range
	if(cr.Cond != COND_LIFE) return (dist <= (int)(FOnline->LookMinimum));

    int front_range=(cr.Params[DAMAGE_EYE]!=0)?1:(CLAMP((cr.Params[ST_PERCEPTION]+cr.Params[ST_PERCEPTION_EXT]),1,10));
	if(cr.Params[PE_SHARPSHOOTER]) front_range+=2*cr.Params[PE_SHARPSHOOTER];
    front_range*=3;
    front_range+= cr.Params[ST_BONUS_LOOK];
	front_range+=(int)(FOnline->LookNormal);

	if(dist > front_range) return false;

	// max perception range
	int max_range = front_range;

	// transform direction from critter A to critter B into "character coord-space"
	uint8 dir = (uint8)GetDirection(cx, cy, ox, oy);

	dir = cr.Dir>dir?cr.Dir-dir:dir-cr.Dir;

    // adjust distance based on fov (NOT only for sneakers)
    switch(dir)
    {
        case 0:
            max_range -= (max_range* (int)(FOnline->LookDir[0]))/100; // front
            break;
        case 1:
        case 5:
            max_range -= (max_range* (int)(FOnline->LookDir[1]))/100; // frontsides
            break;
        case 2:
        case 4:
            max_range -= (max_range* (int)(FOnline->LookDir[2]))/100; // backsides
            break;
        default:
            max_range -= (max_range* (int)(FOnline->LookDir[3]))/100; // back
    }

	if(dist > max_range) return false;

	if(_CritHasMode(opponent, MODE_HIDE))
	{
		// wall gives success
		if (!_CritHasExtMode(cr,MODE_EXT_NO_WALL_CHECK))
		{
			max_range = TraceWall(cx, cy, ox, oy, map, max_range); // in case wall is blocking
			if(dist > max_range) return false;
		}

		int sk = opponent.Params[SK_SNEAK];

		// bonuses before clamp

		// 1. next to a wall
		WallDist* wd=GetProtoDists(map);
		if(wd->distances[oy*wd->proto->Header.MaxHexX+ox] <=
			(opponent.Params[PE_GHOST] ? 5 : 1)) sk+=BONUS_WALL;

		// 2. occluder bonus
		if (!_CritHasExtMode(cr,MODE_EXT_NO_WALL_CHECK))
			sk += CheckOccluder(ox, oy, cx, cy, map);

		// 3. stealth boy
		if(opponent.Params[ST_SNEAK_FLAGS]&4) sk+=BONUS_STEALTH_BOY;
		
		// 4. night TODO?

		// clamp
		sk=CLAMP(sk,-300,300);

		switch(dir)
        {
            case 0:
                sk -= (int)(FOnline->LookSneakDir[0]); // front
                break;
            case 1:
            case 5:
                sk -= (int)(FOnline->LookSneakDir[1]); // frontsides
                break;
            case 2:
            case 4:
                sk -= (int)(FOnline->LookSneakDir[2]); // backsides
                break;
            default: ;
                sk -= (int)(FOnline->LookSneakDir[3]); // back
        }

		// armor penalty, TODO: move values to protos?
		switch(opponent.ItemSlotArmor->GetProtoId())
		{
			case PID_METAL_ARMOR:
			case PID_METAL_ARMOR_MK_II:
			case PID_TESLA_ARMOR:
				sk+=BONUS_ARMOR_METAL;
				break;
			case PID_COMBAT_ARMOR:
			case PID_COMBAT_ARMOR_MK_II:
			case PID_BROTHERHOOD_COMBAT_ARMOR:
				sk+=BONUS_ARMOR_COMBAT;
				break;
			case PID_POWERED_ARMOR :
			case PID_HARDENED_POWER_ARMOR:
			case PID_ADVANCED_POWER_ARMOR :
			case PID_ADVANCED_POWER_ARMOR_MK2:
				sk=-10000;
				break;
			default: ;
		}

		// weapons penalty
		const ProtoItem* proto=opponent.ItemSlotMain->Proto;
		if(proto->Type==ITEM_TYPE_WEAPON && FLAG(proto->Flags,ITEM_TWO_HANDS))
		{
			switch(proto->Weapon_Anim1)
			{
			case ANIM1_HEAVY_RIFLE:
			case ANIM1_MINIGUN:
			case ANIM1_ROCKET_LAUNCHER:
			case ANIM1_FLAMER:
				sk+=BONUS_WEAPON_HEAVY;
				break;
			default:
				if(proto->Weapon_Skill[0]<=SK_ENERGY_WEAPONS) sk+=BONUS_WEAPON_RIFLE;
				break;
			}
		}

		// running
		if(IsMoving(opponent) && opponent.IsRuning && !opponent.Params[PE_SILENT_RUNNING]) sk+=BONUS_RUNNING;

		// active explosive held
		if(opponent.Params[ST_SNEAK_FLAGS]&1) sk+=BONUS_ACTIVE_EXPLOSIVES;

		if(sk <= 0)	return true;

        sk/=(int)(FOnline->SneakDivider);
        return front_range >= dist+sk;
	}
	else // opponent doesn't sneak
	{
	  if (!cr.CritterIsNpc)
	  {
			max_range = TraceWall(cx, cy, ox, oy, map, max_range); // in case wall is blocking
			return dist <= max_range; // behind a wall
	  }
	  return true; // max pe range handled before
	}
}

int GetEngineLook(Critter& cr)
{
	int look=(cr.Params[DAMAGE_EYE]!=0)?1:(CLAMP((cr.Params[ST_PERCEPTION]+cr.Params[ST_PERCEPTION_EXT]),1,10));
    look*=3;
    look+= cr.Params[ST_BONUS_LOOK];
	look+=(int)(FOnline->LookNormal);
    if( look < (int) FOnline->LookMinimum )
        look = FOnline->LookMinimum;
    return look;
}

EXPORT bool check_trap_look(Map& map, Critter& cr, Item& trap)
{
	int dist=GetDistantion(cr.HexX,cr.HexY,trap.AccHex.HexX,trap.AccHex.HexY);
	int perception=CLAMP(cr.Params[ST_PERCEPTION]+cr.Params[ST_PERCEPTION_EXT],1,10);
	int skilldiff=cr.Params[SK_TRAPS]-trap.TrapGetValue();
	return dist<=perception/2 + skilldiff/50;
}

EXPORT uint Map_WallDistance(Map& map, uint16 hx, uint16 hy) // test
{
	WallDist* wd=GetProtoDists(map);
	return wd->distances[hy*wd->proto->Header.MaxHexX+hx];
}
