using Newtonsoft.Json;
using Refactoring.Infrastructure;

namespace Refactoring.MessageWay.Decorators
{
    public class AuditingHandler<TIn, TOut> : IMessageHandler<TIn, TOut> where TIn : IMessage<TOut>
    {
        private readonly IMessageHandler<TIn, TOut> _handler;
        private readonly IAuditingService _auditingService;
        private readonly IUserService _userService;

        public AuditingHandler(IMessageHandler<TIn, TOut> handler, IAuditingService auditingService, IUserService userService)
        {
            _handler = handler;
            _auditingService = auditingService;
            _userService = userService;
        }

        public TOut Handle(TIn input)
        {
            var @out = _handler.Handle(input);

            var currentUser = _userService.GetCurrentUser();
            var serializedInput = JsonConvert.SerializeObject(input);
            _auditingService.Audit(currentUser, typeof(TIn).Name.Replace("Message", serializedInput));
            return @out;
        }
    }
}