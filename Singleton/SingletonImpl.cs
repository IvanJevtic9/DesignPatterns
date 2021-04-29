using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();
        private static int instanceCount;
        public static int Count => instanceCount; 
        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database");
            instanceCount++;

            capitals = File.ReadAllLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,"capitals.txt"))
                .Batch(2)
                .ToDictionary(
                   list => list.ElementAt(0).Trim(),
                   list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            try
            {
                return capitals[name];
            }
            catch
            {
                return 0;
            }
        }

        // Na ovaj nacin imamcemo samo jednu instancu u celoj aplikaciji
        // Lazy inicijalizacija... U nekim slucajevima necemo da pristupimo ovoj istanci , a i pored toga 
        // cemo je inicijalizovati , Lazy nam omogucava da istanciramo ovu klasu tek odna kad nam stvarno zatreba
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value; //Tek ovde ce se pozvati konstruktor - kad pristupimo ovom propertiju
    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();
        public OrdinaryDatabase()
        {
            Console.WriteLine("Initializing database");

            capitals = File.ReadAllLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
                .Batch(2)
                .ToDictionary(
                   list => list.ElementAt(0).Trim(),
                   list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            try
            {
                return capitals[name];
            }
            catch
            {
                return 0;
            }
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name); //Testiranje ovoga moze biti problem
            }
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase database;
        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                // Sa ovim vec kroz DI mozemo da ubacimo svasta nesto... Mozemo da napravimo fake klasu koja
                // implementira IDatabase i da u getPopulation fakujemo return value
                result += database.GetPopulation(name); 
            }
            return result;
        }
    }

    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;

            Assert.AreSame(db, db2, "Two static instance are not equal");
            Assert.That(SingletonDatabase.Count == 1, "It's not singleton, it has more then one instance");
        }
        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();

            // Ovako navodimo da je singleton
            cb.RegisterType<OrdinaryDatabase>().As<IDatabase>().SingleInstance(); 

            // OridinaryDatabase je naveden kao IDatabase, taj IDatabase se koristi za ovu klasu
            // Prilikom Builda i Resolv-a pozvace se konstruktor i za IDatabase ce se Inejctati IDatabase
            cb.RegisterType<ConfigurableRecordFinder>(); 

            using(var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }
        }
    }

    public class SingletonImpl
    {
        public static void MainFunc()
        {
            var db = SingletonDatabase.Instance;
            Console.WriteLine(db.GetPopulation("Tokyo"));

            var finder = new SingletonRecordFinder();

            Console.WriteLine(finder.GetTotalPopulation(new List<string>(){ "Tokyo", "S" }));
        }
    }
}
