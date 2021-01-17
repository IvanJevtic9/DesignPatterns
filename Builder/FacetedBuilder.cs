using System;
using System.Collections.Generic;
using System.Text;

namespace Builder.Faceted
{
    public class Person
    {
        // Address information
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    
        // Employment
        public string CompanyName { get; set; }
        public string Position { get; set; }

        public int AnnualIncom { get; set; }


        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}\n{nameof(PostCode)}: {PostCode}\n{nameof(City)}: {City}\n\n{nameof(CompanyName)}: {CompanyName}\n{nameof(Position)}: {Position}\n{nameof(AnnualIncom)}: {AnnualIncom}\n";
        }
    }

    public class PersonBuilder // facade
    {
        //reference
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    /*Nasledjujemo*/
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }     

        public PersonJobBuilder At(string companyName)
        {
            this.person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            this.person.Position = position;
            return this;
        }

        public PersonJobBuilder EarningInAmount(int amount)
        {
            this.person.AnnualIncom = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string address)
        {
            this.person.StreetAddress = address;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postCode)
        {
            this.person.PostCode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            this.person.City = city;
            return this;
        }
    }

    public class FacetedBuilder
    {
        public static void MainFunc(string[] args)
        {
            var pb = new PersonBuilder();

            var person = (Person)pb.Works.At("Adacta")
                                 .AsA("Tester")
                                 .EarningInAmount(2500)
                           .Lives.At("Volgina 20A")
                                 .In("Belgrade")
                                 .WithPostCode("10050");

            Console.WriteLine(person.ToString());
        }
    }
}
