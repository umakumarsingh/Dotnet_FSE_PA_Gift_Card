
using GiftCards.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftCards.BusinessLayer.Interface
{
  public interface IContactUsServices
    {
        //methods for completing all ContactUs function
        Task<ContactUs> ContactUs(ContactUs contact);
        Task<bool> DeleteContactUsAsync(string ContactUsId);
        Task<ContactUs> UpdateContactUs(string ContactUsId);
        Task<IEnumerable<ContactUs>> GetAllContactUs();
    }
}
