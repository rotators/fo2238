#define SKIP_PRAGMAS
#include "../fonline2238.h"

void RegisterDevConnect();

int __stdcall DllMain(void* module, unsigned long reason, void* reserved)
{
	switch(reason)
	{
	case 1: // Process attach
		break;
	case 2: // Thread attach
		break;
	case 3: // Thread detach
		break;
	case 0: // Process detach
		break;
	}
	return 1;
}

FONLINE_DLL_ENTRY(compiler)
{
	RegisterDevConnect();
}