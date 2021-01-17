using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionalBuilderNem
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int LevelOfEducation { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(new string('*', 10) + " Person Information " + new string('*', 10));
            sb.AppendLine($"Name: { Name }");
            sb.AppendLine($"Date of birth: { DateOfBirth }");
            sb.AppendLine($"Level of education: { LevelOfEducation }");
            sb.AppendLine($"Position: { Position }");
            sb.AppendLine(new string('*', 40));

            return sb.ToString();
        }
    }

    public abstract class FunctionalApstractBuilder<TSubject, TSelf>
        where TSubject : new()
        where TSelf : FunctionalApstractBuilder<TSubject, TSelf>
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public TSelf Borned(DateTime date) => Do(p => p.DateOfBirth = date);
        public TSelf Educated(int level) => Do(p => p.LevelOfEducation = level);
        public TSelf ClearInfo() => Do(p => p = new Person());
        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        public TSelf Do(Action<Person> action) => AddAction(action);

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf) this;
        }
    }

    public sealed class PersonBuilder : FunctionalApstractBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name) => Do(p => p.Name = name);
    }

    /*Kad dodajemo ekstenzije buildera ... Ako nije moguce naslediti i ne zelimo da sirimo baznu klasu, onda ovako sirimo stvari*/
    public static class PersonBuilderExtension 
    {
        public static PersonBuilder WorksAsA(PersonBuilder builder, string position)
        {
            return builder.Do(p => p.Position = position);
        }
    }

    public class FunctionalBuilder
    {
        public static void MainFunc(string[] args)
        {
            var person = PersonBuilderExtension.WorksAsA(new PersonBuilder(), "Software develpoer") /*Ulaz za nasledjene metode*/
                            .Borned(new DateTime(1995, 10, 16))
                            .Called("Ivan Jevtic")
                            .Educated(4)
                            .Build();

            Console.WriteLine(person.ToString());
        }
    }
}
