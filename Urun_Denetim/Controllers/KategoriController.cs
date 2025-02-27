using Microsoft.AspNetCore.Mvc;
using Urun_Denetim.Data;
using Urun_Denetim.Models;

namespace Urun_Denetim.Controllers
{
    public class KategoriController : Controller
    {
        private readonly UygulamaDbContext _context;

        public KategoriController(UygulamaDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            var kategori = _context.Kategoris.ToList(); // Veritabanından markaları çekiyoruz
            return PartialView(kategori); // Kategorileri model olarak gönderiyoruz
        }


        //Ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Kategoris.Add(kategori);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            return View(kategori);
        }
    }
}
