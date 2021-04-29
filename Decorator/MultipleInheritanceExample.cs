using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if(Age >= 10)
            {
                Console.WriteLine("I am flying.");
            }
            
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl() // default member
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am crawling.");
            }
        }
    }

    public class Organism
    {

    }

    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    class MultipleInheritanceWithDefaultMemberExample
    {
        public static void MainFunc(string[] args)
        {
            var d = new Dragon();
            d.Age = 11;

            /*First possibilities*/
            ((ILizard)d).Crawl();

            if(d is IBird bird)
            {
                bird.Fly();
            }

            if(d is ILizard lizard)
            {
                lizard.Crawl();
            }
        }
    }
}
