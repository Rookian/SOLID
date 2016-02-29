using NHibernate;
using Refactoring.Infrastructure;

namespace Refactoring.MessageWay.Decorators
{
    public class TransactionHandler<TIn, TOut> : IMessageHandler<TIn, TOut> where TIn : IMessage<TOut>
    {
        private readonly IMessageHandler<TIn, TOut> _handler;
        private readonly ISession _session;

        public TransactionHandler(IMessageHandler<TIn, TOut> handler, ISession session)
        {
            _handler = handler;
            _session = session;
        }

        public TOut Handle(TIn input)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var @out = _handler.Handle(input);
                transaction.Commit();
                return @out;
            }
        }
    }
}