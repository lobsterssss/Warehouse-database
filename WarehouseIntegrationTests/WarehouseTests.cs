using InterfacesDal.DTOs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace WarehouseIntegrationTests
{
    [TestClass]
    public sealed class WarehouseTests
    {
        public WarehouseCollection WarehouseCollection;
        public Warehouse Warehouse;

        [TestInitialize]
        public async Task SetUpTests()
        {
            //Arrange
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();
            SetupWarehouseCollection(config);

            Warehouse = await WarehouseCollection.GetWarehouse(1);
        }

        private void SetupWarehouseCollection(IConfiguration configuration)
        {
            var dbConnection = new DatabaseConnection(configuration);

            var warehouseRepo = new WarehouseRepository(dbConnection);
            var shelveRepo = new ShelveRepository(dbConnection);
            var productRepo = new ProductRepository(dbConnection);

            WarehouseCollection = new WarehouseCollection(warehouseRepo, shelveRepo, productRepo);
        }


        [TestMethod]
        public async Task EditWarehouse_SettingNamePostCodeAndStreet_ReturnsNothing()
        {
            //Act
            await Warehouse.EditWarehouse("warehouse 3", "BS5421", "Street");

            //Assert
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(Warehouse.ID);
            Assert.IsNotNull(warehouse);
            Assert.AreEqual("warehouse 3", warehouse.Name);
            Assert.AreEqual("BS5421", warehouse.Postcode);
            Assert.AreEqual("Street", warehouse.Street);

            await Warehouse.EditWarehouse("Warehouse", "1245BS", "Street 04");

        }
        [TestMethod]
        public async Task GetShelves_GivenWarehouse1_ReturnsAllShelvesWarehouse1()
        {
            //Act
            await Warehouse.GetShelves();

            //Assert
            Assert.AreEqual(2, Warehouse.Shelves.Count());
            foreach(Shelve shelve in Warehouse.Shelves) 
            {
                Assert.AreNotEqual(null, shelve);
                Assert.IsNotNull(shelve.ID);
                Assert.AreEqual("Shelve", shelve.Name);

            }
        }

        [TestMethod]
        public async Task GetProducts_GivenWarehouse1And2Shelves_ReturnsAllProducts()
        {
            //Arrange
            await Warehouse.GetShelves();

            //Act
            await Warehouse.GetProducts();

            //Assert
            foreach (Shelve shelve in Warehouse.Shelves)
            {
                Assert.IsNotNull(shelve.Products);
                foreach (Product product in shelve.Products)
                {
                    Assert.IsNotNull(product);
                    Assert.IsNotNull(product.ID);
                    Assert.AreEqual("beans", product.Name);
                    Assert.AreEqual("test", product.Description);
                    Assert.IsNotNull(product.ProductCode);
                    Assert.IsNotNull(product.Amount);
                }
            }
        }
    }
}
