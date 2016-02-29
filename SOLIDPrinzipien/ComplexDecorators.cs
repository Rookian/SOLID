using System.Collections.Generic;
using System.Transactions;

namespace SOLIDPrinzipien
{
    // Aufgabe: Advanced: Möglichkeit auch einen Rückgabewert zu haben
    public interface IRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }

    public interface IDb
    {
        void Save<T>(T item);
        T GetById<T>(int id) where T : new();
    }

    public class Db : IDb
    {
        public void Save<T>(T item)
        {
            //Save
        }

        public T GetById<T>(int id) where T : new()
        {
            return new T();
        }
    }

    public class SaveOrderRequest
    {
        public Order Order { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OrderLine
    {
        public int OrderId { get; set; }
        public int Id { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
    }

    public class SaveOrderRequestHandler : IRequestHandler<SaveOrderRequest>
    {
        private readonly IDb _db;

        public SaveOrderRequestHandler(IDb db)
        {
            _db = db;
        }

        public void Handle(SaveOrderRequest request)
        {
            // Save Order
            _db.Save(request.Order);

            // Save all Orderlines
            foreach (var orderLine in request.OrderLines)
            {
                _db.Save(orderLine);
            }
        }
    }

    public class TransactionHandler<T> : IRequestHandler<T>
    {
        private readonly IRequestHandler<T> _handler;

        public TransactionHandler(IRequestHandler<T> handler)
        {
            _handler = handler;
        }

        public void Handle(T request)
        {
            using (var transaction = new TransactionScope())
            {
                _handler.Handle(request);
                transaction.Complete();
            }
        }
    }

    public class Consuming
    {
        public void Execute()
        {
            var request = new SaveOrderRequest();
            var handler = new TransactionHandler<SaveOrderRequest>(new SaveOrderRequestHandler(new Db()));
            handler.Handle(request);
        }
    }
}