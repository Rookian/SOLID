using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Refactoring.Core;
using Refactoring.Infrastructure;
using Refactoring.Infrastructure.Clock;

namespace Refactoring.MessageWay
{
    public class PlaceOrderMessage : IMessage<Basket>
    {
        public IList<Item> Items { get; set; }
    }

    public class PlaceOrderMessageHandler : IMessageHandler<PlaceOrderMessage, Basket>
    {
        private readonly IClock _clock;
        private readonly IUserService _userService;
        private readonly ISession _session;

        public PlaceOrderMessageHandler(IClock clock, IUserService userService, ISession session)
        {
            _clock = clock;
            _userService = userService;
            _session = session;
        }

        public Basket Handle(PlaceOrderMessage placeOrderMessage)
        {
            var orderDate = _clock.Now();

            var basket = new Basket
            {
                Items = placeOrderMessage.Items,
                OrderDate = orderDate
            };

            basket.TotalPrice = basket.Items.Sum(x => x.Price);
            var currentUser = _userService.GetCurrentUser();
            if (currentUser.IsPremiumUser && orderDate.Day % 2 == 0 && orderDate.Year < 2017)
            {
                basket.TotalPrice /= 2;
            }

            _session.SaveOrUpdate(basket);

            return basket;
        }
    }
}