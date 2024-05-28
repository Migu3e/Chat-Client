﻿using Chat.Interfaces;
using Client.Firestore;
using ClientServer.MainOperations;
using ClientServer.ProgramOptions;
using Google.Cloud.Firestore;

namespace Chat.MainOperations;

public class LoginAndRegister : ILoginAndRegister
{
    public static async Task LoginUserAsync()
    {
        string username = null;
        string password;
        bool loginCondition = false;

        while (!loginCondition)
        {
            Console.WriteLine("Enter Username");
            username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            password = Console.ReadLine();

            loginCondition = LoginAndRegister.LoginNewUser(username, password);
        }

        ProgramClientOptions client = new ProgramClientOptions(username);
        await client.ProgramClient();
    }
    public static async Task<Task> RegisterUserAsync()
    {
        string username = null;
        string password;
        string email;
        bool registerCondition = false;

        while (!registerCondition)
        {
            Console.WriteLine("Enter Username");
            username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            password = Console.ReadLine();
            Console.WriteLine("Enter Email");
            email = Console.ReadLine();

            registerCondition = LoginAndRegister.RegisterNewUser(username, password, email);
        }

        return Task.CompletedTask;
    }

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
    public static bool RegisterNewUser(string currUsername, string currPassword, string currEmail)
    {
        if (string.IsNullOrEmpty(currUsername) || string.IsNullOrEmpty(currPassword) ||
            string.IsNullOrEmpty(currEmail))
        {
            Console.WriteLine("Fields are empty");
            return false;
        }

        var db = FirestoreHelper.database;
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

        DocumentReference docRef = db.Collection("UserData").Document(data.UserName);
        docRef.SetAsync(data).Wait(); // Using Wait() to make it synchronous
        Console.WriteLine("User registered successfully");
        return true;
    }
}






