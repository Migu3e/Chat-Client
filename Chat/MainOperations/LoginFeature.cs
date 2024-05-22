using Client.Firestore;
using Google.Cloud.Firestore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MainOperations
{
    public class LoginFeature
    {
        
        public static bool LoginNewUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Error: No Fields");
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
                        Console.WriteLine("Login successful");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Password doesn't match");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Username doesn't exist");
                    return false;
                }
            }

        }

    }
}
