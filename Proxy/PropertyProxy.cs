using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    /* Temperature, Kelvin , Farenhajt - zelimo na primer da brz okonvertujemo... 
     Veoma lako za izgraditi. Ideja je koristiti object kao property*/

    public class Property<T> where T : new()
    {
        private T value;
        public Property() : this(Activator.CreateInstance<T>())
        {

        }
        public Property(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return this.value; }
            set
            {
                if (Equals(this.value, value)) return;
                Console.WriteLine($"Assigned value to {value}");
                this.value = value;
            }
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value; //int n = p_int;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 1000;
        }
        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
        }
    }

    // Da ne bi kreirali stalno novi Propety objekat kad imamo implicitnu konverziju ... Vec samo da kopiramo vrednost. necmeo da pravimo
    // novi objekat vec da updejtamo postojeci.
    public class Creature
    {
        private Property<int> agility;

        public Creature()
        {
            agility = new Property<int>();
        }
        public int Agility
        {
            get => agility.Value;
            set { this.agility.Value = value; }
        }
    }

    public static class PropertyProxy
    {
        public static void MainFunc(string[] args)
        {
            var c = new Creature();
            c.Agility = 10; // return new Property<T>(value); // Property<int> p = 1000;
            c.Agility = 10; // Dodelice se samo jednom

            Console.WriteLine(c.Agility);
        }
    }
}
