using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftCards.Entities
{
   public class Buyer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BuyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string Category { get; set; }
    }
}
