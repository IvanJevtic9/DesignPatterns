using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Vector can be in 2 dimensional space, 3 dime ... Can be made from diferent types , can be integer , decimal ... 
    // We need generic types to implement Vector2f, Vector3i
    // In C# we can not put literals inside generic types onli types ...

    public interface IDimension
    {
        int Value { get;}
    }

    public static class Dimensions
    {
        public class TwoDimension : IDimension
        {
            public int Value => 2;
        }

        public class ThreeDimension : IDimension
        {
            public int Value => 3;
        }
    }
    
    // ax + by + cz = 0 Vector
    public class Vector<TSelf, T, D> 
        where D : IDimension, new() //D also have default constructor
        where TSelf : Vector<TSelf, T, D> , new() // recursive generic
    {
        protected T[] data;

        public Vector()
        {
            data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new D().Value;
            data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
            {
                data[i] = values[i];
            }
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            int dimSize = 0;
            foreach(var d in data)
            {
                strBuilder.AppendLine($"{++dimSize}: {d}");
            }
            return strBuilder.ToString();
        }

        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();

            var requiredSize = new D().Value;
            result.data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
            {
                result.data[i] = values[i];
            }

            return result;
        }
    }

    public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D> where D : IDimension, new()
    {
        public VectorOfInt()
        {

        }

        public VectorOfInt(params int[] values) : base(values)
        {

        }

        public static VectorOfInt<D> operator +(VectorOfInt<D> lhs, VectorOfInt<D> rhs)
        {
            var result = new VectorOfInt<D>(lhs.data);

            for (int index = 0; index < rhs.data.Length; index++)
            {
                result[index] += rhs[index];
            }

            return result;
        }
    }

    public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D> 
        where D : IDimension, new()
        where TSelf : Vector<TSelf, float, D> ,new()
    {
        public VectorOfFloat()
        {

        }

        public VectorOfFloat(params float[] values) : base(values)
        {

        }

        public static VectorOfFloat<TSelf, D> operator +(VectorOfFloat<TSelf, D> lhs, VectorOfFloat<TSelf, D> rhs)
        {
            var result = new VectorOfFloat<TSelf, D>(lhs.data);

            for (int index = 0; index < rhs.data.Length; index++)
            {
                result[index] += rhs[index];
            }

            return result;
        }
    }

    public class Vector2i : VectorOfInt<Dimensions.TwoDimension>
    {
        public Vector2i()
        {

        }
        public Vector2i(params int[] values) : base(values)
        {

        }
    }

    public class Vector3f : VectorOfFloat<Vector3f ,Dimensions.ThreeDimension>
    {
        
    }


    public class GenericAdapterDemo
    {
        public static void MainFunc(string[] args)
        {
            var vect = new Vector2i();
            vect[0] = 0;
            vect[1] = 100;

            var p1 = new Vector2i(1, 2);
            var p2 = new Vector2i(6, 2, -3);

            Console.WriteLine((p1+p2).ToString());
            Console.WriteLine(vect.ToString());

            var vec3f = Vector3f.Create(2.2f,3.6f,1.2f);
            var vec3fSec = Vector3f.Create(1.2f, 2.6f, -12.2f);

            Console.WriteLine((vec3f + vec3fSec).ToString());
        }
    }
}
