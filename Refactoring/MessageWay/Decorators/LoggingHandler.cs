using Refactoring.Infrastructure;

namespace Refactoring.MessageWay.Decorators
{
    public class LoggingHandler<TIn, TOut> : IMessageHandler<TIn, TOut> where TIn : IMessage<TOut>
    {
        private readonly IMessageHandler<TIn, TOut> _handler;
        private readonly ILogger _logger;

        public LoggingHandler(IMessageHandler<TIn, TOut> handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public TOut Handle(TIn input)
        {
            _logger.Log($"Calling {typeof(TIn).Name}");
            var @out = _handler.Handle(input);
            _logger.Log($"Called {typeof(TIn).Name}");
            return @out;
        }
    }
}