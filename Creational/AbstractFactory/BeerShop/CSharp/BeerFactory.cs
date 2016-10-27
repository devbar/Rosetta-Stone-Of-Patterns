namespace BeerShop{
    public class BeerFactory : IBeerFactory{
        public IBeer Create(string name, decimal price){
            return new Beer(name, price);
        }
    }
}