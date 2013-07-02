#include "common.h"
#include <string>
#include <sstream>

#define LOG_ERROR(err) do{ Log("Utils.dll@%s : %s\n", __FUNCTION__, err); }while(false)

namespace ScriptUtils
{

const char* ExtEvents[] =
{
	"bool %s(Critter&, Map&)", // map in
	"bool %s(Critter&, Map&)"  // map out
};

extern bool ParseScript(const string& script, string& module, string& func);

}

int* GetEventPtr(Critter* cr, int event_type)
{
	return (int*)(&(cr->UserData[CRITTER_EVENT_EXT_INDEX_START + event_type * sizeof(int)]));
}

EXPORT void Critter_ClearExtEvents(Critter* cr)
{
    if( cr->IsNotValid )
	{
        LOG_ERROR("This nullptr.");
		return;
	}
	for(int i=0;i<CRITTER_EVENT_EXT_MAX;i++)
		*GetEventPtr(cr,i) = 0;
}

EXPORT bool Critter_SetEventExt(Critter* cr, int event_type, ScriptString* func_name)
{
    if( cr->IsNotValid )
	{
        LOG_ERROR("This nullptr.");
		return false;
	}
    if(event_type < 0 || event_type >= CRITTER_EVENT_EXT_MAX)
	{
        LOG_ERROR("Invalid event type arg.");
		return false;
	}
    if( !func_name || !func_name->length() )
        *GetEventPtr(cr,event_type) = 0;
    else
    {
		string module, func;
		if(!ScriptUtils::ParseScript(func_name->c_std_str(), module, func))
		{
			LOG_ERROR("Invalid function");
			return false;
		}
		
		static char buf[2048];
		sprintf(buf, ScriptUtils::ExtEvents[event_type], func.c_str());

        *GetEventPtr(cr,event_type) = FOnline->ScriptBind(module.c_str(), buf, false);
        if(*GetEventPtr(cr,event_type) <= 0)
		{
            LOG_ERROR( "Function not found." );
			return false;
		}
    }
    return true;
}

EXPORT bool Critter_EventExtMapIn(Critter* cr, Map* map)
{
	int id = *GetEventPtr(cr, CRITTER_EVENT_EXT_MAP_IN);
	if(id <= 0) return false;
	if(FOnline->ScriptPrepare(id))
	{
		FOnline->ScriptSetArgObject(cr);
		FOnline->ScriptSetArgObject(map);
		FOnline->ScriptRunPrepared();
		return FOnline->ScriptGetReturnedBool();
	}
	return false;
}

EXPORT bool Critter_EventExtMapOut(Critter* cr, Map* map)
{
	int id = *GetEventPtr(cr, CRITTER_EVENT_EXT_MAP_OUT);
	if(id <= 0) return false;
	if(FOnline->ScriptPrepare(id))
	{
		FOnline->ScriptSetArgObject(cr);
		FOnline->ScriptSetArgObject(map);
		FOnline->ScriptRunPrepared();
		return FOnline->ScriptGetReturnedBool();
	}
	return false;
}
