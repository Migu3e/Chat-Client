using Client.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Const;
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

            // Create private chats for the new user
            CreatePrivateChats(currUsername).Wait();
            
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
        }

        public static async Task CreatePrivateChats(string newUsername)
        {
            try
            {
                var collection = MongoDBHelper.GetCollection<RoomDB>("chats");
                var clientsCollection = MongoDBHelper.GetCollection<UserData>("dataclient");

                // Get all clients from the database
                var existingClients = await clientsCollection.Find(_ => true).ToListAsync();

                // Log the number of clients fetched
                Console.WriteLine($"Number of clients fetched: {existingClients.Count}");

                // Get all existing room names from the database
                var existingRooms = await collection.Find(_ => true).ToListAsync();
                var existingRoomNames = new HashSet<string>(existingRooms.Select(r => r.RoomName));

                // List to accumulate new rooms to be inserted
                var newRooms = new List<RoomDB>();

                foreach (var existingClient in existingClients)
                {
                    if (existingClient.UserName != newUsername)
                    {
                        string chatName = $"|private|.{existingClient.UserName}.-.{newUsername}.";
                        string chatNameSecondWay = $"|private|.{newUsername}.-.{existingClient.UserName}.";

                        // Check if the room already exists in the existingRoomNames set
                        if (!existingRoomNames.Contains(chatName) && !existingRoomNames.Contains(chatNameSecondWay))
                        {
                            // Add the new room to the list
                            newRooms.Add(new RoomDB
                            {
                                RoomName = chatName,
                                MList = new List<string>()
                            });

                            // Add the room names to the set to avoid duplicates
                            existingRoomNames.Add(chatName);
                            existingRoomNames.Add(chatNameSecondWay);
                        }
                    }
                }

                // Perform a batch insertion for all new rooms
                if (newRooms.Count > 0)
                {
                    await collection.InsertManyAsync(newRooms);
                }

                Console.WriteLine("Private chats created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
