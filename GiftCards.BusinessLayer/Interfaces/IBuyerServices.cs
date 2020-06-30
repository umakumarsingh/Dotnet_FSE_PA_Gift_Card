using GiftCards.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Interface
{
  public  interface IBuyerServices
    {
        //methods for completing all Buyer function
        Task<Buyer> RegisterAsync(Buyer buyer);
        Task<Buyer> Login(Buyer buyer);
        Task<Buyer> ChangeBuyerPassword(string BuyerId, string newpassword);
        Task<bool> LogOut(Buyer buyer);
        Task<IEnumerable<Buyer>> GetAllBuyersAsync();
        Task<Buyer> GetBuyerByIdAsync(string BuyerId);
        Task<IEnumerable<Gift>> SearchGiftCardByName(string GiftName);
        Task<GiftOrder> PlaceGiftOrderAsync(GiftOrder Order);
        Task<Gift> SendPurchasedGiftCard(string GiftName);
    }
}