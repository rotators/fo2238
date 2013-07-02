#include "Move.h"

uint8* Lookup=NULL;

void Trace(uint16 hx, uint16 hy, uint16 tx, uint16 ty) // used in generation
{
  float dir=GetDirectionF(hx,hy,tx,ty);
  uint8 dir1,dir2;
  if ((30.0f  <= dir) && (dir <  90.0f)) { dir1=5; dir2=0; }
  else
  if ((90.0f  <= dir) && (dir < 150.0f)) { dir1=4; dir2=5; }
  else
  if ((150.0f <= dir) && (dir < 210.0f)) { dir1=3; dir2=4; }
  else
  if ((210.0f <= dir) && (dir < 270.0f)) { dir1=2; dir2=3; }
  else
  if ((270.0f <= dir) && (dir < 330.0f)) { dir1=1; dir2=2; }
  else
    { dir1=0; dir2=1; }

  uint16 cx=hx;
  uint16 cy=hy;

  uint16 t1x,t1y,t2x,t2y;

  float x1=3*float(hx) + BIAS_FLOAT;
  float y1=SQRT3T2_FLOAT*float(hy) - (float(hx%2))*SQRT3_FLOAT + BIAS_FLOAT;

  float x2=3*float(tx) + BIAS_FLOAT;
  float y2=SQRT3T2_FLOAT*float(ty) - (float(tx%2))*SQRT3_FLOAT + BIAS_FLOAT;

  float dx=x2-x1;
  float dy=y2-y1;

  float c1x,c1y,c2x,c2y; // test hex
  float dist1,dist2;

  int idx = (SIZE_LIN*ty+tx)*MAX_ARR;
  for (int i=0;i<MAX_ARR;i++)
  {
    t1x=cx; t2x=cx;
    t1y=cy; t2y=cy;
    Move(t1x,t1y,dir1);
    Move(t2x,t2y,dir2);
    c1x=3*float(t1x);
    c1y=SQRT3T2_FLOAT*float(t1y) - (float(t1x%2))*SQRT3_FLOAT;
    c2x=3*float(t2x);
    c2y=SQRT3T2_FLOAT*float(t2y) - (float(t2x%2))*SQRT3_FLOAT;
    dist1=dx*(y1-c1y) - dy*(x1-c1x);
    dist2=dx*(y1-c2y) - dy*(x1-c2x);
    dist1=((dist1>0)?dist1:-dist1);
    dist2=((dist2>0)?dist2:-dist2);
    if (dist1<=dist2) { cx=t1x; cy = t1y; Lookup[idx++]=dir1; } // left hand biased
      else { cx=t2x; cy = t2y; Lookup[idx++]=dir2; }

  }
}

void InitLookup() // generation
{
    Lookup=(uint8*)malloc(SIZE_LIN*SIZE_LIN*MAX_ARR*sizeof(char));
    for (int y=0;y<SIZE_LIN;y++) for (int x=0;x<SIZE_LIN;x++)
      Trace(MAX_TRACE,MAX_TRACE,x,y);
}

void FinishLookup()
{
	free(Lookup);
}