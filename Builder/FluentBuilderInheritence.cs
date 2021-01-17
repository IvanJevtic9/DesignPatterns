using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New() => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: Position";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF> // Genericka rekurzija
    {
        public SELF Called(string name)
        {
            this.person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    /* Problem kod nasledjivanja , nece moci da se slaze api iz bazne klase sa ovim apijem ako vracamo tip klase
    public class PersionInfoBuilder
    {
        protected Person person = new Person();

        public PersionInfoBuilder Called(Person person)
        {
            this.person = person;
            return this;
        }
    }

    public class PersonJobBuilder : PersionInfoBuilder
    {
        public PersonJobBuilder WorkAsA(string position)
        {
            this.person.Position = position;
            return this;
        }
    }
    */
    public class FluentBuilderInheritence
    {
        public static void MainFunc(string[] args)
        {
            var person = Person.New()
                            .Called("Ivan")
                            .WorkAsA("Programmer")
                            .Build();

            Console.WriteLine(person.ToString());
        }
    }
}
