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



    }



}
