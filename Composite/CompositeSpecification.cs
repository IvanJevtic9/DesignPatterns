using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composite
{
    public enum Size
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }
    public class Product
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"Product: {Name}, Color: {Color}, Size: {Size}";
        }
    }
    public interface IFilter<T>
    {
        IEnumerable<T> FilterItems(IEnumerable<T> items, Specification<T> spec);
    }

    public class Filter : IFilter<Product>
    {
        public IEnumerable<Product> FilterItems(IEnumerable<Product> items, Specification<Product> spec)
        {
            return items.Where(p => spec.IsSatisfied(p)).ToList();
        }
    }

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T p);
        public static Specification<T> operator &(Specification<T> first, Specification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly Specification<T>[] items;
        public AndSpecification(params Specification<T>[] items) : base(items)
        {
            this.items = items;
        }

        public override bool IsSatisfied(T p)
        {
            return items.All(s => s.IsSatisfied(p));
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly Specification<T>[] items;
        public OrSpecification(params Specification<T>[] items) : base(items)
        {
            this.items = items;
        }

        public override bool IsSatisfied(T p)
        {
            return items.Any(s => s.IsSatisfied(p));
        }
    }

    public abstract class CompositeSpecification<T> : Specification<T>
    {
        private readonly Specification<T>[] items;

        public CompositeSpecification(params Specification<T>[] items)
        {
            this.items = items;
        }
    }

    public class SizeSpecification : Specification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public override bool IsSatisfied(Product p)
        {
            return size == p.Size;
        }
    }

    public class ColorSpecification : Specification<Product>
    {
        private string color;
        public ColorSpecification(string color)
        {
            this.color = color;
        }
        public override bool IsSatisfied(Product p)
        {
            return color == p.Color;
        }
    }

    public class CompositeSpecification
    {
        public static void MainFunc(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "Dukserica",
                    Color = "Blue",
                    Size = Size.ExtraLarge
                },
                new Product()
                {
                    Name = "Kacket",
                    Color = "Red",
                    Size = Size.Small
                },
                new Product()
                {
                    Name = "Bicikl",
                    Color = "Red",
                    Size = Size.Small
                },
                new Product()
                {
                    Name = "Ranac",
                    Color = "Yellow",
                    Size = Size.Large
                },
                new Product()
                {
                    Name = "Kacket",
                    Color = "Blue",
                    Size = Size.Medium
                }
            };

            var productFilter = new Filter();
            var compositeSpec = new AndSpecification<Product>(new ColorSpecification("Yellow"),
                                                             new SizeSpecification(Size.Large));

            products = (List<Product>)productFilter.FilterItems(products, compositeSpec);

            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
