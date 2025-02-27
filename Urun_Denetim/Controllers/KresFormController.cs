using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Urun_Denetim.Data;
using Urun_Denetim.Models;
using Urun_Denetim.Models.FormApi;

namespace Urun_Denetim.Controllers
{
    
    [Route("/api/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KresFormController : ControllerBase
    {
        private readonly UygulamaDbContext _context;
        private readonly ILoggerService _loggerService;
        private readonly ILogger<KresFormController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KresFormController(UygulamaDbContext context, ILogger<KresFormController> logger, ILoggerService loggerService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;         
            _logger = logger;
            _loggerService = loggerService;
            _httpContextAccessor = httpContextAccessor;
        }

        //[HttpGet]
        //public IActionResult GetKresForm()
        //{
        //    var kresForms = _context.KresForms.Where(f=> f.Aktif==true).ToList();

        //    return Ok(kresForms);

        //}
        //[HttpGet]
        //public IActionResult GetKresForm()
        //{
        //    var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    _loggerService.Log(kullaniciId, "GET", "/api", "Kres form verileri çekildi.");

        //    var kresForms = _context.KresForms.Where(f => f.Aktif == true).ToList();
        //    return Ok(kresForms);
        //}

        [HttpGet]
        public IActionResult GetKresForm()
        {
            var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ipAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";

            _loggerService.Log(kullaniciId, "GET", "/api", "Kres form verileri çekildi.", ipAdresi);

            var kresForms = _context.KresForms.Where(f => f.Aktif == true).ToList();
            return Ok(kresForms);
        }

        [HttpGet]
        [Route("Getir/{id:int}")]
        public IActionResult GetVatandasById(int id)
        {
            var Formget = _context.KresForms.Find(id);
            if (Formget == null)
            {
                return NotFound();
            }

            return Ok(Formget);
        }


        [HttpGet]
        [Route("Filtre")]
        public IActionResult GetKresFormByFilter([FromQuery] string? isim, [FromQuery] string? soyisim, [FromQuery] string? tcNo)
        {
            var query = _context.KresForms.AsQueryable();
            var filtreler = new List<string>();

            if (!string.IsNullOrEmpty(isim))
            {
                query = query.Where(f => f.isim.Contains(isim));
                filtreler.Add($"İsim: {isim}");
            }
            if (!string.IsNullOrEmpty(soyisim))
            {
                query = query.Where(f => f.soyisim.Contains(soyisim));
                filtreler.Add($"Soyisim: {soyisim}");
            }
            if (!string.IsNullOrEmpty(tcNo))
            {
                query = query.Where(f => f.tc == tcNo);
                filtreler.Add($"TC No: {tcNo}");
            }

            var results = query.ToList();
            var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonim";
            var ipAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var detay = results.Count > 0
                ? $"Arama yapıldı. {string.Join(", ", filtreler)}"
                : $"Arama yapıldı ancak sonuç bulunamadı. {string.Join(", ", filtreler)}";

            _loggerService.Log(kullaniciId, "GET", "/api/Filtre", detay, ipAdresi);

            if (results.Count == 0)
            {
                return NotFound(new { mesaj = "Sonuç bulunamadı", filtreler = filtreler });
            }

            return Ok(results);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostKresForm(KresFormEkleDto kresFormDto)
        {
            //var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ipAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";

            _loggerService.Log("Anonim", "POST", "/api/", $"Yeni kayıt eklendi: {kresFormDto.tc}", ipAdresi);

            if (string.IsNullOrEmpty(kresFormDto.tc))
            {
                return BadRequest("TC Kimlik Numarası zorunludur.");
            }

            var mevcutBasvuru = _context.KresForms.FirstOrDefault(f => f.tc == kresFormDto.tc);
            if (mevcutBasvuru != null)
            {
                return Conflict("Bu TC Kimlik Numarası ile daha önce başvuru yapılmış.");
            }

            var FormEntitiy = new KresForm()
            {
                isim = kresFormDto.isim,
                soyisim = kresFormDto.soyisim,
                telno = kresFormDto.telno,
                dtarihi = kresFormDto.dtarihi,
                tc = kresFormDto.tc,
                ilce = kresFormDto.ilce,
                mahalle = kresFormDto.mahalle
            };

            _context.KresForms.Add(FormEntitiy);
            _context.SaveChanges();

            return Ok(FormEntitiy);
        }



        //[HttpGet]
        //[Route("Filtre")]
        //public IActionResult GetKresFormByFilter([FromQuery] string? isim, [FromQuery] string? soyisim, [FromQuery] string? tcNo)
        //{
        //    var query = _context.KresForms.AsQueryable();
        //    var filtreler = new List<string>();

        //    // Arama kriterlerini loglamak için ekledik
        //    if (!string.IsNullOrEmpty(isim))
        //    {
        //        query = query.Where(f => f.isim.Contains(isim));
        //        filtreler.Add($"İsim: {isim}");
        //    }
        //    else
        //    {
        //        filtreler.Add("İsim: (Boş)");
        //    }

        //    if (!string.IsNullOrEmpty(soyisim))
        //    {
        //        query = query.Where(f => f.soyisim.Contains(soyisim));
        //        filtreler.Add($"Soyisim: {soyisim}");
        //    }
        //    else
        //    {
        //        filtreler.Add("Soyisim: (Boş)");
        //    }

        //    if (!string.IsNullOrEmpty(tcNo))
        //    {
        //        query = query.Where(f => f.tc == tcNo);
        //        filtreler.Add($"TC No: {tcNo}");
        //    }
        //    else
        //    {
        //        filtreler.Add("TC No: (Boş)");
        //    }

        //    var results = query.ToList();
        //    var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonim";
        //    var detay = results.Count > 0
        //        ? $"Arama yapıldı. {string.Join(", ", filtreler)}"
        //        : $"Arama yapıldı ancak sonuç bulunamadı. {string.Join(", ", filtreler)}";

        //    // Log kaydı ekleniyor
        //    var log = new KullaniciLoglari
        //    {
        //        KullaniciId = kullaniciId,
        //        IslemTipi = "GET",
        //        EndPoint = "/api/Filtre",
        //        Tarih = DateTime.Now,
        //        Detay = detay
        //    };

        //    _context.KullaniciLoglaris.Add(log);
        //    _context.SaveChanges();

        //    // Eğer sonuç yoksa 404 Not Found dönelim
        //    if (results.Count == 0)
        //    {
        //        return NotFound(new { mesaj = "Sonuç bulunamadı", filtreler = filtreler });
        //    }

        //    return Ok(results);
        //}



        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult PostKresForm(KresFormEkleDto kresFormDto)
        //{
        //    var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    _loggerService.Log(kullaniciId, "POST", "/api/", $"Yeni kayıt eklendi: {kresFormDto.tc}");

        //    if (string.IsNullOrEmpty(kresFormDto.tc))
        //    {
        //        return BadRequest("TC Kimlik Numarası zorunludur.");
        //    }

        //    var mevcutBasvuru = _context.KresForms.FirstOrDefault(f => f.tc == kresFormDto.tc);
        //    if (mevcutBasvuru != null)
        //    {
        //        return Conflict("Bu TC Kimlik Numarası ile daha önce başvuru yapılmış.");
        //    }

        //    var FormEntitiy = new KresForm()
        //    {
        //        isim = kresFormDto.isim,
        //        soyisim = kresFormDto.soyisim,
        //        telno = kresFormDto.telno,
        //        dtarihi = kresFormDto.dtarihi,
        //        tc = kresFormDto.tc,
        //        ilce = kresFormDto.ilce,
        //        mahalle = kresFormDto.mahalle
        //    };

        //    _context.KresForms.Add(FormEntitiy);
        //    _context.SaveChanges();

        //    return Ok(FormEntitiy);
        //}





        [HttpPut]
        [Route("{id:int}")]
        public IActionResult GuncelleForm(int id, FormGuncelleDto guncelleformDto)
        {
            var formBasvuru = _context.KresForms.Find(id);
            if (formBasvuru is null)
            {
                return NotFound();
            }

            var eskiDegerler = new
            {
                formBasvuru.isim,
                formBasvuru.soyisim,
                formBasvuru.telno,
                formBasvuru.dtarihi,
                formBasvuru.tc,
                formBasvuru.ilce,
                formBasvuru.mahalle
            };

            formBasvuru.isim = guncelleformDto.isim;
            formBasvuru.soyisim = guncelleformDto.soyisim;
            formBasvuru.telno = guncelleformDto.telno;
            formBasvuru.dtarihi = guncelleformDto.dtarihi;
            formBasvuru.tc = guncelleformDto.tc;
            formBasvuru.ilce = guncelleformDto.ilce;
            formBasvuru.mahalle = guncelleformDto.mahalle;

            _context.SaveChanges();

            var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonim";
            var detay = $"Önceki: {System.Text.Json.JsonSerializer.Serialize(eskiDegerler)} | Yeni: {System.Text.Json.JsonSerializer.Serialize(guncelleformDto)}";
            var ipAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            // IP adresini almak
            //var ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Log kaydını eklemek
            var log = new KullaniciLoglari
            {
                KullaniciId = kullaniciId,
                IslemTipi = "PUT",
                EndPoint = $"/api/{id}",
                Detay = detay,
                IpAdresi=ipAdresi
            };


            _context.KullaniciLoglaris.Add(log);
            _context.SaveChanges();

            // Log metodunu ipAdresi parametresiyle çağırma
            _loggerService.Log(kullaniciId, "PUT", $"/api/{id}", detay, ipAdresi);

            return Ok(formBasvuru);
        }



        //[HttpPut("Aktif/{id:int}")]
        //public IActionResult GuncelleAktif(int id)
        //{
        //    var formAktif = _context.KresForms.Find(id);
        //    if (formAktif is null)
        //    {
        //        return NotFound();
        //    }
        //    bool oncekiDurum = formAktif.Aktif;
        //    formAktif.Aktif = !formAktif.Aktif;

        //    _context.SaveChanges();


        //    var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonim";
        //    var detay = $"Önceki Durum: {oncekiDurum}, Yeni Durum: {formAktif.Aktif}";

        //    var log = new KullaniciLoglari
        //    {
        //        KullaniciId = kullaniciId,
        //        IslemTipi = "PUT",
        //        EndPoint = $"/api/Aktif/{id}",
        //        Detay = detay
        //    };

        //    _context.KullaniciLoglaris.Add(log);
        //    _context.SaveChanges();

        //    return Ok(formAktif);
        //}

        [HttpPut("Aktif/{id:int}")]
        public IActionResult GuncelleAktif(int id)
        {
            var formAktif = _context.KresForms.Find(id);
            if (formAktif is null)
            {
                return NotFound();
            }

            bool oncekiDurum = formAktif.Aktif;
            formAktif.Aktif = !formAktif.Aktif;

            _context.SaveChanges();

            var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonim";
            var detay = $"Önceki Durum: {oncekiDurum}, Yeni Durum: {formAktif.Aktif}";
            var ipAdresi = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            // IP adresini almak
            //var ipAdresi = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAdresi))
            {
                ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            // Log kaydını eklemek
            var log = new KullaniciLoglari
            {
                KullaniciId = kullaniciId,
                IslemTipi = "PUT",
                EndPoint = $"/api/Aktif/{id}",
                Detay = detay,
                IpAdresi = ipAdresi
            };

            _context.KullaniciLoglaris.Add(log);
            _context.SaveChanges();

            // Log metodunu ipAdresi parametresiyle çağırma
            _loggerService.Log(kullaniciId, "PUT", $"/api/Aktif/{id}", detay, ipAdresi);

            return Ok(formAktif);
        }




    }
}
