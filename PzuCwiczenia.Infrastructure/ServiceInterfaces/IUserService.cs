namespace PzuCwiczenia.Infrastructure.ServiceInterfaces;

public interface IUserService
{
    bool ValidateCredentials(string username, string password);
}
