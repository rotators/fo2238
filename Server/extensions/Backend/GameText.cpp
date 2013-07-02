#include "StdAfx.h"
#include "GameText.h"
#include <fstream>
namespace std { using ::type_info; }
#define _STLP_MSVC _MSC_VER
#include <exception>
#include "ilog.h"

GameText::GameText(const std::string& path)
{
	ifstream f(path.c_str());

	while(f.good())
	{
 		string line;
		getline(f, line);
		// get the number
		int ob = line.find("{");
		int cb = line.find("}");
		if(cb > 0 && ob > 0)
		{
			int lineno = std::atoi(line.substr(ob+1, cb - ob - 1).c_str());
			ob = line.find("{", cb+2); // next group {X}{}{Y}
			cb = line.find("}", ob);
			
			if(ob > 0 && cb > 0)
				strings[lineno] = line.substr(ob+1, cb-1);
		}
	}
}

GameText::~GameText(void)
{
}
