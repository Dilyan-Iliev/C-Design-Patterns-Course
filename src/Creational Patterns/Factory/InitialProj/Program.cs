namespace InitialProj
{
    //with factory pattern the object creation is at once (not like the builder where it is step-by-step)
    // - the object cration can be outsorced to 
    // a separate function (Factory method)
    // a separate class (Factory)
    // (rarely) can create hierarchy of factoris with Abstract Factory

    //Sometimes using only ctors could result into a problem :
    public class Point
    {
        private double x;
        private double y;

        public Point(double a, double b,
            CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }

        //what if we want to have constructor which also takes two double params 
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

        }
    }
}
