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
    public class ContactUsServices : IContactUsServices
    {
        //creating fiels for injecting dbcontext and registering mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<ContactUs> _dbCollection;

        //injecting dbContext and geetting collection
        public ContactUsServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<ContactUs>(typeof(ContactUs).Name);
        }

        //Add new contactUs
        public async Task<ContactUs> ContactUs(ContactUs contact)
        {
            try
            {
                if (contact == null)
                {
                    throw new ArgumentNullException(typeof(ContactUs).Name + " object is null");
                }
                _dbCollection = _mongoContext.GetCollection<ContactUs>(typeof(ContactUs).Name);
                await _dbCollection.InsertOneAsync(contact);
                return contact;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Delete contact
        public async Task<bool> DeleteContactUsAsync(string ContactUsId)
        {
            try
            {
                var objectId = new ObjectId(ContactUsId);

                FilterDefinition<ContactUs> filter = Builders<ContactUs>.Filter.Eq("_id", objectId);

                _dbCollection = _mongoContext.GetCollection<ContactUs>(typeof(ContactUs).Name);
                DeleteResult actionResult = await _dbCollection.DeleteOneAsync(filter);
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get all contact list
        public Task<IEnumerable<ContactUs>> GetAllContactUs()
        {
            //Do code here
            throw new NotImplementedException();
        }


        public Task<ContactUs> UpdateContactUs(string ContactUsId)
        {
            //Do code here
            throw new NotImplementedException();
        }
    }
}
