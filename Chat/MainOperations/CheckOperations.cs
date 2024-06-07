using Client.MongoDB;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Const;
using Chat.Interfaces;
using MongoDB.Driver;


namespace ClientServer.MainOperations
{
    static class CheckOperations
    {
        public static bool CheckIfAlreadyExist(string currUsername, string currEmail)
        {
            var collection = MongoDBHelper.GetCollection<UserData>("dataclient");

            var userExists = collection.Find(u => u.UserName == currUsername).Any();
            if (userExists)
            {
                return true;
            }

            var emailExists = collection.Find(u => u.Email == currEmail).Any();
            if (emailExists)
            {
                Console.WriteLine(ConstMasseges.EmailAlreadyExist);
                return true;
            }

            return false;
        }

        public static bool CheckUsernameCondition(string Username)
        {
            if (Username.Length >= 3)
            {
                return true;
            }
            else
            {
                Console.WriteLine(ConstMasseges.UsernameToShort);
                return false;
            }
        }

        public static bool CheckPasswordCondition(string Password)
        {
            if (Password.Length >= 7 && Password.Any(char.IsDigit))
            {
                return true;
            }
            else
            {
                Console.WriteLine(ConstMasseges.PasswordNeedsToContainNumbers);
                return false;
            }
        }

        public static bool CheckEmailCondition(string email)
        {
            if (!email.Contains("@gmail.com"))
            {
                Console.WriteLine(ConstMasseges.EmailAlreadyExist);
                return false;
            }
            return true;
        }
    }
}
