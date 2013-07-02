// dllmain.cpp : Defines the entry point for the DLL application.
#include "common.h"
#include <locale.h>
#include "../fonline2238.h"

bool isCompiler=true;

void InitLocationEx();
void FinishMapLocationExt();
void TryInitNoob();
void RegisterAngelScriptExtensions();

int __stdcall DllMain(void* module, unsigned long reason, void* reserved)
{
	switch(reason)
	{
	case 1: // Process attach
		setlocale(LC_ALL, "English");
		break;
	case 2: // Thread attach
		break;
	case 3: // Thread detach
		break;
	case 0: // Process detach
		if(!isCompiler) FinishMapLocationExt();
		break;
	}
	return 1;
}

FONLINE_DLL_ENTRY(compiler)
{
	isCompiler=compiler;
	if(!isCompiler)
	{
		InitLocationEx();
		TryInitNoob();
	}
	RegisterAngelScriptExtensions();
}