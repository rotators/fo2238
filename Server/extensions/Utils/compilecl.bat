@echo off
cl.exe /nologo /MT /W3 /EHsc /O2 /Gd /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "_CRT_SECURE_NO_WARNINGS" /D "_CRT_SECURE_NO_DEPRECATE" /D "_HAS_ITERATOR_DEBUGGING=0" /D "_SECURE_SCL=0" /D "_HAS_EXCEPTIONS=0" /Fo".\\" /Fd".\\" /FD /c "dllmain.cpp"

cl.exe /nologo /MT /W3 /EHsc /O2 /Gd /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "_CRT_SECURE_NO_WARNINGS" /D "_CRT_SECURE_NO_DEPRECATE" /D "_HAS_ITERATOR_DEBUGGING=0" /D "_SECURE_SCL=0" /D "_HAS_EXCEPTIONS=0" /Fo".\\" /Fd".\\" /FD /c "ProtoMapEx.cpp"

cl.exe /nologo /MT /W3 /EHsc /O2 /Gd /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "_CRT_SECURE_NO_WARNINGS" /D "_CRT_SECURE_NO_DEPRECATE" /D "_HAS_ITERATOR_DEBUGGING=0" /D "_SECURE_SCL=0" /D "_HAS_EXCEPTIONS=0" /Fo".\\" /Fd".\\" /FD /c "Utils.cpp"

link /DLL dllmain.obj ProtoMapEx.obj Utils.obj /OUT:Utils.dll
copy Utils.dll ..\..\scripts\
del *.exp *.lib *.obj vc90.idb