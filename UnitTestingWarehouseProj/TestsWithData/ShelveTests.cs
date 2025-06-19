using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithData.TestClasses;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithData
{
    [TestClass]
    public sealed class ShelveTests
    {
        public Shelve Shelve;
        public ShelveTestRespository ShelveTestRespositoryValues;
        public ProductTestRespository ProductTestRespositoryValues;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            SetupRepositories();
            SetupShelve();
        }

        private void SetupRepositories()
        {
            ShelveTestRespositoryValues = new ShelveTestRespository();
            ProductTestRespositoryValues = new ProductTestRespository();
        }

        private void SetupShelve()
        {
            Shelve = new Shelve(ShelveTestRespositoryValues, ProductTestRespositoryValues)
            {
                ID = 1,
                Name = "Shelve 1",
            };
        }


        [TestMethod]
        public async Task EditWarehouse_SettingNamePostCodeAndStreet_ReturnsNothing()
        {
            //Arange
            Shelve.Name = "hello";

            //Act
            await Shelve.EditShelve(1);

            //Assert
            Assert.AreEqual("hello", Shelve.Name);
            Assert.AreEqual("hello", ShelveTestRespositoryValues.LastGivenDto.Name);
            Assert.AreEqual(1, ShelveTestRespositoryValues.WarehouseId);
        }

        [TestMethod]
        public async Task GetProducts_GivenWarehouse1And4Shelves_ReturnsAllProducts()
        {

            //Act
            await Shelve.GetProducts();

            //Assert
            foreach (Product product in Shelve.Products)
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
