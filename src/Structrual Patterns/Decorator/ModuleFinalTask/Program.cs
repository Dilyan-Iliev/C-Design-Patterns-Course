namespace ModuleFinalTask
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Dragon // no need for interfaces
    {
        private Bird bird = new();
        private Lizard lizard = new();
        private int age;

        public int Age
        {
            get => age;
            set
            {
                age = value;
                bird.Age = value;
                lizard.Age = value;
            }
        }

        public string Fly()
        {
            return bird.Fly();
        }

        public string Crawl()
        {
            return lizard.Crawl();
        }
    }
}
