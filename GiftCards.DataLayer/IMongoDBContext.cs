using MongoDB.Driver;

namespace GiftCards.DataLayer
{
  public  interface IMongoDBContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
