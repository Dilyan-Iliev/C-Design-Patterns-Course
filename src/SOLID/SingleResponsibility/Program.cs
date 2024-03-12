namespace SingleResponsibility
{
    public class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("first");
            journal.AddEntry("second");
            journal.AddEntry("third");
            Console.WriteLine(journal.ToString());

            Saver.SaveToFile("../../../journal.txt", journal, true);
        }
    }

    public class Journal
    {
        //the journal class MUST be responsible only for one thing - for example managing entries
        //but for example method Save is not for this class

        private readonly IList<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add(text);
            count++;

            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        //THIS METHOD IS NOT FOR THIS CLASS - MUST BE IN OTHER CLASS
        //public void Save(string filename)
        //{
        //    File.WriteAllText(filename, this.ToString());
        //}

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public static class Saver
    {
        //now this class is responsible for saving to files and it could work with different objects

        public static void SaveToFile(string filename, Journal journal, bool overwite = false)
        {
            if (overwite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }
}
