using System.Text;

namespace TextFormatting
{
    public class FormattedText
    {
        private readonly string plainText;
        private bool[] capitalize;

        public FormattedText(string plainText)
        {
            this.plainText = plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }

            return sb.ToString().TrimEnd();
        }
    }

    public class BetterFormattedText
    {
        private readonly string plainText;
        private List<TextRange> formatting = new();

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char c = plainText[i];

                foreach (var range in formatting)
                {
                    if (range.Covers(i) && range.Capitalize)
                    {
                        c = char.ToUpper(c);
                    }
                }

                sb.Append(c);
            }

            return sb.ToString().TrimEnd();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("the input text");
            ft.Capitalize(4, 9);
            Console.WriteLine(ft.ToString());

            //----------------------------

            var betterFormatting = new BetterFormattedText("the second input text");
            betterFormatting.GetRange(4, 9).Capitalize = true;
            Console.WriteLine(betterFormatting.ToString());
        }
    }
}
