using System;
using System.Collections.Generic;
using NHibernate;
using Refactoring.Core;
using Refactoring.Infrastructure;
using Refactoring.Infrastructure.Clock;
using Refactoring.Infrastructure.NHibernate;
using Refactoring.MessageWay;
using Refactoring.MessageWay.Decorators;
using SimpleInjector;

namespace Refactoring
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = new Container();

            container.RegisterSingleton(SessionFactoryBuilder.Build);

            container.Register(() =>
            {
                var session = container.GetInstance<ISessionFactory>().OpenSession();
                session.FlushMode = FlushMode.Commit;
                return session;
            }, Lifestyle.Singleton);

            container.Register<IOrderService, OrderService>();
            container.Register<ILogger, Logger>();
            container.Register<IClock, Clock>();
            container.Register<IUserService, UserService>();
            container.Register<IAuditingService, AuditingService>();

            // MessageWay
            var assemblies = new[] { typeof(Program).Assembly };
            container.Register<IDispatcher, Dispatcher>();

            container.Register(typeof(IMessageHandler<,>), assemblies);

            // Decorators
            container.RegisterDecorator(typeof(IMessageHandler<,>), typeof(AuditingHandler<,>));
            container.RegisterDecorator(typeof(IMessageHandler<,>), typeof(TransactionHandler<,>));
            container.RegisterDecorator(typeof(IMessageHandler<,>), typeof(ExceptionHandler<,>));
            container.RegisterDecorator(typeof(IMessageHandler<,>), typeof(LoggingHandler<,>));


            container.Verify();

            var dispatcher = container.GetInstance<IDispatcher>();

            var basket = dispatcher.Dispatch(new PlaceOrderMessage
            {
                Items = new List<Item>
                {
                    new Item {Name = "Schoki", Quantity = 100, Price = 1.99},
                    new Item {Name = "Zuckerli", Quantity = 13, Price = 0.99}
                }
            });

            //var orderService = container.GetInstance<IOrderService>();

            //orderService.PlaceOrder(new List<Item>
            //{
            //    new Item {Name = "Schoki", Quantity = 100, Price = 1.99},
            //    new Item {Name = "Zuckerli", Quantity = 13, Price = 0.99}
            //});
        }
    }



}
