using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Urun_Denetim.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        public string KategoriAd { get; set; }
        [ForeignKey("Marka")]
        public int? MarkaID { get; set; }

        public virtual Marka? Marka { get; set; }
        public virtual ICollection<Urunler>? Urunler { get; set; }

    }
}
