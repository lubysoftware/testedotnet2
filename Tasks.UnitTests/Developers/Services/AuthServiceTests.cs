using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Tasks.Domain._Common.Security;
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
        private readonly TokenConfiguration _tokenConfiguration;

        public AuthServiceTests(TasksFixture fixture) : base(fixture) {
            _developerRepository = new Mock<IDeveloperRepository>();
            _tokenConfiguration = new TokenConfiguration { 
                Issuer = "Issuer",
                Signature = "uY76$657gD7D0YrsF%d8g7",
                Seconds = 60
            };
        }

        [Fact]
        public async void LoginWithSuccessTest()
        {
            var password = "senha";
            var developer = EntitiesFactory.NewDeveloper(password).Get();
            var loginDto = new LoginDto { Login = developer.Login, Password = password };
            _developerRepository.Setup(d => d.ExistByLoginAsync(developer.Login)).ReturnsAsync(true);
            _developerRepository.Setup(d => d.FindByLoginAsync(developer.Login)).ReturnsAsync(developer);

            var service = new AuthService(_developerRepository.Object, _tokenConfiguration);
            var result = await service.LoginAsync(loginDto);

            var data = result.Data;
            Assert.True(result.Success);
            Assert.Equal(developer.Id, data.Id);
            Assert.Equal(developer.Login, data.Login);
            Assert.Equal(TimeSpan.FromSeconds(_tokenConfiguration.Seconds), data.ExpiresAt - data.CreatedAt);
            Assert.NotEmpty(data.Token);
            Assert.StartsWith("Bearer", data.Token);
            var jwt = new JwtSecurityToken(data.Token.Split("Bearer").Last());
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
            _developerRepository.Setup(d => d.FindByLoginAsync(developer.Login)).ReturnsAsync(developer);

            var service = new AuthService(_developerRepository.Object, _tokenConfiguration);
            var result = await service.LoginAsync(loginDto);

            Assert.False(result.Success);
            Assert.Null(result.Data);
        }
    }
}
