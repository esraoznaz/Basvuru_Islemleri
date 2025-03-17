using Basvurular.Business;
using Basvurular.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basvurular.DataAccess;




namespace Urun_Denetim.Controllers
{
    [Route("api/Onay")]
    [ApiController]
    [Authorize(Policy = "OnayOrAdmin")]
    public class OnayController : ControllerBase
    {

        private readonly OnayService _service;
        public OnayController(OnayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            var forms = await _service.GetActiveOnayAsync();
            return Ok(forms);
        }



        [HttpPut("Durum/{id:int}")]
        public async Task<IActionResult> UpdateForm(int id, OnayGuncelleDto onayguncelle)
        {
            // Servisten gelen sonucu alıyoruz
            var updatedForm = await _service.UpdateOnayStatusAsync(id, onayguncelle);

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
