using Chat.Const;
using Chat.Interfaces;
using Client.MongoDB;
using ClientServer.MainOperations;
using ClientServer.ProgramOptions;
using Google.Cloud.Firestore;
using MongoDB.Driver;


namespace Chat.MainOperations
{
public class Login
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

                loginCondition = LoginNewUser(username, password);
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
                var collection = MongoDBHelper.GetCollection<UserData>("dataclient");
                var user = collection.Find(u => u.UserName == username).FirstOrDefault();

                if (user != null)
                {
                    if (password == user.Password)
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
    
}