using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using DTO.Request;
using DTO.Response;
using AutoMapper;
using Infrastructure.Interfaces.Repositories.Domain; 
 using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using System.Linq;
using Application.Interfaces.Services.Util;
using System.Net.Http;

namespace Application.Services.Util
{
    public class HttpClientService : IHttpClientService
    {
        private readonly byte[] _appSecret;
        private readonly ILogger<HttpClientService> _logger;
        private HttpClient Client { get; set; } = new HttpClient();

        public HttpClientService(IConfiguration config, ILogger<HttpClientService> logger)
        {
            _logger = logger;
            _appSecret = Encoding.ASCII.GetBytes(config["AppSecret"]);
        }

        public async Task<string> GetAsync(string RequestUri)
        {
            var requestAll = await Client.GetAsync(RequestUri);
            var responseAll = await requestAll.Content.ReadAsStringAsync();
            return responseAll;
        }
    }
}
