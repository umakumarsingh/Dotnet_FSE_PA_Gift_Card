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
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<GiftOrder> _dbCollection;

        public ViewOrderServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<GiftOrder>(typeof(GiftOrder).Name);
        }


      public async Task<IEnumerable<GiftOrder>> ViewAllGiftCardOrders()
        {
            //Do code here
            throw new NotImplementedException();
        }
    }
}
