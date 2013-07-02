/*
 * Shared between Utils.dll and client_parameters.dll
 * AngelScript related only!
 */

#include "common.h"
#include <string>
//#include <sstream>

namespace ScriptUtils
{

bool ParseScript(const string& script, string& module, string& func)
{
	int pos = script.find("@");
	if(pos == string::npos)
	{
		asIScriptContext* ctx = ::ScriptGetActiveContext();
		if(!ctx) return false;
		asIScriptFunction* f = ctx->GetFunction();
		if(!f) return false;
		module = string(f->GetModuleName());
		func = script;
		return true;
	}
	module = script.substr(0, pos);
	func = script.substr(pos + 1);
	return true;
}

}

#ifdef __SERVER

uint GetGenericFunctionBind(const ScriptString& scriptfunc, bool temp)
{
	string module;
	string func;
	ScriptUtils::ParseScript(scriptfunc.c_std_str(), module, func);
	func = "int " + func + "(IObject@)";

	uint bind_id = FOnline->ScriptBind(module.c_str(), func.c_str(), temp);
	if(!bind_id)
		Log("Bind function fail: <%s> in module <%s>.\n", func.c_str(), module.c_str());

	return bind_id;
}
int CallGenericFunctionById(uint bind_id, void* object)
{
	if(!FOnline->ScriptPrepare(bind_id))
	{
		Log("Failed to prepare context, bind id<%d>\n", bind_id);
		return -2;
	}

	FOnline->ScriptSetArgObject(object);
	if(FOnline->ScriptRunPrepared())
		return FOnline->ScriptGetReturnedInt();

	return -3;
}

class ScriptFuncBind
{
private:
	uint bind_id;

public:
	ScriptFuncBind() : bind_id(0) {}
	bool IsValid() const
	{
		return bind_id > 0;
	}

	int Call(void* object) const
	{
		if(!IsValid())
		{
			return -1;
			Log("Tried to call an invalid function.\n");
		}
		return CallGenericFunctionById(bind_id, object);
	}

	bool SetFunction(const ScriptString& scriptfunc)
	{
		bind_id = GetGenericFunctionBind(scriptfunc, false);
		if(!bind_id) Log("Trying to bind a nonexistent function% :%s.", scriptfunc.c_str());

		return IsValid();
	}
};

void ScriptFuncBind_Construct(void *memory)
{
	new(memory) ScriptFuncBind();
}

void ScriptFuncBind_Destruct(void *memory)
{
	((ScriptFuncBind*)memory)->~ScriptFuncBind();
}

EXPORT int Global_CallGenericFunction(const ScriptString& scriptfunc, void* object)
{
	uint bind_id = GetGenericFunctionBind(scriptfunc, true);
	if(!bind_id)
		return -1;

	return CallGenericFunctionById(bind_id, object);
}

EXPORT void Global_RunScript(const ScriptString& scriptfunc, int p0, int p1, int p2, const ScriptString& p3)
{
	string module;
	string func;
	ScriptUtils::ParseScript(scriptfunc.c_std_str(), module, func);
	func = "void " + func + "(Critter&,int,int,int)";

	uint bind_id = FOnline->ScriptBind(module.c_str(), func.c_str(), true);
	if(!bind_id)
	{
		Log("Bind function fail: <%s> in module <%s>.\n", func.c_str(), module.c_str());
		return;
	}

	if(!FOnline->ScriptPrepare(bind_id))
	{
		Log("Failed to prepare context: <%s> in module <%s>.\n", func.c_str(), module.c_str());
		return;
	}

	FOnline->ScriptSetArgInt(p0);
	FOnline->ScriptSetArgInt(p1);
	FOnline->ScriptSetArgInt(p2);
	FOnline->ScriptSetArgObject((void*)&p3);
	FOnline->ScriptRunPrepared();
}

#endif // __SERVER

uint Global_GetCallstack(ScriptArray* modules, ScriptArray* names, ScriptArray* lines, ScriptArray* columns)
{
	asIScriptContext* ctx = ::ScriptGetActiveContext();
	if(!ctx) return NULL;

    int                      line, column;
    const asIScriptFunction* func;
    int                      stack_size = ctx->GetCallstackSize();

	uint idx = 0;
	for(int i = 0; i < stack_size; i++)
		if(ctx->GetFunction(i)) idx++;

	modules->Resize(idx);
	names->Resize(idx);
	lines->Resize(idx);
	columns->Resize(idx);
	idx = 0;

    for(int i = 0; i < stack_size; i++)
    {
        func = ctx->GetFunction(i);
        line = ctx->GetLineNumber(i, &column);
        if(func)
		{
			ScriptString& modname = ScriptString::Create(func->GetModuleName());
			*((ScriptString**)(modules->At(idx))) = &modname;
			ScriptString& funcname = ScriptString::Create(func->GetName());
			*((ScriptString**)(names->At(idx))) = &funcname;
			*((uint*)(lines->At(idx)))=line;
			*((uint*)(columns->At(idx)))=column;
			idx++;
		}
    }
	return idx;
}

bool Global_GetCurrentModule( ScriptString& moduleName )
{
	asIScriptContext* ctx = ::ScriptGetActiveContext();
	if( !ctx )
		return( false );

	asIScriptFunction* function = ctx->GetFunction();
	if( !function )
		return( false );

	moduleName = ScriptString::Create( function->GetModuleName() );

	return( true );
}

bool Global_GetCurrentFunction( ScriptString& moduleName, ScriptString& functionName, ScriptString& declaration, bool includeNamespace = false )
{
	asIScriptContext* ctx = ::ScriptGetActiveContext();
	if( !ctx )
		return( false );

	asIScriptFunction* function = ctx->GetFunction();
	if( !function )
		return( false );

	moduleName = ScriptString::Create( function->GetModuleName() );
	functionName = ScriptString::Create( function->GetName() );
	declaration = ScriptString::Create( function->GetDeclaration( true, includeNamespace ));

	return( true );
}

uint Global_GetEnumList_module( ScriptString& moduleName, ScriptArray* names )
{
	names->Resize(0);

	asIScriptModule* module = ASEngine->GetModule( moduleName.c_str(), asGM_ONLY_IF_EXISTS );
	if( !module )
		return( 0 );

	uint count = module->GetEnumCount();
	if( count <= 0 )
		return( count );

	names->Resize( count );

	for( uint e=0; e<count; e++ )
	{
		int enumTypeId;
		const char* enumName = module->GetEnumByIndex( e, &enumTypeId );
		ScriptString& outEnumName = ScriptString::Create( enumName );
		*((ScriptString**)(names->At(e))) = &outEnumName;
	}

	return( count );
}

uint Global_GetEnumList( ScriptArray* names )
{
	ScriptString& moduleName = ScriptString::Create( "" );
	if( Global_GetCurrentModule( moduleName ))
	{
		return( Global_GetEnumList_module( moduleName, names ));
	}

	return( 0 );
}

uint Global_GetEnum_module( ScriptString& moduleName, ScriptString& enumName, ScriptArray* names, ScriptArray* values )
{
	names->Resize(0);
	values->Resize(0);

	asIScriptModule* module = ASEngine->GetModule( moduleName.c_str(), asGM_ONLY_IF_EXISTS );
	if( !module )
		return( 0 );

	for( int e=0, elen=module->GetEnumCount(); e<elen; e++ )
	{
		int enumTypeId;
		const char* eName = module->GetEnumByIndex( e, &enumTypeId );

		if( !strcmp(enumName.c_str(), eName) )
		{
			int count = module->GetEnumValueCount( enumTypeId );
			if( count <= 0 )
				return( 0 );

			names->Resize( count );
			values->Resize( count );

			for( int v=0; v<count; v++ )
			{
				int outValue;
				const char* valueName = module->GetEnumValueByIndex( enumTypeId, v, &outValue );

				ScriptString& outValueName = ScriptString::Create( valueName );
				*((ScriptString**)(names->At(v))) = &outValueName;
				*((int*)(values->At(v))) = outValue;
			}
			return( count );
		}
	}

	return( 0 );
}

uint Global_GetEnum( ScriptString& enumName, ScriptArray* names, ScriptArray* values )
{
	ScriptString& moduleName = ScriptString::Create( "" );
	if( Global_GetCurrentModule( moduleName ))
	{
		return( Global_GetEnum_module( moduleName, enumName, names, values ));
	}

	return( 0 );
}

bool Global_EnumContains_module( ScriptString& moduleName, ScriptString& enumName, int value )
{
	asIScriptModule* module = ASEngine->GetModule( moduleName.c_str(), asGM_ONLY_IF_EXISTS );
	if( !module )
		return( false );

	for( int e=0, elen=module->GetEnumCount(); e<elen; e++ )
	{
		int enumTypeId;
		const char* eName = module->GetEnumByIndex( e, &enumTypeId );

		if( !strcmp(enumName.c_str(), eName) )
		{
			int count = module->GetEnumValueCount( enumTypeId );
			if( count <= 0 )
				return( false );

			for( int v=0; v<count; v++ )
			{
				int outValue;
				module->GetEnumValueByIndex( enumTypeId, v, &outValue );

				if( outValue == value )
					return( true );
			}
		}
	}

	return( false );
}

bool Global_EnumContains( ScriptString& enumName, int value )
{
	ScriptString& moduleName = ScriptString::Create( "" );
	if( Global_GetCurrentModule( moduleName ))
	{
		return( Global_EnumContains_module( moduleName, enumName, value ));
	}

	return( false );
}

uint Global_GetEnumValueCount_module( ScriptString& moduleName, ScriptString& enumName )
{
	asIScriptModule* module = ASEngine->GetModule( moduleName.c_str(), asGM_ONLY_IF_EXISTS );
	if( !module )
		return( 0 );

	for( int e=0, elen=module->GetEnumCount(); e<elen; e++ )
	{
		int enumTypeId;
		const char* eName = module->GetEnumByIndex( e, &enumTypeId );

		if( !strcmp(enumName.c_str(), eName) )
		{
			int count = module->GetEnumValueCount( enumTypeId );
			if( count <= 0 )
				return( 0 );
			return( count );
		}
	}

	return( 0 );
}

uint Global_GetEnumValueCount( ScriptString& enumName )
{
	ScriptString& moduleName = ScriptString::Create( "" );
	if( Global_GetCurrentModule( moduleName ))
	{
		return( Global_GetEnumValueCount_module( moduleName, enumName ));
	}

	return( 0 );
}

void RegisterAngelScriptExtensions()
{
	const char* fail = "Failed to register";

	if(ASEngine->RegisterGlobalFunction("uint GetCallstack(string@[]@+ modules, string@[]@+ names, uint[]@+ lines, uint[]@+ columns)", asFUNCTION(Global_GetCallstack), asCALL_CDECL) < 0)
		Log("%s GetCallstack()\n", fail);
	if(ASEngine->RegisterGlobalFunction("bool GetCurrentModule(string& moduleName)", asFUNCTION(Global_GetCurrentModule), asCALL_CDECL) < 0)
		Log("%s GetCurrentModule()\n", fail);
	if(ASEngine->RegisterGlobalFunction("bool GetCurrentFunction(string& moduleName, string& functionName, string& declaration, bool includeNamespace = false)", asFUNCTION(Global_GetCurrentFunction), asCALL_CDECL) < 0)
		Log("%s GetCurrentFunction()\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnum(string& enumName, string@[]@+ names, int[]@+ values)", asFUNCTION(Global_GetEnum), asCALL_CDECL) < 0)
		Log("%s GetEnum()\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnum(string& moduleName, string& enumName, string@[]@+ names, int[]@+ values)", asFUNCTION(Global_GetEnum_module), asCALL_CDECL) < 0)
		Log("%s GetEnum(moduleName)\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnumList(string@[]@+ names)", asFUNCTION(Global_GetEnumList), asCALL_CDECL) < 0)
		Log("%s GetEnumList()\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnumList(string& moduleName, string@[]@+ names)", asFUNCTION(Global_GetEnumList_module), asCALL_CDECL) < 0)
		Log("%s GetEnumList(moduleName)\n", fail);
	if(ASEngine->RegisterGlobalFunction("bool EnumContains(string& enumName, int value)", asFUNCTION(Global_EnumContains), asCALL_CDECL) < 0)
		Log("%s EnumContains()\n", fail);
	if(ASEngine->RegisterGlobalFunction("bool EnumContains(string& moduleName, string& enumName, int value)", asFUNCTION(Global_EnumContains_module), asCALL_CDECL) < 0)
		Log("%s EnumContains(moduleName)\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnumValueCount(string& enumName)", asFUNCTION(Global_GetEnumValueCount), asCALL_CDECL) < 0 )
		Log("%s GetEnumValueCount()\n", fail);
	if(ASEngine->RegisterGlobalFunction("uint GetEnumValueCount(string& moduleName, string& enumName)",asFUNCTION(Global_GetEnumValueCount_module), asCALL_CDECL) < 0 )
		Log("%s GetEnumValueCount(moduleName)\n", fail);

#ifdef __SERVER
	if(ASEngine->RegisterInterface("IObject") < 0)
		Log("%s IObject\n", fail);
	if(ASEngine->RegisterGlobalFunction("int CallGenericFunction(const string& funcname, IObject@+ object)",asFUNCTION(Global_CallGenericFunction), asCALL_CDECL) < 0 )
		Log("%s CallGenericFunction()\n", fail);
	if(ASEngine->RegisterGlobalFunction("void RunScript(string& funcname, int p0, int p1, int p2, const string& p3)",asFUNCTION(Global_RunScript), asCALL_CDECL) < 0 )
		Log("%s RunScript()\n", fail);

	if(ASEngine->RegisterObjectType("FuncBind", sizeof(ScriptFuncBind), asOBJ_VALUE) < 0)
		Log("%s FuncBind\n", fail);
	if(ASEngine->RegisterObjectBehaviour("FuncBind", asBEHAVE_CONSTRUCT, "void f()", asFUNCTION(ScriptFuncBind_Construct), asCALL_CDECL_OBJFIRST) < 0)
		Log("%s FuncBind ctor\n", fail);
	if(ASEngine->RegisterObjectBehaviour("FuncBind", asBEHAVE_DESTRUCT, "void f()", asFUNCTION(ScriptFuncBind_Destruct), asCALL_CDECL_OBJFIRST) < 0)
		Log("%s FuncBind dtor\n", fail);
	if(ASEngine->RegisterObjectMethod("FuncBind", "bool IsValid() const", asMETHOD(ScriptFuncBind, IsValid), asCALL_THISCALL) < 0)
		Log("%s FuncBind::IsValid()\n", fail);
	if(ASEngine->RegisterObjectMethod("FuncBind", "int Call(IObject@+ object) const", asMETHOD(ScriptFuncBind, Call), asCALL_THISCALL) < 0)
		Log("%s FuncBind::Call(...)\n", fail);
	if(ASEngine->RegisterObjectMethod("FuncBind", "bool SetFunction(string& funcname)", asMETHOD(ScriptFuncBind, SetFunction), asCALL_THISCALL) < 0)
		Log("%s FuncBind::SetFunction(...)\n", fail);
#endif // __SERVER

}