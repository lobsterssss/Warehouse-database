using InterfacesDal;
using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithoutData.TestClasses;
using Warehouse_Dal;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData
{
    [TestClass]
    public sealed class LoginTests
    {
        public Login Login;
        public LoginTestRespository LoginRepo;

        [TestInitialize]
        public void SetUpTests()
        {
            //Arrange
            SetupRepositories();
            SetupLoginCollection();
        }

        private void SetupRepositories()
        {
            LoginRepo = new LoginTestRespository();
        }
        private void SetupLoginCollection()
        {
            Login = new Login(LoginRepo);
        }


        [TestMethod]
        public async Task Login_User_GavePassword_ReturnsExeption()
        {
            //Act
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await Login.Login_User(null, "Password");
            });

            //Assert
            Assert.AreEqual("Name", ex.ParamName);
        }

        [TestMethod]
        public async Task Login_User_GaveUserNameAndPassword_ReturnsNothing()
        {
            //Act
            User user = await Login.Login_User("Steve", "Password");

            //Assert
            Assert.AreEqual(null, user);
        }
    }
}
