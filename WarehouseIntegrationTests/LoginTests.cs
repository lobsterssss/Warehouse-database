using InterfacesDal;
using InterfacesDal.DTOs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace WarehouseIntegrationTests
{
    [TestClass]
    public sealed class LoginTests
    {
        public Login Login;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

            SetupLoginCollection(config);
        }

        private void SetupLoginCollection(IConfiguration configuration)
        {
            var dbConnection = new DatabaseConnection(configuration);

            LoginRepository LoginRepo = new LoginRepository(dbConnection);
            Login = new Login(LoginRepo);
        }


        [TestMethod]
        public async Task Login_User_GaveUserNameAndPassword_ReturnsUser()
        {
            //Act
            User user = await Login.Login_User("Steve", "test");

            //Assert
            Assert.AreEqual(5, user.ID);
            Assert.AreEqual("Steve", user.Name);
            Assert.AreEqual(Role.Roles.admin, user.Role);

        }
        [TestMethod]
        public async Task Login_User_GaveUserNameAndPassword_ReturnsNothing()
        {
            //Act
            User user = await Login.Login_User("Steve", "pass");

            //Assert
            Assert.AreEqual(null, user);
        }
    }
}
