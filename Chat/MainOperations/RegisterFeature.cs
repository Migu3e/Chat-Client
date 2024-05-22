using Client.Firestore;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MainOperations
{
    internal class RegisterFeature
    {
        public static bool RegisterNewUser(string currUsername, string currPassword, string currEmail)
        {
            if (string.IsNullOrEmpty(currUsername) || string.IsNullOrEmpty(currPassword) || string.IsNullOrEmpty(currEmail))
            {
                Console.WriteLine("Fields are empty");
                return false;
            }

            var db = FirestoreHelper.database;
            if (MainOperations.CheckIfAlreadyExist(currUsername, currEmail))
            {
                // Display message handled in CheckIfAlreadyExist
                return false;
            }

            if (!MainOperations.CheckEmailCondition(currEmail) & !MainOperations.CheckUsernameCondition(currUsername) && !MainOperations.CheckPasswordCondition(currPassword))
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
            Console.WriteLine("User registered successfully");
            return true;
        }

    }
}
