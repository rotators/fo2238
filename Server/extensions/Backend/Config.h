#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __CONFIG_H__
#define __CONFIG_H__

#include <string>
#include <map>

class Config
{
public:
	Config(const std::string&);
	~Config(void);

	const std::string& GetValue(const std::string& name) { return values[name]; }
	// notes that dll is called by compiled, not server
	bool IsCompiled() const { return compiled; }
private:
	std::map<std::string, std::string> values;
	bool compiled;
};

#endif