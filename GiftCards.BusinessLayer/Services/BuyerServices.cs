using GiftCards.BusinessLayer.Interface;
using GiftCards.DataLayer;
using GiftCards.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Services
{
    public class BuyerServices : IBuyerServices
    {
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<Buyer> _buyerdbCollection;
        private IMongoCollection<GiftOrder> _giftOrderdbCollection;


        public BuyerServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _buyerdbCollection = _mongoContext.GetCollection<Buyer>(typeof(Buyer).Name);
            _giftOrderdbCollection = _mongoContext.GetCollection<GiftOrder>(typeof(GiftOrder).Name);

        }

        public async Task<Buyer> RegisterAsync(Buyer buyer)
        {
            try
            {
                if (buyer == null)
                {
                    throw new ArgumentNullException(typeof(Buyer).Name + " object is null");
                }
                _buyerdbCollection = _mongoContext.GetCollection<Buyer>(typeof(Buyer).Name);
                await _buyerdbCollection.InsertOneAsync(buyer);
                return buyer;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<Buyer>> GetAllBuyersAsync()
        {
            try
            {
                var all = await _buyerdbCollection.FindAsync(Builders<Buyer>.Filter.Empty, null);
                return await all.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Task<Buyer> Login(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public Task<Buyer> ChangeBuyerPassword(string BuyerId, string newpassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOut(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public async Task<Buyer> GetBuyerByIdAsync(string BuyerId)
        {
            var objectId = new ObjectId(BuyerId);

            FilterDefinition<Buyer> filter = Builders<Buyer>.Filter.Eq("_id", objectId);

            _buyerdbCollection = _mongoContext.GetCollection<Buyer>(typeof(Buyer).Name);

            return await _buyerdbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

        public Task<IEnumerable<Gift>> SearchGiftCardByName(string GiftName)
        {
            throw new NotImplementedException();
        }

        public Task<GiftOrder> PlaceGiftOrderAsync(GiftOrder Order)
        {
            throw new NotImplementedException();
        }
    }
}
