#include "StdAfx.h"
#include "FactionsData.h"
#include "ilog.h"
#include <sstream>
#include <stdio.h>
#include "helpers.h"

FactionsData::FactionsData(MYSQL* conn)
: conn(conn)
{
	// check if table exists
	MYSQL_RES* res;

	mysql_query(conn, "show tables like 'factions_data'");	
	res = mysql_store_result(conn);
	if(res && mysql_num_rows(res) == 0)
	{
		// create table
		stringstream query;
		query << "create table factions_data (faction int, player_name varchar(" << MAX_NAME << "), player_faction int, player_rank tinyint, player_status tinyint, player_reputation tinyint)";
		if(mysql_query(conn, query.str().c_str()))
		{
			ilog(mysql_error(conn));
			return;
		}
	}
	if(res) mysql_free_result(res);
	FillCache();
}

FactionsData::~FactionsData(void)
{
}

/*bool FactionsData::InvitePlayer(int faction, const string& name)
{
	// check if player does not exist in fdb
	if(!RecordExists(faction, name))
	{
		// add him
		stringstream query; 
		query << "insert into factions_data(faction, player_name) values";
		query << "(" << faction << ", '" << name << "')";
		if(mysql_query(conn, query.str().c_str())) 
		{
			ilog(mysql_error(conn));
			return false;
		}
	}
	// modify status
	stringstream query;
	query << "update factions_data set ";
	query << "player_status=" << Status::Invited << " where faction=" << faction;
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return false;
	}
	status_cache[fkey(faction, name)] = Status::Invited;
	return true;
}*/

bool FactionsData::RecordExists(int faction, const string& name)
{
	stringstream query;
	query << "select * from factions_data where player_name='" << name << "'";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return false;
	}
	MYSQL_RES* res = mysql_store_result(conn);
	if(res)
	{
		my_ulonglong count = mysql_num_rows(res);
		mysql_free_result(res);
		return count >= 1; // safer than == 1
	}
	return false;
}

int FactionsData::GetFaction(int faction, const string& name)
{
	return faction_cache[fkey(faction, name)];
}
int FactionsData::GetStatus(int faction, const string& name)
{
	return status_cache[fkey(faction, name)];
}
int FactionsData::GetRank(int faction, const string& name)
{
	return rank_cache[fkey(faction, name)];
}
int FactionsData::GetReputation(int faction, const string& name)
{
	return reputation_cache[fkey(faction, name)];
}

void FactionsData::ModifyFaction(int faction, const string& name, int newFaction)
{
	fkey key(faction, name);
	// add player if doesn't exist in db
	if(faction_cache.find(fkey(faction, name)) == faction_cache.end())
		AddPlayer(faction, name);
	// update data in db
	stringstream query;
	query << "update factions_data set player_faction=" << newFaction << " where faction = " << faction << " and player_name = '" << name << "'";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	// refresh cache
	faction_cache[key] = newFaction;
}

void FactionsData::ModifyStatus(int faction, const string& name, int newStatus)
{
	fkey key(faction, name);
	// add player if doesn't exist in db
	if(status_cache.find(key) == status_cache.end())
		AddPlayer(faction, name);
	// update data in db
	stringstream query;
	query << "update factions_data set player_status=" << newStatus << " where faction = " << faction << " and player_name = '" << name << "'";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	// refresh cache
	status_cache[key] = newStatus;
}

void FactionsData::ModifyRank(int faction, const string& name, int newRank)
{
	fkey key(faction, name);
	// add player if doesn't exist in db
	if(rank_cache.find(key) == rank_cache.end())
		AddPlayer(faction, name);
	// update data in db
	stringstream query;
	query << "update factions_data set player_rank=" << newRank << " where faction = " << faction << " and player_name = '" << name << "'";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	// refresh cache
	rank_cache[key] = newRank;
}
void FactionsData::ModifyReputation(int faction, const string& name, int newRep)
{
	fkey key(faction, name);
	// add player if doesn't exist in db
	if(reputation_cache.find(key) == reputation_cache.end())
		AddPlayer(faction, name);
	// update data in db
	stringstream query;
	query << "update factions_data set player_reputation=" << newRep << " where faction = " << faction << " and player_name = '" << name << "'";
	if(mysql_query(conn, query.str().c_str()))
	{
		ilog(mysql_error(conn));
		return;
	}
	// refresh cache
	reputation_cache[key] = newRep;
}

void FactionsData::FillCache()
{
	stringstream query;
	query << "select faction, player_name, player_faction, player_status, player_rank, player_reputation from factions_data";
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
		
			int faction = ParseFieldInt(row[0], lengths[0]);
			string name = ParseFieldStr(row[1], lengths[1]);
			int pfaction = ParseFieldInt(row[2], lengths[2]);
			int status = ParseFieldInt(row[3], lengths[3]);
			int rank = ParseFieldInt(row[4], lengths[4]);
			int reputation = ParseFieldInt(row[5], lengths[5]);
			
			this->status_cache[fkey(faction, name)] = status;
			this->faction_cache[fkey(faction, name)] = pfaction;
			this->rank_cache[fkey(faction, name)] = rank;
			this->reputation_cache[fkey(faction, name)] = reputation;

		}
		mysql_free_result(result);
	}
}

FDResult::Enum FactionsData::AddPlayer(int faction, const std::string& name)
{
	if(RecordExists(faction, name))
		return FDResult::AlreadyExists;
	
	stringstream query; 
	query << "insert into factions_data(faction, player_name) values";
	query << "(" << faction << ", '" << name << "')";
	if(mysql_query(conn, query.str().c_str())) 
	{
		ilog(mysql_error(conn));
		return FDResult::DatabaseError;
	}
	return FDResult::Success;
}
