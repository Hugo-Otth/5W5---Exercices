using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntraFormatif.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraFormatif.Data;
using Microsoft.EntityFrameworkCore;
using IntraFormatif.Models;

namespace IntraFormatif.Services.Tests
{
    [TestClass()]
    public class BillServiceTests
    {
        DbContextOptions<ApplicationDbContext> options;
        public BillServiceTests()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BillService")
                .Options;
        }


        [TestMethod()]
        public void CreateBillNameTest()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
            BillService service = new BillService(db);
            Item i1 = new Item()
            {
                Id = 1,
                Name = "item 1",
                Price = 20
            };
            Item i2 = new Item()
            {
                Id = 2,
                Name = "item 2",
                Price = 10
            };
            Item i3 = new Item()
            {
                Id = 3,
                Name = "item 3",
                Price = 30
            };
            List<Item> liste = new List<Item>() { i1, i2, i3 };
            IEnumerable<Item> list = liste;

            Assert.ThrowsException<Exception>(() => service.CreateBill(null, list));
        }


        [TestMethod()]
        public void CreateBillItemCountTest()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
            BillService service = new BillService(db);
            List<Item> liste = new List<Item>() {};
            IEnumerable<Item> list = liste;
            var result = service.CreateBill("bill 1", list);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CreateBillItemPriceNegativeTest()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
            BillService service = new BillService(db);
            Item i1 = new Item()
            {
                Id = 1,
                Name = "item 1",
                Price = -20
            };
            Item i2 = new Item()
            {
                Id = 2,
                Name = "item 2",
                Price = 10
            };
            Item i3 = new Item()
            {
                Id = 3,
                Name = "item 3",
                Price = 30
            };
            List<Item> liste = new List<Item>() { i1, i2, i3 };
            IEnumerable<Item> list = liste;
            AreYouInsaneException e = Assert.ThrowsException<AreYouInsaneException>(() => service.CreateBill("bill 2", list));
            Assert.AreEqual("Not paying for you to take something!", e.Message);
        }


        [TestMethod()]
        public void CreateBillItemPriceZeroTest()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
            BillService service = new BillService(db);
            Item i1 = new Item()
            {
                Id = 1,
                Name = "item 1",
                Price = 0
            };
            Item i2 = new Item()
            {
                Id = 2,
                Name = "item 2",
                Price = 10
            };
            Item i3 = new Item()
            {
                Id = 3,
                Name = "item 3",
                Price = 30
            };
            List<Item> liste = new List<Item>() { i1, i2, i3 };
            IEnumerable<Item> list = liste;
            AreYouInsaneException e = Assert.ThrowsException<AreYouInsaneException>(() => service.CreateBill("bill 2", list));
            Assert.AreEqual("Not giving free stuff either!!", e.Message);
        }
    }
}
