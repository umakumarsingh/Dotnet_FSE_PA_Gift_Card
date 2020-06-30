
using GiftCards.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Interface
{
   public  interface IViewOrderServices
    {
        Task<IEnumerable<GiftOrder>> ViewAllGiftCardOrders();
    }
}
