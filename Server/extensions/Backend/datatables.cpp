#include "StdAfx.h"
#include "datatables.h"
#include <sstream>

const string ItemTypeName(uint8 type)
{
	switch(type)
	{
	case ITEM_TYPE_ARMOR: return string("Armor");
	case ITEM_TYPE_DRUG: return string("Drug");
	case ITEM_TYPE_WEAPON: return "Weapon";
	case ITEM_TYPE_AMMO: return "Ammo";
	case ITEM_TYPE_MISC: return "Misc";
	case ITEM_TYPE_KEY: return "Key";
	default: return "Other";
	}
}

// updates/creates Items record for given proto
void UpdateProtoItem(MYSQL* conn, GameText* foobj, ProtoItem* proto)
{
	MYSQL_RES* res;

	mysql_query(conn, "show tables like 'items'");	
	res = mysql_store_result(conn);
	if(res && mysql_num_rows(res) == 0)
	{
		// create table
		if(mysql_query(conn, "create table items (pid int, name varchar(50), type varchar(10), primary key(pid))"))
		{
			ilog(mysql_error(conn));
			return;
		}
	}
	if(res) mysql_free_result(res);
	// check if row already exists
	stringstream pid; pid << proto->ProtoId;
	mysql_query(conn, ("select pid from Items where pid="+pid.str()).c_str());
	res = mysql_store_result(conn);
	if(res)
	{
		if(mysql_num_rows(res) > 0)
		{
			// update?
			/*stringstream query;
			query << "update items set pid=" << proto->GetPid() 
				<< ", name='" << foobj->GetString(proto->GetPid()*100) 
				<< ", type='" << ItemTypeName(proto->GetType()) 
				<< "' where pid=" << proto->GetPid();
			if(mysql_query(conn, query.str().c_str())) ilog(mysql_error(conn));*/
		}
		else
		{
			string oname = foobj->GetString(proto->ProtoId*100);
			string tname = ItemTypeName(proto->Type);
			replacein(oname, "'", "''");
			replacein(tname, "'", "''");
			// insert
			stringstream query;
			query << "insert into items values(" << proto->ProtoId << ", '" 
				<< oname <<  "', '"
				<< tname << "')";
			if(mysql_query(conn, query.str().c_str())) ilog(mysql_error(conn));
		}
	}
	else ilog(mysql_error(conn));
}