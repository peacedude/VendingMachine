using System;

namespace VendingMachine
{
    internal class VendingFirst : Vending
    {

        public VendingFirst()
        {
            var cola = new Drinks("Cola", "Brun dryck", 5);
            var fanta = new Drinks("Fanta", "Orange dryck", 5);
            var pepsi = new Drinks("Pepsi", "Brun dryck", 5);
            var zingo = new Drinks("Zingo", "Orange dryck", 5);

            var baguette = new Food("Baguette", "Långt bröd med ost", 20, 200);
            var kycklingspett = new Food("Kycklingspett", "Kyckligng på spett", 30, 120);
            var rakburk = new Food("Räkburk", "Burk med räkor", 500, 100);
            var smorgastarta = new Food("Smörgåstårta", "Smörgås i lager", 195, 1000);
            var paj = new Food("Paj", "Ost och skinkpaj", 30, 200);

            var sten = new Stuff("Sten", "Vanlig sten", 1, 20);
            var diamant = new Stuff("Diamant", "Dyr sten", 9999, 10);
            var skruv = new Stuff("Skruv", "Vanlig skruv", 1, 5);
            var borrmaskin = new Stuff("Borrmaskin", "Maskin att borra med", 595, 2052);

            NumberOfFirst = 4;
            NumberOfSecond = 5;
            NumberOfThird = NumberOfFirst + NumberOfSecond;

            Stock = new IProduct[] { cola, fanta, pepsi, zingo, baguette, kycklingspett, rakburk, smorgastarta, paj, sten, diamant, skruv, borrmaskin };
        }
    }
}
