namespace OpenClosed
{
    public class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);

            Product[] products = { apple, tree };
            var result = ProductFilter.FilterBySize(products, Size.Small).ToArray();

            //better way:
            var betterFilter = new BetterFilter();
            var colorFilter = betterFilter.Filter(products, new ColorSpecification(Color.Green)).ToArray();
            var sizeFilter = betterFilter.Filter(products, new SizeSpecificaation(Size.Small)).ToArray();

            var sizeAndColorFilter = betterFilter.Filter(products, new ColorAndSizeSpecification<Product>(
                new ColorSpecification(Color.Green), new SizeSpecificaation(Size.Small))
                ).ToArray();
        }
    }

    //Starts the bad way
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            ArgumentNullException.ThrowIfNull(name);

            this.Name = name;
            this.Color = color;
            this.Size = size;
        }
    }

    public class ProductFilter
    {
        //OLD (NOT BY OPEN-CLOSED)
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var pr in products)
            {
                if (pr.Size == size)
                {
                    yield return pr;
                }
            }
        }

        public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var pr in products)
            {
                if (pr.Color == color)
                {
                    yield return pr;
                }
            }
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var pr in products)
            {
                if (pr.Color == color && pr.Size == size)
                {
                    yield return pr;
                }
            }
        }

        //and each time i have to add like this new method in order to statisfy certain functionallity
        //but the idea is not this
        //Open-Closed means that classes should be open for extensions,
        //but closed for modifications (nobody should go inside the class and add new methods for new functionallity)

        //We could achieve this by inheritance, interfaces, etc.
    }

    //Starts the good way (we have two interfaces which allow us to have different specifications
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFIlter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> filters, ISpecification<T> specification);
    }

    //and now we could create specific class for certain specification

    public class BetterFilter : IFIlter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var pr in items)
            {
                if (specification.IsSatisfied(pr))
                {
                    yield return pr;
                }
            }
        }
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecificaation : ISpecification<Product>
    {
        private Size size;

        public SizeSpecificaation(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class ColorAndSizeSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first;
        private ISpecification<T> second;

        public ColorAndSizeSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }
}
