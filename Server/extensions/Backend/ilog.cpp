#include "StdAfx.h"
#include "ilog.h"

#include <fstream>

HANDLE LogMutex;

void ilog(const std::string& msg)
{
	if(LogMutex == 0)
		LogMutex = CreateMutex(NULL, FALSE, NULL);
	WaitForSingleObject(LogMutex, INFINITE);
	ofstream log("logs/backend.log", ios::app);	
	//SYSTEMTIME st;
    //GetSystemTime(&st);

	log << msg << endl;
	cout << msg << endl;
	log.close();
	ReleaseMutex(LogMutex);
}
