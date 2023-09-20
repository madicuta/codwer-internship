using System;

//a)
int myLength, myNumber;
string myString;
bool myTry;
int i=0;
do {
	if (i>0)
		Console.WriteLine("Nu ați dat un număr de minimum 3 cifre");	//dacă a fost introdus greșit
	Console.WriteLine("Dați un număr de minimum 3 cifre:");
	myString = Console.ReadLine(); 
	myLength = myString.Length;
	myTry = int.TryParse(myString, out myNumber);	//încerc să convertesc string-ul în int
	i++;
} while(myLength < 3 || myTry==false);	//bucla se repetă dacă șirul are mai puțin de 3 caractere sau dacă nu este un număr întreg

//b)
string myMirror="";
Console.WriteLine("\nValoarea în oglindă:");
for(i = myLength-1; i >= 0; i--)
	myMirror += myString[i];	//inversez numărul citit
Console.WriteLine(myMirror);

//c)
double mySqrt = Math.Sqrt(int.Parse(myMirror));	//calculez radicalul
if(mySqrt % 1 == 0)	//verific dacă radicalul este un număr întreg
	Console.WriteLine($"Numărul {myMirror} este un pătrat perfect");
else
	Console.WriteLine($"Numărul {myMirror} nu este un pătrat perfect");