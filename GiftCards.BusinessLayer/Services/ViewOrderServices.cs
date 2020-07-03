using GiftCards.BusinessLayer.Interface;
using GiftCards.DataLayer;
using GiftCards.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Services
{
    public class ViewOrderServices : IViewOrderServices
    {
        //creating fiels for injecting dbcontext and registering mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<GiftOrder> _dbCollection;

        //injecting dbContext and geetting collection
        public ViewOrderServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<GiftOrder>(typeof(GiftOrder).Name);
        }

        //view all gift order
      public async Task<IEnumerable<GiftOrder>> ViewAllGiftCardOrders()
        {
            //Do code here
            throw new NotImplementedException();
        }
    }
}
