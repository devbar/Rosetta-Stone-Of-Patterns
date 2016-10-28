using System;
using Autofac;
using BeerShop.DataAccessLayer;

namespace BeerShop
{
    public class Program
    {
        private static readonly IContainer container;

        static Program()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<JsonRepository>().As<JsonRepository>();
            containerBuilder.RegisterType<MemoryRepository>().As<MemoryRepository>();

            containerBuilder.RegisterType<JsonRepositoryFactory>().Keyed<IRepositoryFactory>(RepositoryType.Json);
            containerBuilder.RegisterType<MemoryRepositoryFactory>().Keyed<IRepositoryFactory>(RepositoryType.Memory);

            containerBuilder.RegisterType<BeerShopService>().As<IBeerShopService>();
            containerBuilder.RegisterType<BeerShopServiceFactory>().As<IBeerShopServiceFactory>();
            containerBuilder.RegisterType<BeerFactory>().As<IBeerFactory>();

            container = containerBuilder.Build();
        }

        public static void Main(string[] args)
        {
            using (var lifeTimeScope = container.BeginLifetimeScope())
            {
                var beerShopServiceFactory = container.Resolve<IBeerShopServiceFactory>();
                var beerShopService = beerShopServiceFactory.Create(GetDatabase());

                FillRandomData(beerShopService);
                Search(beerShopService);
            }
        }

        private static void Search(IBeerShopService beerShopService)
        {
            for (;;)
            {
                Console.Write("Search: ");
                var term = Console.ReadLine();
                var results = beerShopService.Search(term);

                foreach (var result in results)
                    Console.WriteLine("Found: " + result.Name + ", Price: " + result.Price);
            }
        }

        private static RepositoryType GetDatabase()
        {

            Console.WriteLine("Use JSON File [j]");
            Console.WriteLine("Use Memory [m]");


            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.J && key.Key != ConsoleKey.M);

            Console.WriteLine();

            return key.Key == ConsoleKey.M ? RepositoryType.Memory : RepositoryType.Json;
        }

        private static void FillRandomData(IBeerShopService beerShopService)
        {
            var beers = new string[] { "Corona", "Becks", "Jever", "Heinecken" };
            var prices = new decimal[] { 1.56m, 2.01m, 1.99m, 0.99m, 0.56m, 3.13m };

            Console.WriteLine("Fill Random Data? [y/n]");
            if (Console.ReadKey().Key == ConsoleKey.N)
                return;

            var random = new Random();

            Console.WriteLine();

            foreach (var beer in beers)
            {
                uint amount = (uint)random.Next(0, 80);
                var price = random.Next(0, prices.Length - 1);

                beerShopService.Add(beer, price, amount);
                Console.WriteLine("Added: " + beer + ", Amount: " + amount + ", Price: " + price);
            }

            Console.WriteLine();
        }
    }
}
