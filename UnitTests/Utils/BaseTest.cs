using Brazil.Data;
using TesteDotnet.Data;

namespace UnitTests.Utils
{
    public class BaseTest
    {
        public static readonly CpfGenerator _cpfGenerator = new CpfGenerator();
        public Factory Factory { get; private set; }
        public Context Context { get; private set; }

        public BaseTest(DatabaseFixture fixture)
        {
            Context = fixture.Context;
            Factory = fixture.Factory;
            // ConfigureCurrentRequestAsync();
        }

        //private async void ConfigureCurrentRequestAsync()
        //{
        //    var developer = await Factory.CreateDeveloper();
        //    Context.Developer.Add(developer);
        //}
    }
}
