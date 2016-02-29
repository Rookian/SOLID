using System;
using Refactoring.Infrastructure;

namespace Refactoring.MessageWay.Decorators
{
    public class ExceptionHandler<TIn, TOut> : IMessageHandler<TIn, TOut> where TIn : IMessage<TOut>
    {
        private readonly IMessageHandler<TIn, TOut> _handler;
        private readonly ILogger _logger;

        public ExceptionHandler(IMessageHandler<TIn, TOut> handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public TOut Handle(TIn input)
        {
            try
            {
                var @out = _handler.Handle(input);
                return @out;
            }
            catch (Exception exception)
            {
                _logger.Log(exception.ToString());
                throw;
            }
        }
    }
}