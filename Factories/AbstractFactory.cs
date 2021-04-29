using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is amaizing!");
        }
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is so sweet!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare();
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare()
        {
            Console.WriteLine("Coffee preparation ...");
            return new Coffee();
        }
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare()
        {
            Console.WriteLine("Tea preparation ...");
            return new Tea();
        }
    }

    public class HotDrinkFactory
    {
        // Ovoim enumom rusimo open-closed prinicp , jer ako dodajemo neki noviu tip pica bez dodavanja u enum , to nam nece raditi ... Zelimo
        // neki interaktivniji pristup svemu ovome.
        //public enum AvailableDrink   
        //{
        //    Coffee,
        //    Tea
        //}

        //private Dictionary<AvailableDrink, IHotDrinkFactory> drinks = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkFactory()
        //{
        //    foreach (AvailableDrink ad in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        // Kreira istancu odredjenog tipa Factories.CoffeeFactory (klase)
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("Factories." + Enum.GetName(typeof(AvailableDrink), ad) + "Factory"));
        //        drinks.Add(ad, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink)
        //{
        //    return drinks[drink].Prepare();
        //}

        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkFactory()
        {
            foreach (var t in typeof(HotDrinkFactory).Assembly.GetTypes())
            {
                // Ovako proveravamo da li t moze da se konvertuje u ovaj tip(Da li ta vrednost moze da joj se dodeli)
                // Preko assenblija ovo podrzava plugin arhitekturu , jer plugin ce imati isti namespace ako ocemo nesto da overajdamo dodamo
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                            t.Name.Replace("Factory", string.Empty),
                            (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public List<string> GetAvailableDrinks()
        {
            var drinks = new List<string>();

            foreach (var drink in factories)
            {
                drinks.Add(drink.Item1);
            }
            return drinks;
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks");
            var availableDrinks = GetAvailableDrinks();

            for (var index = 0; index < availableDrinks.Count; index++)
            {
                Console.WriteLine($"{ index }: {availableDrinks[index]}");
            }
            while (true)
            {
                string s;
                Console.Write("Please pick drink from the menu: ");
                if((s = Console.ReadLine()) != null &&
                    int.TryParse(s, out int i) &&
                    i >= 0 &&
                    i < availableDrinks.Count)
                {
                    return factories[i].Item2.Prepare();
                }
                else
                {
                    Console.WriteLine("Wrong input, please enter right number of the drink.");
                }
            }
        }
    }

    public class AbstractFactory
    {
        public static void MainFunc(string[] args)
        {
            var drinkFactory = new HotDrinkFactory();

            while (true)
            {
                var drink = drinkFactory.MakeDrink();
                drink.Consume();
            }
        }
    }
}
