using ControleHoras.API.AppModels;
using ControleHoras.API.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleHoras.API.Services
{
    public class TokenService
    {
        public static TokenService Instance { get; } = new TokenService();

        public string GenerateToken(User user)
        {
            var developer = DesenvolvedorRepository.Instance.ById(user.Id);

            if (developer == null)
                throw new Exception("Desenvolvedor not found");

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = GetEncodedApiSecret();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaimsIdentity(developer),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaimsIdentity(EntityModels.Desenvolvedor developer)
        {
            return new ClaimsIdentity(new Claim[]
                {
                    new Claim(ApiClaimTypes.IdDesenvolvedor, developer.Id.ToString()),
                    new Claim(ApiClaimTypes.Name, developer.Nome)
                });
        }

        private byte[] GetEncodedApiSecret()
        {
            return Encoding.ASCII.GetBytes(App.Configuration["ApiSecret"]);
        }
    }
}
