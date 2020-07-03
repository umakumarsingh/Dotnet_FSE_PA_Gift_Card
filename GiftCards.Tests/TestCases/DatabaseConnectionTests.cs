using GiftCards.DataLayer;
using GiftCards.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.IO;
using Xunit;

namespace GiftCards.Tests.TestCases
{
  public  class DatabaseConnectionTests
    {
        //write code here
        //mocking Object
        private Mock<IOptions<Mongosettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        //initilizing new mocking object with constructor
        public DatabaseConnectionTests()
        {
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        //write code here
        //creating test outpt file for saving test result
        static DatabaseConnectionTests()
        {
            if (!File.Exists("../../../../output_database_revised.txt"))
                try
                {
                    File.Create("../../../../output_database_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_database_revised.txt");
                File.Create("../../../../output_database_revised.txt");
            }
        }

        //check mongoDb collction creation and Constructor_Success with mongo settings
        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {
            //Arrange
            var res = false;
            var settings = new Mongosettings()
            {
                Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
                DatabaseName = "guestbook"
            };
            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoDBContext(_mockOptions.Object);
            if (context != null)
            {
                res = true;
            }
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_database_revised.txt", "MongoBookDBContext_Constructor_Success=" + res + "\n");

            //Assert 
            Assert.IsType<MongoDBContext>(context);
            Assert.NotNull(context);
        }

        //get the mongoDb collction by passing collection name
        [Fact]
        public void MongoBookDBContext_GetCollection_ValidName_Success()
        {
            //Arrange
            var res = false;
            var settings = new Mongosettings()
            {
                Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
                DatabaseName = "guestbook"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new MongoDBContext(_mockOptions.Object);
            var myCollection = context.GetCollection<Buyer>("Buyer");
            if (context != null)
            {
                res = true;
            }
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_database_revised.txt", "MongoBookDBContext_GetCollection_ValidName_Success=" + res + "\n");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
