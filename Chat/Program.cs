using Client.Firestore;
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
                    Console.WriteLine("Enter Username");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        Console.WriteLine("Error: No Fields");
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
                            }
                            else
                            {
                                Console.WriteLine("Password doesn't match");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Username doesn't exist");
                        }
                    }
                    break;

                case 2:
                    bool condition = false;
                    while (!condition)
                    {
                        Console.WriteLine("Enter Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();
                        Console.WriteLine("Enter Email");
                        string email = Console.ReadLine();
                        condition = RegisterNewUser(username, password, email);
                    }
                    break;
            }
        }

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

            if (!currEmail.Contains("@gmail.com"))
            {
                Console.WriteLine("Email is not legal");
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
    }


}
