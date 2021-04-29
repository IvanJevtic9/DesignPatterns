using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facade.FacadeExample
{
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            Console.WriteLine("------Spliting the square------");
            Console.WriteLine("           The rows            ");

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                {
                    Console.Write($"{array[r][c]} ");
                    theRow.Add(array[r][c]);
                }
                Console.WriteLine();
                result.Add(theRow);
            }
            Console.WriteLine("           The colms            ");
            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                {
                    Console.Write($"{array[r][c]} ");
                    theCol.Add(array[r][c]);
                }
                Console.WriteLine();
                result.Add(theCol);
            }

            Console.WriteLine("           The diagonals           ");
            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                    {
                        Console.Write($"{array[r][c]} ");
                        diag1.Add(array[r][c]);
                    }
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                    {
                        Console.Write($"{array[r][c]} ");
                        diag2.Add(array[r][c]);
                    }
                }
                Console.WriteLine();
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            Console.WriteLine("------Expecting values------");
            if (!array.Any()) return false;

            var expected = array.First().Sum();
            Console.WriteLine($"{ expected }");
            Console.WriteLine();
            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            var g = new Generator();
            var s = new Splitter();
            var v = new Verifier();

            var square = new List<List<int>>();

            do
            {
                square = new List<List<int>>();
                for (int i = 0; i < size; ++i)
                    square.Add(g.Generate(size));
            } while (!v.Verify(s.Split(square)));

            return square;
        }
    }
}