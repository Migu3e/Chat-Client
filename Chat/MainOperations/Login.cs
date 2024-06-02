using Chat.Const;
using Chat.Interfaces;
using Client.Firestore;
using ClientServer.MainOperations;
using ClientServer.ProgramOptions;
using Google.Cloud.Firestore;

namespace Chat.MainOperations;

public class Login : ILogin
{
    public static async Task LoginUserAsync()
    {
        string username = null;
        string password;
        bool loginCondition = false;

        while (!loginCondition)
        {
            Console.WriteLine(ConstMasseges.EnterUsername);
            username = Console.ReadLine();
            Console.WriteLine(ConstMasseges.EnterPassword);
            password = Console.ReadLine();

            loginCondition = Login.LoginNewUser(username, password);
        }

        ProgramClientOptions client = new ProgramClientOptions(username);
        await client.ProgramClient();
    }
    public static bool LoginNewUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine(ConstMasseges.OneOfFieldEmpty);
            return false;
        }
        else if (!CheckOperations.CheckUsernameCondition(username) && !CheckOperations.CheckPasswordCondition(password))
        {
            return false;
        }
        else
        {
            var db = FirestoreHelper.database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync().Result;

            if (snapshot.Exists)
            {
                UserData data = snapshot.ConvertTo<UserData>();
                if (password == data.Password)
                {
                    Console.WriteLine(ConstMasseges.LoginSuccess);
                    return true;
                }
                else
                {
                    Console.WriteLine(ConstMasseges.PasswordIsIncorrect);
                    return false;
                }
            }
            else
            {
                Console.WriteLine(ConstMasseges.UsernameDoesntExist);
                return false;
            }
        }


    }
}