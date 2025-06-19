using InterfacesDal.DTOs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace WarehouseIntegrationTests
{
    [TestClass]
    public sealed class ShelveCollectionTests
    {
        public ShelveCollection ShelveCollection;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

            SetupShelveCollection(config);
        }

        private void SetupShelveCollection(IConfiguration configuration)
        {
            var dbConnection = new DatabaseConnection(configuration);

            var shelveRepo = new ShelveRepository(dbConnection);
            var productRepo = new ProductRepository(dbConnection);
            ShelveCollection = new ShelveCollection(shelveRepo, productRepo);
        }


        [TestMethod]
        public async Task GetAllShelvesFromWarehouse_ForWarehouse2_ReturnsAllShelvesFromWarehouse2()
        {
            //Act
            List<Shelve> shelves = await ShelveCollection.GetAllShelvesFromWarehouse(2).ToListAsync();

            //Assert
            Assert.IsTrue(shelves.Count() >= 2);
            foreach (Shelve shelve in shelves)
            {
                Assert.IsNotNull(shelve);
                Assert.IsNotNull(shelve.ID);
                Assert.IsNotNull(shelve.Name);
            }
        }

        [TestMethod]
        public async Task GetShelve_GivenID15_ReturnsShelve15AndProducts()
        {
            //Act
            Shelve shelve = await ShelveCollection.GetShelve(15);

            //Assert
            Assert.IsNotNull(shelve);
            Assert.AreEqual(15, shelve.ID);
            Assert.AreEqual("MasterShelve", shelve.Name);

            foreach (Product product in shelve.Products)
            {
                Assert.IsNotNull(product);
                Assert.IsNotNull( product.ID);
                Assert.AreEqual("beans", product.Name);
                Assert.AreEqual("test", product.Description);
                Assert.IsNotNull(product.ProductCode);
                Assert.IsNotNull(product.Amount);
            }
        }

        [TestMethod]
        public async Task CreateShelve_GivenName_ReturnsID()
        {
            //Act
            int ShelveID = await ShelveCollection.CreateShelve("Shelve 2", 1);
            
            //Assert
            Assert.IsNotNull(ShelveID);
            Shelve shelve = await ShelveCollection.GetShelve(ShelveID);
            Assert.AreEqual("Shelve 2", shelve.Name);

            await ShelveCollection.DeleteShelve(ShelveID);

        }

        [TestMethod]
        public async Task DeleteWarehouse_GivenID1_ReturnsNothing()
        {
            //Arrange
            int ShelveID = await ShelveCollection.CreateShelve("Shelve 2", 1);

            //Act
            await ShelveCollection.DeleteShelve(ShelveID);

            //Assert
            Shelve shelve = await ShelveCollection.GetShelve(ShelveID);

            Assert.IsNull(shelve);
        }
    }
}
