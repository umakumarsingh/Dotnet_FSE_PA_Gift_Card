﻿
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GiftCards.DataLayer
{
   public class MongoDBContext : IMongoDBContext
    {
            private IMongoDatabase _db { get; set; }
            private MongoClient _mongoClient { get; set; }
            public IClientSessionHandle Session { get; set; }
            public MongoDBContext(IOptions<Mongosettings> configuration)
            {
                _mongoClient = new MongoClient(configuration.Value.Connection);
                _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
            }

            public IMongoCollection<T> GetCollection<T>(string name)
            {
                return _db.GetCollection<T>(name);
            }
        }
}