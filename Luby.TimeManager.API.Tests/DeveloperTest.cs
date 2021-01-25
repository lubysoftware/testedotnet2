
using DTO.Request;
using DTO.Pagination;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using DTO.Response;
using System.Linq;

namespace Luby.TimeManager.API.Tests
{
    [TestFixture]
    public class DeveloperTest
    {
        private Context.TestContext _testContext;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            _testContext = new Context.TestContext();
        }

        [Test]
        public async Task AddAsync()
        {
            var requestDTO = new DeveloperRequestDTO
            {

            };

            var request = await _testContext.Client.PostAsJsonAsync($"/Developer", requestDTO);
            _ = await request.Content.ReadAsStringAsync();

            Assert.True(request.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetByIdAsync()
        {

            var requestAll = await _testContext.Client.GetAsync($"/Developer");
            var responseAll = await requestAll.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<PaginatedList<Developer, DeveloperResponseDTO>>(responseAll);

            var request = await _testContext.Client.GetAsync($"/Developer/{obj.Result.FirstOrDefault()?.Id}");
            _ = await request.Content.ReadAsStringAsync();
            Assert.True(request.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetAllPaginatedAsync()
        {
            var request = await _testContext.Client.GetAsync($"/Developer");
            _ = await request.Content.ReadAsStringAsync();
            Assert.True(request.IsSuccessStatusCode);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var request = await _testContext.Client.GetAsync($"/Developer");
            _ = await request.Content.ReadAsStringAsync();
            Assert.True(request.IsSuccessStatusCode);
        }
    }
}