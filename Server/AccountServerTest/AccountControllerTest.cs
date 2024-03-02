using AccountServer.Controllers;
using AccountServer.Service.Contract;
using Moq;

namespace AccountServerTest
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public async Task 계정_생성()
        {
            //Arrange
            var AccountSingupResDto = new AccountSignupResDto();

            var mockService = new Mock<IAccountService>();
            var accountController = new AccountController(mockService.Object);

            //Act

            //Assert
        }
    }
}
