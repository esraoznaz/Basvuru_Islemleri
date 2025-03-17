using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
    public interface ILoggerService
    {
        // Loglama metodu, IP adresini de parametre olarak alacak şekilde güncellendi.
        void Log(string kullaniciId, string islemTipi, string endpoint, string detay, string ipAdresi);
    }
}
