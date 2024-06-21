﻿using Chat.Const;
using MongoDB.Driver;

namespace Client.MongoDB
{
    public static class MongoDBRoomHelper
    {
        private static IMongoDatabase database;

        static MongoDBRoomHelper()
        {
            var client = new MongoClient(ConstMasseges.DatabaseConnection);
            database = client.GetDatabase(ConstMasseges.DatabaseName);
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}