using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urun_Denetim.Data;
using Urun_Denetim.Models;
using Urun_Denetim.Models.FormApi;




namespace Urun_Denetim.Controllers
{
    [Route("api/Onay")]
    [ApiController]
    [Authorize(Roles = "Onay, Admin")]
    public class OnayController : ControllerBase
    {

        private readonly UygulamaDbContext _context;
        public OnayController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOnay()
        {
            var onay = _context.KresForms
                .Where(f => f.Aktif == true)
                .Select(f => new
                {
                    f.KresFormId,
                    f.isim,
                    f.soyisim,
                    f.telno,
                    f.dtarihi,
                    f.tc,
                    f.ilce,
                    f.mahalle,
                    f.isturu,
                    f.durumu,
                    f.SonucAciklama
                })
                .ToList();

            return Ok(onay);
        }

        [HttpPut]
        [Route("Durum/{id:int}")]
        public IActionResult UpdateDurum(int id, OnayGuncelleDto onayguncelle)
        {
            var form = _context.KresForms.Find(id);
            if (form == null)
            {
                return NotFound();
            }

            form.durumu = onayguncelle.durumu;
            form.SonucAciklama = onayguncelle.SonucAciklama;

            _context.SaveChanges();

            return Ok();
        }






    }
}
