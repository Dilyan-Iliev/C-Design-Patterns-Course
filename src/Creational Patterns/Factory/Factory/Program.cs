namespace Factory
{
    //Factory class

    public static class PointFactory
    {
        public static Point CreatePoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point CreateDifferentPoint(double a, double b)
        {
            return new Point(a * Math.Cos(b), a * Math.Sin(b));
        }
    }

    public class Point
    {
        private double x;
        private double y;

        public Point(double x, double y) //ctor is now private
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var point = PointFactory.CreatePoint(5, 10);
        }
    }
}
