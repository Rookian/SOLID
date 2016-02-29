namespace SOLIDPrinzipien
{
    // Was ist das Decorator Pattern?
    // Wie kann das Decorator Pattern in C# implementiert werden
    // Aufgabe weitere Dekoratoren hinzufügen
    // Diskussion: Wie kann man Dekoratoren für mehr als eine Methode bauen?
    public class Consumer
    {
        public void Do()
        {
            var businessFunction = new LoggingDecoratorHandler(new BusinessFunction(), new Logger());
            businessFunction.DoSomething("Foo");
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {

        }
    }

    public interface IBusinessFunction
    {
        void DoSomething(string foo);
    }

    public class BusinessFunction : IBusinessFunction
    {
        public void DoSomething(string foo)
        {
            // foohoo
        }
    }

    public class LoggingDecoratorHandler : IBusinessFunction
    {
        private readonly IBusinessFunction _businessFunction;
        private readonly ILogger _logger;

        public LoggingDecoratorHandler(IBusinessFunction businessFunction, ILogger logger)
        {
            _businessFunction = businessFunction;
            _logger = logger;
        }

        public void DoSomething(string foo)
        {
            _logger.Log($"Calling DoSomething({foo}) ...");
            _businessFunction.DoSomething(foo);
            _logger.Log($"Called DoSomething({foo})");
        }
    }
}