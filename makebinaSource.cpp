#include<iostream>
#include<string>
#include<fstream>
using namespace std;

class charStudent {
public: int ID = 802323147;
		char name[40] = "student1";
};

class stringStudent {
public: int ID = 880419;
		string name = "student2";
};

int main() {
	charStudent student1;
	stringStudent student2;
	ofstream outfile;
	ifstream infile;
	outfile.open("student.dat", ios::out | ios::binary);
	outfile.write(reinterpret_cast<const char*>(&student1), sizeof(charStudent));
	outfile.write(reinterpret_cast<const char*>(&student2), sizeof(stringStudent));
	outfile.close();

	infile.open("student.dat", ios::in | ios::binary);
	infile.read(reinterpret_cast<char*>(&student1), sizeof(charStudent));
	infile.read(reinterpret_cast<char*>(&student2), sizeof(stringStudent));
	infile.close();

	cout << "Student1 ID:" << student1.ID << endl;
	cout << "Student1 name: " << student1.name << endl;
	cout << "\n\n";
	cout << "Student2 ID:" << student2.ID << endl;
	cout << "Student2 name: " << student2.name << endl;

	system("pause");
	return 0;
}