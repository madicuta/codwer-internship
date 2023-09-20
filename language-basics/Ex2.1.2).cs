using System;

//a)
Console.WriteLine("Dați un șir de numere separate prin spațiu");
string[] myInput = Console.ReadLine().Split(); //citesc șirul și-l stochez în array

//b)
int count=0, i;
double[] myArray = new double[myInput.Length];
for (i=0; i < myArray.Length; i++) 
	myArray[i] = double.NaN; //inițializez cu NaN pentru a putea ulterior calcula minimum, nu am nevoie să se inițializeze cu zero

for (i=0; i < myInput.Length; i++) {
	try {
		myArray[i] = Double.Parse(myInput[i]);	//verific dacă elementul este un număr
		if (myArray[i]%1 == 0){	//verific dacă elementul este întreg
			count++;
			if (count==1)
				Console.Write("\nNumere întregi din șir: "); //se afișează dacă s-a întâlnit un număr întreg
			Console.Write($"{myArray[i]} ");			
		}
	} catch {};
}
if (count==0)
	Console.WriteLine("\nNu au fost găsite numere întregi în șir");

//c)
double min;
i=0;
do{
	min = myArray[i];
	i++;
} while(double.IsNaN(min) && i<myArray.Length); //min se va inițializa cu primul element din tablou care nu este NaN
	
foreach (double n in myArray)
	if (n < min)
		min = n;
if (!double.IsNaN(min))	//verific dacă există numere în tablou
	Console.WriteLine($"\nNumărul minim din șir: {min}");