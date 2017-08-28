// Definition of class Date
#ifndef DATE1_H
#define DATE1_H
#include <iostream>
#include <string>
using namespace std;
class Date {
	friend ostream &operator<<(ostream &, const Date &);
public:
	Date(int m = 1, int d = 1, int y = 1900);
	Date & setDate(int, int, int);
	Date &operator=(const Date &rhs); // assignment operator
	bool isLeapYear(int) const;
	bool isEndOfMonth(int) const;
	int getMonth() const;
	int getDay() const;
	int getYear() const;
	string getMonthString() const;
private:
	int month;
	int day;
	int year;
	static const int days[]; // array of days per month
	static const string monthName[]; // array of month names
	void helpIncrement(); /* utility function - cannot be accessed
						  by outside functions */
};
#endif

