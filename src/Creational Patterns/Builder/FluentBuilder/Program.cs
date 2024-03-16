using System.Text;

namespace FluentBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var htmlBuilder = new HtmlBuilder("ul");

            //this is the fluent builder (we chaining method calls)
            htmlBuilder
                .AddChild("li", "hello")
                .AddChild("li", "world");

            Console.WriteLine(htmlBuilder.ToString());
        }
    }

    public class HtmlElement
    {
        public string Name; //main tag
        public string Content; //content in the tag
        public List<HtmlElement> NestedElements = new(); //nested elements - for example <ul><li></li></ul>
        private const int indentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string content)
        {
            this.Name = name;
            this.Content = content;
        }

        public override string ToString()
        {
            return this.ToStringImplementation(0);
        }

        private string ToStringImplementation(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);

            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Content))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Content);
            }

            foreach (var child in NestedElements)
            {
                sb.Append(child.ToStringImplementation(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootElement;
        HtmlElement root = new();

        public HtmlBuilder(string rootElement)
        {
            this.rootElement = rootElement;
            root.Name = rootElement;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var html = new HtmlElement(childName, childText);
            root.NestedElements.Add(html);

            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootElement };
        }
    }
}
