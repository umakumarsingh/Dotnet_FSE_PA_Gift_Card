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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GiftCards.Tests.TestCases
{
    public class FuctionalTests
    {
        private Mock<IMongoCollection<Buyer>> _mockCollection;
        private Mock<IMongoCollection<ContactUs>> _contactmockCollection;
        private Mock<IMongoCollection<GiftOrder>> _giftOrdermockCollection;
        private Mock<IMongoCollection<Gift>> _giftmockCollection;
        private Mock<IMongoDBContext> _mockContext;
        private Buyer _buyer;
        private ContactUs _contactUs;
        private GiftOrder _giftOrder;
        private Gift _gift;

        private readonly IList<Buyer> _list;
        private readonly IList<ContactUs> _contactlist;
        private readonly IList<GiftOrder> _orederlist;
        private readonly IList<Gift> _giftlist;
        private Mock<IOptions<Mongosettings>> _mockOptions;
        static FuctionalTests()
        {
            if (!File.Exists("../../../../output_Functional_revised.txt"))
                try
                {
                    File.Create("../../../../output_Functional_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_Functional_revised.txt");
                File.Create("../../../../output_Functional_revised.txt");
            }
        }

        public FuctionalTests()
        {
            _buyer = new Buyer
            {
                FirstName = "buyers",
                LastName = "Two",
                PhoneNumber = 123456789,
                Email = "buyertwo@gmail.com",

                Address = "Bangalore",
            };

            _contactUs =new ContactUs
            {
            Name="John",
            PhoneNumber=9888076466,
            Email="example@gmail.com",
            Address="Paris"
              };
            _gift = new Gift
            {
                GiftName = "Toys",
                Ammount = 500
            };
            _giftOrder = new GiftOrder
            {
                RecepientName = "john",
                ShippingAddress = "Delhi",
                GiftId = _gift.GiftId,
                BuyerId = _buyer.BuyerId
            };
            _mockCollection = new Mock<IMongoCollection<Buyer>>();
            _mockCollection.Object.InsertOne(_buyer);


            _contactmockCollection = new Mock<IMongoCollection<ContactUs>>();
            _contactmockCollection.Object.InsertOne(_contactUs);
            _giftOrdermockCollection = new Mock<IMongoCollection<GiftOrder>>();
            _giftOrdermockCollection.Object.InsertOne(_giftOrder);
            _giftmockCollection = new Mock<IMongoCollection<Gift>>();
            _giftmockCollection.Object.InsertOne(_gift);

            _contactlist = new List<ContactUs>();
            _contactlist.Add(_contactUs);
            _giftlist = new List<Gift>();
            _giftlist.Add(_gift);

            _orederlist = new List<GiftOrder>();
            _orederlist.Add(_giftOrder);
            _mockContext = new Mock<IMongoDBContext>();
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _list = new List<Buyer>();
            _list.Add(_buyer);
        }
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://localhost:27017",
            DatabaseName = "guestbook"
        };

        [Fact]
        public async void TestFor_ContactUsAsync()
        {
            //mocking
            var res = false; // new Codde added
            _contactmockCollection.Setup(op => op.InsertOneAsync(_contactUs, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<ContactUs>(typeof(ContactUs).Name)).Returns(_contactmockCollection.Object);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new ContactUsServices(context);

            //Action
            var contact = await userRepo.ContactUs(_contactUs);

            // new Codde added
            if (contact != null)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_Functional_revised.txt", "TestFor_ContactUsAsync=" + res + "\n");

            //Assert
            Assert.NotNull(contact);
            Assert.Equal(_contactUs.Name, contact.Name);

        }

        [Fact]
        public async void TestFor_DeleteContactUs()
        {
            //mocking
            _contactmockCollection.Setup(op => op.DeleteOneAsync(_contactUs.ContactUsId, null, default(CancellationToken)));
            _mockContext.Setup(c => c.GetCollection<ContactUs>(typeof(ContactUs).Name)).Returns(_contactmockCollection.Object);
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var contactRepo = new ContactUsServices(context);

            //Act
            var contact = await contactRepo.ContactUs(_contactUs);
            var IsDeleted = await contactRepo.DeleteContactUsAsync(_contactUs.ContactUsId);
            File.AppendAllText("../../../../output_Functional_revised.txt", "TestFor_DeleteContactUs=" + IsDeleted.ToString() + "\n");

            //Assert
            Assert.True(IsDeleted);
        }

        //[Fact]
        //public async Task TestFor_GetAllBuyersAsync()
        //{
        //    //Arrange
        //    Mock<IAsyncCursor<Buyer>> _userCursor = new Mock<IAsyncCursor<Buyer>>();
        //    _userCursor.Setup(_ => _.Current).Returns(_list);
        //    _userCursor
        //        .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
        //        .Returns(true)
        //        .Returns(false);

        //    _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Buyer>>(),
        //    It.IsAny<FindOptions<Buyer, Buyer>>(),
        //     It.IsAny<CancellationToken>())).Returns(_userCursor.Object);
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name)).Returns(_mockCollection.Object);
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);
        //    //Action
        //    var result = await userRepo.GetAllBuyersAsync();

        //    //Assert 
        //    foreach (Buyer user in result)
        //    {
        //        Assert.NotNull(user);
        //        break;
        //    }
        //}

        //[Fact]
        //public async void TestFor_BuyerRegisterAsync()
        //{
        //    //mocking
        //    _mockCollection.Setup(op => op.InsertOneAsync(_buyer, null,
        //    default(CancellationToken))).Returns(Task.CompletedTask);
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name)).Returns(_mockCollection.Object);

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    //Action
        //   var buyer= await userRepo.RegisterAsync(_buyer);

        //    //Assert
        //    Assert.NotNull(buyer);
        //   Assert.Equal(_buyer.FirstName, buyer.FirstName);
        //  
        //}

        //[Fact]
        //public async void TestFor_BuyerLogout()
        //{
        //    //mocking
        //    _mockCollection.Setup(op => op.InsertOneAsync(_buyer, null,
        //    default(CancellationToken))).Returns(Task.CompletedTask);
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name)).Returns(_mockCollection.Object);

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    //Action
        //    var isLogOut = await userRepo.LogOut(_buyer);

        //    //Assert
        //    Assert.True(isLogOut);
        //}
        //[Fact]
        //public async void TestFor_BuyerLogin()
        //{
        //    //mocking
        //    _mockCollection.Setup(op => op.InsertOneAsync(_buyer, null,
        //    default(CancellationToken))).Returns(Task.CompletedTask);
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name)).Returns(_mockCollection.Object);

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    //Act
        //   var loginBuye = await userRepo.Login(_buyer);
        //    var result = await userRepo.GetBuyerByIdAsync(_buyer.BuyerId);

        //    //Assert
        //    Assert.Equal(_buyer.FirstName, loginBuye.FirstName);
        //    Assert.Equal(_buyer.FirstName, result.FirstName);
        //}

        //[Fact]
        //public async void TestFor_ChangeBuyerPassword()
        //{
        //    //mocking
        //    _mockCollection.Setup(s => s.UpdateOneAsync(It.IsAny<FilterDefinition<Buyer>>(), It.IsAny<UpdateDefinition<Buyer>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()));
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name)).Returns(_mockCollection.Object);

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    var newPassword = "password";
        //    //Action
        //    var upadtedbuyer = await userRepo.ChangeBuyerPassword(_buyer.BuyerId,newPassword);
        //    var result = await userRepo.GetBuyerByIdAsync(_buyer.BuyerId);

        //    //Assert
        //    Assert.Equal(newPassword, upadtedbuyer.Password);
        //    Assert.Equal(_buyer.FirstName, result.FirstName);
        //}


        //[Fact]
        //public async Task TestFor_GetBuyerByIdAsync()
        //{
        //    //Arrange
        //    //mocking
        //    _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Buyer>>(),
        //    It.IsAny<FindOptions<Buyer, Buyer>>(),
        //     It.IsAny<CancellationToken>()));
        //    _mockContext.Setup(c => c.GetCollection<Buyer>(typeof(Buyer).Name));

        //    //Craetion of new Db
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    //Act
        //    await userRepo.RegisterAsync(_buyer);
        //    var result = await userRepo.GetBuyerByIdAsync(_buyer.BuyerId);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task TestFor_PlaceGiftOrderAsync()
        //{
        //    //mocking
        //    _giftOrdermockCollection.Setup(op => op.InsertOneAsync(_giftOrder, null,
        //    default(CancellationToken))).Returns(Task.CompletedTask);
        //    _mockContext.Setup(c => c.GetCollection<GiftOrder>(typeof(GiftOrder).Name)).Returns(_giftOrdermockCollection.Object);

        //    //Craetion of new Db
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var buyerRepo = new BuyerServices(context);

        //    //Action
        //    var result = await buyerRepo.PlaceGiftOrderAsync(_giftOrder);

        //    //Assert
        //    Assert.Equal(_buyer.BuyerId, result.BuyerId);
        //    Assert.Equal(_gift.GiftId, result.GiftId);
        //}

        //[Fact]
        //public async Task TestFor_SearchGiftCardByName()
        //{
        //    //Arrange
        //    Mock<IAsyncCursor<Gift>> _userCursor = new Mock<IAsyncCursor<Gift>>();
        //    _userCursor.Setup(_ => _.Current).Returns(_giftlist);
        //    _userCursor
        //        .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
        //        .Returns(true)
        //        .Returns(false);
        //    //mocking
        //    _giftmockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<Gift>>(),
        //    It.IsAny<FindOptions<Gift, Gift>>(),
        //     It.IsAny<CancellationToken>()));
        //    _mockContext.Setup(c => c.GetCollection<Gift>(typeof(Gift).Name));

        //    //Craetion of new Db
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new BuyerServices(context);

        //    //Action
        //    var result = await userRepo.SearchGiftCardByName(_gift.GiftName);

        //    //Assert
        //    foreach (Gift gift in result)
        //    {
        //        Assert.NotNull(gift);
        //        break;
        //    }
        //}

        //[Fact]
        //public async Task TestFor_GetAllContactUs()
        //{
        //    //Arrange
        //    //Mock MoveNextAsync
        //    Mock<IAsyncCursor<ContactUs>> _userCursor = new Mock<IAsyncCursor<ContactUs>>();
        //    _userCursor.Setup(_ => _.Current).Returns(_contactlist);
        //    _userCursor
        //        .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
        //        .Returns(true)
        //        .Returns(false);

        //    //Mock FindSync
        //    _contactmockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<ContactUs>>(),
        //    It.IsAny<FindOptions<ContactUs, ContactUs>>(),
        //     It.IsAny<CancellationToken>())).Returns(_userCursor.Object);

        //    //Mock GetCollection
        //    _mockContext.Setup(c => c.GetCollection<ContactUs>(typeof(ContactUs).Name)).Returns(_contactmockCollection.Object);
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var ContactUsRepo = new ContactUsServices(context);

        //    //Action
        //    var result = await ContactUsRepo.GetAllContactUs();

        //    //Assert 
        //    foreach (ContactUs user in result)
        //    {
        //        Assert.NotNull(user);
        //        break;
        //    }
        //}
        //[Fact]
        //public async void TestFor_UpdateContactUs()
        //{
        //    //mocking
        //    _contactmockCollection.Setup(s => s.UpdateOneAsync(It.IsAny<FilterDefinition<ContactUs>>(), It.IsAny<UpdateDefinition<ContactUs>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()));
        //    _mockContext.Setup(c => c.GetCollection<ContactUs>(typeof(ContactUs).Name)).Returns(_contactmockCollection.Object);

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var contactRepo = new ContactUsServices(context);

        //    //Action
        //    var upadtedContactUs = await contactRepo.UpdateContactUs(_contactUs.ContactUsId);

        //    //Assert
        //    Assert.NotNull(upadtedContactUs);
        //}




        //[Fact]
        //public async Task TestFor_ViewGiftCardOrders()
        //{
        //    //Arrange
        //    //Mock MoveNextAsync
        //    Mock<IAsyncCursor<GiftOrder>> _userCursor = new Mock<IAsyncCursor<GiftOrder>>();
        //    _userCursor.Setup(_ => _.Current).Returns(_orederlist);
        //    _userCursor
        //        .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
        //        .Returns(true)
        //        .Returns(false);

        //    //Mock FindSync
        //    _giftOrdermockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<GiftOrder>>(),
        //    It.IsAny<FindOptions<GiftOrder, GiftOrder>>(),
        //     It.IsAny<CancellationToken>())).Returns(_userCursor.Object);

        //    //Mock GetCollection
        //    _mockContext.Setup(c => c.GetCollection<GiftOrder>(typeof(GiftOrder).Name)).Returns(_giftOrdermockCollection.Object);
        //    _mockOptions.Setup(s => s.Value).Returns(settings);

        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var repository = new ViewOrderServices(context);

        //    //Action
        //    var result = await repository.ViewAllGiftCardOrders();

        //    //Assert 
        //    //loop only first item and assert
        //    foreach (GiftOrder giftOrder in result)
        //    {
        //        Assert.NotNull(giftOrder);
        //        break;
        //    }
        //}

    }
}
