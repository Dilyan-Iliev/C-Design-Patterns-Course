using System.Text;

namespace CustomStringBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Add behavior without altering the class itself without rewriting the existing code
            //Want to keep new functionality separate
            //Need to be able to interact with existing structures
            //Two options:
            // - inherit from required object if possible (some classes are sealed)
            // - build a decorator, which simplify references the decorated object(s)

        }
    }

    //For example we want additional behaviour of StringBuilder - we cannot inherit from it because this class is sealed, so we need decorator
    public class CodeBuilder
    {
        private StringBuilder sb = new();

        //public StringBuilder Clear()
        //{
        //we cannot return StringBuilder, we want to return our type - CodeBuilder
        //return sb.Clear();
        //}


        //So we refactor this method :
        public CodeBuilder Clear()
        {
            sb.AppendLine();
            sb.Clear();
            return this;
        }
    }
}
