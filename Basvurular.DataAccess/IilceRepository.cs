using Basvurular.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
	public interface IilceRepository
	{
		Task<IEnumerable<Ilce>> GetAllAsync();
		//Task<Ilce> GetByIdAsync(int id);

	}
}
