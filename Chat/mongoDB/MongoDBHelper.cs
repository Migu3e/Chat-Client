using MongoDB.Driver;

namespace Client.MongoDB
{
    public static class MongoDBHelper
    {
        private static IMongoDatabase database;

        static MongoDBHelper()
        {
            // MongoDB connection string
            var client = new MongoClient("mongodb+srv://pc:123123gg123123@cluster0.tjadqzu.mongodb.net/");
            
            // Database name
            database = client.GetDatabase("client");
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}