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
        private readonly IBuyerServices _buyereservices;

        public BuyerController(IBuyerServices repository)
        {
            _buyereservices = repository;
        }

        [HttpGet]
        [Route("api/buyer")]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetAllBuyers()
        {
            var buyers = await _buyereservices.GetAllBuyersAsync();
            return Ok(buyers);
        }



        [HttpPost]
        [Route("api/buyer/addValues")]
        public ActionResult Post(Buyer model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                    return BadRequest("Please enter Name");
                else if (string.IsNullOrWhiteSpace(model.Email))
                    return BadRequest("Please enter Email");
                else if (string.IsNullOrWhiteSpace(model.Address))
                    return BadRequest("Please enter Address");

                _buyereservices.RegisterAsync(model);

                //return Ok("Your product has been added successfully");
                return Ok("Buyer has been added successfully"); //need to change
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetBuyerByIdAsync(string BuyerId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("api/buyer/login")]
        public ActionResult Login(Buyer buyer)
        {
            return Ok();
        }

        [HttpPut]
        [Route("api/buyer/changepassword")]
        public ActionResult ChangePassword(string BuyerId, string newpassword)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/buyer/addValues")]
        public ActionResult LogOut(Buyer buyer)
        {
            return Ok();
        }


        [HttpGet]
        [Route("api/gift")]
        public ActionResult SearchGiftCardByName(string GiftName)
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/giftOrder/addValues")]
        public ActionResult PlaceGiftOrderAsync(GiftOrder Order)
        {
            return Ok();
        }

    }
}