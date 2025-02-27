using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Urun_Denetim.Models
{
    public class KullaniciLoglari
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? KullaniciId { get; set; } // Kullanıcıyı belirlemek için

        [Required]
        [MaxLength(50)]
        public string IslemTipi { get; set; } // GET, POST, PUT, DELETE vb.

        [Required]
        [MaxLength(255)]
        public string EndPoint { get; set; } // Hangi API endpoint kullanıldı

        public DateTime Tarih { get; set; } = DateTime.Now; // İşlem tarihi (default olarak şimdiki zaman)

        public string Detay { get; set; } // Ekstra bilgiler (JSON formatında olabilir)
        public string IpAdresi { get; set; }
    }
}
