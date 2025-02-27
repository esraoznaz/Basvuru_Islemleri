using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Urun_Denetim.Models;
using Urun_Denetim.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
namespace Urun_Denetim.Controllers
{
    [Route("api/Token")]
    [ApiController]
    [Authorize]

    public class TokenController : ControllerBase
    {
        private readonly JwtAyarlari _jwtAyarlari;
        private readonly UygulamaDbContext _context;


        public TokenController(IOptions<JwtAyarlari> jwtAyarlari, UygulamaDbContext context)
        {
            _jwtAyarlari = jwtAyarlari.Value;
            _context = context;

        }
        [AllowAnonymous]
        [HttpPost("Olustur")]
        public IActionResult Giris([FromBody] Admins request)
        {
            var yetki = KimlikDenetimiYap(request);
            if (yetki == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }

            var token = TokenOlustur(yetki);
            return Ok(token);
        }


        private string TokenOlustur(Admins yetki)
        {
            if (_jwtAyarlari.Key == null)
            {
                throw new Exception("Jwt ayarlarınındaki Key değeri null olmaz");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAyarlari.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimDizisi = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,yetki.AdminAd),
                new Claim(ClaimTypes.Role,yetki.AdminYetki!)
            };
            var token = new JwtSecurityToken(_jwtAyarlari.Issuer,
                _jwtAyarlari.Audience,
                claimDizisi,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        private Admins? KimlikDenetimiYap(Admins request)
        {
            return _context.Adminses.FirstOrDefault(x => x.AdminAd.ToLower() == request.AdminAd && x.AdminSifre == request.AdminSifre);
        }


    }
}