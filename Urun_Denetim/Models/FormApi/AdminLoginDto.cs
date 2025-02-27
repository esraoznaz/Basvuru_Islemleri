using System.ComponentModel.DataAnnotations;

namespace Urun_Denetim.Models.FormApi
{
    public class AdminLoginDto
    {

        public string AdminAd { get; set; }
       
        public string AdminSifre { get; set; }
    }
}
