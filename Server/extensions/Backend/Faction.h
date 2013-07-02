#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __FACTION_H__
#define __FACTION_H__

#include "angelscript.h"
#include "refcount.h"
#include "factionsdata.h"
#include "factionsinfo.h"
#include "..\fonline2238.h"

#define _GroupIndex(cr)   (cr->Params[ST_TEAM_ID]/100)
#define _GroupRank(cr)    ((cr->Params[ST_TEAM_ID]/10)%10)
#define _GroupMode(cr)    (cr->Params[ST_TEAM_ID]%10)
#define _TeamId(__faction, __rank, __mode)  (__faction*100 + 10*__rank + __mode)

// Faction object to be exposed in scripts
class Faction
{
public:
	Faction(int);
	~Faction(void);

	int Id;
	std::string Name;

	bool IsPlayerDriven() const;

	const std::string GetLeader() const;
	void SetLeader(const std::string&);
	uint GetLeaderTime() const;
	void SetLeaderTime(uint);

	const std::string GetClaim() const;
	void SetClaim(const std::string&);
	uint GetClaimTime() const;
	void SetClaimTime(uint);

	uint16 GetRadioFreq() const;
	void SetRadioFreq(uint16);

	uint GetLastUsed() const;
	void SetLastUsed(uint);

	void AddPlayer(const std::string&);

	void ModifyStatus(const std::string&, int);
	int  GetStatus(const std::string&) const;

	void ModifyFaction(const std::string&, int);
	int  GetFaction(const std::string&) const;

	void ModifyRank(const std::string&, int);
	int  GetRank(const std::string&) const;

	void ModifyReputation(const std::string&, int);
	int  GetReputation(const std::string&) const;
};

void RegisterFactionType(asIScriptEngine*, MYSQL*);
void CleanFactionType();

#endif