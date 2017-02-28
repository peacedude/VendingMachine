using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingEmpty : Vending
    {
        public VendingEmpty()
        {
            var empty = new Stuff("Empty", "Empty", 0, 0) {StoreStock = 0};
            Stock = new IProduct[] {empty};
        }
        
    }
}
