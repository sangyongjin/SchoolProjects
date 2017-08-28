#ifndef COFFEE_H
#define COFFEE_H
#include <cstring>
#include <iostream>
#include <fstream>
#include "Date.h"
class Beverage
{
public:
	//Beverage(char*, int ,double , Date ); // constructor
	~Beverage() {}; // deconstructor
	Beverage & setDrinkName(char*);
	Beverage & setcaffeineAmount(int);
	Beverage & setPrice(double);
	Beverage & setDatePurchased(Date);//set functions - you must make these
	char getDrinkName();
	int getcaffeineAmount();
	double getPrice();
	Date getDatePurchased();
	void print();
	void print(ofstream& out);
private:
	char drinkName[25];
	int caffeineAmount;
	double price;
	Date datePurchased;
};
#endif