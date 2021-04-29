using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    public interface ICar
    {
        void Drive();
    }

    public enum LicenceType
    {
        NONE,
        AM,
        A1,
        A2,
        A,
        B1,
        B,
        BE,
        C1,
        C1E,
        C,
        CE,
        D1,
        D1E,
        D,
        DE,
        F,
        M
    }

    public class Driver
    {
        public string Name { get; set; }
        public LicenceType Licence { get; set; }
        public int Age { get; set; }
    }

    public class Car : ICar
    {
        // Ovde mozemo dodati informacije o kolima i u proxiju imati jos neke poruke da li kola mogu da se voze ? Da li iam odgovarajucu dozvolu.
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();
        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }
        public void Drive()
        {
            if (driver.Age > 18)
            {
                car.Drive();
            }
            else
            {
                Console.WriteLine($"{driver.Name} is too young to drive.");
            }
        }
    }


    public static class ProtectionProxy
    {
        public static void MainFunc(string [] args)
        {
            ICar car = new CarProxy(new Driver() { Name="Ivan",Age=19,Licence=LicenceType.B});

            car.Drive();
        }
    }
}
