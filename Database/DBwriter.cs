using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeFromTheWoods.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EscapeFromTheWoods
{
    public class DBwriter
    {
        private readonly IMongoDatabase _database;

        public DBwriter(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public async Task WriteWoodRecordsAsync(List<DBWoodRecord> data)
        {
            var woodRecordsCollection = _database.GetCollection<BsonDocument>("WoodRecords");
            var documents = data.ConvertAll(record => new BsonDocument
            {
                {"woodID", record.WoodID},
                {"treeID", record.TreeID},
                {"x", record.X},
                {"y", record.Y}
            });

            await woodRecordsCollection.InsertManyAsync(documents).ConfigureAwait(false);
        }

        public async Task WriteMonkeyRecordsAsync(List<DBMonkeyRecord> data)
        {
            var monkeyRecordsCollection = _database.GetCollection<BsonDocument>("MonkeyRecords");
            var documents = data.ConvertAll(record => new BsonDocument
            {
                {"monkeyID", record.MonkeyID},
                {"monkeyName", record.MonkeyName},
                {"woodID", record.WoodID},
                {"seqNr", record.SeqNr},
                {"treeID", record.TreeID},
                {"x", record.X},
                {"y", record.Y}
            });

            await monkeyRecordsCollection.InsertManyAsync(documents).ConfigureAwait(false);
        }

        public async Task WriteLogsAsync(List<DBLogRecord> logs)
        {
            var logsCollection = _database.GetCollection<BsonDocument>("Logs");
            var documents = logs.ConvertAll(log => new BsonDocument
            {
                {"woodID", log.WoodID},
                {"monkeyID", log.MonkeyID},
                {"message", log.Message}
            });

            await logsCollection.InsertManyAsync(documents).ConfigureAwait(false);
        }
    }
}
