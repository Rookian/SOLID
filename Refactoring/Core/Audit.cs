using System;

namespace Refactoring.Core
{
    public class Audit : BaseEntity
    {
        public virtual int UserId { get; set; }
        public virtual string Action { get; set; }
        public virtual DateTime DateTime { get; set; }
    }
}