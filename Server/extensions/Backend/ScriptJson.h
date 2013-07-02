#ifndef SCRIPTJSON_H
#define SCRIPTJSON_H

#include "angelscript.h"
#include "json.h"
#include "RefCount.h"

void RegisterScriptJson(asIScriptEngine *engine);
std::string JsonToString(const Json::Value&);

#endif
