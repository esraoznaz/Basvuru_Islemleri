using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;  // IP adresini almak için
using System;
using Basvurular.DataAccess;

namespace Basvurular.Entities
{
    public class LoggerService : ILoggerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Konstruktoru güncelle
        public LoggerService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Log(string kullaniciId, string islemTipi, string endpoint, string detay, string ipAdresi)
        {
            // Eğer IP adresi verilmemişse, HTTP bağlamından alın
            ipAdresi ??= _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";

            // Log kaydını oluştur
            var log = new KullaniciLoglari
            {
                KullaniciId = kullaniciId,
                IslemTipi = islemTipi,
                EndPoint = endpoint,
                Detay = detay,
                Tarih = DateTime.Now,
                IpAdresi = ipAdresi // IP adresini kaydet
            };

            // Log kaydını veritabanına ekle
            _context.KullaniciLoglaris.Add(log);
            _context.SaveChanges();
        }
    }
}
