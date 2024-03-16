namespace FacetedBuilder
{
    public class Person
    {
        //we might want to have a builder that builds the address
        public string StreetAddress;
        public string Postcode;
        public string City;

        //we might want also to have a builde that build work info 
        public string CompanyName;
        public string Position;
        public int AnnualIncome;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //sometimes a single builder is not enought to build an object
            //we would need different builders which build different parts of an object

            var personBulder = new PersonBuilder();
            var person = personBulder
                .Works
                    .At("Some company")
                    .As("manager")
                    .WithIncome(10000)
                .Lives
                    .In("London")
                    .WithAddress("some address")
                    .WithPostcode("some postcode")
                .Build();
        }
    }

    public class PersonBuilder //this is a facade for other builders
    {
        //reference object to which different builders will point in their ctors
        protected Person person = new();


        //through this property we will have access to PersonWorkBuilder methods
        public PersonWorkBuilder Works
            => new PersonWorkBuilder(this.person);

        public PersonAddressBuilder Lives
            => new PersonAddressBuilder(this.person);

        public Person Build()
        {
            return this.person;
        }
    }

    //This builder classes could be subclasses of PersonBuilder class in order to hide them from end-user

    //Builder that builds the address info of a person
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder In(string city)
        {
            this.person.City = city;
            return this;
        }

        public PersonAddressBuilder WithAddress(string streetAddress)
        {
            this.person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            this.person.Postcode = postcode;
            return this;
        }
    }


    //Builder that builds the work info of a person
    public class PersonWorkBuilder : PersonBuilder
    {
        public PersonWorkBuilder(Person person)
        {
            this.person = person;
        }

        public PersonWorkBuilder At(string companyName)
        {
            this.person.CompanyName = companyName;
            return this;
        }

        public PersonWorkBuilder As(string position)
        {
            this.person.Position = position;
            return this;
        }

        public PersonWorkBuilder WithIncome(int salary)
        {
            this.person.AnnualIncome = salary;
            return this;
        }
    }
}
