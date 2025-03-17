using Basvurular.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.Entities.DTOs;

namespace Basvurular.DataAccess
{
    public class Repository :IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KresForm>> GetAllAsync()
        {
            return await _context.KresForms.Where(f => f.Aktif).ToListAsync();
        }

        public async Task<KresForm> GetByIdAsync(int id)
        {
            return await _context.KresForms.FindAsync(id);
        }

        public async Task<IEnumerable<KresForm>> GetByFilterAsync(string isim, string soyisim, string tcNo)
        {
            var query = _context.KresForms.AsQueryable();

            if (!string.IsNullOrEmpty(isim))
            {
                query = query.Where(f => f.isim.Contains(isim));
            }
            if (!string.IsNullOrEmpty(soyisim))
            {
                query = query.Where(f => f.soyisim.Contains(soyisim));
            }
            if (!string.IsNullOrEmpty(tcNo))
            {
                query = query.Where(f => f.tc == tcNo);
            }

            var result = await query.ToListAsync();
            return result;
        }



        public async Task<KresForm> AddAsync(KresFormEkleDto kresFormDto)
        {
            if (string.IsNullOrEmpty(kresFormDto.tc))
            {
                throw new ArgumentException("TC Kimlik Numarası zorunludur.");
            }

            var mevcutBasvuru = await _context.KresForms.FirstOrDefaultAsync(f => f.tc == kresFormDto.tc);
            if (mevcutBasvuru != null)
            {
                throw new InvalidOperationException("Bu TC Kimlik Numarası ile daha önce başvuru yapılmış.");
            }

            var formEntity = new KresForm
            {
                isim = kresFormDto.isim,
                soyisim = kresFormDto.soyisim,
                telno = kresFormDto.telno,
                dtarihi = kresFormDto.dtarihi,
                tc = kresFormDto.tc,
                ilce = kresFormDto.ilce,
                mahalle = kresFormDto.mahalle,
                isturu = kresFormDto.isturu
            };

            await _context.KresForms.AddAsync(formEntity);
            await _context.SaveChangesAsync();

            return formEntity;
        }


        public async Task<KresForm> UpdateAsync(int id, FormGuncelleDto guncelleformDto)
        {
            // Güncellenecek formu bul
            var existingForm = await _context.KresForms.FindAsync(id);
            if (existingForm == null)
            {
                throw new KeyNotFoundException("Form bulunamadı");
            }

            // Alanları güncelle
            existingForm.isim = guncelleformDto.isim;
            existingForm.soyisim = guncelleformDto.soyisim;
            existingForm.telno = guncelleformDto.telno;
            existingForm.dtarihi = guncelleformDto.dtarihi;
            existingForm.tc = guncelleformDto.tc;
            existingForm.ilce = guncelleformDto.ilce;
            existingForm.mahalle = guncelleformDto.mahalle;

            _context.KresForms.Update(existingForm);
            await _context.SaveChangesAsync();

            return existingForm;

        }


        public async Task<KresForm> ToggleAktifAsync(int id)
        {
            var form = await _context.KresForms.FindAsync(id);
            if (form == null)
            {
                throw new KeyNotFoundException();
            }

            form.Aktif = !form.Aktif;
            await _context.SaveChangesAsync();
            return form;
        }

        //public async Task<bool> ToggleAktifAsync(int id)
        //{
        //    var form = await _context.KresForms.FindAsync(id);
        //    if (form == null) return false;

        //    form.Aktif = !form.Aktif;
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
}
