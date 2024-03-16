namespace AsyncFactoryMethod
{
    public class Foo
    {
        private Foo()
        {
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            //this method is async and we initialize instance of our class
            //perform the async operations that are hold in another method which returns this
            var result = new Foo();
            return result.InitAsync();
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var foo = await Foo.CreateAsync();
        }
    }
}
