#include <iostream>
#include <string>
using namespace std;


void backward(string value, int pos)
{
	if (pos >= 0)
	{
		cout << value[pos];
		backward(value, pos - 1);
	}
}


int main()
{
	
	string word = "tool";
	
	int Len = word.length();

	backward(word, Len);

	cout << endl;
}


