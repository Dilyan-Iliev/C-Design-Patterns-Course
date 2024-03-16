namespace FluentBuilderInheritanceWithRecursiveGenerics
{
    //this is the final product
    public class Person
    {
        public string Name { get; set; } = default!;
        public string Position { get; set; } = default!;

        public class Builder : PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new Builder();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //builder inherited from another builders

            var person = Person.New //this line returns Person.Builder but the Builder inherits from JobBuilder
                //which inherits from InfoBuilder which inherits from PersonBuilder
                .WithName("John")
                .WorksAs("manager")
                .Build();
        }
    }

    //common things for different builders
    public abstract class PersonBuilder
    {
        protected Person person = new();

        public Person Build() => this.person;
    }

    //builds the personal info a person
    public class PersonInfoBuilder<TSelf>
        //self reference and we want to restrict TSelf to be only classes that inherit from PersonBuilder
        : PersonBuilder
        where TSelf : PersonInfoBuilder<TSelf> //for example class Foo inherits from Bar<Foo> class  
    {
        public TSelf WithName(string name)
        {
            person.Name = name;
            return (TSelf)this;
        }
    }

    //we have a builder which has all the above builder functionallity but also a additional functionallity
    public class PersonJobBuilder<TSelf>
        : PersonInfoBuilder<PersonJobBuilder<TSelf>>
        where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorksAs(string position)
        {
            person.Position = position;
            return (TSelf)this;
        }
    }
}
