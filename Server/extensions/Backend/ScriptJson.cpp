#include "StdAfx.h"
#include "ScriptJson.h"

using namespace std;

static void Construct(Json::Value* thisPointer)
{
	new(thisPointer) Json::Value();
}

static void CopyConstruct(const Json::Value& other, Json::Value* thisPointer)
{
	new(thisPointer) Json::Value(other);
}

static void Destruct(Json::Value* thisPointer)
{
	thisPointer->~Value();
}

Json::Value& opAssign(Json::Value& json, int i)
{
	json = i;
	return json;
}
Json::Value& opAssign(Json::Value& json, ScriptString* s)
{
	json = s->c_std_str();
	return json;
}
Json::Value& opIndex(Json::Value& json, ScriptString* key)
{
	return json[key->c_std_str()];
}
bool isMember(Json::Value& json, ScriptString* member)
{
	return json.isMember(member->c_std_str());
}
string JsonToString(const Json::Value& json)
{
	Json::FastWriter writer;
	return writer.write(json);
}
ScriptString* ToString(const Json::Value& json)
{
	return &(ScriptString::Create(JsonToString(json).c_str()));
}

void ToString(const Json::Value& json, ScriptString* res)
{
	res = &(ScriptString::Create(JsonToString(json).c_str()));
}
unsigned int JsonSize(const Json::Value& json)
{
	return json.size();
}

ScriptString* Type(const Json::Value& json)
{
	string str;
	switch(json.type())
	{
		using namespace Json;
	case nullValue:
		str="null";
		break;
	case intValue:
		str="int";
		break;
	case uintValue:
		str="uint";
		break;
	case realValue:
		str="double";
		break;
	case stringValue:
		str="string";
		break;
	case booleanValue:
		str="bool";
		break;
	case arrayValue:
		str="array";
		break;
	case objectValue:
		str="object";
		break;
	default:
		str="unknown";
	}
	return &(ScriptString::Create(str.c_str()));
}
ScriptString* AsString(const Json::Value& json)
{
	return &(ScriptString::Create(json.asString().c_str()));
}

bool AsString(const Json::Value& json, ScriptString* out)
{
	if(!json.isString()) return false;
	out = &(ScriptString::Create(json.asString().c_str()));
	return true;
}
bool AsInt(const Json::Value& json, int& out)
{
	if(!json.isInt()) return false;
	out = json.asInt();
	return true;
}
bool AsUint(const Json::Value& json, unsigned int& out)
{
	if(!json.isUInt()) return false;
	out = json.asUInt();
	return true;
}
bool AsBool(const Json::Value& json, bool& out)
{
	if(!json.isBool()) return false;
	out = json.asBool();
	return true;
}
bool AsFloat(const Json::Value& json, float& out)
{
	if(!json.isDouble()) return false;
	out = json.asFloat();
	return true;
}
bool AsDouble(const Json::Value& json, double& out)
{
	if(!json.isDouble()) return false;
	out = json.asDouble();
	return true;
}

void RegisterJsonType(asIScriptEngine* asEngine)
{
	int r;
	// Register the type
	r = asEngine->RegisterObjectType("Json", sizeof(Json::Value), asOBJ_VALUE | asOBJ_APP_CLASS_CDAK); AS_CHECK(r);

	//r = asEngine->RegisterObjectBehaviour("Json", asBEHAVE_FACTORY,    "Json @f()",                 asFUNCTION(JsonDefaultFactory), asCALL_CDECL); AS_CHECK(r);
	//r = asEngine->RegisterObjectBehaviour("Json", asBEHAVE_FACTORY,    "Json @f(const string &in)", asFUNCTION(JsonParseFactory), asCALL_CDECL); AS_CHECK(r);

	r = asEngine->RegisterObjectBehaviour("Json", asBEHAVE_CONSTRUCT,  "void f()",                    asFUNCTION(Construct), asCALL_CDECL_OBJLAST); AS_CHECK(r);
	r = asEngine->RegisterObjectBehaviour("Json", asBEHAVE_CONSTRUCT,  "void f(const Json&in)",    asFUNCTION(CopyConstruct), asCALL_CDECL_OBJLAST); AS_CHECK(r);
	r = asEngine->RegisterObjectBehaviour("Json", asBEHAVE_DESTRUCT,   "void f()",                    asFUNCTION(Destruct),  asCALL_CDECL_OBJLAST); AS_CHECK(r);

	r = asEngine->RegisterObjectMethod("Json", "Json& opAssign(const Json&in)", asMETHOD(Json::Value, operator=), asCALL_THISCALL); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "Json& opAssign(int)", asFUNCTIONPR(opAssign, (Json::Value&, int), Json::Value&), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "Json& opAssign(const string&in)", asFUNCTIONPR(opAssign, (Json::Value&, ScriptString*), Json::Value&), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "Json& opIndex(const string&in)", asFUNCTION(opIndex), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "Json& opIndex(int)", asMETHODPR(Json::Value, operator[], (int), Json::Value&), asCALL_THISCALL); AS_CHECK(r);

	// methods
	//r = asEngine->RegisterObjectMethod("string", "bool opEquals(const string &in) const", asFUNCTIONPR(operator ==, (const string &, const string &), bool), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	//r = asEngine->RegisterObjectMethod("string", "int opCmp(const string &in) const", asFUNCTION(StringCmp), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	//r = asEngine->RegisterObjectMethod("string", "string@ opAdd(const string &in) const", asFUNCTIONPR(operator +, (const ScriptString &, const ScriptString &), ScriptString*), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "int AsInt() const", asMETHOD(Json::Value, asInt), asCALL_THISCALL); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool AsInt(int&out) const", asFUNCTIONPR(AsInt, (const Json::Value&, int&), bool), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "string@ AsString() const", asFUNCTIONPR(AsString, (const Json::Value&), ScriptString*), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool AsString(string&out) const", asFUNCTIONPR(AsString, (const Json::Value&, ScriptString*), bool), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool AsBool() const", asMETHOD(Json::Value, asBool), asCALL_THISCALL); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool AsBool(bool&out) const", asFUNCTIONPR(AsBool, (const Json::Value&, bool&), bool), asCALL_CDECL_OBJFIRST); AS_CHECK(r);	
	
	r = asEngine->RegisterObjectMethod("Json", "bool IsMember(const string &in) const", asFUNCTION(isMember), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool IsString() const", asMETHOD(Json::Value, isString), asCALL_THISCALL); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool IsArray() const", asMETHOD(Json::Value, isArray), asCALL_THISCALL); AS_CHECK(r);

	r = asEngine->RegisterObjectMethod("Json", "string@ Type() const", asFUNCTIONPR(Type, (const Json::Value&), ScriptString*), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "string@ ToString() const", asFUNCTIONPR(ToString, (const Json::Value&), ScriptString*), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "void ToString(string&out) const", asFUNCTIONPR(ToString, (const Json::Value&, ScriptString*), void), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "uint Size() const", asFUNCTION(JsonSize), asCALL_CDECL_OBJFIRST); AS_CHECK(r);
	r = asEngine->RegisterObjectMethod("Json", "bool Empty() const", asMETHOD(Json::Value, empty), asCALL_THISCALL); AS_CHECK(r);
}