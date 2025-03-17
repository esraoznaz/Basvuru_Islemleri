using Basvurular.Entities.DTOs;
using Basvurular.Entities;
using Basvurular.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.Business
{
    public class OnayService 
    {
        private readonly IRepositoryOnay _repository;

        public OnayService(IRepositoryOnay repository)
        {
            _repository = repository;
        }

        // Aktif formları al
        public async Task<List<KresForm>> GetActiveOnayAsync()
        {
            return await _repository.GetActiveFormsAsync();
        }

        // Formun durumunu güncelle
        public async Task<KresForm> UpdateOnayStatusAsync(int id, OnayGuncelleDto onayguncelle)
        {
            return await _repository.UpdateFormStatusAsync(id, onayguncelle);
        }
    }
}
