#include "StdAfx.h"
#include "Faction.h"
#include "assert.h"
#include <hash_map>

FactionsInfo* Info;
FactionsData* Data;
using namespace stdext;

hash_map<int, Faction*> Factions;

Faction::Faction(int id)
: Id(id)
{
	Name = Info->GetFactionName(id);
}

Faction::~Faction(void)
{
}

Faction* GetFaction(int id)
{
	// null for unknown
	if(!Info->Exists(id)) return NULL;
	// create wrapper on demand
	if(!Factions[id]) Factions[id] = new Faction(id);
	return Factions[id];
}
bool Faction::IsPlayerDriven() const
{
	return Info->GetPlayerDriven(Id);
}
void Faction::SetLastUsed(uint time)
{
	Info->SetLastUsed(Id, time);
}
uint Faction::GetLastUsed() const
{
	return Info->GetLastUsed(Id);
}

const string Faction::GetLeader() const
{
	return Info->GetLeader(Id);
}
void Faction::SetLeader(const string& leader)
{
	Info->SetLeader(Id, leader);
}
uint Faction::GetLeaderTime() const
{
	return Info->GetLeaderTime(Id);
}
void Faction::SetLeaderTime(uint time)
{
	Info->SetLeaderTime(Id,time);
}
const string Faction::GetClaim() const
{
	return Info->GetClaim(Id);
}
void Faction::SetClaim(const string& leader)
{
	Info->SetClaim(Id, leader);
}
uint Faction::GetClaimTime() const
{
	return Info->GetClaimTime(Id);
}
void Faction::SetClaimTime(uint time)
{
	Info->SetClaimTime(Id,time);
}

uint16 Faction::GetRadioFreq() const
{
	return Info->GetRadioFreq(Id);
}
void Faction::SetRadioFreq(uint16 freq)
{
	Info->SetRadioFreq(Id, freq);
}

// players 'db' related
void Faction::AddPlayer(const std::string& name)
{
	Data->AddPlayer(Id, name);
}

void Faction::ModifyStatus(const std::string& playername, int status)
{
	Data->ModifyStatus(Id, playername, status);
}
int Faction::GetStatus(const std::string& playername) const
{
	return Data->GetStatus(Id, playername);
}
void Faction::ModifyFaction(const std::string& playername, int faction)
{
	Data->ModifyFaction(Id, playername, faction);
}
int Faction::GetFaction(const std::string& playername) const
{
	return Data->GetFaction(Id, playername);
}
void Faction::ModifyRank(const std::string& playername, int rank)
{
	Data->ModifyRank(Id, playername, rank);
}
int Faction::GetRank(const std::string& playername) const
{
	return Data->GetRank(Id, playername);
}
void Faction::ModifyReputation(const std::string& playername, int reputation)
{
	Data->ModifyReputation(Id, playername, reputation);
}
int Faction::GetReputation(const std::string& playername) const
{
	return Data->GetReputation(Id, playername);
}

extern __declspec(dllexport)
Faction* Critter_GetFaction(Critter* cr)
{
	return GetFaction(_GroupIndex(cr));
}

void RegisterFactionType(asIScriptEngine* asEngine, MYSQL* mysql)
{
	if(mysql)
	{
		Info = new FactionsInfo(mysql);
		Data = new FactionsData(mysql);
	}
	// todo: blasted asserts are disabled by some define or what, need to use normal WriteLog here

	/*int res = asEngine->RegisterObjectType("Faction", sizeof(Faction), asOBJ_REF); assert(res>=0);
	res = asEngine->RegisterGlobalFunction("Faction@+ GetFaction(int)", asFUNCTION(GetFaction), asCALL_CDECL); assert(res>=0);

	res = asEngine->RegisterObjectMethod("Critter", "Faction@+ GetFaction()", asFUNCTION(Critter_GetFaction), asCALL_CDECL_OBJFIRST);  assert(res >= 0);

	res = asEngine->RegisterObjectBehaviour("Faction",asBEHAVE_ADDREF,"void f()",asMETHOD(Faction,AddRef),asCALL_THISCALL);  assert(res >= 0);
	res = asEngine->RegisterObjectBehaviour("Faction",asBEHAVE_RELEASE,"void f()",asMETHOD(Faction,Release),asCALL_THISCALL);  assert(res >= 0);
	// properties
	res = asEngine->RegisterObjectProperty("Faction","const string Name",offsetof(Faction, Name)); assert(res >= 0);
	res = asEngine->RegisterObjectProperty("Faction","const int Id",offsetof(Faction, Id)); assert(res >= 0);
	// methods
	res = asEngine->RegisterObjectMethod("Faction","bool get_IsPlayerDriven()",asMETHOD(Faction,IsPlayerDriven),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","uint16 get_RadioFreq() const",asMETHOD(Faction,GetRadioFreq), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_RadioFreq(uint16)",asMETHOD(Faction,SetRadioFreq),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","string@+ get_Leader() const",asMETHOD(Faction,GetLeader), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_Leader(string@+)",asMETHOD(Faction,SetLeader),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","string@+ get_Claim() const",asMETHOD(Faction,GetClaim), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_Claim(string@+)",asMETHOD(Faction,SetClaim),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","uint get_LeaderTime() const",asMETHOD(Faction,GetLeaderTime), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_LeaderTime(uint)",asMETHOD(Faction,SetLeaderTime),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","uint get_ClaimTime() const",asMETHOD(Faction,GetClaimTime), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_ClaimTime(uint)",asMETHOD(Faction,SetClaimTime),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","uint get_LastUsed() const",asMETHOD(Faction,GetClaimTime), asCALL_THISCALL); assert(res >= 0);
	res = asEngine->RegisterObjectMethod("Faction","void set_LastUsed(uint)",asMETHOD(Faction,SetClaimTime),asCALL_THISCALL); assert(res>=0);
	
	res = asEngine->RegisterObjectMethod("Faction","void AddPlayer(const string@+)",asMETHOD(Faction,AddPlayer),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","void ModifyStatus(const string@+,int)",asMETHOD(Faction,ModifyStatus),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","int GetStatus(const string@+) const",asMETHOD(Faction,GetStatus),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","void ModifyFaction(const string@+,int)",asMETHOD(Faction,ModifyFaction),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","int GetFaction(const string@+) const",asMETHOD(Faction,GetFaction),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","void ModifyRank(const string@+,int)",asMETHOD(Faction,ModifyRank),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","int GetRank(const string@+) const",asMETHOD(Faction,GetRank),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","void ModifyReputation(const string@+,int)",asMETHOD(Faction,ModifyReputation),asCALL_THISCALL); assert(res>=0);
	res = asEngine->RegisterObjectMethod("Faction","int GetReputation(const string@+) const",asMETHOD(Faction,GetReputation),asCALL_THISCALL); assert(res>=0);*/
}

void CleanFactionType()
{
	for(hash_map<int, Faction*>::iterator it=Factions.begin(); it!=Factions.end(); ++it)
		delete (*it).second;
	delete Info;
	delete Data;
}
