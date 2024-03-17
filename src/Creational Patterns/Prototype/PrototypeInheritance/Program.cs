
namespace PrototypeInheritance
{
    public class Address : IDeepCopyable<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public void CopyTo(Address target)
        {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName} ; {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person : IDeepCopyable<Person>
    {
        public string[] Names;
        public Address Address;

        public Person()
        {
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public void CopyTo(Person target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy(); //this DeepCopy method comes from ExtensionMethods.DeepCopy
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(", ", Names)} ; {nameof(Address)}: {Address.StreetName}-{Address.HouseNumber}";
        }
    }

    //Now we have class that inherits from Person
    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {
        }

        public Employee(string[] names, Address address, int salary)
            : base(names, address)
        {
            Salary = salary;
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()} ; {nameof(Salary)}: {Salary}$";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var john = new Employee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new Address { HouseNumber = 123, StreetName = "John Street" };
            john.Salary = 123;

            var copy = john.DeepCopy();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumber = 321;
            copy.Salary = 321;

            Console.WriteLine(john.ToString());
            Console.WriteLine(copy.ToString());
        }
    }

    public interface IDeepCopyable<T> //T is the class we are copying
        where T : new()
    {
        void CopyTo(T target); //each class should be able to copy its state into some target
        T DeepCopy()
        {
            T t = new();
            CopyTo(t);
            return t;
        }
    }

    public static class ExtensioMethods
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item)
            where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person)
            where T : Person, new()
        {
            return ((IDeepCopyable<T>)person).DeepCopy();
        }
    }
}
