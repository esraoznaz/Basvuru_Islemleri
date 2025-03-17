using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.DataAccess;
using Basvurular.Entities;


namespace Basvurular.Business
{
	public class MahalleService
	{
		private readonly IMahalleRepository _repository;
		public MahalleService(IMahalleRepository repository)
		{
			_repository = repository;
		}
		public async Task<IEnumerable<Mahalle>> GetAllAsyncByIlceId(int ilceId)
		{
			return await _repository.GetAllAsyncByIlceId(ilceId);
		}
	}
}
