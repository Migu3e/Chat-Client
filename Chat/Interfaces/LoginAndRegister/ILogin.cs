namespace Chat.Interfaces;

public interface ILogin
{
    static abstract Task LoginUserAsync();
    static abstract bool LoginNewUser(string username, string password);

}