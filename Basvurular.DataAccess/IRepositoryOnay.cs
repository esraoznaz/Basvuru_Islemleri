using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
    public interface IRepositoryOnay
    {
        Task<List<KresForm>> GetActiveFormsAsync();
        
        Task<KresForm> UpdateFormStatusAsync(int id, OnayGuncelleDto onayguncelle);
    }

}
