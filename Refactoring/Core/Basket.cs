using System;
using System.Collections.Generic;

namespace Refactoring.Core
{
    public class Basket : BaseEntity
    {
        public virtual DateTime OrderDate { get; set; }
        public virtual double TotalPrice { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}