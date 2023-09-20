using System;

//a)
Console.WriteLine("Dați trei nume separate prin spațiu");
string myNames=Console.ReadLine(); //citesc șirul și-l stochez

//b)
char[,] myLetters = new char[2,31]; 
int[] myCounts = new int[31];
char j='a',  k='A';
for(int i=0; i<26; i++,j++,k++) {	//voi folosi alfabetul latin
	myLetters[0,i]=j;
	myLetters[1,i]=k;
}
char[] diac1 = { 'ă', 'î', 'ș', 'ț', 'â' }, diac2 = { 'Ă', 'Î', 'Ș', 'Ț', 'Â' };	//voi folosi diacritice
for(int i=0; i<5; i++) {
	myLetters[0,26+i]=diac1[i];
	myLetters[1,26+i]=diac2[i];
}

int count=0;
foreach(char c in myNames)
	for(int i=0; i<31; i++)
		try {
			if(c==myLetters[0,i] || c==myLetters[1,i]) { //verific dacă caracterul există în alfabet
				myCounts[i]++;
				count++;
			}
		} catch{}

for(int i=0; i<31; i++)
	if(myCounts[i]>0){
		Console.WriteLine($"Litera {myLetters[0,i]} sau {myLetters[1,i]} a apărut de {myCounts[i]} ori");	//afișez literele în concordanță cu numărul de apariții
}
string[] myInput = myNames.Split(); 
if (count==0 || myInput.Length!=3 )	//dacă nu au fost găsite litere în șir sau numărul de cuvinte nu este 3
	Console.WriteLine("Nu au fost introduse trei nume");