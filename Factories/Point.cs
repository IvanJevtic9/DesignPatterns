using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Factories
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        //public static PointFactory Factory => new PointFactory(); Ako hocemo da imamo static polje i da preko tog
        // polja direktno pistupamo metodama za kreiranje 

        // public static Point Origin => new Point(0, 0); // Property

        public static Point Origin2 = new Point(0, 0); // Field better (samo jedanput inicijalizujemo)

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Sin(theta), theta * Math.Cos(rho));
            }
        }
        public override string ToString()
        {
            return $"{nameof(this.x)}: {this.x}\n{nameof(this.y)}: {this.y}";
        }

        public static void MainFunc(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(Math.PI / 6, Math.PI / 2);

            var p2 = Point.Origin2;

            Console.WriteLine(point.ToString());
        }
    }
}
