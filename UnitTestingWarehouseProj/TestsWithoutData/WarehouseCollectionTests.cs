using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData
{
    [TestClass]
    public sealed class WarehouseCollectionTests
    {
        public WarehouseCollection WarehouseCollection;
        public WarehouseTestRespository WarehouseTestRespositoryValues;
        public ShelveTestRespository ShelveTestRespositoryValues;
        public ProductTestRespository ProductTestRespositoryValues;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            SetupRepositories();
            SetupWarehouseCollection();
        }

        private void SetupRepositories()
        {
            WarehouseTestRespositoryValues = new WarehouseTestRespository();
            ShelveTestRespositoryValues = new ShelveTestRespository();
            ProductTestRespositoryValues = new ProductTestRespository();
        }
        private void SetupWarehouseCollection()
        {
            WarehouseCollection = new WarehouseCollection(WarehouseTestRespositoryValues, ShelveTestRespositoryValues, ProductTestRespositoryValues);
        }


        [TestMethod]
        public async Task GetAllWarehouses_asUser1_ReturnsNothing()
        {
            //Act
            List<Warehouse> warehouses = await WarehouseCollection.GetAllWarehouses(1).ToListAsync();

            //Assert
            Assert.AreEqual(0 ,warehouses.Count());
        }

        [TestMethod]
        public async Task GetWarehouse_GivenID1_ReturnsNothing()
        {
            //Act
            Warehouse warehouse = await WarehouseCollection.GetWarehouse(1);

            //Assert
            Assert.AreEqual(null, warehouse);
        }

        [TestMethod]
        public async Task CreateWarehouse_GivenPostcodeAndStreet_ReturnsExeption()
        {
            //Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await WarehouseCollection.CreateWarehouse(null, "12345", "Main Street");
            });

            Assert.AreEqual("name", ex.ParamName);
        }
    }
}
