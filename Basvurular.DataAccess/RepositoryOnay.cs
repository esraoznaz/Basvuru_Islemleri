using Basvurular.Entities.DTOs;
using Basvurular.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Basvurular.DataAccess
{
    public class RepositoryOnay : IRepositoryOnay
    {
        private readonly ApplicationDbContext _context;

        public RepositoryOnay(ApplicationDbContext context)
        {
            _context = context;
        }

        // Aktif form verilerini al
        public async Task<List<KresForm>> GetActiveFormsAsync()
        {
            return await _context.KresForms
                .Where(f => f.Aktif == true)
                .Select(f => new KresForm
                {
                    KresFormId = f.KresFormId,
                    isim = f.isim,
                    soyisim = f.soyisim,
                    telno = f.telno,
                    dtarihi = f.dtarihi,
                    tc = f.tc,
                    ilce = f.ilce,
                    mahalle = f.mahalle,
                    isturu = f.isturu,
                    durumu = f.durumu,
                    SonucAciklama = f.SonucAciklama
                })
                .ToListAsync();
        }

      

        // Formun durumunu güncelle
        public async Task<KresForm> UpdateFormStatusAsync(int id, OnayGuncelleDto onayguncelle)
        {
            var form = await _context.KresForms.FindAsync(id);
            if (form == null)
            {
                return null;
            }

            form.durumu = onayguncelle.durumu;
            form.SonucAciklama = onayguncelle.SonucAciklama;

            _context.KresForms.Update(form);
            await _context.SaveChangesAsync();

            return form;
        }
    }
}
