namespace Per_ThreadSingleton
{
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> instance
            => new ThreadLocal<PerThreadSingleton>(
                () => new PerThreadSingleton());

        public int Id;

        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => instance.Value;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //one singleton per thread

            var thread1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"thread1: {PerThreadSingleton.Instance.Id}");
                Console.WriteLine($"thread1: {PerThreadSingleton.Instance.Id}");
            });

            var thread2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"thread2: {PerThreadSingleton.Instance.Id}");
                Console.WriteLine($"thread2: {PerThreadSingleton.Instance.Id}");
            });
            Task.WaitAll(thread1, thread2);
        }
    }
}
