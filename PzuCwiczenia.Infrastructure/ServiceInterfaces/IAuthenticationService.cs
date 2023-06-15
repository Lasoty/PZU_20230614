namespace PzuCwiczenia.Infrastructure.ServiceInterfaces;

public interface IAuthenticationService
{
    string Auhtenticate(string username, string password);
}
