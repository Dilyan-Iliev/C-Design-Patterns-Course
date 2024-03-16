using System.Text;

namespace EndModuleTask
{
    //class responsible for fields in the class
    public class Field
    {
        public Field(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
    }

    //class responsible for className and its fields
    public class Code
    {
        public string ClassName { get; set; } = default!;
        public List<Field> Fields = new();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");
            foreach (var field in Fields)
            {
                sb.AppendLine($"public {field.Type} {field.Name};");
            }

            sb.AppendLine("}");

            return sb.ToString().TrimEnd();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person")
                .AddField("Name", "string")
                .AddField("Age", "int");

            Console.WriteLine(cb);
        }
    }

    public class CodeBuilder
    {
        private Code code = new();

        public CodeBuilder(string className)
        {
            this.code.ClassName = className;
        }

        public CodeBuilder AddField(string fieldName, string fieldType)
        {
            this.code.Fields.Add(new Field(fieldName, fieldType));
            return this;
        }

        public override string ToString()
        {
            return this.code.ToString();
        }
    }
}
