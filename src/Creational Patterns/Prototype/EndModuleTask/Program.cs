namespace EndModuleTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start;
        public Point End;

        public Line DeepCopy()
        {
            var newStart = new Point { X = Start.X, Y = Start.Y };
            var newEnd = new Point { X = End.X, Y = End.Y };
            return new Line { Start = newStart, End = newEnd };
        }
    }
}
