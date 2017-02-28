using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingGames : Vending
    {

        public VendingGames()
        {
            var doom = new Games("Doom", "Döda demoner", 599);
            var csgo = new Games("Counter-Strike", "Poliser och terrorister", 299);

            var wow = new Games("World of Warcraft", "MMORPG", 199);
            var eve = new Games("EVE Online", "MMORPG", 199);

            var sims = new Games("The Sims 10", "Simulator", 599);
            var city = new Games("SimCity 2017", "Bygg städer", 399);

            NumberOfFirst = 2;
            NumberOfSecond = 2;
            NumberOfThird = NumberOfFirst + NumberOfSecond;

            Stock = new IProduct[] { doom, csgo, wow, eve, sims, city };
        }
    }
}
