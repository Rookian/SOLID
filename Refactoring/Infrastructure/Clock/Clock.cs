using System;

namespace Refactoring.Infrastructure.Clock
{
    public class Clock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }

    public interface IClock
    {
        DateTime Now();
    }
}