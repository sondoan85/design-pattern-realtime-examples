namespace ContentManagementSystem
{
    //ArticleMemento - This will store the state of the article.
    public class ArticleMemento
    {
        public string Title { get; }
        public string Content { get; }
        public DateTime Timestamp { get; }

        public ArticleMemento(string title, string content, DateTime timestamp)
        {
            this.Title = title;
            this.Content = content;
            this.Timestamp = timestamp;
        }
    }

    //Article - This represents our main entity. 
    //It creates a memento and can restore its state from a memento.
    public class Article
    {
        public string Title { get; set; }
        public string Content { get; set; }

        // Saves the current state
        public ArticleMemento Save()
        {
            return new ArticleMemento(Title, Content, DateTime.Now);
        }

        // Restores the state from a memento
        public void Restore(ArticleMemento memento)
        {
            this.Title = memento.Title;
            this.Content = memento.Content;
        }
    }

    //History - This will manage the saved versions of the article.
    public class ArticleHistory
    {
        private Stack<ArticleMemento> _history = new Stack<ArticleMemento>();

        public void Save(Article article)
        {
            _history.Push(article.Save());
        }

        public ArticleMemento GetLatestVersion()
        {
            return _history.Pop();
        }
    }

    // Testing the Memento Design Pattern
    // Client Code
    public class Program
    {
        public static void Main()
        {
            Article article = new Article();
            ArticleHistory history = new ArticleHistory();
            article.Title = "Memento Pattern in C#";
            article.Content = "The Memento Pattern is...";

            history.Save(article); // Save the initial version

            article.Content += " It's useful for implementing undo functionality.";
            history.Save(article); // Save after adding more content
            article.Content += " However, care must be taken about memory usage.";

            // Oops! The last sentence was not needed. Let's restore the previous version.
            var previousVersion = history.GetLatestVersion();
            article.Restore(previousVersion);

            Console.WriteLine(article.Content);
            // Outputs: "The Memento Pattern is... It's useful for implementing undo functionality."

            Console.ReadKey();
        }
    }
}
