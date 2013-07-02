#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef __REFCOUNT_H__
#define __REFCOUNT_H__

// ref counting behavior for Angelscript reference types
class RefCount
{
protected:
	mutable int refCount;
public:
	void AddRef()
	{
		refCount++;
	}

	void Release()
	{
		if( --refCount == 0 )
			delete this;
	}


	RefCount(void) : refCount(1) {};
	virtual ~RefCount(void) {};
};

#endif