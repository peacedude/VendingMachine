using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public interface IProduct
    {
        int Price { get; set; }
        int PersonalStock { get; set; }
        int StoreStock { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        
        void Buy();
        string Inspect();
    }
}
