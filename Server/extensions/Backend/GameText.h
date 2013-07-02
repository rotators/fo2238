#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __GAMETEXT_H__
#define __GAMETEXT_H__

#include <map>
#include <string>

class GameText
{
public:
	GameText(const std::string&);
	~GameText(void);

	const std::string& GetString(unsigned int idx) { return strings[idx]; };
private:
	map<int, std::string> strings;
};

#endif