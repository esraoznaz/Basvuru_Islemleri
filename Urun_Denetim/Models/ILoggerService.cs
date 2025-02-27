namespace Urun_Denetim.Models
{
    public interface ILoggerService
    {
        // Loglama metodu, IP adresini de parametre olarak alacak şekilde güncellendi.
        void Log(string kullaniciId, string islemTipi, string endpoint, string detay, string ipAdresi);
    }
}
