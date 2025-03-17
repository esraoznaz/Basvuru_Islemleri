using Basvurular.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.Entities.DTOs;

namespace Basvurular.DataAccess
{
    public interface IRepository
    {

        Task<IEnumerable<KresForm>> GetAllAsync();
        Task<KresForm> GetByIdAsync(int id);
        Task<IEnumerable<KresForm>> GetByFilterAsync(string isim, string soyisim, string tcNo);
        Task<KresForm> AddAsync(KresFormEkleDto kresFormDto);
        Task<KresForm> UpdateAsync(int id, FormGuncelleDto guncelleformDto);
        Task<KresForm> ToggleAktifAsync(int id);
        //Task<bool> ToggleAktifAsync(int id);

    }
}
