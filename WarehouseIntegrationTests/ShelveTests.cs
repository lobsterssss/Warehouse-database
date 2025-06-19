using InterfacesDal.DTOs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace WarehouseIntegrationTests
{
    [TestClass]
    public sealed class ShelveTests
    {
        public ShelveCollection ShelveCollection;
        public Shelve Shelve;

        [TestInitialize]
        public async Task SetUpTests()
        {
            //Arrange
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();


            SetupShelve(config);

            Shelve = await ShelveCollection.GetShelve(15);
        }


        private void SetupShelve(IConfiguration configuration)
        {
            var dbConnection = new DatabaseConnection(configuration);

            var shelveRepo = new ShelveRepository(dbConnection);
            var productRepo = new ProductRepository(dbConnection);
            ShelveCollection = new ShelveCollection(shelveRepo, productRepo);
        }


        [TestMethod]
        public async Task EditWarehouse_SettingNamePostCodeAndStreet_UpdatesShelve()
        {
            //Arange
            Shelve.Name = "hello";

            //Act
            await Shelve.EditShelve(2);

            //Assert
            Shelve shelve = await ShelveCollection.GetShelve(15);
            Assert.AreEqual("hello", shelve.Name);

            Shelve.Name = "MasterShelve";

            await Shelve.EditShelve(2);

        }

        [TestMethod]
        public async Task GetProducts_GivenShelve15_ReturnsAllProducts()
        {

            //Act
            await Shelve.GetProducts();

            //Assert
            foreach (Product product in Shelve.Products)
            {
                Assert.IsNotNull(product);
                Assert.IsNotNull(product.ID);
                Assert.AreEqual("beans", product.Name);
                Assert.AreEqual("test", product.Description);
                Assert.IsNotNull(product.ProductCode);
                Assert.IsNotNull(product.Amount);
            }

        }
    }
}
