namespace LoggingSystem
{
    //LogLevel Enums
    [Flags]
    public enum LogLevel
    {
        None = 0,
        Debug = 1,
        Info = 2,
        Error = 4
    }

    //Abstract Handler
    public abstract class Logger
    {
        protected Logger nextLogger;
        protected LogLevel logMask;
        public Logger(LogLevel mask)
        {
            this.logMask = mask;
        }
        public void SetNext(Logger nextLogger)
        {
            this.nextLogger = nextLogger;
        }
        public void LogMessage(LogLevel severity, string message)
        {
            if ((severity & logMask) != 0)
            {
                WriteMessage(message);
            }
            if (nextLogger != null)
            {
                nextLogger.LogMessage(severity, message);
            }
        }
        protected abstract void WriteMessage(string msg);
    }
    //Concrete Handlers
    public class DebugLogger : Logger
    {
        public DebugLogger() : base(LogLevel.Debug) { }
        protected override void WriteMessage(string message)
        {
            Console.WriteLine("Debug: " + message);
        }
    }
    public class InfoLogger : Logger
    {
        public InfoLogger() : base(LogLevel.Info) { }
        protected override void WriteMessage(string message)
        {
            Console.WriteLine("Info: " + message);
        }
    }
    public class ErrorLogger : Logger
    {
        public ErrorLogger() : base(LogLevel.Error) { }
        protected override void WriteMessage(string message)
        {
            Console.WriteLine("Error: " + message);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Setting up the chain
            Logger debugLogger = new DebugLogger();
            Logger infoLogger = new InfoLogger();
            Logger errorLogger = new ErrorLogger();

            debugLogger.SetNext(infoLogger);
            infoLogger.SetNext(errorLogger);

            debugLogger.LogMessage(LogLevel.Debug, "This is a Debug Message.");
            debugLogger.LogMessage(LogLevel.Info, "System is Operating Normally.");
            debugLogger.LogMessage(LogLevel.Error, "System Encountered an Error!");

            Console.ReadLine();
        }
    }
}
