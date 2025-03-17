using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
	public class Mahalle
	{
		[Key]
		public int mahalleId { get; set; }
		public string mahalleAdi { get; set; }

		[ForeignKey("ilceId")]
		public int ilceId { get; set; }
		//public virtual Ilce? ilces { get; set; }
	}
}
