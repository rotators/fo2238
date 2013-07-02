#include "StdAfx.h"
#include "FactionsInfo.h"
#include <sstream>
#include "helpers.h"

bool FactionsInfo::Exists(int id)
{
	// 0 means nothing, and rest depends on the vector holding each faction
	return id > 0 && id < (int)(cache_info.size());
}

const string FactionsInfo::GetFactionName(int id)
{
	return cache_info[id].name;
}

bool FactionsInfo::GetPlayerDriven(int id)
{
	return cache_info[id].playerDriven;
}
void FactionsInfo::SetPlayerDriven(int id, bool pd)
{
	stringstream query;
	query << "update factions_info set playerdriven=" << (pd?1:0) << " where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].playerDriven = pd;
}

uint FactionsInfo::GetLastUsed(int id)
{
	return cache_info[id].lastUsed;
}
void FactionsInfo::SetLastUsed(int id, uint nt)
{
	stringstream query;
	query << "update factions_info set lastused=" << nt << " where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].lastUsed = nt;
}

int FactionsInfo::GetScore(int id)
{
	return cache_info[id].score;
}
void FactionsInfo::SetScore(int id, int ns)
{
	stringstream query;
	query << "update factions_info set score=" << ns << " where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].score = ns;
}

uint FactionsInfo::GetClaimTime(int id)
{
	return cache_info[id].claimTime;
}
void FactionsInfo::SetClaimTime(int id, uint nt)
{
	stringstream query;
	query << "update factions_info set claimtime=" << nt << " where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].claimTime = nt;
}

uint FactionsInfo::GetLeaderTime(int id)
{
	return cache_info[id].leaderTime;
}

void FactionsInfo::SetLeaderTime(int id, uint nt)
{
	stringstream query;
	query << "update factions_info set leadertime=" << nt << " where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].leaderTime = nt;
}

const string FactionsInfo::GetClaim(int id)
{
	return cache_info[id].claim;
}

void FactionsInfo::SetClaim(int id, const string& name)
{
	stringstream query;
	query << "update factions_info set claim='" << name << "' where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].claim = name;
}

const string FactionsInfo::GetLeader(int id)
{
	return cache_info[id].leader;
}

void FactionsInfo::SetLeader(int id, const string& name)
{
	stringstream query;
	query << "update factions_info set leader='" << name << "' where id=" << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	cache_info[id].leader = name;
}

uint16 FactionsInfo::GetRadioFreq(int id)
{
	return cache_info[id].freq;
}

void FactionsInfo::SetRadioFreq(int id, uint16 nfreq)
{
	// update db info
	stringstream query;
	query << "update factions_info set freq=" << nfreq << " where id = " << id;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	// update cache info
	cache_info[id].freq = nfreq;
}

uint FactionsInfo::GetFactionId(const string& name)
{
	return cache_id[name];
}

uint FactionsInfo::AddFaction(const string& name)
{
	char* escaped = new char[2*name.length()+1];
	mysql_real_escape_string(conn, escaped, name.c_str(), name.length());
	string fname(escaped);
	delete escaped;

	stringstream query; 
	query << "insert into factions_info(name) values";
	query << "('" << fname << "')";
	if(mysql_query(conn, query.str().c_str())) 
	{
		ilog(mysql_error(conn));
		return 0;
	}
	// check what id had been assigned
	query.str("");
	query << "select id from factions_info where name = '" << fname << "' order by id desc"; // though we probably won't allow few factions share the same name, but it shouldn't hurt
	MYSQL_RES* result;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return 0;
	}
	result = mysql_store_result(conn);
	if(result)
	{
		MYSQL_ROW row = mysql_fetch_row(result);
		if(row)
		{
			int id = 0;
			if(row[0]) sscanf(row[0], "%d", &id);

			mysql_free_result(result);
			cache_id[name] = id;
			cache_info.resize(max(cache_info.size(), (uint)id+1));
			cache_info[id].id = id; // todo: not covered in test suite
			cache_info[id].name = name;
			return id;
		}
		else
		{
			ilog("Couldn't obtain id of newly created faction");
			return 0;
		}
	}
	else
	{
		ilog("Couldn't obtain id of newly created faction");
		return 0;
	}
}

void FactionsInfo::FillCache()
{
	stringstream query;
	query << "select id, name, freq, leader, claim, leadertime, claimtime, score, playerdriven, lastused from factions_info";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return; //todo: exception?
	}
	MYSQL_RES* result = mysql_use_result(conn);
	if(result)
	{
		MYSQL_ROW row;
		while(row = mysql_fetch_row(result))
		{
			unsigned long *lengths = mysql_fetch_lengths(result);
		
			Info info;

			info.id = ParseFieldInt(row[0], lengths[0]);
			info.name = ParseFieldStr(row[1], lengths[1]);
			info.freq = ParseFieldInt(row[2], lengths[2]);
			info.leader = ParseFieldStr(row[3], lengths[3]);
			info.claim = ParseFieldStr(row[4], lengths[4]);
			info.leaderTime = ParseFieldInt(row[5], lengths[5]);
			info.claimTime = ParseFieldInt(row[6], lengths[6]);
			info.score = ParseFieldInt(row[7], lengths[7]);
			info.playerDriven = ParseFieldInt(row[8], lengths[8])!=0;
			info.lastUsed = ParseFieldInt(row[9], lengths[9]);

			cache_id[info.name] = info.id;
			cache_info.resize(max(cache_info.size(), info.id+1));
			cache_info[info.id] = info;
		}
		mysql_free_result(result);
	}
}

FactionsInfo::Info::Info()
: id(0), freq(0), leaderTime(0), claimTime(0), score(0), playerDriven(false), lastUsed(0)
{
}

FactionsInfo::FactionsInfo(MYSQL* conn)
: conn(conn)
{
	// check if table exists
	MYSQL_RES* res;

	mysql_query(conn, "show tables like 'factions_info'");	
	res = mysql_store_result(conn);
	if(res && mysql_num_rows(res) == 0)
	{
		// create table
		stringstream query;
		query << "create table factions_info ";
		query << "(id int not null auto_increment primary key";
		query << ",name varchar(" << MAX_FACTION_NAME << ") not null";
		query << ",freq int unsigned";
		query << ",leader varchar(" << MAX_NAME << ")";
		query << ",claim varchar(" << MAX_NAME << ")";
		query << ",leadertime int unsigned";
		query << ",claimtime int unsigned";
		query << ",score int";
		query << ",playerdriven tinyint(1)";
		query << ",lastused int unsigned)";
	
		if(mysql_query(conn, query.str().c_str()))
		{
			ilog(mysql_error(conn));
			return;
		}
		if(mysql_query(conn, "alter table factions_info auto_increment=0"))
		{
			ilog(mysql_error(conn));
			return;
		}
		// insert rows for 'default' factions
		query.str("");
		query << "insert into factions_info(name) values(" << "'None'" << ")";
		if(mysql_query(conn, query.str().c_str()))
		{
			ilog(mysql_error(conn));
			return;
		}
		cache_id["None"] = 1;
		cache_info.resize(2);
		cache_info[1].name = "None";
		cache_info[1].id = 1;
	}
	if(res) mysql_free_result(res);
	FillCache();
}

FactionsInfo::~FactionsInfo(void)
{
}
