namespace Chat.Interfaces;

public interface ILoginAndRegister
{
    static abstract bool LoginNewUser(string username, string password);
    static abstract bool RegisterNewUser(string currUsername, string currPassword, string currEmail); 
    static abstract Task LoginUserAsync();
    static abstract Task<Task> RegisterUserAsync();

}