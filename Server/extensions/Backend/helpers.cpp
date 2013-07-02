#include "stdafx.h"
#include "helpers.h"

int ParseFieldInt(char* field, unsigned long length)
{
	if(field)
	{
		int i = 0;
		sscanf_s(field, "%d", &i);
		return i;
	}
	else return 0;
}

string ParseFieldStr(char* field, unsigned long length)
{
	if(field)
	{
		string str;
		str.insert(0, field, length);
		return str;
	}
	else return "";
}

std::string ToString(const int value)
{
   char buffer[12];
   sprintf_s(buffer, "%i", value);
   return buffer;
}
