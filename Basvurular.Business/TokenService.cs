using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Basvurular.DataAccess;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Basvurular.Business
{
    public class TokenService
    {
        private readonly ITokenRepository _repository;
        private readonly JwtAyarlari _jwtSettings;

        public TokenService(ITokenRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _jwtSettings = new JwtAyarlari
            {
                Key = configuration["Token:Key"],
                Issuer = configuration["Token:Issuer"],
                Audience = configuration["Token:Audience"]
            };


        }



        public string? Authenticate(string adminAd, string adminSifre)
        {
            var admin = _repository.Authenticate(adminAd, adminSifre);
            if (admin == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, admin.AdminAd),
            new Claim(ClaimTypes.Role, admin.AdminYetki!) 
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}
