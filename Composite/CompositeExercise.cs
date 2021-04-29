using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    public class CompositeExercise
    {
        public static void MainFunc(string[] args)
        {
            var manyValues = new ManyValues();
            manyValues.Add(34);
            manyValues.Add(345);
            manyValues.Add(12);

            var list = new List<IValueContainer>()
            {
                new SingleValue() {Value = 23 },
                manyValues
            };

            Console.WriteLine("Sum of the Value containers: "+ list.Sum());
        }
    }
}
