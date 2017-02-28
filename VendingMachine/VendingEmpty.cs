namespace VendingMachine
{
    internal class VendingEmpty : Vending
    {
        public VendingEmpty()
        {
            var empty = new Stuff("Empty", "Empty", 0, 0) {StoreStock = 0};
            Stock = new IProduct[] {empty};
        }
    }
}