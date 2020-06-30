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
    [Route("api/[controller]")]
    [ApiController]
    public class ViewOrderController : ControllerBase
    {
        private readonly IViewOrderServices _services;

        public ViewOrderController(IViewOrderServices repository)
        {
            _services = repository;
        }

        [HttpGet]
        [Route("api/giftOrder")]
        public async Task<ActionResult<IEnumerable<GiftOrder>>> ViewGiftOrders()
        {
           return Ok();
        }
    }
}