#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __FACTIONS_DATA_H__
#define __FACTIONS_DATA_H__

#include <hash_map>

namespace FDResult
{
	enum Enum
	{
		Success = 0,
		AlreadyExists = 1,
		DatabaseError = 3, // same as FD_ANY_DATA_ERROR in scripts, because it's analogue - it refers to the storage
	};
}

// Access to table containing info about players stored in each faction 'database'
class FactionsData
{
public:
	__declspec(dllexport)
		FactionsData(MYSQL*);
	__declspec(dllexport)
		~FactionsData(void);

	__declspec(dllexport)
		FDResult::Enum AddPlayer(int, const std::string&); 

	// Checks if record for given player exists at given faction' database
	__declspec(dllexport)
		bool RecordExists(int, const std::string&);
	
	__declspec(dllexport)
		int GetFaction(int, const std::string&);
	__declspec(dllexport)
		int GetRank(int, const std::string&);
	__declspec(dllexport)
		int GetStatus(int, const std::string&);
	__declspec(dllexport)
		int GetReputation(int, const std::string&);

	__declspec(dllexport)
		void ModifyFaction(int, const std::string&, int);
	__declspec(dllexport)
		void ModifyStatus(int, const std::string&, int);
	__declspec(dllexport)
		void ModifyRank(int, const std::string&, int);
	__declspec(dllexport)
		void ModifyReputation(int, const std::string&, int);

private:
	MYSQL* conn;

	// cache
	struct fkey // factionid playername - compound key to row denoting player's data in factions 'databases'
		: public pair<int, string>
	{
		fkey(int f, const string& s) : pair<int, string>(f, s) {}
		operator size_t() const { return sizeof(pair<int, string>); }
	};

	struct fkey_hash
	{
		size_t operator()(const struct fkey& k1) const
		{
			return 2654435761*k1.first; // by the power of Knuth
		}
	}; 

	struct fkey_eq
	{
		bool operator()(const struct fkey& k1, const struct fkey& k2) const
		{
			return k1.first==k2.first && k1.second==k2.second;
		}
	}; 

	std::hash_map<fkey, int, fkey_hash, fkey_eq> faction_cache;
	std::hash_map<fkey, int, fkey_hash, fkey_eq> status_cache;
	std::hash_map<fkey, int, fkey_hash, fkey_eq> rank_cache;
	std::hash_map<fkey, int, fkey_hash, fkey_eq> reputation_cache;

	void FillCache();
};

#endif