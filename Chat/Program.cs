using Client.Firestore;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore;
using System;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("1 - Login\n2 - Register");
            FirestoreHelper.SetEnvironmentVariable();
            int option = int.Parse(Console.ReadLine());
            string username;
            string password;

            switch (option)
            {
                case 1:

                    bool loginCondition = false;

                    while(!loginCondition)
                    {
                        Console.WriteLine("Enter Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();
                        loginCondition = LoginNewUser(username, password);

                    }

                    break;

                case 2:
                    bool registerCondition = false;
                    while (!registerCondition)
                    {
                        Console.WriteLine("Enter Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();
                        Console.WriteLine("Enter Email");
                        string email = Console.ReadLine();
                        registerCondition = RegisterNewUser(username, password, email);
                    }
                    break;
            }
        }


        // returns false if failed to register.
        public static bool RegisterNewUser(string currUsername, string currPassword, string currEmail)
        {
            if (string.IsNullOrEmpty(currUsername) || string.IsNullOrEmpty(currPassword) || string.IsNullOrEmpty(currEmail))
            {
                Console.WriteLine("Fields are empty");
                return false;
            }

            var db = FirestoreHelper.database;
            if (CheckIfAlreadyExist(currUsername, currEmail))
            {
                // Display message handled in CheckIfAlreadyExist
                return false;
            }

            if (!CheckEmailCondition(currEmail)&!CheckUsernameCondition(currUsername)&&!CheckPasswordCondition(currPassword))
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
        // returns false if failed to login.
        public static bool LoginNewUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Error: No Fields");
                return false;
            }
            else if (!CheckUsernameCondition(username) && !CheckPasswordCondition(password))
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
        public static bool CheckIfAlreadyExist(string currUsername, string currEmail)
        {
            var db = FirestoreHelper.database;

            DocumentReference docRefUser = db.Collection("UserData").Document(currUsername);
            DocumentSnapshot snapshotUser = docRefUser.GetSnapshotAsync().Result;

            if (snapshotUser.Exists)
            {
                Console.WriteLine("Username exists");
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
            if (Username.Length >= 8)
            {
                return true;
            }
            else
            {
                Console.WriteLine("user needs at least 8 characters");
                return false;
            }

        }
        public static bool CheckPasswordCondition(string Password)
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
                Console.WriteLine("Password needs to contain ant least one number");
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
