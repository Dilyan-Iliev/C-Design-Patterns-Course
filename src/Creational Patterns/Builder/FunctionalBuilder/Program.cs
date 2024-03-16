namespace FunctionalBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .Called("John")
                .WorksAs("manager") //this is an extension method to the sealed PersonBuilder class
                .Build();
        }
    }

    public sealed class PersonBuilder //this class is sealed => cant be inherited
    {
        private readonly List<Func<Person, Person>> actions = new();

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });

            return this;
        }

        public PersonBuilder Called(string name)
        {
            return this.Do(person => person.Name = name);
        }

        public PersonBuilder Do(Action<Person> action)
        {
            return this.AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (person, func) => (func(person)));
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
        {
            builder.Do(p => p.Position = position);
            return builder;
        }
    }

    public class Person
    {
        public string Name;
        public string Position;
    }

    //OR
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions = new();

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });

            return (TSelf)this;
        }

        public TSelf Called(string name)
        {
            return this.Do(person => person.Name = name);
        }

        public TSelf Do(Action<Person> action)
        {
            return this.AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (person, func) => (func(person)));
        }
    }

    public sealed class PersonBuilder2 : FunctionalBuilder<Person, PersonBuilder2>
    {
        public PersonBuilder2 Called(string name) => Do(p => p.Name = name);
    }
}
