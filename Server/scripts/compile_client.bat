@echo Compiling: %1
@ascompiler.exe %1 -client -p prep.txt -d __CLIENT 
@if not exist donotpause @pause
