using System.Text;

namespace LifeWithoutBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            //sometimes we would use Builder pattern when building a objects is complicated

            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");

            Console.WriteLine(sb.ToString());

            //That was easy, but what if we need something more than just a single paragraph
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat($"<li>{word}</li>");
            }
            sb.Append("</ul>");

            Console.WriteLine(sb.ToString());

            //this is not convenient, so probably we want to create some sort of HtmlBuilder for this job
        }
    }
}
