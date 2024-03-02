using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Service;
using AccountServer.Utils;
using AutoMapper;
using Moq;

namespace AccountServerTest
{
    [TestClass]
    public class AccountServiceTest
    {
        private readonly Mock<IAccountRepository> _mockRepository;
        private readonly Mock<PasswordEncryptor> _mockPasswordEncryptor;
        private readonly Mock<IMapper> _mockImapper;
        private readonly AccountService _accountService;

        public AccountServiceTest()
        {
            _mockRepository = new Mock<IAccountRepository>();
            _mockPasswordEncryptor = new Mock<PasswordEncryptor>();
            _mockImapper = new Mock<IMapper>();

            _accountService = new AccountService(_mockPasswordEncryptor.Object, _mockRepository.Object, _mockImapper.Object);
        }

        [TestMethod]
        public async Task AddAcount()
        {
            //Arrange
            var reqDto = new AccountSignupReqDto()
            {
                AccountName = "test1234",
                Password = "test1234",
                ConfirmPassword = "test1234",
                Nickname = "testNick",
            };

            Account account = null;

            _mockRepository.Setup(r => r.AddAccount(It.IsAny<Account>()))
                .Callback<Account>(x => account = x);

            //Act
            _accountService.AddAccount(reqDto);
            _mockRepository.Verify(x => x.AddAccount(It.IsAny<Account>()), Times.Once);

            //Assert
            Assert.AreEqual(reqDto.AccountName, account.AccountName);
            Assert.AreEqual(reqDto.Nickname, account.Nickname);

            bool passwordMatch = _mockPasswordEncryptor.Object.IsmatchPassword(reqDto.Password, account.Password);
            Assert.IsTrue(passwordMatch);
        }
    }
}
