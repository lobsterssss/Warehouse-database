using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestClasses;
using WarehouseBLL;

namespace UnitTestingWarehouseProj
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
        public async Task EditWarehouse_SettingNamePostCodeAndStreet_ReturnsNothing()
        {
            //Act
            await warehouse.EditWarehouse("warehouse 3", "BS5421", "Street");

            //Assert
            Assert.AreEqual("warehouse 3", WarehouseTestRespositoryValues.LastUpdatedDto.Name);
            Assert.AreEqual("BS5421", WarehouseTestRespositoryValues.LastUpdatedDto.Postcode);
            Assert.AreEqual("Street", WarehouseTestRespositoryValues.LastUpdatedDto.Street);
        }
        [TestMethod]
        public async Task GetShelves_GivenWarehouse1_ReturnsAllShelvesWarehouse1()
        {
            //Act
            await warehouse.GetShelves();

            //Assert
            Assert.AreEqual(4, warehouse.Shelves.Count());
            foreach(Shelve shelve in warehouse.Shelves) 
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
            await warehouse.GetShelves();

            //Act
            await warehouse.GetProducts();

            //Assert
            foreach (Shelve shelve in warehouse.Shelves)
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
