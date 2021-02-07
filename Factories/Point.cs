using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Factories
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }
    public class Point
    {
        private double x, y;
        
        /// <summary>
        /// Initializes a point from EITHER cartesian or polar
        /// </summary>
        /// <param name="a"> x coordinate </param>
        /// <param name="b"> y coordinate</param>
        /// <param name="system"> Coordinate system </param>
        public Point(double a,double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = b * Math.Cos(a);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }
        /* Ne mozemo imati jos jedan konstruktor sa istim setom promenljivih (istih tipova), u tom slucaju pravimo nove tipove
        public Point(double rho, double theta)
        {
            this.x = rho;
            this.y = theta;
        }
        */
        public override string ToString()
        {
            return $"{nameof(this.x)}: {this.x}\n{nameof(this.y)}: {this.y}";
        }

        public static void MainFunc(string[] args)
        {
            var point = new Point(12.32, 45.00, CoordinateSystem.Polar);

            Console.WriteLine(point.ToString());
        }
    }
}
