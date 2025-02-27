namespace Urun_Denetim.Models
{
    public class Marka
    {
        public int MarkaID { get; set; }
        public string MarkaAd { get; set; }
        public virtual ICollection<Kategori>? Kategoris { get; set; }
        public virtual ICollection<Urunler>? Urunlers { get; set; }

    }
}
