namespace FactoryMethod
{
    //Factory method is the simplier pattern

    public class Point
    {
        private double x;
        private double y;

        //Factory method
        public static Point CreatePoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point CreateDifferentPoint(double a, double b)
        {
            return new Point(a * Math.Cos(b), a * Math.Sin(b));
        }

        private Point(double x, double y) //ctor is now private
        {
            this.x = x;
            this.y = y;
        }
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var firstPoint = Point.CreatePoint(5, 10);
            var secondPoint = Point.CreateDifferentPoint(2, 3);
        }
    }
}
