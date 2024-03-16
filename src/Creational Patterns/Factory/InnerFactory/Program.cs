namespace InnerFactory
{
    public class Point
    {
        private double x;
        private double y;

        private Point(double x, double y) //this ctor must be private
        {
            this.x = x;
            this.y = y;
        }

        public static class Factory
        {
            //this factory must somehow access the private ctor of Point class

            public static Point CreatePoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point CreateDifferentPoint(double a, double b)
            {
                return new Point(a * Math.Cos(b), a * Math.Sin(b));
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.CreatePoint(5, 10);
        }
    }
}
