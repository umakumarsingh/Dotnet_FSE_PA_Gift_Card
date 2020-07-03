using GiftCards.BusinessLayer;
using GiftCards.BusinessLayer.Services;
using GiftCards.DataLayer;
using GiftCards.Entities;
using GiftCards.Tests.Exceptions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace GiftCards.Tests.TestCases
{
    public class ExceptionalTest
    {
        //write code here
        //mocking Object
        private Mock<IMongoCollection<Gift>> _mockCollection;
        private Mock<IMongoCollection<Buyer>> _buyermockCollection;
        private Mock<IMongoDBContext> _mockContext;
        private Gift _gift;
        private Buyer _buyer;
        private ContactUs _contactUs;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
        static ExceptionalTest()
        {
            if (!File.Exists("../../../../output_exception_revised.txt"))
                try
                {
                    File.Create("../../../../output_exception_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_exception_revised.txt");
                File.Create("../../../../output_exception_revised.txt");
            }
        }

        //creating or mocking dummy object
        public ExceptionalTest()
        {
            _gift = new Gift
            {
               GiftName = "Toys",
               Ammount=500
            };
            _contactUs = new ContactUs
            {
                Name = "John",
                PhoneNumber = 9888076466,
                Email = "jjj@gmail.com",
                Address = "Paris"
            };
            _buyer = new Buyer
            {
                FirstName = "buyers",
                LastName = "BC",
                PhoneNumber = 90876554321,
                Email = "buyers@gmail.com",

                Address = "123456789",

            };
            _mockCollection = new Mock<IMongoCollection<Gift>>();
            _mockCollection.Object.InsertOne(_gift);
            _buyermockCollection = new Mock<IMongoCollection<Buyer>>();
            _buyermockCollection.Object.InsertOne(_buyer);
            _mockContext = new Mock<IMongoDBContext>();
            _mockOptions = new Mock<IOptions<Mongosettings>>();

        }

        //connecting to mongo local host databse
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
            DatabaseName = "guestbook"
        };

        //test for creating new buyer if buyer object is null test to check buyer is null.
        [Fact]
        public async void CreateNewBuyer_Null_Failure()
        {
            // Arrange
            _buyer = null;
            var res = true;
            //Act 
            var bookRepo = new BuyerServices(_mockContext.Object);

            // Assert
            var create = bookRepo.RegisterAsync(_buyer);

            await Assert.ThrowsAsync<ArgumentNullException>(() => create);
            if (create.IsCompletedSuccessfully)
            {
                res = false;
            }
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_exception_revised.txt", "CreateNewBuyer_Null_Failure=" + res + "\n");

        }

        //test for creating new ContactUs if ContactUs object is null test to check _contactUs is null.
        [Fact]
        public async void CreateNewContactUs_Null_Failure()
        {
            // Arrange
            ContactUs _contactUs = null;
            var res = true;

            //Act 
            var bookRepo = new ContactUsServices(_mockContext.Object);
            var contact = bookRepo.ContactUs(_contactUs);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => contact);
            if (contact.IsCompletedSuccessfully)
            {
                res = false;
            }

            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_exception_revised.txt", "CreateNewContactUs_Null_Failure=" + res + "\n");

        }


        //[Fact]
        //public async Task ExceptionTestFor_InValidContactUs()
        //{
        //    //Arrange
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var userRepo = new ContactUsServices(context);
        //    var res = false;
        //    //Action

        //    //Assert
        //    try
        //    {
        //        await Assert.ThrowsAsync<ContactUsAlreadyExist>(() => userRepo.ContactUs(_contactUs));
        //    }
        //    catch(ContactUsAlreadyExist exx)
        //    {
        //        if (exx.Messages == "Already Exist")
        //        {
        //            res = true;
        //            throw (exx);
        //        }
        //    }
        //    File.AppendAllText("../../../../output_Exception_revised.txt", "ExceptionTestFor_InValidContactUs=" + res + "\n");

        //}

        //[Fact]
        //public async Task ExceptionTestFor_InValidBuyerLogin()
        //{

        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var buyerRepo = new BuyerServices(context);
        //    //Action
        //    //Assert
        //    var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => buyerRepo.Login(_buyer));

        //    Assert.Equal("User Not Found", ex.Messages);
        //}
    }
}
