namespace BeerShop{
    public class Beer : IBeer{
        public string Name { get; private set; }
        public decimal Price {get; private set; }
        public Beer(string name, decimal price){
            Name = name;
            Price = price;
        }
    }
}