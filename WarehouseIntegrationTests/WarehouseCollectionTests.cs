using InterfacesDal.DTOs;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
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
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

            SetupWarehouseCollection(config);
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
        public async Task GetAllWarehouses_BasedOnUser1_ReturnsAllWarehouses()
        {
            //Act
            List<Warehouse> warehouses = await WarehouseCollection.GetAllWarehouses(5).ToListAsync();

            //Assert
            Assert.IsTrue(warehouses.Count() >= 2);
            foreach (Warehouse warehouse in warehouses)
            {
                Assert.IsNotNull( warehouse);
                //Assert.AreNotEqual(null, warehouse.ID);
                Assert.IsNotNull( warehouse.Name);
                Assert.IsNotNull( warehouse.Postcode);
                Assert.IsNotNull( warehouse.Street);
            }
        }

        [TestMethod]
        public async Task GetWarehouse_GivenID1_ReturnsWarehouse1()
        {
            //Act
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(1);

            //Assert
            Assert.AreNotEqual(null, warehouse);
            Assert.AreNotEqual(null, warehouse.Name);
            Assert.AreNotEqual(null, warehouse.Postcode);
            Assert.AreNotEqual(null, warehouse.Street);
            //Assert.AreEqual(null, warehouse.Shelves);


        }

        [TestMethod]
        public async Task CreateWarehouse_GivenNamePostcodeAndStreet_ReturnsID3()
        {
            //Act
            int WarehouseID = await WarehouseCollection.CreateWarehouse("new test warehouse", "3132GS", "street 3");

            //Assert
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(WarehouseID);

            Assert.AreEqual("new test warehouse", warehouse.Name);
            Assert.AreEqual("3132GS", warehouse.Postcode);
            Assert.AreEqual("street 3", warehouse.Street);

            WarehouseCollection.DeleteWarehouse(WarehouseID);
        }

        [TestMethod]
        public async Task DeleteWarehouse_GivenID1_ReturnsNothing()
        {
            //Arrange
            int WarehouseID = await WarehouseCollection.CreateWarehouse("new test warehouse", "3132GS", "street 3");

            //Act
            await WarehouseCollection.DeleteWarehouse(WarehouseID);

            //Assert
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(WarehouseID);

            Assert.IsNull(warehouse);
        }
    }
}
