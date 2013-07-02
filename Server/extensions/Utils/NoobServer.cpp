#include "common.h"
#include <set>
#include <strstream>

// you know it's a hack when you see this
#define private public
#define protected public
#include "../3rdParty/angelscript/source/as_scriptfunction.h"
#include "../3rdParty/angelscript/source/as_callfunc.h"
#include "../3rdParty/angelscript/source/as_objecttype.h"
#undef private
#undef protected

std::set<string> Allowed; // file class can be instanced only inside these modules, and in null module

typedef ScriptFile* (*ScriptFileFactory)();
ScriptFileFactory OldFactory = NULL;

ScriptFile* NewFactory()
{
	asIScriptContext* ctx = ScriptGetActiveContext();
	asIScriptFunction* func = ctx->GetFunction();
	const char* s = func->GetModuleName();
	if(!s || Allowed.count(string(s))) return OldFactory();

	ctx->SetException("file class cannot be created here.");
	return NULL;
}

// return false on any sign of failure
bool HookFileFactory()
{
	asCObjectType* ot = (asCObjectType*)(ASEngine->GetObjectTypeByName("file"));
	if(!ot) false;

	int id = ot->beh.factory;
	if(id < 0) return false;

	asCScriptFunction* func = ((asCScriptFunction*)ASEngine->GetFunctionById(id));
	if(!func) return false;

	OldFactory = (ScriptFileFactory)(func->sysFuncIntf->func);
	func->sysFuncIntf->func=(asFUNCTION_t)NewFactory;
	return true;
}

void TryInitNoob()
{
	FILE* f = fopen("./scripts/scripts.cfg.repo", "r");
	if(!f) return;

	HookFileFactory();

	char buf[1024];
	string value;
	while(!feof(f))
	{
		fgets(buf, 1024, f);
		if(buf[0] != '@')
			continue;

		istrstream str(&buf[1]);
		str >> value;
		if(str.fail() || value != "server")
			continue;
		str >> value;
		if(str.fail() || value != "module")
			continue;

		str >> value;
		Allowed.insert(value);
	}
	fclose(f);

	// modules allowed for modification are not allowed to use files
	f = fopen("./../allowed.txt", "r");
	if(!f) return;

	while(!feof(f))
	{
		fgets(buf, 1024, f);
		string s(buf);
		int pos = s.rfind(".fos");
		if(pos < 0) continue;
		s = s.substr(0, pos);
		pos = s.find_last_of("\\/");
		if(pos < 0) continue;
		s = s.substr(pos + 1, s.length() - pos);
		Allowed.erase(s);
	}
	fclose(f);
}