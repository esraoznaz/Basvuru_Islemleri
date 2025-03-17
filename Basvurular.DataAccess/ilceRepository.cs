using Basvurular.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
	public class ilceRepository:IilceRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;
		public ilceRepository(ApplicationDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<IEnumerable<Ilce>> GetAllAsync()
		{
			if (!_memoryCache.TryGetValue("ilceList", out List<Ilce> ilceCache))
			{
				var ilceler = await _context.Ilces.ToListAsync();
				ilceCache = new List<Ilce>(ilceler);
				_memoryCache.Set("ilceList", ilceCache, TimeSpan.FromSeconds(10));
			}

			return ilceCache ;

		}

		//public async Task<Ilce> GetByIdAsync(int id)
		//{
		//	return await _context.Ilces.FindAsync(id);
		//}

	}
}
