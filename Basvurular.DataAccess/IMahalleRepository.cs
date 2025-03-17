using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.Entities;

namespace Basvurular.DataAccess
{
	public interface IMahalleRepository
	{
		Task<IEnumerable<Mahalle>> GetAllAsyncByIlceId(int ilceId);
		
	}
}
