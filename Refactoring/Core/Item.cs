namespace Refactoring.Core
{
    public class Item : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual int Quantity { get; set; }
        public virtual double Price { get; set; }
    }
}