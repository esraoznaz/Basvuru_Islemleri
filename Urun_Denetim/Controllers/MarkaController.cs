using Microsoft.AspNetCore.Mvc;
using Urun_Denetim.Models;
using Urun_Denetim.Data;

namespace Urun_Denetim.Controllers
{
    public class MarkaController : Controller
    {
        private readonly UygulamaDbContext _context;

        public MarkaController(UygulamaDbContext context)
        {
            _context = context;
        }

        // Markaların listelendiği sayfa
        [HttpGet]
        public IActionResult Index()
        {
            var markalar = _context.Markas.ToList(); // Veritabanından markaları çekiyoruz
            return PartialView(markalar); // Markaları model olarak gönderiyoruz
        }


        // Marka ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Marka marka)
        {
            if (ModelState.IsValid)
            {
                _context.Markas.Add(marka);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Liste sayfasına yönlendirir
            }
            return View(marka);
        }
    }
}
