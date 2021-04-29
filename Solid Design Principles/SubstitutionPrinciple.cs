using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_Design_Principles
{
    public class SubstitutionPrinciple
    {
        private class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }
            public Rectangle()
            {

            }

            public Rectangle(int w, int h)
            {
                Width = w;
                Height = h;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        private class Square : Rectangle
        {
            // Wrong , if we try do this Rectangle sq = new Square(); sq.Width = 4 we will get error.
            /*
            public new int Width
            {
                set { base.Width = base.Height = value; }
            }
            public new int Height
            {
                set { base.Width = base.Height = value; }
            }
            */
            /*Resenje jeste omoguciti overrajdovanje - Iako sada koristimo referencu na Rectangle i tamo imamo jednu definiciju
             za propertije , posto su setovane na virtual kompajler gleda nasledjenu klasu i koristi novu definiciju. Kad se samo stavi 
             (Bez virtual) new nece se gledati klasa Square ako drzimo referencu na Rectangle*/
            public override int Width
            {
                set { base.Width = base.Height = value; }
            }
            public override int Height
            {
                set { base.Width = base.Height = value; }
            }
        }

        private static int Area(Rectangle r) => r.Width * r.Height;

        public static void MainFunc(string[] args)
        {
            var rc = new Rectangle(2, 3);

            Rectangle square = new Square();
            square.Height = 3;
            square.Width = 6;

            Console.WriteLine($"{rc} has area: {Area(rc)}");
            Console.WriteLine($"{square} has area: {Area(square)}");
        }
    }
}
