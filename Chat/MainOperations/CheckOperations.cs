using Client.Firestore;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Interfaces;

namespace ClientServer.MainOperations
{
    static class CheckOperations
    {

        public static bool CheckIfAlreadyExist(string currUsername, string currEmail)
        {
            var db = FirestoreHelper.database;

            DocumentReference docRefUser = db.Collection("UserData").Document(currUsername);
            DocumentSnapshot snapshotUser = docRefUser.GetSnapshotAsync().Result;

            if (snapshotUser.Exists)
            {
                return true;
            }

            Query emailQuery = db.Collection("UserData").WhereEqualTo("Email", currEmail);
            QuerySnapshot emailQuerySnapshot = emailQuery.GetSnapshotAsync().Result;

            if (emailQuerySnapshot.Count > 0)
            {
                Console.WriteLine("Email exists");
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
                Console.WriteLine("user needs at least 3 characters");
                return false;
            }

        }
        public static bool CheckPasswordCondition(string Password)
        {
            if (Password.Length >= 7)
            {
                if (Password.Contains("0") ||
                    Password.Contains("1") ||
                    Password.Contains("2") ||
                    Password.Contains("3") ||
                    Password.Contains("4") ||
                    Password.Contains("5") ||
                    Password.Contains("6") ||
                    Password.Contains("7") ||
                    Password.Contains("8") ||
                    Password.Contains("9"))
                {

                    return true;
                }
                else
                {
                    Console.WriteLine("Password needs to contain at least 7 characters and at least one number(or only numbers)");
                    return false;
                }            }
            else
            {
                Console.WriteLine("Password needs at least 7 characters");
                return false;
            }
            
            

        }
        public static bool CheckEmailCondition(string email)
        {
            if (!email.Contains("@gmail.com"))
            {
                Console.WriteLine("Email is not legal");
                return false;
            }
            return true;
        }
    }
}
