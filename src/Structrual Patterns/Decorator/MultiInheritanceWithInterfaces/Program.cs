namespace MultiInheritanceWithInterfaces
{
    public class Bird : IBird
    {
        public int Weight { get; set; }


        public void Fly()
        {
            Console.WriteLine("Flying");
        }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine("Crawling");
        }
    }

    public class Dragon : IBird, ILizard
    { //but now we need explicit to implement the methods, and we would like to make change of the methods only from one place

        private Bird bird;
        private Lizard lizard;
        private int weight;

        public Dragon(Bird bird, Lizard lizard)
        {
            this.bird = bird;
            this.lizard = lizard;
        }

        public int Weight
        {
            get => this.weight;
            set
            {
                this.weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        }

        public void Crawl()
        {
            this.lizard.Crawl();
        }

        public void Fly()
        {
            this.bird.Fly();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //in .NET we dont have the opportunity for multiple class inheritance

            var b = new Bird();
            var l = new Lizard();
            var d = new Dragon(b, l);
            d.Weight = 50;
            d.Fly();
            d.Crawl();
        }
    }

    public interface IBird
    {
        int Weight { get; }

        void Fly();
    }

    public interface ILizard
    {
        int Weight { get; }

        void Crawl();
    }
}
