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
        private Mock<IOptions<Mongosettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public DatabaseConnectionTests()
        {
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }
        //static DatabaseConnectionTests()
        //{
        //    if (!File.Exists("../../../../output_databaseConnection_revised.txt"))
        //        try
        //        {
        //            File.Create("../../../../output_databaseConnection_revised.txt");
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    else
        //    {
        //        File.Delete("../../../../output_businessLogic_revised.txt");
        //        File.Create("../../../../output_businessLogic_revised.txt");
        //    }
        //}
        //[Fact]
        //public void MongoBookDBContext_Constructor_Success()
        //{
        //    var settings = new Mongosettings()
        //    {
        //        Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
        //        DatabaseName = "guestbook"
        //    };
        //    _mockOptions.Setup(s => s.Value).Returns(settings);
        //    _mockClient.Setup(c => c
        //    .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
        //        .Returns(_mockDB.Object);

        //    //Act 
        //    var context = new MongoDBContext(_mockOptions.Object);

        //    //Assert 
        //    Assert.IsType<MongoDBContext>(context);
        //    Assert.NotNull(context);
        //}


        //[Fact]
        //public void MongoBookDBContext_GetCollection_ValidName_Success()
        //{
        //    //Arrange
        //    var settings = new Mongosettings()
        //    {
        //        Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
        //        DatabaseName = "guestbook"
        //    };

        //    _mockOptions.Setup(s => s.Value).Returns(settings);

        //    _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

        //    //Act 
        //    var context = new MongoDBContext(_mockOptions.Object);
        //    var myCollection = context.GetCollection<Buyer>("Buyer");

        //    //Assert 
        //    Assert.NotNull(myCollection);
        //}
    }
}
