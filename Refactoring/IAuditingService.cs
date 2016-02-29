using System;
using NHibernate;
using Refactoring.Core;
using Refactoring.Infrastructure.Clock;

namespace Refactoring
{


    public interface IAuditingService
    {
        void Audit(User user, string action);
    }

    public class AuditingService : IAuditingService
    {
        private readonly ISession _session;
        private readonly IClock _clock;

        public AuditingService(ISession session, IClock clock)
        {
            _session = session;
            _clock = clock;
        }

        public void Audit(User user, string action)
        {
            Console.WriteLine($"User {user.Name}/{user.Id} did the following: {action}");
            var audit = new Audit { Action = action, UserId = user.Id, DateTime = _clock.Now() };

            _session.SaveOrUpdate(audit);
        }
    }
}