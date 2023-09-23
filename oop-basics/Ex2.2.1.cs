using System;
using System.Collections.Generic; //pentru listă

//1. test
public struct Dimensiune {
        public decimal lungime;
        public decimal latime;
        public decimal inaltime;
    } 

public abstract class Animal {
	public static int contor = 0;
    public string nume;
    public readonly decimal greutate;
    public Dimensiune dimensiune;
	public readonly decimal viteza;
	public Mancare[] stomac = new Mancare[10]; 
	public Animal(){}
	public Animal(string nume, decimal greutate, decimal viteza, decimal lungime, decimal latime, decimal inaltime)
	{
		contor++;
		dimensiune = new Dimensiune 
			{
				lungime = lungime,
				inaltime = inaltime,
				latime = latime			
			};
		this.nume=nume;
		this.viteza = viteza;
		this.greutate=greutate;
    }
    public virtual void Mananca (Mancare m) {
		if (m.greutate < this.greutate / 8)
		{
			int i = 0;
			while(this.stomac[i]!=null)
				i++;
			this.stomac[i] = m;
			Console.WriteLine("mananca "+ m);
		}
		else
			Console.WriteLine("Prea multă mâncare");
	}
	public abstract double Energie(); //=> this.stomac[0].energie;
	public virtual void Alearga(decimal distanta) {
		decimal e = (decimal)this.Energie();
		if(e==0 || this.viteza==0 ) {
			Console.WriteLine("Divizare la zero");
		}	
		else {
			decimal timp = distanta * e / this.viteza ;
			Console.WriteLine(timp.ToString("F4")+" s"); //afișare cu 4 cifre după virgulă
		}
	}
	public override string ToString() {
		return ($"Tip animal: {GetType()}\nNume: {this.nume}\nGreutate {greutate.ToString("F2")} kg\nDimensiuni " + 
			$"{dimensiune.lungime.ToString("F2")} x {dimensiune.latime.ToString("F2")} x " + 
			$"{dimensiune.inaltime.ToString("F2")}\nViteza {viteza.ToString("F2")} m/s\n");
	}
}

class Carnivor : Animal {
	public static int contor = 0;
	public new Mancare[] stomac = new Mancare[10]; 
	public Carnivor(){}
	public Carnivor(string nume, decimal greutate, decimal viteza, decimal lungime, decimal latime, decimal inaltime)
        : base(nume, greutate, viteza, lungime, latime, inaltime) {contor++;}
    
    public override double Energie() {
    	decimal sumEnergie=0, sumGreutate=0, medGreutate;
    	int count=0;
    	foreach(Mancare m in ((Animal)this).stomac)
			if (m!=null) {
			sumEnergie+=m.energie;
			sumGreutate+=m.greutate;
			count++;
		}
    	try {
    		medGreutate = sumGreutate/count;
    		return (double)(0.2m - medGreutate/5 + sumEnergie);
    	}
    	catch { 
    		Console.WriteLine("Lipsă de energie");
    		return 0;
    	}
    } 
    public override void Alearga(decimal distanta){
    	base.Alearga(distanta);
    }
    public override void Mananca(Mancare m){
    	if (m is Carne)
    		base.Mananca(m);
    	else
    		Console.WriteLine("Mâncare nepotrivită");
    }
}

class Erbivor : Animal {
	public static int contor = 0;
	public Erbivor() {}
	public Erbivor (string nume, decimal greutate, decimal viteza, decimal lungime, decimal latime, decimal inaltime)
        : base(nume, greutate, viteza, lungime, latime, inaltime){
        	stomac = new Mancare[10];	
        	contor++;
    }
    new Mancare[] stomac;
    public override double Energie() {
    	decimal sumEnergie=0, sumGreutate=0, medGreutate;
    	int count=0;
    	foreach(Mancare m in ((Animal)this).stomac)
			if (m!=null) {
			sumEnergie+=m.energie;
			sumGreutate+=m.greutate;
			count++;
		}
    	medGreutate = sumGreutate/count;
    	return (double)(0.5m + medGreutate/3 + sumEnergie);
    }
    public override void Alearga(decimal distanta){
    	base.Alearga(distanta);
    }
    public override void Mananca(Mancare m){
    	if (m is Planta)
    		base.Mananca(m);
    	else
    		Console.WriteLine("Mâncare nepotrivită");
    }
}

class Omnivor : Animal {
	public static int contor = 0;
	public Omnivor(){}
	public Omnivor (string nume, decimal greutate, decimal viteza, decimal lungime, decimal latime, decimal inaltime)
        : base(nume, greutate, viteza, lungime, latime, inaltime){
        	stomac = new Mancare[10];
        	contor++;
    }
    new Mancare[] stomac;
    public override double Energie() {
    	decimal sumEnergie=0, sumGreutate=0, medCoefGreutate, coefGreutate=1;
    	int count=0;
    	foreach(Mancare m in ((Animal)this).stomac)
			if (m!=null) {
				if (m is Carne) coefGreutate=-1/2m;
				if (m is Planta) coefGreutate=1/2m;
				
				sumEnergie+=m.energie;
				sumGreutate+=m.greutate*coefGreutate;
				count++;
			}
    	medCoefGreutate = sumGreutate/count; //Console.WriteLine((double)(0.35m + medCoefGreutate + sumEnergie));
    	return (double)(0.35m + medCoefGreutate + sumEnergie);
    } 
    public override void Alearga(decimal distanta){
    	base.Alearga(distanta);
    }
}

public abstract class Mancare {
    public decimal greutate { get; set; }
    private decimal _energie;
    public decimal energie {
        get { return _energie; }
        set
        {
            if (value >= 0 && value <= 0.5m)
                _energie = value;
            else
                Console.WriteLine("Energia trebuie să fie între 0 și 0.5.");
        }
    }
	public Mancare(decimal greutate, decimal energie){this.greutate=greutate; this.energie=energie;}
}
class Carne : Mancare {
	public Carne(decimal greutate, decimal energie):base(greutate, energie){}
}
public class Planta : Mancare {
	public Planta(decimal greutate, decimal energie):base(greutate, energie){}
}

//2.
public enum TipAnimal { Lup, Urs, Oaie, Veverita, Pisica, Vaca }

class Program{
	public static Animal CreeazaAnimal(TipAnimal tip, string nume, decimal greutate, decimal viteza, 
		decimal lungime, decimal latime, decimal inaltime) {
		switch(tip) {
			case TipAnimal.Lup:
			case TipAnimal.Pisica:
				return new Carnivor(nume, greutate, viteza, lungime, latime, inaltime);
			case TipAnimal.Oaie:
			case TipAnimal.Vaca:
				return new Erbivor(nume, greutate, viteza, lungime, latime, inaltime);
			case TipAnimal.Urs:
			case TipAnimal.Veverita:
				return new Omnivor(nume, greutate, viteza, lungime, latime, inaltime);
			default:
				Console.WriteLine("Tip incorect");
				return null;
		}
	}

	public static int Main(){
		Carnivor lup = new Carnivor("Woofey", 25.5m, 11m, 1.2m, 0.6m, 0.8m);
		Erbivor oaie = new Erbivor("Beh", 30.0m, 9m, 1.0m, 0.5m, 0.7m); 
		Omnivor urs = new Omnivor("Roar", 300.0m, 14m, 2.0m, 1.0m, 1.5m);  

		var salata = new Planta(0.2m, 0.3m); 
		var sunca = new Carne(0.4m, 0.5m);  
		
		lup.Mananca(sunca);	lup.Mananca(sunca);
		oaie.Mananca(salata); oaie.Mananca(salata); oaie.Mananca(salata);
		urs.Mananca(sunca); urs.Mananca(salata); urs.Mananca(salata); urs.Mananca(salata);
		
		lup.Alearga(200);
		oaie.Alearga(200);
		urs.Alearga(200);
		

		Console.WriteLine( oaie.ToString() );
		Console.WriteLine( (new Carnivor("Woofey", 25.5m, 11m, 1.2m, 0.6m, 0.8m)).ToString() );

		var veverita1 = CreeazaAnimal(TipAnimal.Veverita,"Squirl", 0.3m, 5m, 0.4m, 0.2m, 0.15m);	
		Console.WriteLine( veverita1.ToString() );

		
		//2.i
		List<Animal> lista = new List<Animal>();
		Random random = new Random();
		Random randomGenerator = new Random();
		TipAnimal[] numeTipuri = (TipAnimal[])Enum.GetValues(typeof(TipAnimal));
		for(int i=0; i<10; i++){
			TipAnimal tipRandom = numeTipuri[randomGenerator.Next(numeTipuri.Length)];
			decimal greutateRandom = (decimal)random.NextDouble() * (750 - 0.2m) + 0.2m;
			decimal vitezaRandom = (decimal)random.NextDouble() * (15 - 0.5m) + 0.5m;
			decimal lungimeRandom = (decimal)random.NextDouble() * (2.5m - 0.1m) + 0.1m;
			decimal latimeRandom = (decimal)random.NextDouble() * (1.2m - 0.1m) + 0.1m;
			decimal inaltimeRandom = (decimal)random.NextDouble() * (1.7m - 0.1m) + 0.1m;
			var randomInstanta = CreeazaAnimal(tipRandom,"Animal", greutateRandom, vitezaRandom, 
				lungimeRandom, latimeRandom, inaltimeRandom);
			lista.Add(randomInstanta);
		}
		bool hranaShift = true;
		foreach (var animal in lista) {			
			switch(animal.GetType().Name){
				case "Carnivor":
					animal.Mananca(sunca);
					break;
				case "Erbivor":
					animal.Mananca(salata);
					break;
				case "Omnivor":
		            if (hranaShift) 
		                animal.Mananca(sunca);
		            else 
		                animal.Mananca(salata);
	           		hranaShift = !hranaShift; // Schimbă hrana pentru următorul "Omnivor"
           			break;
				default:
					Console.WriteLine("Animalul nu poate fi hrănit");
				 	break;
			}
		    Console.WriteLine(animal.ToString());
		}

		Console.WriteLine(Animal.contor+" animale instanțiate");
		Console.WriteLine(Omnivor.contor+" animale mănâncă mâncare");
		Console.WriteLine(Carnivor.contor+" animale mănâncă carne");
		Console.WriteLine(Erbivor.contor+" animale mănâncă plante");

	return 0;
	}
}