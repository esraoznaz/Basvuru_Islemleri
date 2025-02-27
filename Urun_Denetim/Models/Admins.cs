using System.ComponentModel.DataAnnotations;

namespace Urun_Denetim.Models
{
    public class Admins
    {
        [Key]
        public int AdminID { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string AdminAd { get; set; }
        [Display(Name = "Kullanıcı Şifre")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string AdminSifre { get; set; }
        public string? AdminYetki { get; set; }

    }
}
