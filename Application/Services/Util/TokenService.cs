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

namespace Application.Services.Util
{
    public class TokenService : ITokenService
    {
        private readonly byte[] _appSecret;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration config, ILogger<TokenService> logger)
        {
            _logger = logger;
            _appSecret = Encoding.ASCII.GetBytes(config["AppSecret"]);
        }

        public string GenerateToken(Developer developer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, developer.Name),
                    new Claim("Email", developer.Email),
                    new Claim("DeveloperId", developer.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_appSecret), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogInformation("[invalid token]: {0}", e);
                return false;
            }
        }

        public JwtSecurityToken DecodeToken(string authToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(authToken);
                return token;
            }
            catch (Exception e)
            {
                _logger.LogInformation("[invalid token]: {0}", e);
                return null;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Validate expiration in the generated token
                ValidateAudience = false, // Validate audiance in the generated token
                ValidateIssuer = false,   // Validate issuer in the generated token
                //ValidIssuer = "Sample",
                //ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(_appSecret) // The same key as the one that generate the token
            };
        }
    }
}
