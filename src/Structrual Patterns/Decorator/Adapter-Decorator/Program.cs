using System.Text;

namespace Adapter_Decorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyStringBuilder s = "hello ";
            s += "world";
            Console.WriteLine(s);
        }
    }

    public class MyStringBuilder
    {
        private StringBuilder sb = new();
        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.sb.Append(s);
            return msb;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
