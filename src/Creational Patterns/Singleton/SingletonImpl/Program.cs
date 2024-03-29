using MoreLinq;

namespace SingletonImpl
{
    //Singleton design pattern - 
    //for some components it only makes sense to have one instance in the system
    //For example - DB Repository; Object factory; etc

    //So singleton is a component which is instantiated only once


    public interface IDatabase
    {
        int GetPopulation(string cityName);
    }

    public class SingletonDB : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDB() //we make the ctor private 
        {
            Console.WriteLine("Initializing database..");

            capitals = File.ReadAllLines("Cities.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),// this is the city name
                    list => int.Parse(list.ElementAt(1)) //this is the population
                    );
        }

        private static Lazy<SingletonDB> instance = new(() => new SingletonDB());
        //The use of Lazy<T> ensures thread safety during the creation of the singleton instance.
        //It guarantees that only one instance will be created even in a multithreaded environment

        public static SingletonDB Instance => instance.Value; //now the instance will be created only when someone access the value

        //or this method instead of the public property
        public static SingletonDB GetInstance()
        {
            return instance.Value;
        }

        public int GetPopulation(string cityName)
        {
            return capitals[cityName];
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDB.Instance; // always returns the same instance of the SingletonDB class.
            var population = db.GetPopulation("Sofia");

        }
    }
}
