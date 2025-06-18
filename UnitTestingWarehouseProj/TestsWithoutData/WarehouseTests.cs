using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData
{
    [TestClass]
    public sealed class WarehouseTests
    {
        public Warehouse warehouse;
        public WarehouseTestRespository WarehouseTestRespositoryValues;
        public ShelveTestRespository ShelveTestRespositoryValues;
        public ProductTestRespository ProductTestRespositoryValues;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            SetupRepositories();
            SetupWarehouse();
        }

        private void SetupRepositories()
        {
            WarehouseTestRespositoryValues = new WarehouseTestRespository();
            ShelveTestRespositoryValues = new ShelveTestRespository();
            ProductTestRespositoryValues = new ProductTestRespository();
        }

        private void SetupWarehouse()
        {
            warehouse = new Warehouse(WarehouseTestRespositoryValues, ShelveTestRespositoryValues, ProductTestRespositoryValues)
            {
                ID = 1,
                Name = "warehouse",
                Postcode = "BS1245",
                Street = "TestingStreet"
            };
        }

        [TestMethod]
        public async Task EditWarehouse_GivenPostcodeAndStreet_ReturnsExeption()
        {
            //Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await warehouse.EditWarehouse(null, "12345", "Main Street");
            });
        }

        [TestMethod]
        public async Task GetShelves_GivenWarehouse1_ReturnsNoShelvesFromWarehouse1()
        {
            //Act
            await warehouse.GetShelves();

            //Assert
            Assert.AreEqual(0, warehouse.Shelves.Count());

        }

        [TestMethod]
        public async Task GetProducts_GivenWarehouse1And4Shelves_ReturnsAllProducts()
        {
            //Arrange
            warehouse.Shelves.Add(new Shelve(ShelveTestRespositoryValues, ProductTestRespositoryValues) 
            {
            ID = 1,
            });

            //Act
            await warehouse.GetProducts();

            //Assert
            foreach (Shelve shelve in warehouse.Shelves)
            {
                Assert.AreEqual(0, shelve.Products.Count());

            }
        }
    }
}
