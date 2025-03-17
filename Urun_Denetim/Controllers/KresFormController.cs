using Basvurular.Business;
using Basvurular.DataAccess;
using Basvurular.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Basvurular.Entities.DTOs;
using Urun_Denetim.Models;



namespace Urun_Denetim.Controllers
{
    [Route("/api/")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class KresFormController : ControllerBase
    {
        private readonly KresFormService _service;

        public KresFormController(KresFormService service)
        {
            _service = service;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            var forms = await _service.GetAllFormsAsync();
            return Ok(forms);
        }

        [HttpGet("Getir/{id:int}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            var form = await _service.GetFormByIdAsync(id);
            if (form == null) return NotFound();
            return Ok(form);
        }

        [HttpGet("Filtre")]
        public async Task<IActionResult> GetByFilterAsync(string? isim, string? soyisim, string? tcNo)
        {
            var result = await _service.GetFormsByFilterAsync(isim, soyisim, tcNo);

            if (!result.Any()) // Eğer sonuç bulunmazsa
            {
                return NotFound(new { mesaj = "Sonuç bulunamadı" }); // 404 döndür
            }

            return Ok(result); // 200 OK ile sonuçları döndür
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddForm(KresFormEkleDto kresFormDto)
        {
            var addedForm = await _service.AddFormAsync(kresFormDto);
            return Ok(addedForm);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateForm(int id, FormGuncelleDto guncelleformDto)
        {
            var updatedForm = await _service.UpdateFormAsync(id, guncelleformDto);

            return Ok(updatedForm);
        }

        
        [HttpPut("Aktif/{id:int}")]
        public async Task<IActionResult> ToggleAktif(int id)
        {
            var result = await _service.ToggleAktifAsync(id);

            // Eğer result null ise (aslında null dönmemeli çünkü KeyNotFoundException fırlatılıyor)
            if (result == null)
            {
                return NotFound(); // Bulunamadı
            }

            // Güncelleme başarılıysa
            return Ok(result);
        }
    }
}
