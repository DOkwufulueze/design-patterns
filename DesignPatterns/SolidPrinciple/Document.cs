using static System.Console;

// SOLID Principle: Interface Segregation with the Decorator Pattern (delegating functionality)
namespace DesignPatterns.SolidPrinciple
{
    class Document
    {
        public string Name { get; set; }
        public byte Size { get; set; }
        public string Content { get; set; }

        public Document(string name, byte size, string content)
        {
            Name = name;
            Size = size;
            Content = content;
        }
    }

    class Printer : IPrinter
    {
        public void Print(Document document)
        {
            WriteLine("PRINTING...\nName: {0} ::: Size: {1}\nContent:\n\t{2}\n\n", document.Name, document.Size, document.Content);
        }
    }

    class Copier : ICopier
    {
        public void Copy(Document document)
        {
            WriteLine("COPYING...\nName: {0} ::: Size: {1}\nContent:\n\t{2}\n\n", document.Name, document.Size, document.Content);
        }
    }

    class Scanner : IScanner
    {
        public void Scan(Document document)
        {
            WriteLine("SCANNING...\nName: {0} ::: Size: {1}\nContent:\n\t{2}\n\n", document.Name, document.Size, document.Content);
        }
    }

    class Faxer : IFaxer
    {
        public void Fax(Document document)
        {
            WriteLine("FAXING...\nName: {0} ::: Size: {1}\nContent:\n\t{2}\n\n", document.Name, document.Size, document.Content);
        }
    }

    class MultiFunctionMachine : IMultiFunctionMachine
    {
        private readonly IPrinter _printer;
        private readonly ICopier _copier;
        private readonly IScanner _scanner;
        private readonly IFaxer _faxer;

        public MultiFunctionMachine(IPrinter printer, ICopier copier, IScanner scanner, IFaxer faxer)
        {
            _printer = printer;
            _copier = copier;
            _scanner = scanner;
            _faxer = faxer;
        }

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Copy(Document document)
        {
            _copier.Copy(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
        }

        public void Fax(Document document)
        {
            _faxer.Fax(document);
        }
    }

    class DocumentDemo
    {
        public static void Run()
        {
            Document document = new Document("Test Doc", 32, "This is a test document.\n\tJust to try out usage of the document processing machines.");
            IPrinter printer = new Printer();
            IMultiFunctionMachine multiFunctionMachine = new MultiFunctionMachine(new Printer(), new Copier(), new Scanner(), new Faxer());

            printer.Print(document);
            multiFunctionMachine.Print(document);
            multiFunctionMachine.Copy(document);
            multiFunctionMachine.Scan(document);
            multiFunctionMachine.Fax(document);
        }
    }
}
