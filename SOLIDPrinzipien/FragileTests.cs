using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;

namespace SOLIDPrinzipien
{
    public class FavoriteList
    {
        public int Id { get; set; }
        public List<int> Items { get; set; }
    }

    // Diskussion:
    // Erfüllt die Klasse das SRP?
    // Ist die Klasse kohäsiv?
    // Ist diese Klasse von ihren Abhängigkeiten entkoppelt?
    public class CustomerService
    {
        private readonly IDb _db;

        public CustomerService(IDb db)
        {
            _db = db;
        }

        public int CalculateCustomerDiscount(int x, int y)
        {
            return (int)Math.Pow(x + y, 2) - (x - y);
        }

        public void AddItemsToFavoriteList(int[] ids, int favoriteId)
        {
            var favoriteList = _db.GetById<FavoriteList>(favoriteId);
            favoriteList.Items.AddRange(ids);
            _db.Save(favoriteList);
        }

        //public void SendCustomerMail(string content, string customer)
        //{
        //    var smtpServerAdress = _config.GetSmtpServerAdress();
        //    _smtpClient.SendMail(smtpServerAdress, "Comparex", customer, content);
        //}
    }

    // Diskussion
    // Was fällt beim Hinzufügen von neuen Funktionen auf?
    // Wie kann man das Problem minimieren?
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_CalculateCustomerDiscount()
        {
            var IDb = A.Fake<IDb>();
            var customerService = new CustomerService(IDb);

            var result = customerService.CalculateCustomerDiscount(4, 4);

            Assert.AreEqual(64, result);
        }

        //[Test]
        //public void Test_AddItemsToFavoriteList()
        //{
        //    var db = A.Fake<IDb>();

        //    var customerService = new CustomerService(db);

        //    var favoriteList = new FavoriteList {Id = 1, Items = new List<int>()};

        //    A.CallTo(() => db.GetById<FavoriteList>(1))
        //        .Returns(favoriteList);

        //    var itemIds = new[] { 1, 2, 3, 4 };
        //    customerService.AddItemsToFavoriteList(itemIds, 1);
        //    A.CallTo(() => db.Save(favoriteList)).MustHaveHappened(Repeated.Exactly.Once);
        //    Assert.True(itemIds.SequenceEqual(favoriteList.Items));
        //}
    }


    public interface IConfig
    {
        string GetSmtpServerAdress();
    }

    public class Config : IConfig
    {
        public string GetSmtpServerAdress()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISmtpClient
    {
        void SendMail(string serverAdress, string from, string to, string content);
    }

    public class SmtpClient : ISmtpClient
    {
        public void SendMail(string serverAdress, string @from, string to, string content)
        {
            throw new NotImplementedException();
        }
    }
}