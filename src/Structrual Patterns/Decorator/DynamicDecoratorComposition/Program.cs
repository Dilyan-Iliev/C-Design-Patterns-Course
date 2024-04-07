namespace DynamicDecoratorComposition
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public string AsString()
        {
            return $"A circle with radius {this.radius}";
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class Square : IShape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }

        public string AsString()
        {
            return $"A square with side {this.side}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(1.23f);
            Console.WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            Console.WriteLine(redSquare.AsString());

            var transparencySquare = new TransparentShape(redSquare, 1.2f);
            Console.WriteLine(transparencySquare.AsString());
        }
    }

    //dynamic decorator = runtime, not compile time
    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            this.shape = shape;
            this.color = color;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has transparency {transparency}";
        }
    }
}
