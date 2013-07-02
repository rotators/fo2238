#include <strstream>
#include <string>
#include <windows.h>
#include "../fonline2238.h"
#include <stdio.h>

int WalkTime[MAX_CRIT_TYPES];
int TickDiff=0;

EXPORT bool InitWalkProcessing2(Critter& cr)
{
	TickDiff=(int)timeGetTime()-cr.WaitEndTick;

	// read critter types
#ifdef _MSC_VER
	FILE* f;
	fopen_s(&f,"./data/CritterTypes.cfg","r");
#else
	FILE* f=fopen("./data/CritterTypes.cfg","r");
#endif
	if(!f)
	{
		printf("Terrible error, see InitWalkProcessing in parameters.dll.\n");
		return false;
	}
	char line[2048];
	bool found=false;

	while(!feof(f))
	{
		fgets(line,2048,f);
		int id=0;
		int walk=0;
		//@ Id Name    Alias MH  3d  Walk  Run  Aim Armor Rotate  A B C D E F G H I J K L M N O P Q R S T U V W X Y Z   Walk  Run   Walk steps Sound name
		istrstream str(line);

		string svalue;
		int value;

		str >> svalue;
		if(str.fail() || svalue!="@") continue;

		str >> id;
		if(str.fail()) continue;

		str >> svalue;
		if(str.fail()) continue;
		str >> svalue;
		if(str.fail()) continue;
		bool fail=false;
		for(int j=0;j<33;j++)
		{
			str >> value;
			if(str.fail())
			{
				fail=true;
				break;
			}
		}
		if(fail) continue;

		str >> walk;
		if(str.fail()) continue;
		WalkTime[id]=walk;
		found=true;
	}
	fclose(f);
	if(!found)
	{
		Log("Error: no crtypes read, see InitWalkProcessing in parameters.dll.");
		return false;
	}
	return true;
}

bool IsMoving(Critter& cr)
{
	return (int)timeGetTime()-TickDiff-(int)cr.PrevHexTick<WalkTime[cr.BaseType];
}