using Chat.Const;
using Client.Firestore;
using ClientServer.MainOperations;
using Google.Cloud.Firestore;

namespace Chat.MainOperations;

public class Register
{
    public static bool RegisterNewUser(string currUsername, string currPassword, string currEmail)
    {
        if (string.IsNullOrEmpty(currUsername) || string.IsNullOrEmpty(currPassword) ||
            string.IsNullOrEmpty(currEmail))
        {
            Console.WriteLine(ConstMasseges.OneOfFieldEmpty);
            return false;
        }

        var db = FirestoreHelper.database;
        if (CheckOperations.CheckIfAlreadyExist(currUsername, currEmail))
        {
            // Display message handled in CheckIfAlreadyExist
            return false;
        }

        if (!CheckOperations.CheckEmailCondition(currEmail) ||
            !CheckOperations.CheckUsernameCondition(currUsername) ||
            !CheckOperations.CheckPasswordCondition(currPassword))
        {
            return false;
        }

        var data = new UserData
        {
            UserName = currUsername,
            Password = currPassword,
            Email = currEmail
        };

        DocumentReference docRef = db.Collection("UserData").Document(data.UserName);
        docRef.SetAsync(data).Wait(); // Using Wait() to make it synchronous
        Console.WriteLine(ConstMasseges.RegisteredSuccess);
        return true;
    }
    public static async Task<Task> RegisterUserAsync()
    {
        string username = null;
        string password;
        string email;
        bool registerCondition = false;

        while (!registerCondition)
        {
            Console.WriteLine(ConstMasseges.EnterUsername);
            username = Console.ReadLine();
            Console.WriteLine(ConstMasseges.EnterPassword);
            password = Console.ReadLine();
            Console.WriteLine(ConstMasseges.EnterEmail);
            email = Console.ReadLine();

            registerCondition = Register.RegisterNewUser(username, password, email);
        }

        return Task.CompletedTask;
    }

}