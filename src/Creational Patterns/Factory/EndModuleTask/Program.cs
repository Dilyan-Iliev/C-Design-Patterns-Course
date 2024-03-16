namespace EndModuleTask
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var person = PersonFactory.CreatePerson("Test");
            var person2 = PersonFactory.CreatePerson("Test2");
            Console.WriteLine(person.Id);
            Console.WriteLine(person2.Id);
        }
    }

    public class PersonFactory
    {
        private static int count = 0;

        public static Person CreatePerson(string name)
        {
            return new Person { Name = name, Id = count++ };
        }
    }
}
