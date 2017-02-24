using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public interface Product
    {
        int price { get; set; }
        int personalStock { get; set; }
        int storeStock { get; set; }
        string name { get; set; }
        string description { get; set; }
        
        void Buy();
        string Inspect();
    }
}
