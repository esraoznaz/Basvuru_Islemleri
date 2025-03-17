using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Basvurular.DataAccess
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Admins? Authenticate(string adminAd, string adminSifre)
        {
            return _context.Set<Admins>().FirstOrDefault(a => a.AdminAd == adminAd && a.AdminSifre == adminSifre);
        }
    }
}
