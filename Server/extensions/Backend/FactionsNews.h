#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __FACTIONSNEWS_H__
#define __FACTIONSNEWS_H__

#include "mysql.h"

// access to data containing the story of membership management
class FactionNews
{
	MYSQL* conn;
public:
	FactionNews(MYSQL*);
	~FactionNews(void);
};

#endif