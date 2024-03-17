namespace TheBadExample
{
    //This pattern is all about object copy
    //If we have already definied complicated type and then we want to make variations of this complicated object
    //In order to make such variations we make copy (clone) the prototype and customize it


    public class Person : ICloneable
        //the problem with IClonable is that we dont know if it is deep copy or shallow copy
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public object Clone()
        {
            return new Person(Names, Address); //shallow copy
            //So IClonable is not the right interface for deep copy
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(new[] { "John", "Smith" },
                new Address("Street name", 123));

            //now we would want to copy this person
            //var secondPerson = person; // this wont work - we copy the refference only


        }
    }
}
