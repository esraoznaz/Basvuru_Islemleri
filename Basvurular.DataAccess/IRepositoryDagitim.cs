using Basvurular.Entities.DTOs;
using Basvurular.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
    public interface IRepositoryDagitim
    {

        Task<List<KresForm>> GetDagitimFormsAsync();

        Task<KresForm> UpdateDagitimFormAsync(int id, DagitimGuncelleDto dagitimguncelle);
    }
}
