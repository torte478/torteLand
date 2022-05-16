namespace TorteLand.WebAPI2.Auth;

public interface IAuth
{
    void Register(string password);
    string Login(string password);
}