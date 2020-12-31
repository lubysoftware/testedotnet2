using Tasks.IntegrationTests._Common;
using Xunit;

namespace Tasks.IntegrationTests.Developers
{
    public class LoginDeveloperTest : BaseTest
    {
        public LoginDeveloperTest(TasksFixture fixture) : base(fixture, "/auth/login") { }

        [Fact]
        public void LoginWithSuccess()
        {

        }
    }
}
