// Member function definitions for Date class
#include <iostream>
#include "date.h"
#include <string>
// Initialize static members at file scope;
// one class-wide copy.
const int Date::days[] = { 0, 31, 28, 31, 30, 31, 30,
31, 31, 30, 31, 30, 31 };
const string Date::monthName[] = { "", "January",
"February", "March", "April", "May", "June",
"July", "August", "September", "October",
"November", "December" };
// Date constructor
Date::Date(int m, int d, int y) { setDate(m, d, y); }
// Set the date
Date & Date::setDate(int mm, int dd, int yy)
{
	month = (mm >= 1 && mm <= 12) ? mm : 1;
	year = (yy >= 1900 && yy <= 2100) ? yy : 1900;
	// test for a leap year
	if (month == 2 && isLeapYear(year))
		day = (dd >= 1 && dd <= 29) ? dd : 1;
	else
		day = (dd >= 1 && dd <= days[month]) ? dd : 1;
	return (*this);
}
// Assignment Operator
Date & Date::operator=(const Date &rhs)
{
	(*this).setDate(rhs.getMonth(), rhs.getDay(), rhs.getYear());
	return (*this);
}
// If the year is a leap year, return true;
// otherwise, return false
bool Date::isLeapYear(int testYear) const
{
	if (testYear % 400 == 0 || (testYear % 100 != 0 && testYear % 4 == 0))
		return true; // a leap year
	else
		return false; // not a leap year
}
// Determine if the day is the end of the month
bool Date::isEndOfMonth(int testDay) const
{
	if (month == 2 && isLeapYear(year))
		return (testDay == 29); // last day of Feb. in leap year
	else
		return (testDay == days[month]);
}
// Function to help increment the date
void Date::helpIncrement()
{
	if (!isEndOfMonth(day)) { // date is not at the end of the month
		day++;
	}
	else if (month < 12) { // date is at the end of the month, but month < 12
		day = 1;
		++month;
	}
	else // end of month and year: last day of the year
	{
		day = 1;
		month = 1;
		++year;
	}
}
// Overloaded output operator
ostream &operator<<(ostream &output, const Date &d)
{
	output << d.monthName[d.month] << ' '
		<< d.day << ", " << d.year;
	return output; // enables cascading
}
//get functions 
int Date::getMonth() const
{
	return month;
}
int Date::getDay() const
{
	return day;
}
int Date::getYear() const
{
	return year;
}
string Date::getMonthString() const
{
	return monthName[month];
}