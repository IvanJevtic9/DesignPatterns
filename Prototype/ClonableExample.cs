using System;
using System.Text;

namespace Prototype
{
    public class Home : ICloneable
    {
        public Person[] HouseHolders;
        public Address Address;

        public Home(Person[] houseHolders, Address address)
        {
            if (houseHolders is null)
            {
                throw new ArgumentNullException(nameof(houseHolders));
            }
            HouseHolders = houseHolders;
            Address = address;
        }
        //Copy constructor
        public Home(Home copyObj)
        {
            HouseHolders = new Person[copyObj.HouseHolders.Length];

            int index = 0;
            foreach (var p in copyObj.HouseHolders)
            {
                HouseHolders[index++] = new Person(p);
            }
            Address = new Address(copyObj.Address);
        }

        public object Clone()
        {
            Person[] clonedPersons = new Person[HouseHolders.Length];

            int index = 0;
            foreach (var person in HouseHolders)
            {
                clonedPersons[index++] = (Person)person.Clone();
            }

            return new Home(clonedPersons, (Address)Address.Clone());
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("List of holders: ");
            foreach (var person in HouseHolders)
            {
                str.AppendLine(person.ToString());
            }
            str.AppendLine("Address: ");
            str.AppendLine(Address.ToString());

            return str.ToString();
        }
    }

    public class Person : ICloneable
    {
        public string Name;

        public Person(string name)
        {
            Name = name;
        }

        public Person(Person copyObj)
        {
            Name = copyObj.Name;
        }

        public object Clone()
        {
            return new Person(Name);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }

    public class Address : ICloneable
    {
        public string StreetName;
        public string HouseNumber;

        public Address(string streetName, string houseNumber)
        {
            if (streetName == null)
            {
                throw new ArgumentNullException(paramName: nameof(streetName));
            }

            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address copyObj)
        {
            StreetName = copyObj.StreetName;
            HouseNumber = copyObj.HouseNumber;
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class ClonableExample
    {
        public static void MainFunc(string[] args)
        {
            var exp = new Home(new Person[3] { new Person("Ivan"), new Person("Bane"), new Person("Jeca") },new Address("Volgina", "20A"));
            Console.WriteLine(exp.ToString());

            var newExp = (Home)exp.Clone();
            newExp.HouseHolders[0].Name = "Aleksa";
            newExp.HouseHolders[1].Name = "Sinisa";
            newExp.HouseHolders[2].Name = "Mirko";

            newExp.Address.StreetName = "Vranjani";
            newExp.Address.HouseNumber = "10";

            Console.WriteLine(newExp.ToString());

            var copyObj = new Home(newExp);
            copyObj.HouseHolders[0].Name = "Mravke";
            copyObj.HouseHolders[1].Name = "Bole";
            copyObj.HouseHolders[2].Name = "Milosna";

            copyObj.Address.StreetName = "Banjica";
            copyObj.Address.HouseNumber = "x2";

            Console.WriteLine(copyObj.ToString());
        }
    }
}