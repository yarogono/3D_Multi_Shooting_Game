using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Service;
using Microsoft.AspNetCore.Authentication;
using Moq;
using System.Security.Claims;

namespace AccountServerTest.Service
{
    public class OauthServiceTest
    {

        [Fact(DisplayName = "구글 로그인")]
        public void GoogleLogin()
        {
            // Arrange
            var succedMessage = "Google Login";

            string testName = "Test user";
            string testEmail = "TestEmail@naver.com";

            var claims = new[] { new Claim(ClaimTypes.Name, testName), new Claim(ClaimTypes.Email, testEmail) };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "TestScheme");

            var mockResult = AuthenticateResult.Success(ticket);

            var mockOauthRepository = new Mock<IOauthRepository>();
            var mockAccountRepository = new Mock<IAccountRepository>();

            var oauthService = new OauthService(mockOauthRepository.Object, mockAccountRepository.Object);

            // Act
            var result = oauthService.GoogleLogin(mockResult, null);

            // Assert
            Assert.Equal(succedMessage, result.Message);
            Assert.Equal(testName, result.Data.name);
            Assert.Equal(testEmail, result.Data.email);
        }
    }
}
