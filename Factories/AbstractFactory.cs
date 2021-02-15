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
        public enum AvailableDrink
        {
            Coffee,
            Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> drinks = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkFactory()
        {
            foreach (AvailableDrink ad in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("Factories." + Enum.GetName(typeof(AvailableDrink),ad) + "Factory"));
                drinks.Add(ad, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink)
        {
            return drinks[drink].Prepare();
        }
    }

    public class AbstractFactory
    {
        public static void MainFunc(string[] args)
        {
            var drinkFactory = new HotDrinkFactory();
            var tea = drinkFactory.MakeDrink(HotDrinkFactory.AvailableDrink.Tea);
            var coffee = drinkFactory.MakeDrink(HotDrinkFactory.AvailableDrink.Coffee);

            tea.Consume();
            coffee.Consume();
        }
    }
}
