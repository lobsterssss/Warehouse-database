using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData
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
            };
        }


        [TestMethod]
        public async Task EditShelve_GivingWarehouseID1_ReturnsNothing()
        {

            //Act
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await Shelve.EditShelve(1);
            });

            //Assert
            Assert.AreEqual("Name", ex.ParamName);

        }

        [TestMethod]
        public async Task GetProducts_GivenWarehouse1And4Shelves_ReturnsAllProducts()
        {
            //Act
            await Shelve.GetProducts();

            //Assert
            Assert.AreEqual(0, Shelve.Products.Count());

        }
    }
}
