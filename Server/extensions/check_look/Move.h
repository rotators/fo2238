// everything is inlined
#include "Defines.h"

inline void Move(uint16& cur_hx, uint16& cur_hy, char dir_move)
{
	switch(dir_move)
	{
        case 0:
            cur_hx--;
            if(!(cur_hx%2)) cur_hy--;
            break;
        case 1:
            cur_hx--;
            if(cur_hx%2) cur_hy++;
            break;
        case 2:
            cur_hy++;
            break;
        case 3:
            cur_hx++;
            if(cur_hx%2) cur_hy++;
            break;
        case 4:
            cur_hx++;
            if(!(cur_hx%2)) cur_hy--;
            break;
        case 5:
            cur_hy--;
            break;
        default:
            return;
	}
}

inline float GetDirectionF(uint16 hx, uint16 hy, uint16 tx, uint16 ty) // used in generation
{
  float nx=3*(float(tx) - float(hx));
  float ny=SQRT3T2_FLOAT*(float(ty) - float(hy)) - (float(tx%2) - float(hx%2))*SQRT3_FLOAT;
  return 180.0f + RAD2DEG*atan2(ny,nx); // in degrees, because cvet loves the degrees
}