#include "common.h"
#include <stdio.h>

struct ClientEx : Client
{
	uint UID[5];
	volatile int Sock; // in fact, SOCKET
	sockaddr_in From;
};

// just float<->int converter, for serializator
EXPORT float IntToFloat(int value)
{
	return *(float*)(&value);
}

EXPORT int FloatToInt(float value)
{
	return *(int*)(&value);
}

EXPORT unsigned long long getticks()
{
    LARGE_INTEGER t;
    unsigned long long ret=0;
    QueryPerformanceCounter(&t);
    ret=t.HighPart;
    ret<<=32;
    ret+=t.LowPart;
	return ret;
}

EXPORT int Critter_GetSocket(Critter& cr)
{
    if (cr.CritterIsNpc) return 0;
    return ((ClientEx&)cr).Sock;
}

EXPORT int Critter_GetScore(Critter& cr, uint8 score)
{
    return cr.Scores[score];
}

EXPORT uint Critter_GetUID(Critter& cr, uint8 num)
{
    if (cr.CritterIsNpc) return 0;
    return ((ClientEx&)cr).UID[num];
}

EXPORT uint Critter_GetIp( Critter& cr )
{
	if( cr.CritterIsNpc )
		return( 0 );
	else
	{
		//return( ((ClientEx&)cr).From.sin_addr.s_addr );
		uint idx = cr.DataExt->CurrentIp;
		if( idx >= MAX_STORED_IP )
			return( 0 );
		else
			return( cr.DataExt->PlayIp[idx] );
	}
}

EXPORT void Critter_SetAccess( Critter& player, int access )
{
  if( !player.CritterIsNpc )
  {
	  Client* cl=(Client*)&player;
	  (uint8)(cl->Access) = access; // 1 << (access);
  }
}

EXPORT uint Scenery_GetParam(MapObject& scenery, uint8 num)
{
    return scenery.MScenery.Param[num];
}

EXPORT bool __cdecl _AllowSlotHand1(uint8 slot, Item& item, Critter& fromCr, Critter& toCr)
{
	return toCr.Params[PE_AWARENESS]!=0;
}

EXPORT int _AllowParameterIfAwareness(uint parameter, Critter& fromCr, Critter& toCr)
{
	return (toCr.Params[PE_AWARENESS]>0)?fromCr.Params[parameter]:0;
}

EXPORT int _AllowParameterIfEqualNZ(uint parameter, Critter& fromCr, Critter& toCr)
{
	if(fromCr.Params[parameter]>0 && (fromCr.Params[parameter]==toCr.Params[parameter])) return fromCr.Params[parameter];
	return 0;
}

EXPORT int String_ReplaceText(ScriptString& str, ScriptString& text, ScriptString& replacement)
{
	int size=text.length();
	if(!size) return 0;
	int size_rep=replacement.length();
	string st=str.c_std_str();
	int pos=st.find(text.c_std_str(),0);
	int num=0;
	while(pos>=0)
	{
		st.replace(pos,size,replacement.c_std_str());
		pos=st.find(text.c_std_str(),pos+size_rep);
		num++;
	}
	str=st;
	return num;
}

EXPORT void String_ParseFloat(ScriptString& str, float val, uint8 precision)
{
	static char prec[32];
	sprintf(prec,"%%.%df",precision);
	static char buf[1024];
	sprintf(buf,prec,val);
	str.assign(buf);
}

EXPORT void Map_ProtoName(Map& map, ScriptString& result)
{
	result=map.Proto->Name;
}

EXPORT void Location_ProtoName(Location& location, ScriptString& result)
{
	result=location.Proto->Name;
}

// todo: remove and replace with fonline-provided callers
EXPORT void RunScript(const ScriptString& scriptfunc, int p0, int p1, int p2, const ScriptString& p3)
{
	int delim = scriptfunc.c_std_str().find("@");
	if(delim == -1) return;
	string modulename = scriptfunc.c_std_str().substr(0, delim);
	string funcname = scriptfunc.c_std_str().substr(delim + 1);

	asIScriptModule* module = ASEngine->GetModule(modulename.c_str());
	if(!module) return;
	asIScriptFunction* script_func = module->GetFunctionByName(funcname.c_str());
	if(!script_func) return;

	asIScriptContext* ctx = ASEngine->CreateContext();
	ctx->Prepare(script_func);
	ctx->SetArgDWord(0, p0);
	ctx->SetArgDWord(1, p1);
	ctx->SetArgDWord(2, p2);
	ctx->SetArgObject(3, (void*)&p3);
	ctx->Execute();
	
	ctx->Release();
}

EXPORT void Critter_GetLexems( Critter& cr, ScriptString& output )
{
	output.assign(cr.Lexems);
	return;
}

EXPORT void Item_GetLexems( Item& it, ScriptString& output )
{
	if(!it.Lexems)
	{
		output.assign("");
		return;
	}
	output.assign(it.Lexems);
	return;
}
