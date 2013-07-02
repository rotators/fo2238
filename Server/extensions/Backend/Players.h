#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __PLAYERS_H__
#define __PLAYERS_H__

#include "mysql.h"

// access to the table storing info about players (name and its current id)
class Players
{
public:
	Players(MYSQL*);
	~Players(void);
};

#endif