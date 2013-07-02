#pragma once

#include "mysql.h"
#include <hash_map>

// Access to the data table containing general info about faction
class FactionsInfo
{
	static const int MAX_FACTION_NAME = 100;

	struct Info
	{
		/** Unique identifier */
		uint id;
		/** Name of the faction */
		std::string name;
		/** Radio frequency */
		uint freq;
		/** faction leader */
		std::string leader;
		/** member who claimed leadership, and wants to overthrown current leader */
		std::string claim;
		/** time that will allow claimee to claim leadership even without forcing current leader to resign/killing him
		 this is time for leader to act */
		uint leaderTime;
		/** time that will allow another player to claim leadership
		 this is time for claimee to act */
		uint claimTime;
		/** score */
		uint score;
		/** Denotes whether it's player-driven gang or not. */
		bool playerDriven;
		/** Tracks the time when faction has been last accessed. */
		uint lastUsed;

		Info();
	};

	std::vector<Info> cache_info;
	std::hash_map<string, uint> cache_id;

	void FillCache();
	
	MYSQL* conn;
public:
	__declspec(dllexport)
		FactionsInfo(MYSQL*);
	__declspec(dllexport)
		~FactionsInfo(void);

	__declspec(dllexport)
		bool Exists(int);

	__declspec(dllexport)
		uint AddFaction(const std::string&);
	__declspec(dllexport)
		uint GetFactionId(const std::string&);
	__declspec(dllexport)
		const std::string GetFactionName(int);

	__declspec(dllexport)
		uint16 GetRadioFreq(int);
	__declspec(dllexport)
		void SetRadioFreq(int, uint16);

	__declspec(dllexport)
		const std::string GetLeader(int);
	__declspec(dllexport)
		void SetLeader(int, const std::string&);
	__declspec(dllexport)
		const std::string GetClaim(int);
	__declspec(dllexport)
		void SetClaim(int, const std::string&);

	__declspec(dllexport)
		uint GetLeaderTime(int);
	__declspec(dllexport)
		void SetLeaderTime(int, uint);
	__declspec(dllexport)
		uint GetClaimTime(int);
	__declspec(dllexport)
		void SetClaimTime(int, uint);
	__declspec(dllexport)
		int GetScore(int);
	__declspec(dllexport)
		void SetScore(int, int);
	__declspec(dllexport)
		bool GetPlayerDriven(int);
	__declspec(dllexport)
		void SetPlayerDriven(int, bool);
	__declspec(dllexport)
		uint GetLastUsed(int);
	__declspec(dllexport)
		void SetLastUsed(int, uint);
};
