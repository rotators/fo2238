#include "StdAfx.h"
#include "FactionsNews.h"
#include <sstream>

FactionNews::FactionNews(MYSQL* conn)
: conn(conn)
{
	MYSQL_RES* res;

	mysql_query(conn, "show tables like 'factions_news'");	
	res = mysql_store_result(conn);
	if(res && mysql_num_rows(res) == 0)
	{
		// create table
		stringstream query;
		query << "create table factions_news ";
		query << "(faction int";
		query << ",text varchar(160)"; // like on twitter yay
		query << ",time int unsigned";
		query << ")";
		if(mysql_query(conn, query.str().c_str()))
		{
			ilog(mysql_error(conn));
			return;
		}
	}
	if(res) mysql_free_result(res);
	//FillCache();
}

FactionNews::~FactionNews(void)
{
}
