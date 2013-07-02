#include "common.h"
#include <stdio.h>
#include <map>
#include <algorithm>

using namespace std;

#define BUFFER_SIZE				(1024)
#define APPROX_GRID_THRESHOLD	(5)

class LocationEx
{
public:
	bool IsEncounter;
	bool IsTown;
	bool IsCityEncounter;
	bool IsInstancedQuest;
	bool IsBase;
	bool IsReplication;
	bool IsTCTown;
	bool IsTent;
	bool IsMine;
	bool IsCave;
	bool IsGuarded;
	bool IsPublic;

	LocationEx();
	~LocationEx();
};

LocationEx::LocationEx()
{
	IsEncounter=false;
	IsTown=false;
	IsCityEncounter=false;
	IsInstancedQuest=false;
	IsBase=false;
	IsReplication=false;
	IsTCTown=false;
	IsTent=false;
	IsMine=false;
	IsCave=false;
	IsGuarded=false;
	IsPublic=false;
}

LocationEx::~LocationEx() {}

struct GridCarrier
{
	vector<MapObjectVec> ToWM;
	vector<MapObjectVec> ToMap;
};

vector<LocationEx> LocationExArray;
typedef map<uint16, GridCarrier> GridMap;
GridMap GridInfo;

void InitLocationEx()
{
  char buffer[BUFFER_SIZE];
  char *arg;
  char *param;
  char *area = "[Area";
  char *prefix = "#@";

  // add strings here
  char *IsEncounter="IsEncounter";
  char *IsTown="IsTown";
  char *IsCityEncounter="IsCityEncounter";
  char *IsInstancedQuest="IsInstancedQuest";
  char *IsBase="IsBase";
  char *IsReplication="IsReplication";
  char *IsTCTown="IsTCTown";
  char *IsTent="IsTent";
  char *IsCave="IsCave";
  char *IsMine="IsMine";
  char *IsGuarded="IsGuarded";
  char *IsPublic="IsPublic";

  int curpid=0;
  FILE *f=fopen("maps/Locations.cfg","r");
  if (!f) return;
  while (fgets(buffer, BUFFER_SIZE, f)!=NULL)
  {
    if (strncmp(area,buffer,5)==0)
    {
      int i=6;
      while (('0' <= buffer[i]) && (buffer[i] <= '9')) i++;
      buffer[i]=0;
      curpid=atoi(buffer+6);
      if ((uint)curpid>=LocationExArray.size()) LocationExArray.resize(curpid+1);
    }
    else
    if (strncmp(prefix,buffer,2)==0)
    {
      arg=buffer+2;
      int i=0;
      while (arg[i]!=0)
      {
        if (arg[i]=='=') { arg[i]=0; param=arg+(++i);}
        else
        if ((arg[i]==10) || (arg[i]==10)) arg[i++]=0;
        else
        i++;
      };

      // process arg and param here
      if (strcmp(IsEncounter,arg)==0) LocationExArray[curpid].IsEncounter = (atoi(param) != 0);
      else
      if (strcmp(IsTown,arg)==0) LocationExArray[curpid].IsTown = (atoi(param) != 0);
      else
      if (strcmp(IsCityEncounter,arg)==0) LocationExArray[curpid].IsCityEncounter = (atoi(param) != 0);
      else
      if (strcmp(IsInstancedQuest,arg)==0) LocationExArray[curpid].IsInstancedQuest = (atoi(param) != 0);
      else
      if (strcmp(IsBase,arg)==0) LocationExArray[curpid].IsBase = (atoi(param) != 0);
      else
      if (strcmp(IsReplication,arg)==0) LocationExArray[curpid].IsReplication = (atoi(param) != 0);
      else
      if (strcmp(IsTCTown,arg)==0) LocationExArray[curpid].IsTCTown = (atoi(param) != 0);
      else
      if (strcmp(IsTent,arg)==0) LocationExArray[curpid].IsTent = (atoi(param) != 0);
	  else
      if (strcmp(IsCave,arg)==0) LocationExArray[curpid].IsCave = (atoi(param) != 0);
	  else
      if (strcmp(IsMine,arg)==0) LocationExArray[curpid].IsMine = (atoi(param) != 0);
	  else
      if (strcmp(IsGuarded,arg)==0) LocationExArray[curpid].IsGuarded = (atoi(param) != 0);
	  else
      if (strcmp(IsPublic,arg)==0) LocationExArray[curpid].IsPublic = (atoi(param) != 0);
    }
  }
  fclose(f);
}

void FinishMapLocationExt()
{
	LocationExArray.clear();
	GridInfo.clear();
}

EXPORT bool Location_IsEncounter(Location& loc) { return LocationExArray[loc.Data.LocPid].IsEncounter; }
EXPORT bool Location_IsTown(Location& loc) { return LocationExArray[loc.Data.LocPid].IsTown; }
EXPORT bool Location_IsCityEncounter(Location& loc) { return LocationExArray[loc.Data.LocPid].IsCityEncounter; }
EXPORT bool Location_IsInstancedQuest(Location& loc) { return LocationExArray[loc.Data.LocPid].IsInstancedQuest; }
EXPORT bool Location_IsBase(Location& loc) { return LocationExArray[loc.Data.LocPid].IsBase; }
EXPORT bool Location_IsReplication(Location& loc) { return LocationExArray[loc.Data.LocPid].IsReplication; }
EXPORT bool Location_IsTCTown(Location& loc) { return LocationExArray[loc.Data.LocPid].IsTCTown; }
EXPORT bool Location_IsTent(Location& loc) { return LocationExArray[loc.Data.LocPid].IsTent; }
EXPORT bool Location_IsMine(Location& loc) { return LocationExArray[loc.Data.LocPid].IsMine; }
EXPORT bool Location_IsCave(Location& loc) { return LocationExArray[loc.Data.LocPid].IsCave; }
EXPORT bool Location_IsGuarded(Location& loc) { return LocationExArray[loc.Data.LocPid].IsGuarded; }
EXPORT bool Location_IsPublic(Location& loc) { return LocationExArray[loc.Data.LocPid].IsPublic; }

MapObjectVec* FindNearestGroup(vector<MapObjectVec>& groups, uint16 hx, uint16 hy)
{
	if(groups.empty()) return NULL;

	auto cur_group = groups.begin();
	auto closest = cur_group;
	uint16 closest_dist = GetDistantion(hx, hy, cur_group->front()->MapX, cur_group->front()->MapY);
	if(!closest_dist) return closest;
	for(++cur_group; cur_group != groups.end(); ++cur_group)
	{
		uint16 dist = GetDistantion(hx, hy, cur_group->front()->MapX, cur_group->front()->MapY);
		if(dist < closest_dist)
		{
			closest_dist = dist;
			closest = cur_group;
			if(!closest_dist) break; // nothing can be closer
		}
	}
	return closest;
}

MapObject* FindNearestObject(const MapObjectVec& vec, uint16 hx, uint16 hy, bool withPid = false, uint16 toMapPid = 0)
{
	if(vec.empty()) return NULL;
	auto closest = vec.begin();
	auto curr = closest;
	uint16 min_dist = GetDistantion(hx, hy, (*closest)->MapX, (*closest)->MapY);
	if(!min_dist) return *closest;
	for(++curr; curr != vec.end(); ++curr)
	{
		MapObject* obj = *curr;
		if(withPid && obj->MScenery.ToMapPid != toMapPid) continue;

		uint16 dist = GetDistantion(hx, hy, obj->MapX, obj->MapY);
		if(dist < min_dist)
		{
			if(!dist) return *curr;
			min_dist = dist;
			closest = curr;
		}
	}
	return *closest;
}

GridCarrier& ProcessCarrier(const ProtoMap* proto)
{
	GridCarrier carrier;

	// first pass: create initial grids
	for(auto it = proto->GridsVec.begin(), end = proto->GridsVec.end(); it != end; ++it)
	{
		MapObject* obj = *it;
		uint16 hx = obj->MapX;
		uint16 hy = obj->MapY;

		vector<MapObjectVec>& groups = obj->MScenery.ToMapPid ? carrier.ToMap : carrier.ToWM;

		// is it closer than threshold value to some previously chosen initial?
		auto comparator = [&](const MapObjectVec& group) -> bool
		{
			MapObject* o = group.front();
			return GetDistantion(hx, hy, o->MapX, o->MapY) < APPROX_GRID_THRESHOLD;
		};

		bool is_close = find_if(groups.begin(), groups.end(), comparator) != groups.end();
		if(is_close) continue;

		// make a new initial and its group
		groups.push_back(MapObjectVec());
		groups.back().push_back(obj);
	}

	// second pass: assign grids to initials
	for(auto it = proto->GridsVec.begin(), end = proto->GridsVec.end(); it != end; ++it)
	{
		MapObject* obj = *it;
		uint16 hx = obj->MapX;
		uint16 hy = obj->MapY;

		vector<MapObjectVec>& groups = obj->MScenery.ToMapPid ? carrier.ToMap : carrier.ToWM;
		MapObjectVec* closest = FindNearestGroup(groups, hx, hy);
		uint16 closest_dist = GetDistantion(hx, hy, closest->front()->MapX, closest->front()->MapY);
		if(!closest_dist) continue; // is initial
		closest->push_back(obj);
	}

	GridInfo[proto->Pid] = carrier;
	return GridInfo[proto->Pid];
}

GridCarrier& GetCarrier(const ProtoMap* proto)
{
	GridMap::iterator it = GridInfo.find(proto->Pid);
	if(it == GridInfo.end()) return ProcessCarrier(proto);
	return it->second;
}

EXPORT bool Map_FindNearestGridRough(Map& imap, uint16& hx, uint16& hy, bool toWM)
{
	GridCarrier& carrier = GetCarrier(imap.Proto);

	vector<MapObjectVec>& groups = toWM ? carrier.ToWM : carrier.ToMap;
	MapObjectVec* vec = FindNearestGroup(groups, hx, hy);
	if(!vec) return false;
	hx = vec->front()->MapX;
	hy = vec->front()->MapY;
	return true;
}

EXPORT bool Map_FindNearestGridApprox(Map& imap, uint16& hx, uint16& hy, bool toWM)
{
	GridCarrier& carrier = GetCarrier(imap.Proto);

	vector<MapObjectVec>& groups = toWM ? carrier.ToWM : carrier.ToMap;
	MapObjectVec* vec = FindNearestGroup(groups, hx, hy);
	if(!vec) return false;

	MapObject* obj = FindNearestObject(*vec, hx, hy);
	hx = obj->MapX;
	hy = obj->MapY;
	return true;
}

EXPORT bool Map_FindNearestGrid(Map& imap, uint16& hx, uint16& hy, uint16 toMapPid)
{
	MapObject* obj = FindNearestObject(imap.Proto->GridsVec, hx, hy, true, toMapPid);
	if(!obj) return false;
	hx=obj->MapX;
	hy=obj->MapY;
	return true;
}

/*
EXPORT void Map_GetGrids( Map& map, ScriptArray& result )
{
	for( int g=0, gMax=map.Proto->GridsVec.size(); g<gMax; g++ )
	{
		result.InsertLast( (uint16*)map.Proto->GridsVec[g]->MapX );
		result.InsertLast( (uint16*)map.Proto->GridsVec[g]->MapY );
	}
}
*/

EXPORT bool Map_IsGrid( Map& map, uint16 hx, uint16 hy )
{
	if( map.Proto->HexFlags[hy * map.GetMaxHexX() + hx] & FH_SCEN_GRID )
		return( true );
	else
		return( false );
}