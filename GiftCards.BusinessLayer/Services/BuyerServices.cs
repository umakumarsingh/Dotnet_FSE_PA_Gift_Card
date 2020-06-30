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
        //creating fiels for injecting dbcontext and registering mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<Buyer> _buyerdbCollection;
        private IMongoCollection<GiftOrder> _giftOrderdbCollection;

        //injecting dbContext and geetting collection
        public BuyerServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _buyerdbCollection = _mongoContext.GetCollection<Buyer>(typeof(Buyer).Name);
            _giftOrderdbCollection = _mongoContext.GetCollection<GiftOrder>(typeof(GiftOrder).Name);

        }

        //register new buyer
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

        //get all buyer list 
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

        //Login Buyer
        public Task<Buyer> Login(Buyer buyer)
        {
            //write code here
            throw new NotImplementedException();
        }

        //change buyer password
        public Task<Buyer> ChangeBuyerPassword(string BuyerId, string newpassword)
        {
            //write code here
            throw new NotImplementedException();
        }

        //logout buyer
        public Task<bool> LogOut(Buyer buyer)
        {
            //write code here
            throw new NotImplementedException();
        }

        //get buyer by BuyerId
        public async Task<Buyer> GetBuyerByIdAsync(string BuyerId)
        {
            var objectId = new ObjectId(BuyerId);

            FilterDefinition<Buyer> filter = Builders<Buyer>.Filter.Eq("_id", objectId);

            _buyerdbCollection = _mongoContext.GetCollection<Buyer>(typeof(Buyer).Name);

            return await _buyerdbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

        //search gift card by GiftName
        public Task<IEnumerable<Gift>> SearchGiftCardByName(string GiftName)
        {
            //write code here
            throw new NotImplementedException();
        }

        //place gift order
        public Task<GiftOrder> PlaceGiftOrderAsync(GiftOrder Order)
        {
            //write code here
            throw new NotImplementedException();
        }

        //send purchased gift 
        public Task<Gift> SendPurchasedGiftCard(string GiftName)
        {
            //write code here
            throw new NotImplementedException();
        }
    }
}
