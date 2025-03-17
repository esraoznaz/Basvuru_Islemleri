using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

using Basvurular.Entities.DTOs;
using Basvurular.DataAccess;
using Basvurular.Entities;
using Basvurular.Business;
using Urun_Denetim.Models;

namespace Urun_Denetim.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task <IActionResult> GetAdmin()
        {
            var forms = await _service.GetAllAsync();
            return Ok(forms);


        }

        [HttpPost("login")]
        public async Task <IActionResult> AdminLogin([FromBody] AdminLoginDto adminLoginDto)
        {
            var admin = await _service.AdminLoginAsync(adminLoginDto);

            if (admin == null)
            {
                return NotFound("Kullanıcı adı veya şifre hatalı");
            }
            return Ok(admin);
        }


    }
}