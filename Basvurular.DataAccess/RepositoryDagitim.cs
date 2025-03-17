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
    public class RepositoryDagitim : IRepositoryDagitim
    {
        private readonly ApplicationDbContext _context;

        public RepositoryDagitim(ApplicationDbContext context)
        {
            _context = context;
        }

        // Aktif form verilerini al
        public async Task<List<KresForm>> GetDagitimFormsAsync()
        {
            return await _context.KresForms
                .Where(f => f.Aktif == true)
                .Select(f => new KresForm
                {
                    KresFormId = f.KresFormId,
                    isim = f.isim,
                    soyisim = f.soyisim,
                    telno = f.telno,
                    tc = f.tc,
                    ilce = f.ilce,
                    mahalle = f.mahalle,
                    isturu = f.isturu,
                    dagıtım = f.dagıtım,
                })
                .ToListAsync();
        }

       

        // Formun durumunu güncelle
        public async Task<KresForm> UpdateDagitimFormAsync(int id, DagitimGuncelleDto dagitimguncelle)
        {
            var form = await _context.KresForms.FindAsync(id);
            if (form == null)
            {
                return null;
            }

            form.dagıtım = dagitimguncelle.dagıtım;
           

            _context.KresForms.Update(form);
            await _context.SaveChangesAsync();

            return form;
        }
    }
}
