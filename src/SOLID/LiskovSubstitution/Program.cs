namespace LiskovSubstitution
{
    public class Program
    {
        static void Main(string[] args)
        {
            //the idea is that we should be able to substitute (switch) some base type for sub-type

            //initial version:
            var rectangle = new Rectangle(2, 4);
            int area = rectangle.Area(2, 4);

            var square = new Square();

        }
    }

    public class Rectangle
    {
        public virtual int Widht { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
        }

        public Rectangle(int widht, int height)
        {
            this.Widht = widht;
            this.Height = height;
        }

        public int Area(int width, int height) => width * height;
    }

    public class Square : Rectangle
    {
        public override int Widht
        {
            set
            {
                base.Widht = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Height = base.Widht = value;
            }
        }
    }
}
