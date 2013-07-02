#include "WallDist.h"
#include "Move.h"
#include <queue>
#include <stdio.h>
#include <vector>

vector<WallDist*> Dists;

WallDist::WallDist(const ProtoMap* proto)
{
	distances=(uint8*)malloc(proto->Header.MaxHexX*proto->Header.MaxHexY);
	this->proto=proto;
	memset(distances,255,proto->Header.MaxHexX*proto->Header.MaxHexY);
	prepare();
}

WallDist::~WallDist()
{
	free(distances);
}

#define IDX(_x,_y) ((_y)*proto->Header.MaxHexX+(_x))
void WallDist::prepare()
{
	typedef std::pair<uint16,uint16> Hex;
	std::queue<Hex> q;

	for(uint16 x=0;x<proto->Header.MaxHexX;x++)
		for(uint16 y=0;y<proto->Header.MaxHexY;y++)
		{
			if(FLAG(proto->HexFlags[IDX(x,y)],FH_WALL))
			{
				distances[IDX(x,y)]=0;
				q.push(Hex(x,y));
			}
		};

	while(!q.empty())
	{
		Hex hex=q.front();
		q.pop();
		uint level=distances[IDX(hex.first,hex.second)];
		for(uint8 dir=0;dir<6;dir++)
		{
			uint16 hx=hex.first;
			uint16 hy=hex.second;
			Move(hx,hy,dir);
			if((hx>=proto->Header.MaxHexX) || (hy>=proto->Header.MaxHexY)) continue;
			if(level+1<distances[IDX(hx,hy)])
			{
				distances[IDX(hx,hy)]=level+1;
				if(level<MAX_WALLS_DIST) q.push(Hex(hx,hy));
			}
		}
	}
}

//template<int N>
//class ShowSizeof<N>
//{
//	static int arr[-1];
//};

WallDist* GetProtoDists(Map& map)
{
	WallDist** wd=&(Dists[map.Proto->Pid]);
	if(!*wd) *wd=new WallDist(map.Proto);
	return *wd;
}

void InitDists()
{
	Dists.resize(MAX_PROTO_MAPS);
	for(auto it=Dists.begin(),end=Dists.end();it!=end;++it) *it=NULL;
}

void FinishDists()
{
	for(auto it=Dists.begin(),end=Dists.end();it!=end;++it)
		delete *it;
}
