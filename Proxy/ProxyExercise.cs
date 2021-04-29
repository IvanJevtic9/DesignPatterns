using System;

namespace Proxy.Exercise
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private Person person;
        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public string Drink()
        {
            if (person.Age < 18)
            {
                return "too young";
            }
            return person.Drink();
        }

        public string Drive()
        {
            if (person.Age < 16)
            {
                return "too young";
            }
            return person.Drive();
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }

        public int Age { get { return person.Age; } set { person.Age = value; } }
    }

    public static class ProxyExercise
    {
        public static void MainFunc(string[] args)
        {
            var p = new Person();
            p.Age = 17;

            Console.WriteLine(p.DrinkAndDrive());

            var res = new ResponsiblePerson(p);

            Console.WriteLine(res.Drink());
            Console.WriteLine(res.Drive());
            Console.WriteLine(res.DrinkAndDrive());

        }
    }
}
