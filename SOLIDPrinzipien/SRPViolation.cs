namespace SOLIDPrinzipien
{
    public class Booleans
    {
        public void DoSomething(bool shouldDoSomething)
        {
            if (shouldDoSomething)
            {
                // Foo
            }
            else
            {
                // Boo
            }
        }
    }

    public interface IAuditLogger
    {
        void UserPlacedOrder(int basketId);
        void UserAddedItemToBasket(int basketId, int itemId);
    }

    public class OrderService
    {
        private readonly IAuditLogger _auditLogger;

        public OrderService(IAuditLogger auditLogger)
        {
            _auditLogger = auditLogger;
        }

        public void PlaceOrder(int basketId)
        {
            // PlaceOrder code here
            _auditLogger.UserPlacedOrder(basketId);
        }

        public void AddItemToBasket(int basketId, int itemId)
        {
            // AddItemToBasket code here
            _auditLogger.UserAddedItemToBasket(basketId, itemId);
        }
    }
}