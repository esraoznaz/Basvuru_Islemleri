using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Basvurular.Entities.DTOs;
using Basvurular.Entities;
using Basvurular.Business;


namespace Urun_Denetim.Controllers
{
    [Route("api/Dagitim")]
    [ApiController]
    [Authorize(Policy = "DagitimOrAdmin")]
    public class DagitimController : ControllerBase
    {
        private readonly DagitimService _service;
        public DagitimController(DagitimService service)
        {
            _service = service;
        }


        //[HttpGet("test-token")]
        //[Authorize]  // Authorization header'ını kontrol et
        //public IActionResult TestToken()
        //{
        //    var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        //    return Ok(new { AuthorizationHeader = authHeader });
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            var forms = await _service.GetActiveDagitmAsync();
            return Ok(forms);
        }



        [HttpPut("Durumu/{id:int}")]
        public async Task<IActionResult> UpdateForm(int id, DagitimGuncelleDto dagitimguncelle)
        {
            // Servisten gelen sonucu alıyoruz
            var updatedForm = await _service.UpdateDagitimStatusAsync(id, dagitimguncelle);

            // Eğer form bulunamadıysa 404 döndür
            if (updatedForm == null)
            {
                return NotFound("Form bulunamadı.");
            }

            // Başarıyla güncellenen formu döndürüyoruz
            return Ok(updatedForm);
        }

    }
}
