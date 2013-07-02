#include "../fonline2238.h"
#pragma warning(disable: 4005)
#include "../../scripts/_mapper_defines.fos"

int __stdcall DllMain(void* module, unsigned long reason, void* reserved)
{
	return 1;
}

struct MapObjectEx : MapObject
{
	struct _RunTime
	{
		ProtoMap* FromMap;
		uint MapObjId;
		char PicMapName[64];
		char PicInvName[64];
		long RefCounter;
	} RunTime;
};

CritterCl* GetCritterCl(MapObject& mobj)
{
	MapObjectEx& mobj_= (MapObjectEx&)(mobj);
	if(mobj_.MapObjType!=MAP_OBJECT_CRITTER) return NULL;
	Field* field=GetField(mobj_.MapX,mobj_.MapY);
	if(!field) return NULL;
	if(field->Crit && mobj_.RunTime.MapObjId==field->Crit->Id) return const_cast<CritterCl*>(field->Crit);
	else for(CrClVecIt it=field->DeadCrits.begin(),end=field->DeadCrits.end();it!=end;++it)
		if(mobj_.RunTime.MapObjId==(*it)->Id) return *it;

	return NULL;
}

EXPORT void MapObject_CritterCl_SetParam(MapObject& mobj, uint16 param, int val)
{
	CritterCl* cr=GetCritterCl(mobj);
	if(!cr) return;
	const_cast<int*>(cr->Params)[param]=val;
}

EXPORT int MapObject_CritterCl_GetParam(MapObject& mobj, uint16 param)
{
	CritterCl* cr=GetCritterCl(mobj);
	if(!cr) return 0;
	return cr->Params[param];
}

EXPORT void MapObject_CritterCl_SetBaseType(MapObject& mobj, uint crtype)
{
	CritterCl* cr=GetCritterCl(mobj);
	if(!cr) return;
	(uint)(cr->BaseType)=crtype;
	// todo: alias ctype
}

EXPORT uint MapObject_CritterCl_GetBaseType(MapObject& mobj)
{
	CritterCl* cr=GetCritterCl(mobj);
	if(!cr) return 0;
	return cr->BaseType;
	// todo: alias ctype
}

FONLINE_DLL_ENTRY(compiler) { }