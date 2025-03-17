using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Urun_Denetim.Models;
using Basvurular.DataAccess;
using Basvurular.Business;
using Basvurular.Entities;
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
        private readonly TokenService _service;

        public TokenController(TokenService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("Olustur")]
        public IActionResult Login([FromBody] Admins admin)
        {
            if (admin == null || string.IsNullOrWhiteSpace(admin.AdminAd) || string.IsNullOrWhiteSpace(admin.AdminSifre))
            {
                return BadRequest("Kullanıcı adı ve şifre gereklidir.");
            }

            var token = _service.Authenticate(admin.AdminAd, admin.AdminSifre);
            if (token == null)
            {
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            //return Ok(new { Token = token });
            return Ok(token);
        }

    }
}