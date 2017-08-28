#include <iostream>
#include "beverage.h"
#include "date.h"
using namespace std;


Beverage & Beverage::setDrinkName(char *temp)
{
	strcpy_s(drinkName,temp);
	return (*this);
}
Beverage & Beverage::setcaffeineAmount(int x)
{
	caffeineAmount = x;
	return (*this);
}
Beverage & Beverage::setPrice(double pricing)
{
	price = pricing;
	return (*this);
}
Beverage & Beverage::setDatePurchased(Date temp)
{
	datePurchased = temp;
	return (*this);
}//set functions - you must make these
char Beverage::getDrinkName()
{
	return drinkName[25];
}
int Beverage::getcaffeineAmount()
{
	return caffeineAmount;
}

double Beverage::getPrice()
{
	return price;
}
Date Beverage::getDatePurchased()
{
	return datePurchased;
}

void Beverage::print()
{
	//cout << "Printing to terminal..." << endl;
	cout << datePurchased << " ";
	cout << drinkName;
	cout<<" $" << price << " (" << caffeineAmount << ")\n";
	
}

void Beverage::print(ofstream & out)//print into text file
{
	out << datePurchased << " ";
	out << drinkName;
	out << " $" << price << " (" << caffeineAmount << ")\n";
}

//get functions - you must make these
/*print function - NOTICE there are TWO. Observe the order in the file
& in the sample run*/

