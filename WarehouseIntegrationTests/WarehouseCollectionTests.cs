using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace WarehouseIntegrationTests
{
    [TestClass]
    public sealed class WarehouseCollectionTests
    {
        public WarehouseCollection WarehouseCollection;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            bool TestIntegration = false;
            if (!TestIntegration) 
            {
                Assert.Inconclusive("due to not wanting random data in the database this will end the tests");
            }
            SetupWarehouseCollection();
        }
        private void SetupWarehouseCollection()
        {
            WarehouseCollection = new WarehouseCollection(new WarehouseRepository(), new ShelveRepository(), new ProductRepository());
        }


        [TestMethod]
        public async Task GetAllWarehouses_BasedOnUser1_ReturnsAllWarehouses()
        {
            //Act
            List<Warehouse> warehouses = await WarehouseCollection.GetAllWarehouses(1).ToListAsync();

            //Assert
            Assert.AreEqual(2 ,warehouses.Count());
            foreach (Warehouse warehouse in warehouses)
            {
                Assert.AreNotEqual(null, warehouse);
                Assert.AreEqual(1, warehouse.ID);
                Assert.AreEqual("warehouse 1", warehouse.Name);
                Assert.AreEqual("2132GS", warehouse.Postcode);
                Assert.AreEqual("street 1", warehouse.Street);
            }
        }

        [TestMethod]
        public async Task GetWarehouse_GivenID1_ReturnsWarehouse1()
        {
            //Act
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(1);

            //Assert
            Assert.AreEqual(1, warehouse.ID);
            Assert.AreEqual("warehouse 1", warehouse.Name);
            Assert.AreEqual("2132GS", warehouse.Postcode);
            Assert.AreEqual("street 1", warehouse.Street);

            foreach (Shelve shelve in warehouse.Shelves)
            {
                Assert.AreNotEqual(null, shelve);
                Assert.AreEqual(2, shelve.ID);
                Assert.AreEqual("Shelve 2", shelve.Name);

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

        [TestMethod]
        public async Task CreateWarehouse_GivenNamePostcodeAndStreet_ReturnsID3()
        {
            //Act
            int WarehouseID = await WarehouseCollection.CreateWarehouse("warehouse 3", "3132GS", "street 3");
            
            //Assert
            Assert.AreEqual(3, WarehouseID);
        }
    }
}
