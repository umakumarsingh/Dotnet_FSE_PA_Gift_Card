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
    [ApiController]
    public class BuyerController : ControllerBase
    {
        //creating fileld for services interface and inject in constructor
        private readonly IBuyerServices _buyereservices;

        public BuyerController(IBuyerServices repository)
        {
            _buyereservices = repository;
        }

        //get all buyer
        [HttpGet]
        [Route("api/buyer")]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetAllBuyers()
        {
            //do code here
            return Ok();
        }

        //register new buyer
        [HttpPost]
        [Route("api/buyer/addValues")]
        public ActionResult Post(Buyer model)
        {
            //Do code here
            return Ok();
        }

        //Get buyer by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyerByIdAsync(string BuyerId)
        {
            //Write Code Here
            return Ok();
        }

        //login Buyer
        [HttpPut]
        [Route("api/buyer/login")]
        public ActionResult Login(Buyer buyer)
        {
            //Write Code Here
            return Ok();
        }

        //change buyer password
        [HttpPut]
        [Route("api/buyer/changepassword")]
        public ActionResult ChangePassword(string BuyerId, string newpassword)
        {
            //Write Code Here
            return Ok();
        }

        //logout buyer after login
        [HttpGet]
        [Route("api/buyer/addValues")]
        public ActionResult LogOut(Buyer buyer)
        {
            //Write Code Here
            return Ok();
        }

        //search gift car by id
        [HttpGet]
        [Route("api/gift")]
        public ActionResult SearchGiftCardByName(string GiftName)
        {
            //Write Code Here
            return Ok();
        }

        //place gift order
        [HttpPost]
        [Route("api/giftOrder/addValues")]
        public ActionResult PlaceGiftOrderAsync(GiftOrder Order)
        {
            //Write Code Here
            return Ok();
        }

        //send purchased gift
        [HttpPost]
        [Route("api/buyer/sendGift")]
        public ActionResult SendGiftCardAsync(string GiftName)
        {
            //Write Code Here
            return Ok();
        }
    }
}