using Moq;
using System.IdentityModel.Tokens.Jwt;
using Tasks.Domain.Developers.Dtos;
using Tasks.Domain.Developers.Repositories;
using Tasks.Domain.Developers.Services;
using Tasks.UnitTests._Common;
using Xunit;

namespace Tasks.UnitTests.Developers.Services
{
    public class AuthServiceTests : BaseTest
    {
        private readonly Mock<IDeveloperRepository> _developerRepository;

        public AuthServiceTests(TasksFixture fixture) : base(fixture) {
            _developerRepository = new Mock<IDeveloperRepository>();
        }

        [Fact]
        public async void LoginWithSuccessTest()
        {
            var password = "senha";
            var developer = EntitiesFactory.NewDeveloper(password).Get();
            var loginDto = new LoginDto { Login = developer.Login, Password = password };
            _developerRepository.Setup(d => d.ExistByLoginAsync(developer.Login)).ReturnsAsync(true);
            _developerRepository.Setup(d => d.FindByEmailAsync(developer.Login)).ReturnsAsync(developer);

            var service = new AuthService(_developerRepository.Object);
            var result = await service.LoginAsync(loginDto);

            var data = result.Data;
            var jwt = new JwtSecurityToken(data.Token);
            Assert.True(result.Success);
            Assert.Equal(developer.Id, data.DeveloperId);
            Assert.Equal(developer.Login, data.Login);
            Assert.NotEmpty(data.Token);
            Assert.NotEmpty(jwt.Claims);
        }

        [Theory]
        [InlineData("")]
        [InlineData("asdfd")]
        [InlineData("32156")]
        public async void LoginWithRejectTest(string password)
        {
            var developer = EntitiesFactory.NewDeveloper("senha").Get();
            var loginDto = new LoginDto { Login = developer.Login, Password = password };
            _developerRepository.Setup(d => d.ExistByLoginAsync(developer.Login)).ReturnsAsync(true);
            _developerRepository.Setup(d => d.FindByEmailAsync(developer.Login)).ReturnsAsync(developer);

            var service = new AuthService(_developerRepository.Object);
            var result = await service.LoginAsync(loginDto);

            var data = result.Data;
            Assert.False(result.Success);
            Assert.Null(data.Token);
        }
    }
}
