using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
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
