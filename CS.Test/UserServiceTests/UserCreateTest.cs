using AutoMapper;
using CS.BL.Interfaces;
using CS.BL.Services;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Moq;
using NUnit.Framework.Constraints;
using System.Threading;

namespace CS.Test.UserTests
{
    public class UserCreateTest
    {
        private IUserService _userService;
        private Mock<ApplicationContext> _mockContext;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {
            _mockContext = new Mock<ApplicationContext>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockContext.Object, _mockMapper.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _userService = null;
        }

        [Test]
        public async Task CreateUserSuccess()
        {
            // Arrange

            var userDto = new UserSignUpDto()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                RoleName = "User",
            };

            var expectedUser = new User();

            _mockMapper.Setup(t => t.Map<User>(userDto))
                .Returns(expectedUser);

            // Act
            var result = _userService.Create(userDto);

            // Assert
            _mockMapper.Verify(x => x.Map<User>(userDto), Times.Once);
            _mockContext.Verify(x => x.AddAsync(
                expectedUser, 
                CancellationToken.None), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result.Result, Is.EqualTo(true));
        }

    }
}
