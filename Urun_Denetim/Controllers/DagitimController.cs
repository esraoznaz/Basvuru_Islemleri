using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urun_Denetim.Data;
using Urun_Denetim.Models;
using Urun_Denetim.Models.FormApi;
namespace Urun_Denetim.Controllers
{
    [Route("api/Dagitim")]
    [ApiController]
    [Authorize(Roles="Dagitim, Admin")]
    public class DagitimController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public DagitimController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDagitim()
        {
            var dagitim = _context.KresForms
                .Where(f => f.Aktif == true && f.durumu=="Geçti")
                .Select(f => new
                {
                    f.KresFormId,
                    f.isim,
                    f.soyisim,
                    f.tc,
                    f.telno,
                    f.ilce,
                    f.mahalle,
                    f.isturu,
                    f.dagıtım,
                    
                })
                .ToList();

            return Ok(dagitim);
        }


        [HttpPut]
        [Route("Durumu/{id:int}")]
        public IActionResult UpdateDurum(int id, DagitimGuncelleDto dagitimguncelle)
        {
            var form = _context.KresForms.Find(id);
            if (form == null)
            {
                return NotFound();
            }

            form.dagıtım = dagitimguncelle.dagıtım;
            

            _context.SaveChanges();
            return NoContent();
        }

    }
}
