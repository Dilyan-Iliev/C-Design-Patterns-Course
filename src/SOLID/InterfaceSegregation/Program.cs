namespace InterfaceSegregation
{
    public class Program
    {
        static void Main(string[] args)
        {
            //keep interfaces small and focused with specific functionallity
        }

        public class Document
        {

        }

        public class MultiFunctionalPrinter : IMachine
        {
            public void Fax(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Print(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document doc)
            {
                throw new NotImplementedException();
            }
        }

        public class NormalPrinter : IMachine
        {
            //here i dont need all these methods (but they are in the IMachine interface)

            public void Fax(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Print(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document doc)
            {
                throw new NotImplementedException();
            }
        }

        public interface IMachine
        {
            //i might not need all this methods for some class that implements this interface
            //so it is better this methods to be in separate interfaces
            void Print(Document doc);
            void Scan(Document doc);
            void Fax(Document doc);
        }

        //BETTER multiple small interfaces with specific methods
        //and different class will implement these interfaces which methods they only need
        public interface IPrinter
        {
            void Print(Document doc);
        }
        public interface IScanner
        {
            void Scan(Document doc);
        }
        public interface IFaxer
        {
            void Fax(Document doc);
        }

        //this class needs to do different things, so implements many interfaces
        public class BetterMultiPrinter : IPrinter, IScanner, IFaxer
        {
            public void Fax(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Print(Document doc)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document doc)
            {
                throw new NotImplementedException();
            }
        }

        //this class needs only to Print so implements only IPrinter
        public class BetterNormalPrinter : IPrinter
        {
            public void Print(Document doc)
            {
                throw new NotImplementedException();
            }
        }
    }
}
