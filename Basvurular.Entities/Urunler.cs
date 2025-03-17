using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
    public class Urunler
    {
        [Key]
        public int UrunID { get; set; }
        public string? UrunAciklama { get; set; }
        public int? UrunFiyat { get; set; }
        public int? UrunStok { get; set; }
        [ForeignKey("Kategori")]
        [Display(Name = "Ürün Türü")]
        public int KategoriID { get; set; }
        public virtual Kategori? Kategori { get; set; }
        [ForeignKey("Marka")]
        [Display(Name = "Ürün Markası")]
        public int MarkaID { get; set; }
        public virtual Marka? Marka { get; set; }

    }
}
