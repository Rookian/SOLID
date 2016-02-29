namespace Refactoring.Core
{
    public class User : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual bool IsPremiumUser { get; set; }
    }
}