using PzuCwiczenia.Infrastructure.ServiceInterfaces;

namespace PzuCwiczenia.Services.Authentication;

public class UserService : IUserService
{
    public bool ValidateCredentials(string username, string password)
    {
        return username.Equals("admin", StringComparison.InvariantCultureIgnoreCase) && password.Equals("Pa$$w0rd");
    }
}
