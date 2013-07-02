#include "../fonline2238.h"

EXPORT int String_ReplaceText(ScriptString& str, ScriptString& text, ScriptString& replacement)
{
	int size=text.length();
	if(!size) return 0;
	int size_rep=replacement.length();
	string st=str.c_std_str();
	int pos=st.find(text.c_std_str(),0);
	int num=0;
	while(pos>=0)
	{
		st.replace(pos,size,replacement.c_std_str());
		pos=st.find(text.c_std_str(),pos+size_rep);
		num++;
	}
	str=st;
	return num;
}