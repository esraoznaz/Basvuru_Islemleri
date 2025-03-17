using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
	public class Ilce
	{

		[Key]
		public int ilceId { get; set; }
		public string ilceAdi { get; set; }
		//public virtual ICollection<Mahalle>? Mahalles { get; set; }
	}
}

