using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(string name ,int index)
        {
            this.Id = index;
            this.Name = name;
        }
    }

    public class PersonFactory
    {
        private int index = 0;

        public Person CreatePerson(string name)
        {
            return new Person(name, index++);
        }
    }


    public class FactoryExercise
    {
        public static void MainFunc(string[] args)
        {
            var personFactory = new PersonFactory();

            List<Person> students = new List<Person>();

            while (true)
            {
                Console.WriteLine("Enter the student name(Exit loop with -1): ");
                string name;
                if ((name = Console.ReadLine()) != "-1"){
                    students.Add(personFactory.CreatePerson(name));
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Student list: ");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} - {s.Name}");
            }
        }
    }
}
