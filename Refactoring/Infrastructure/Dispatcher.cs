using SimpleInjector;

namespace Refactoring.Infrastructure
{
    public interface IMessage<TOutput>
    {
        
    }

    public interface IMessageHandler<in TInput, out TOutput> where TInput: IMessage<TOutput>
    {
        TOutput Handle(TInput input);
    }

    public interface IDispatcher
    {
        TOutput Dispatch<TOutput>(IMessage<TOutput> message);
    }

    public class Dispatcher : IDispatcher
    {
        private readonly Container _container;

        public Dispatcher(Container container)
        {
            _container = container;
        }

        public TOutput Dispatch<TOutput>(IMessage<TOutput> message)
        {
            var makeGenericType = typeof (IMessageHandler<,>).MakeGenericType(message.GetType(), typeof(TOutput));
            dynamic handler = _container.GetInstance(makeGenericType);
            return (TOutput)handler.Handle((dynamic) message);
        }
    }
}