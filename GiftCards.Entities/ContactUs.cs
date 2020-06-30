using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftCards.Entities
{
   public class ContactUs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactUsId { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
