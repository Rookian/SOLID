using Refactoring.Core;

namespace Refactoring
{
    public interface IUserService
    {
        User GetCurrentUser();
    }

    public class UserService : IUserService
    {
        public User GetCurrentUser()
        {
            return new User{ Name = "Alex", Id = 4, IsPremiumUser = true };
        }
    }
}