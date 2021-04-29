using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Static
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float radius;
        public Circle()
        {
            this.radius = 0.0f;
        }
        public Circle(float radius)
        {
            this.radius = radius;
        }
        public override string AsString()
        {
            return $"A cicle with radius: {radius}";
        }
    }

    public class Square : Shape
    {
        private float side;
        public Square() : this(0)
        {}
        public Square(float side)
        {
            this.side = side;
        }
        public override string AsString()
        {
            return $"A square with side: {side}";    
        }
    }

    public class ColoredShape : Shape
    {
        private Shape shape;
        private string color;
        public ColoredShape(Shape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape), "Missing argument");
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color), "Missing argument");
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape : Shape
    {
        private Shape shape;
        private float opaticy;
        public TransparentShape(Shape shape, float opaticy)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape), "Missing argument");
            this.opaticy = opaticy;
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has the transparent {opaticy}";
        }
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string color;
        T shape = new T();
        public ColoredShape(string color)
        {
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color), "Missing argument");
        }
        public ColoredShape() : this("black")
        {
                
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class StaticDecoratorComposition
    {
        public static void MainFunc(string[] args)
        {
            var redSquare = new ColoredShape<Square>("red");
            Console.WriteLine(redSquare.AsString());
        }
    }
}
