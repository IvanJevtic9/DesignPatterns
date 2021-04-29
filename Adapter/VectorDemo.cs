using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Adapter
{
    public class Point
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    public class Line
    {
        public Point Start, End;
        public Line(Point start, Point end)
        {
            if (start is null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end is null)
            {
                throw new ArgumentNullException(nameof(end));
            }
            Start = start;
            End = end;
        }
        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class ListToPointAdapter : List<Point>
    {
        static Dictionary<int, List<Point>> Cache = new Dictionary<int, List<Point>>();
        public ListToPointAdapter(Line line)
        {
            
            if (Cache.ContainsKey(line.GetHashCode()))
            {
                Console.WriteLine($"Get from cache: {line.GetHashCode()} line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");
                AddRange(Cache[line.GetHashCode()]);
            }
            else
            {
                Console.WriteLine($"{line.GetHashCode()} Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");
                // Convertujemo samo linije koje su uspravne ili horizontalne
                int left = Math.Min(line.Start.X, line.End.X);
                int right = Math.Max(line.Start.X, line.End.X);

                int top = Math.Max(line.Start.Y, line.End.Y);
                int bottom = Math.Min(line.Start.Y, line.End.Y);

                int dx = right - left;
                int dy = top - bottom;

                if (dx == 0)
                {
                    for (int y = bottom; y <= top; y++)
                    {
                        Add(new Point(left, y));
                    }
                }
                else if (dy == 0)
                {
                    for (int x = left; x < right; x++)
                    {
                        Add(new Point(x, top));
                    }
                }

                Cache[line.GetHashCode()] = GetRange(0,Count);
            }
        }
    }

    public class VectorDemo
    {
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>()
        {
            new VectorRectangle(1,1,10,10),
            new VectorRectangle(1,1,10,10),
            new VectorRectangle(3,3,6,6)
        };

        /*Kako Line iscrtati ako imamo samo metodu za crtanje tacke (Treba nam adapter)*/
        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }
        public static void MainFunc(string[] args)
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new ListToPointAdapter(line);

                    foreach (var point in adapter)
                    {
                        DrawPoint(point);
                    }
                    Console.Write('\n');
                }
            }
        }
    }
}
