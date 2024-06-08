using Client.MongoDB;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Chat.Const;
using Client.MongoDB;
using ClientServer.MainOperations;

namespace Chat.MainOperations
{
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

            var collection = MongoDBHelper.GetCollection<UserData>("dataclient");

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

            collection.InsertOne(data);
            Console.WriteLine(ConstMasseges.RegisteredSuccess);
            return true;
        }

        public static async Task RegisterUserAsync()
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

                registerCondition = RegisterNewUser(username, password, email);
            }

            await Task.CompletedTask;
        }
    }
}
