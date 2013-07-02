#include "StdAfx.h"
#include "Log.h"
#include "time.h"
#include <sstream>

bool SQLLog::Write(const std::string& table, const std::string& values)
{
	// table not initialized
	if(!tables[table]) return false;
	// decompose the values
	vector<string> spl = split(values, '|');

	string ins;
	for(vector<string>::const_iterator it=spl.begin(); it!=spl.end(); ++it)
	{	
		char* escaped = new char[2*it->length()+1];
		mysql_real_escape_string(conn, escaped, it->c_str(), it->length());
		ins += "'"+ string(escaped) +"'";
		delete[] escaped;
		if(it+1!=spl.end()) ins += ", ";
	}
	SYSTEMTIME st;
    GetSystemTime(&st);
	stringstream query;
	query << "insert into " << table << " values(";
	query << "'" << st.wYear << '-' << st.wMonth << '-' << st.wDay << " " << st.wHour << ":" << st.wMinute << ":" << st.wSecond << "', ";
	query << ins << ")";
	if(mysql_query(conn, query .str().c_str()))
	{
		string err = mysql_error(conn);
		return false;
	}
	return true;
}

void SQLLog::FastWrite(const std::string& table, const std::string& values)
{
	// statement not prepared
	if(!statements[table]) return;
	
	MYSQL_STMT* stmt = statements[table];
	MYSQL_BIND  bind[3];
	memset(bind, 0, sizeof(bind));

	SYSTEMTIME st;
    GetSystemTime(&st);
	MYSQL_TIME time;
	time.day = st.wDay;
	time.hour = st.wHour;
	time.minute = st.wMinute;
	time.month = st.wMonth;
	time.second = st.wSecond;
	time.year = st.wYear;

	bind[0].buffer_type = MYSQL_TYPE_DATETIME;
	bind[0].buffer = &time;
	bind[0].buffer_length = sizeof(MYSQL_TIME);
	bind[0].is_null = 0;
	bind[0].length = 0;

	// decompose the values
	vector<string> ins;
	size_t pos=0;
	while(pos!=string::npos)
	{
		size_t npos=values.find_first_of('|', pos+1);
		ins.push_back(values.substr(pos==0 ? pos : pos+1, npos-pos)+"'");
		pos=npos;
	}
	int i=1;
	/*for(vector<string>::iterator it=ins.begin(); it!=ins.end(); ++i, ++it)
	{
		bind[i].buffer_type = MYSQL_TYPE_STRING;
		bind[i].buffer = (void*)it->c_str();
		//bind[i].buffer_length = it->length();
		//bind[i].length = new unsigned long(it->length());
	}*/
	
	int idata = 1321;
	bind[1].buffer_type = MYSQL_TYPE_LONG;
	bind[1].buffer = &idata;
	bind[1].buffer_length = 4;
	bind[1].is_null = 0;
	bind[1].length = 0;

	bind[2].buffer = "zzz";
	bind[2].buffer_type = MYSQL_TYPE_VAR_STRING;
	bind[2].is_null = 0;
	bind[2].buffer_length = 3;
	//unsigned long len = 3;
	bind[2].length = 0;

	string err;
	int res = 0;
	res = mysql_stmt_bind_param(stmt, bind);
	err = mysql_stmt_error(stmt);
	res = mysql_stmt_execute(stmt);
	err = mysql_error(conn);
}

bool SQLLog::InitTable(const std::string& table, const std::string& desc)
{
	if(!conn) return false;

	// check if table exists
	mysql_query(conn, ("show tables like '"+table+"'").c_str());
	MYSQL_RES* res = mysql_store_result(conn);
	if(res && mysql_num_rows(res)==1)
	 {
		tables[table] = true;
		mysql_free_result(res);
		return true;
	}
	else 
	{
		// name:type
		typedef vector<pair<string, string> > Columns;
		Columns columns;
		// decompose columns
		size_t pos=0;
		//id:int,name:name
		vector<string> spl = split(desc, ',');
		if(spl.size() < 2) return false;

		for(vector<string>::const_iterator it=spl.begin(); it!=spl.end(); ++it)
		{
			vector<string> nt = split(*it, ':');
			if(nt.size()!=2) return false;
			columns.push_back(pair<string, string>(nt[0], nt[1]));
		}
		string cols;
		for(Columns::const_iterator it=columns.begin(); it!=columns.end(); ++it)
		{
			cols += it->first+" "+GetType(it->second);
			if(it!=columns.end()-1) cols += ", ";
		}
		string init;
			string err = mysql_error(conn);
			int res = mysql_query(conn, ("create table " + table + "(time datetime, "+cols+") engine=archive").c_str());
			if(res) ilog(mysql_error(conn));
			else tables[table] = true;
			return res==0;
	}
		// prepare statement
		/*MYSQL_STMT* stmt = mysql_stmt_init(conn);
		string query = "insert into " + table + " values(?,?,?)";
		int res = mysql_stmt_prepare(stmt, query.c_str(), query.length());
		string err = mysql_stmt_error(stmt);
		statements[table] = stmt;
		return true;*/
}

string SQLLog::GetType(const string& type)
{
	if(type == "name") return "varchar(100)";
	if(type == "text") return "varchar(1000)";
	if(type == "shorttext") return "varchar(200)";
	if(type == "int") return "int";
	if(type == "bool") return "bool";
	return "varchar(100)";
}

SQLLog::SQLLog(MYSQL* conn)
: conn(conn)
{

}

SQLLog::~SQLLog(void)
{
	for(map<string, MYSQL_STMT*>::iterator it=statements.begin(); it!=statements.end(); ++it)
		mysql_stmt_close(it->second);
}
