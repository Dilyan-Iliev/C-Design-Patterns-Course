namespace MultipleInheritanceWithDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; }
    }

    public interface IBird : ICreature
    {
        void Fly() //default member (with default behaviour)
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am flying");
            }
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I am crawling");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Dragon d = new() { Age = 10 };
            //d.Fly(); //by default we dont have access to this method, nor to .Crawl()

            if (d is IBird bird)
                bird.Fly();

            if (d is ILizard lizard)
                lizard.Crawl();
        }
    }

    public class Dragon : IBird, ILizard
    {
        public int Age { get; set; }
    }


    //
}
