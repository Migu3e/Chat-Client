namespace Chat.Interfaces;

public interface IRegister
{
    static abstract Task<Task> RegisterUserAsync();
    static abstract bool RegisterNewUser(string currUsername, string currPassword, string currEmail);

}