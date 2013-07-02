// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __STDAFX_H__
#define __STDAFX_H__

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>

// TODO: reference additional headers your program requires here
#include "../fonline2238.h"

#define __LCC__
#include "mysql.h"
#undef __LCC__

#include <iostream>
#include <string>
#include <sstream>
#include <algorithm>
#include <iterator>
#include "ilog.h"

#define AS_CHECK(x) \
        if(x<0) \
        { \
			stringstream msg; \
			msg << "as_check failed: " << x; \
			ilog(msg.str()); \
        }

void asSetException(const std::string&);

inline std::vector<std::string> &split(const std::string &s, char delim, std::vector<std::string> &elems) {
    std::stringstream ss(s);
    std::string item;
    while(std::getline(ss, item, delim)) {
        elems.push_back(item);
    }
    return elems;
}


inline std::vector<std::string> split(const std::string &s, char delim) {
    std::vector<std::string> elems;
    return split(s, delim, elems);
}

inline std::string &
replacein(std::string &s, const std::string &sub,
const std::string &other)
{
	size_t b = 0;
	for (;;)
	{
		b = s.find(sub, b);
		if (b == s.npos) break;
		s.replace(b, sub.size(), other);
		b += other.size();
	}
	return s;
}

#endif