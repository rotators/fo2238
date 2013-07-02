#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __DATATABLES_H__
#define __DATATABLES_H__

#include "mysql.h"
#include "gametext.h"

void UpdateProtoItem(MYSQL* conn, GameText*, ProtoItem*);

#endif