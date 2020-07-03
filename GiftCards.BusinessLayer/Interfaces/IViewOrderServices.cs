
using GiftCards.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Interface
{
   public  interface IViewOrderServices
    {
        //methods for completing all Order function
        Task<IEnumerable<GiftOrder>> ViewAllGiftCardOrders();
    }
}
