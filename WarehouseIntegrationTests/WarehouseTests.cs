using InterfacesDal.DTOs;
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
            bool TestIntegration = false;
            if (!TestIntegration)
            {
                Assert.Inconclusive("due to not wanting random data in the database this will end the tests");
            }
            //Arrange
            SetupWarehouseCollection();

            Warehouse = await WarehouseCollection.GetWarehouse(1);
        }

        private void SetupWarehouseCollection()
        {
            WarehouseCollection = new WarehouseCollection(new WarehouseRepository(), new ShelveRepository(), new ProductRepository());
        }


        [TestMethod]
        public async Task EditWarehouse_SettingNamePostCodeAndStreet_ReturnsNothing()
        {
            //Arrange

            //Act
            await Warehouse.EditWarehouse("warehouse 3", "BS5421", "Street");

        }
        [TestMethod]
        public async Task GetShelves_GivenWarehouse1_ReturnsAllShelvesWarehouse1()
        {
            //Act
            await Warehouse.GetShelves();

            //Assert
            Assert.AreEqual(4, Warehouse.Shelves.Count());
            foreach(Shelve shelve in Warehouse.Shelves) 
            {
                Assert.AreNotEqual(null, shelve);
                Assert.AreEqual(2, shelve.ID);
                Assert.AreEqual("Shelve 2", shelve.Name);

            }
        }

        [TestMethod]
        public async Task GetProducts_GivenWarehouse1And4Shelves_ReturnsAllProducts()
        {
            //Arrange
            await Warehouse.GetShelves();

            //Act
            await Warehouse.GetProducts();

            //Assert
            foreach (Shelve shelve in Warehouse.Shelves)
            {
                foreach (Product product in shelve.Products)
                {
                    Assert.AreNotEqual(null, product);
                    Assert.AreEqual(2, product.ID);
                    Assert.AreEqual("beans", product.Name);
                    Assert.AreEqual("test", product.Description);
                    Assert.AreEqual("521sgh", product.ProductCode);
                    Assert.AreEqual(26, product.Amount);
                }
            }
        }
    }
}
