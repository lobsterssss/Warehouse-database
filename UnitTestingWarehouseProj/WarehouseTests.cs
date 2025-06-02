using InterfacesDal.DTOs;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestClasses;
using WarehouseBLL;

namespace UnitTestingWarehouseProj
{
    [TestClass]
    public sealed class WarehouseCollectionTests
    {
        [TestMethod]
        public async Task GetAllWarehouses_NoTestData_ReturnsAllWarehouses()
        {
            WarehouseCollection warehouseCollection = new WarehouseCollection(new WarehouseTestDal(), new ShelveTestDal(), new ProductTestDal());

            List<Warehouse> warehouses = await warehouseCollection.GetAllWarehouses().ToListAsync();

            Assert.AreEqual(2 ,warehouses.Count());
            foreach (Warehouse warehouse in warehouses)
            {
                Assert.AreNotEqual(null, warehouse);
                Assert.AreNotEqual(0, warehouse.ID);
                Assert.AreNotEqual(null, warehouse.Name);
                Assert.AreNotEqual(string.Empty, warehouse.Name);
                Assert.AreNotEqual(null, warehouse.Postcode);
                Assert.AreNotEqual(string.Empty, warehouse.Postcode);
                Assert.AreNotEqual(null, warehouse.Street);
                Assert.AreNotEqual(string.Empty, warehouse.Street);
            }
        }
        [TestMethod]
        public async Task GetWarehouse_GivenID2_ReturnsWarehouse2()
        {
            WarehouseCollection warehouseCollection = new WarehouseCollection(new WarehouseTestDal(), new ShelveTestDal(), new ProductTestDal());

            Warehouse warehouse = await warehouseCollection.GetWarehouse(2);

            Assert.AreNotEqual(null, warehouse);
            Assert.AreNotEqual(0, warehouse.ID);
            Assert.AreNotEqual(null, warehouse.Name);
            Assert.AreNotEqual(string.Empty, warehouse.Name);
            Assert.AreNotEqual(null, warehouse.Postcode);
            Assert.AreNotEqual(string.Empty, warehouse.Postcode);
            Assert.AreNotEqual(null, warehouse.Street);
            Assert.AreNotEqual(string.Empty, warehouse.Street);
        }

        [TestMethod]
        public async Task GetWarehouse_GivenID3_ReturnsNull()
        {
            WarehouseCollection warehouseCollection = new WarehouseCollection(new WarehouseTestDal(), new ShelveTestDal(), new ProductTestDal());

            Warehouse warehouse = await warehouseCollection.GetWarehouse(3);

            Assert.AreEqual(null, warehouse);
        }

        [TestMethod]
        public async Task CreateWarehouse_GivenNamePostcodeAndStreet_ReturnsID3()
        {
            WarehouseCollection warehouseCollection = new WarehouseCollection(new WarehouseTestDal(), new ShelveTestDal(), new ProductTestDal());

            int WarehouseID = await warehouseCollection.CreateWarehouse("warehouse 3", "3132GS", "street 3");

            Assert.AreEqual(3, WarehouseID);
            Warehouse warehouse = await warehouseCollection.GetWarehouse(3);
            Assert.AreNotEqual(null, warehouse);
            Assert.AreEqual("warehouse 3", warehouse.Name);
            Assert.AreEqual("3132GS", warehouse.Postcode);
            Assert.AreEqual("street 3", warehouse.Street);

        }
    }
}
