#include "StdAfx.h"
#include "Config.h"

#include <fstream>
#include <algorithm>

Config::Config(const std::string& fname)
{
	ifstream f(fname.c_str());
	if(!f)
	{
		compiled = true;
		ilog("Couldn't open cfg file " + fname + ". It's normal if called by ascompiler.");
		return;
	}
	else compiled = false;

	string line;
	bool section=false;
	while(f.good())
	{
		getline(f, line);
		line.erase(remove_if(line.begin(), line.end(), isspace), line.end());
		if(line[0] == '#') continue;

		if(line.substr(0, 3) == "Sql"
			|| line.substr(0, 7) == "CouchDB")
		{
			int delim = line.find("=");
			if(delim > 0)
			{
				string key = line.substr(0, delim);
				string value = line.substr(delim + 1);
				values[key] = value;
			}
		}
	}
	if(values.size() == 0)
	{
		ilog("Couldn't obtain config values for SQL backend.");
	}
}

Config::~Config(void)
{
}
