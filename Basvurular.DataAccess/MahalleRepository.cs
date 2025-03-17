using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Basvurular.DataAccess
{
	public class MahalleRepository: IMahalleRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;
		public MahalleRepository(ApplicationDbContext context, IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}
		public async Task<IEnumerable<Mahalle>> GetAllAsyncByIlceId(int ilceId)
		{
			//return await Task.FromResult(_context.Mahalles.Where(x => x.ilceId == ilceId).ToList());

			if (!_memoryCache.TryGetValue("mahalleList", out List<Mahalle> mahalleCache))
			{
				var mahalleler = await _context.Mahalles.Where(x => x.ilceId == ilceId).ToListAsync();
				mahalleCache = new List<Mahalle>(mahalleler);
				_memoryCache.Set("mahalleList", mahalleCache, TimeSpan.FromSeconds(10));
			}

			return mahalleCache;
		}
	}
}
