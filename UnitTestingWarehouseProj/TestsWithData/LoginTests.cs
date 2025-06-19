using InterfacesDal;
using InterfacesDal.DTOs;
using System.IO;
using System.Threading.Tasks;
using UnitTestingWarehouseProj.TestsWithData.TestClasses;
using Warehouse_Dal;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithData
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
        public async Task Login_User_GaveUserNameAndPassword_ReturnsUser()
        {
            //Act
            User user = await Login.Login_User("Steve", "Password");

            //Assert
            Assert.AreEqual(1, user.ID);
            Assert.AreEqual("Steve", user.Name);
            Assert.AreEqual(Role.Roles.worker, user.Role);

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
