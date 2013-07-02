#include "Defines.h"

struct WallDist
{
	const ProtoMap* proto;
	uint8* distances;

	WallDist(const ProtoMap* proto);
	~WallDist();

private:
	void prepare();
};