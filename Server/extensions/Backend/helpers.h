#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __FACTION_H__
#define __FACTION_H__

//#include <boost/lexical_cast.hpp>
//#define Str(_str) boost::lexical_cast<std::string>(_str) // some problem with std::type_info

// sql c api wrappers
int ParseFieldInt(char*, unsigned long);
std::string ParseFieldStr(char*, unsigned long);
std::string ToString(const int value);

#endif