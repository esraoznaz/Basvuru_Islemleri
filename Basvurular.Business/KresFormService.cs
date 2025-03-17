using Basvurular.DataAccess;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;

namespace Basvurular.Business
{
    public class KresFormService
    {
        private readonly IRepository _repository;

        public KresFormService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<KresForm>> GetAllFormsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<KresForm> GetFormByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<KresForm>> GetFormsByFilterAsync(string isim, string soyisim, string tcNo)
        {
            return await _repository.GetByFilterAsync(isim, soyisim, tcNo);
        }

        public async Task<KresForm> AddFormAsync(KresFormEkleDto kresFormDto)
        {
            return await _repository.AddAsync(kresFormDto);
        }

        public async Task<KresForm> UpdateFormAsync(int id ,FormGuncelleDto guncelleformDto)
        {
            return await _repository.UpdateAsync(id, guncelleformDto);
        }

        public async Task<KresForm> ToggleAktifAsync(int id)
        {
            return await _repository.ToggleAktifAsync(id);
        }
    }
}
