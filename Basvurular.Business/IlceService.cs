using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.DataAccess;
using Basvurular.Entities;

namespace Basvurular.Business
{
	public class IlceService
	{
		private readonly IilceRepository _repository;

		public IlceService(IilceRepository repository)
		{
			_repository = repository;
		}
		public async Task<IEnumerable<Ilce>> GetAllFormsAsync()
		{
			return await _repository.GetAllAsync();
		}

		//public async Task<Ilce> GetFormByIdAsync(int id)
		//{
		//	return await _repository.GetByIdAsync(id);
		//}


	}
}
