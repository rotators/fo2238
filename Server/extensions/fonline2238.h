#ifndef __FONLINE_2238__
#define __FONLINE_2238__

// Script constants
#define SKIP_PRAGMAS
// Disable macro redefinition warning
#pragma warning (push)
#pragma warning (disable : 4005)
#define __EXTENSIONS_SOLUTION__
#include "../scripts/_defines.fos"
#include "fonline.h"
#pragma warning (pop)

#ifdef __CLIENT
#pragma message ("compiled as client")
#endif
#ifdef __SERVER
#pragma message ("compiled as server")
#endif
#ifdef __MAPPER
#pragma message ("compiled as mapper")
#endif

// Script global variables
#ifdef INCLUDE_GLOBAL_VARIABLES
struct _GlobalVars
{
	int*  CurX;
	int*  CurY;
	uint* HitAimEyes;
	uint* HitAimHead;
	uint* HitAimGroin;
	uint* HitAimTorso;
	uint* HitAimArms;
	uint* HitAimLegs;
} GlobalVars;
#endif

#endif // __FONLINE_2238__