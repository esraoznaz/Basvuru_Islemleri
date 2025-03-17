using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
    public class Marka
    {
        public int MarkaID { get; set; }
        public string MarkaAd { get; set; }
        public virtual ICollection<Kategori>? Kategoris { get; set; }
        public virtual ICollection<Urunler>? Urunlers { get; set; }

    }
}
