namespace BeerShop
{
    public interface IBeerFactory
    {
        IBeer Create(string name, decimal price);
    }
}