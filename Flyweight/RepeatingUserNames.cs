using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flyweight
{
    // JetBrains.DotMemoryUnit za kontrolisanje memorije

    public class User
    {
        private readonly string fullName;
        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    // Saving memory from duplication
    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }
        public override string ToString()
        {
            return string.Join(" ", names.Select(n => strings[n]));
        }
    }

    [TestFixture]
    public class RepeatingUserNames
    {
        public static void MainFunc(string[] args)
        {
           
        }
        [Test]
        public static void TestUser() // Memory 1655033
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new User($"{firstName} {lastName}"));
                }
            }

            ForceGC();

            // Zelimo da viidmo koliko smo memorije uzeli
            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        [Test]
        public static void TestUser2() // Memory 1296991
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new User2($"{firstName} {lastName}"));
                }
            }

            ForceGC();

            // Zelimo da viidmo koliko smo memorije uzeli
            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private static void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private static string RandomString()
        {
            Random rand = new Random();

            return new string(Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
        }
    }
}
