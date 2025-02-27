using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Urun_Denetim.Data;
using Urun_Denetim.Models.FormApi;
using Urun_Denetim.Models;

namespace Urun_Denetim.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public AdminController(UygulamaDbContext dbContext)
        {
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult GetAdmin()
        {
            var Admin = _context.Adminses.ToList();

            return Ok(Admin);

        }

        [HttpPost("login")]
        public IActionResult AdminLogin([FromBody] AdminLoginDto adminLoginDto)
        {
            var admin = _context.Adminses.FirstOrDefault(f => f.AdminAd == adminLoginDto.AdminAd && f.AdminSifre == adminLoginDto.AdminSifre);

            if (admin == null)
            {
                return NotFound("Kullanıcı adı veya şifre hatalı");
            }
            return Ok(admin);
        }


    }
}