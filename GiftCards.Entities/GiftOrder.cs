using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftCards.Entities
{
    public class GiftOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GiftOrderId { get; set; }
        public string RecepientName { get; set; }
        public string ShippingAddress {get; set;}
        public string GiftId { get; set; }
        public string BuyerId { get; set; }
    }
}
