using System;
using Tasks.UnitTests._Common.Factories;
using Xunit;

namespace Tasks.IntegrationTests._Common
{
    public class BaseTest : IClassFixture<TasksFixture>
    {
        protected readonly Uri Uri;
        protected readonly Request Request;
        protected readonly EntitiesFactory EntitiesFactory;
        
        public BaseTest(TasksFixture fixture, string url)
        {
            Request = fixture.Request;
            EntitiesFactory = fixture.EntitiesFactory;
            Uri = new Uri($"{fixture.Client.BaseAddress}{url}");
        }
    }
}
