using Chat.Const;
using MongoDB.Driver;

namespace Client.MongoDB
{
    public static class MongoDBHelper
    {
        private static IMongoDatabase database;

        static MongoDBHelper()
        {
            // MongoDB connection string
            var client = new MongoClient(ConstMasseges.DatabaseConnection);
            
            // Database name
            database = client.GetDatabase(ConstMasseges.DatabaseName);
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}