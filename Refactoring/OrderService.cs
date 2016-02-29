using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Refactoring.Core;
using Refactoring.Infrastructure.Clock;

namespace Refactoring
{
    public interface IOrderService
    {
        Basket PlaceOrder(IList<Item> items);
    }

    public class OrderService : IOrderService
    {
        private readonly ISession _session;
        private readonly IClock _clock;
        private readonly IUserService _userService;
        private readonly IAuditingService _auditingService;
        private readonly ILogger _logger;

        public OrderService(ISession session, IClock clock, IUserService userService,
            IAuditingService auditingService, ILogger logger)
        {
            _session = session;
            _clock = clock;
            _userService = userService;
            _auditingService = auditingService;
            _logger = logger;
        }

        public Basket PlaceOrder(IList<Item> items)
        {
            _logger.Log($"Calling method {nameof(PlaceOrder)}");

            try
            {
                using (var transaction = _session.BeginTransaction())
                {
                    var currentUser = _userService.GetCurrentUser();
                    var orderDate = _clock.Now();

                    var basket = new Basket
                    {
                        Items = items,
                        OrderDate = orderDate
                    };

                    basket.TotalPrice = basket.Items.Sum(x => x.Price);
                    if (currentUser.IsPremiumUser && orderDate.Day % 2 == 0 && orderDate.Year < 2017)
                    {
                        basket.TotalPrice /= 2;
                    }

                    _session.SaveOrUpdate(basket);

                    _auditingService.Audit(currentUser, $"Placed order {basket.Id}");

                    transaction.Commit();
                    _logger.Log($"Called method {nameof(PlaceOrder)}");

                    return basket;
                }
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw;
            }
        }
    }
}