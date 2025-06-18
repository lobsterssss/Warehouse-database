using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithData.TestClasses;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithData
{
    [TestClass]
    public sealed class ShelveCollectionTests
    {
        public ShelveCollection ShelveCollection;
        public ShelveTestRespository ShelveTestRespositoryValues;
        public ProductTestRespository ProductTestRespositoryValues;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            SetupRepositories();
            SetupShelveCollection();
        }

        private void SetupRepositories()
        {
            ShelveTestRespositoryValues = new ShelveTestRespository();
            ProductTestRespositoryValues = new ProductTestRespository();
        }
        private void SetupShelveCollection()
        {
            ShelveCollection = new ShelveCollection(ShelveTestRespositoryValues, ProductTestRespositoryValues);
        }


        [TestMethod]
        public async Task GetAllShelvesFromWarehouse_ForWarehouse1_ReturnsAllShelvesFromWarehouse1()
        {
            //Act
            List<Shelve> shelves = await ShelveCollection.GetAllShelvesFromWarehouse(1).ToListAsync();

            //Assert
            Assert.AreEqual(4 , shelves.Count());
            foreach (Shelve shelve in shelves)
            {
                Assert.AreNotEqual(null, shelve);
                Assert.AreEqual(2, shelve.ID);
                Assert.AreEqual("Shelve 2", shelve.Name);
            }
        }

        [TestMethod]
        public async Task GetShelve_GivenID1_ReturnsShelve1()
        {
            //Act
            Shelve shelve = await ShelveCollection.GetShelve(1);

            //Assert
            Assert.AreNotEqual(null, shelve);
            Assert.AreEqual(1, shelve.ID);
            Assert.AreEqual("Shelve 1", shelve.Name);

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

        [TestMethod]
        public async Task CreateShelve_GivenName_ReturnsID3()
        {
            //Act
            int ShelveID = await ShelveCollection.CreateShelve("Shelve 2", 1);
            
            //Assert
            Assert.AreEqual(3, ShelveID);
        }

        [TestMethod]
        public async Task DeleteWarehouse_GivenID1_ReturnsID1()
        {
            //Act
            await ShelveCollection.DeleteShelve(1);

            //Assert
            Assert.AreEqual(1, ShelveTestRespositoryValues.LastDeletedValue);
        }
    }
}
