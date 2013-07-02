#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __LOG_H__
#define __LOG_H__

#include "mysql.h"
#include <map>

class SQLLog
{
public:
	SQLLog(MYSQL* conn);
	~SQLLog(void);

	bool Write(const std::string&, const std::string&);
	void FastWrite(const std::string&, const std::string&);

	bool InitTable(const std::string&, const std::string&);
private:
	MYSQL *conn;
	map<string, bool> tables;
	map<string, MYSQL_STMT*> statements;

	static string GetType(const std::string&);
};

#endif