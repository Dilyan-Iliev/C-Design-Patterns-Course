
namespace Init
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        private static List<string> strings = new();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);

                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(' ',
            names.Select(i => strings[i]));
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //The idea of this pattern is space optimization - amount of memory that the app takes up
            //Flyweight pattern - a space optimization technique that lets us use less memory by storing externally the data associated with similar objects.

        }
    }
}
