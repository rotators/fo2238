@echo off
cl.exe /nologo /MT /W3 /EHsc /O2 /Gd /D "_USRDLL" /Fo".\\" /Fd".\\" /FD /c "check_look.cpp"
cl /O2 /LD check_look.obj
del check_look.exp check_look.lib check_look.obj vc90.idb
copy check_look.dll ..\..\scripts\
del check_look.dll