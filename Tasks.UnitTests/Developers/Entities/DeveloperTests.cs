using Tasks.Domain._Common.Crypto;
using Tasks.UnitTests._Common;
using Xunit;

namespace Tasks.UnitTests.Developers.Entities
{
    public class DeveloperTests : BaseTest
    {
        public DeveloperTests(TasksFixture fixture) : base(fixture) { }

        [Fact]
        public void HashingPasswordTest()
        {
            var password = "321654";
            var expectedHash = MD5Crypto.Encode(password);

            var developer = EntitiesFactory.NewDeveloper(password).Get();

            Assert.Equal(expectedHash, developer.PasswordHash);
            Assert.NotEqual(password, developer.PasswordHash);
        }

        [Theory]
        [InlineData("123", "123")]
        [InlineData("123", "679875")]
        public void ValidatePasswordTest(string source, string target)
        {
            var developer = EntitiesFactory.NewDeveloper(source).Get();
            var expectedValid = source.Equals(target);

            var valid = developer.ValidatePassword(target);

            Assert.Equal(expectedValid, valid);
        }
    }
}
