using System.Collections.Generic;
using System.Linq;
using Amazon.SimpleDB.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Data.SimpleDB.Tests.Fakes;

namespace Simple.Data.SimpleDB.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        private class Apple
        {
            public string Name { get; set; }
            public string Color { get; set; }
        }

        private FakeAmazonSimpleDB CreateFake()
        {
            return new FakeAmazonSimpleDB
            {
                Domains = 
                {
                    {"Apples", new Dictionary<string, Dictionary<string, string>>
                        {
                            {"1", new Dictionary<string, string>
                                {
                                    {"Color", "Green"}
                                }
                            },
                            {"2", new Dictionary<string, string>
                                {
                                    {"Color", "Red"}
                                }
                            }
                        }
                    }
                }
            };            
        }
        
        [TestMethod]
        public void SelectAllFromDomain()
        {
            var fake = CreateFake();          
            dynamic db = Database.Opener.OpenSimpleDb(fake);
            
            IEnumerable<Apple> apples = db.Apples.All().Cast<Apple>();
            Assert.AreEqual(1, apples.Count());
        }

        [TestMethod]
        public void SelectAllInPieces()
        {
            var fake = CreateFake();
            fake.MaxItems = 1;
            dynamic db = Database.Opener.OpenSimpleDb(fake);

            List<Apple> apples = db.Apples.All().ToList<Apple>();
            Assert.AreEqual(2, apples.Count());
            Assert.AreEqual(3, fake.SelectCount);
        }

        [TestMethod]
        public void InsertIntoDomain()
        {
            var fake = CreateFake();
            dynamic db = Database.Opener.OpenSimpleDb(fake);

            var apple = new Apple()
            {
                Name = "3",
                Color = "Yellow"
            };

            db.Apples.Insert(apple);
        }
    }
}
