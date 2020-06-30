using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftCards.BusinessLayer.Interface;
using GiftCards.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftCards.Controllers
{
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsServices _services;

        public ContactUsController(IContactUsServices repository)
        {
            _services = repository;
        }

        [HttpPost]
        [Route("api/contactUs/addValues")]
        public ActionResult AddContactUs(ContactUs contact)
        {
            //Do code here
            return Ok();
        }

        [HttpDelete]
        [Route("api/contactUs/delete")]
        public ActionResult DeleteContactUs(string ContactUsId)
        {
            //Do code here
            return Ok();
        }

        [HttpGet]
        [Route("api/contactUs")]
        public ActionResult GetAllContactUs()
        {
            //Do code here
            return Ok();
        }

        [HttpPut]
        [Route("api/contactUs/update")]
        public ActionResult UpdateContactUs(string ContactUsId)
        {
            //Do code here
            return Ok();
        }
    }
}