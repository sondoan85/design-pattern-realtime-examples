namespace HistoricalStatesUndoActions
{
    //Prototype - IDocumentStatePrototype Interface
    public interface IDocumentStatePrototype
    {
        IDocumentStatePrototype Clone();
    }

    //Concrete Prototype - DocumentState Class
    public class DocumentState : IDocumentStatePrototype
    {
        public string Content { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }

        public IDocumentStatePrototype Clone()
        {
            return new DocumentState
            {
                Content = this.Content,
                FontName = this.FontName,
                FontSize = this.FontSize
            };
        }
    }

    public class Program
    {
        private static DocumentState currentDocument = new DocumentState();
        private static Stack<DocumentState> history = new Stack<DocumentState>();

        public static void Main(string[] args)
        {
            Stack<DocumentState> history = new Stack<DocumentState>();
            // Initial document state
            DocumentState document = new DocumentState
            {
                Content = "Hello, world!",
                FontName = "Arial",
                FontSize = 12
            };
            // Save initial state
            history.Push((DocumentState)document.Clone());
            // User changes the font
            document.FontName = "Times New Roman";
            // Save state after changing font
            history.Push((DocumentState)document.Clone());
            // User adds more content
            document.Content += "\nAdding some more text.";
            // Save state after adding more content
            history.Push((DocumentState)document.Clone());

            // User decides to undo
            if (history.Count > 0)
            {
                document = history.Pop();
            }

            // Display the document's state to simulate how it would appear in the editor
            Console.WriteLine($"Content: {document.Content}");
            Console.WriteLine($"Font: {document.FontName}");
            Console.WriteLine($"Size: {document.FontSize}");

            Console.ReadKey();
        }
    }
}
