using GiftCards.BusinessLayer;
using GiftCards.BusinessLayer.Services;
using GiftCards.DataLayer;
using GiftCards.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GiftCards.Tests.TestCases
{
    public class BusinessLogicTests
    {
        //write code here
        //mocking Object
        private Mock<IMongoCollection<Buyer>> _mockCollection;
        private Mock<IMongoCollection<ContactUs>> _contactmockCollection;
        private Mock<IMongoCollection<GiftOrder>> _giftOrdercontactmockCollection;
        private Mock<IMongoCollection<Gift>> _giftmockCollection;
        private Mock<IMongoDBContext> _mockContext;
        private Buyer _buyer;
        private ContactUs _contactUs;
        private GiftOrder _giftOrder;
        private Gift _gift;

        private readonly IList<Buyer> _list;
        private readonly IList<ContactUs> _contactlist;
        private readonly IList<GiftOrder> _orederlist;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
        static BusinessLogicTests()
        {
            if (!File.Exists("../../../../output_business_revised.txt"))
                try
                {
                    File.Create("../../../../output_business_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_business_revised.txt");
                File.Create("../../../../output_business_revised.txt");
            }
        }

        //creating or mocking dummy object
        public BusinessLogicTests()
        {
            _buyer = new Buyer
            {
                FirstName = "Jai",
                LastName = "buyer",
                PhoneNumber = 90876543321,
                Email = "jaibuyer@gmail.com",
                Address = "Delhi",
            };

            _contactUs = new ContactUs
            {
                Name = "John",
                PhoneNumber = 9888076466,
                Email = "jognexample@gmail.com",
                Address = "Paris"
            };
            _gift = new Gift
            {
                GiftName = "Toys",
                Ammount = 500
            };
            _giftOrder = new GiftOrder
            {
                  RecepientName="john",
                 ShippingAddress ="Delhi",
                 GiftId =_gift.GiftId,
                  BuyerId =_buyer.BuyerId
             };
            _mockCollection = new Mock<IMongoCollection<Buyer>>();
            _mockCollection.Object.InsertOne(_buyer);


            _contactmockCollection = new Mock<IMongoCollection<ContactUs>>();
            _contactmockCollection.Object.InsertOne(_contactUs);
            _giftOrdercontactmockCollection = new Mock<IMongoCollection<GiftOrder>>();
            _giftOrdercontactmockCollection.Object.InsertOne(_giftOrder);
            _giftmockCollection = new Mock<IMongoCollection<Gift>>();
            _giftmockCollection.Object.InsertOne(_gift);


            _contactlist = new List<ContactUs>();
            _contactlist.Add(_contactUs);

            _orederlist = new List<GiftOrder>();
            _orederlist.Add(_giftOrder);
            _mockContext = new Mock<IMongoDBContext>();
            //MongoSettings initialization
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _list = new List<Buyer>();
            _list.Add(_buyer);
        }
        //connecting to mongo local host databse
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
            DatabaseName = "guestbook"
        };

        // test for valid gift card amount
        [Fact]
        public void TestFor_ValidGiftCardsPrice()
        {
            //Arrange
            var Min = 100;
            var Max = 1000000;
            var res = false;
            //Action
            var actual = _gift.Ammount;

            //Assert
            Assert.InRange(actual, Min, Max);
            
            //writing tset boolean output in text file, that is present in project directory
            if (actual == _gift.Ammount)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_business_revised.txt", "TestFor_ValidGiftCardsPrice=" + res + "\n");
        }

        //test for valid email for contact us
        [Fact]
        public async Task TestFor_ValidContactUsEmail()
        {
            //mocking
            _contactmockCollection.Setup(op => op.InsertOneAsync(_contactUs, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<ContactUs>(typeof(ContactUs).Name)).Returns(_contactmockCollection.Object);

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new ContactUsServices(context);

            //Act
            var result = await userRepo.ContactUs(_contactUs);

            //Action
            bool CheckEmail = Regex.IsMatch(result.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool isEmail = Regex.IsMatch(_contactUs.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_business_revised.txt", "TestFor_ValidContactUsEmail=" + isEmail.ToString() + "\n");
            
            //Assert
            Assert.True(isEmail);
            Assert.True(CheckEmail);
        }

    }
}
