using Basvurular.DataAccess;
using Basvurular.Entities;
using Microsoft.EntityFrameworkCore;
using Basvurular.Entities.DTOs;
using Basvurular.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Business
{
    public class DagitimService 
    {
        private readonly IRepositoryDagitim _repository;

        public DagitimService(IRepositoryDagitim repository)
        {
            _repository = repository;
        }

        // Aktif formları al
        public async Task<List<KresForm>> GetActiveDagitmAsync()
        {
            return await _repository.GetDagitimFormsAsync();
        }

        // Formun durumunu güncelle
        public async Task<KresForm> UpdateDagitimStatusAsync(int id, DagitimGuncelleDto dagitimguncelle)
        {
            return await _repository.UpdateDagitimFormAsync(id, dagitimguncelle);
        }

    }
}
