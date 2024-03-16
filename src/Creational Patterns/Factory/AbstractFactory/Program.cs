namespace AbstractFactory
{
    //Second kind of Factory pattern is Abstract Factory

    public interface IHotDrink
    {
        void ConsumeDrink();
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int quantity);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int quantity)
        {
            Console.WriteLine($"Preparing {quantity} teas");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int quantity)
        {
            Console.WriteLine($"Preparing {quantity} coffees");
            return new Coffee();
        }
    }


    public class HotDrinkMachine
    {
        private List<Tuple<string, IHotDrinkFactory>> factories = new();

        public HotDrinkMachine()
        {
            foreach (var item in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(item) && !item.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        item.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(item)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");
            for (int i = 0; i < factories.Count; i++)
            {
                var tuple = factories[i];
                Console.WriteLine($"{i}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out var result)
                    && result > 0
                    && result < factories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();

                    if (s != null && int.TryParse(s, out var amount)
                        && amount > 0)
                    {
                        return factories[result].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect input, try again");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.ConsumeDrink();
        }
    }

    internal class Tea : IHotDrink
    {
        public void ConsumeDrink()
        {
            Console.WriteLine("Consuming tea..");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void ConsumeDrink()
        {
            Console.WriteLine("Consuming coffee..");
        }
    }
}
