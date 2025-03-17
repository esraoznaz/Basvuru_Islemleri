using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
    public class RepositoryAdmin: IRepositoryAdmin
    {
        private readonly ApplicationDbContext _context;

        public RepositoryAdmin (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Admins>> GetAllAsync()
        {
            return await _context.Adminses.ToListAsync();
        }

        public async Task<Admins> AdminLoginAsync(AdminLoginDto adminLoginDto)
        {
            return await _context.Adminses
               .FirstOrDefaultAsync(a => a.AdminAd == adminLoginDto.AdminAd && a.AdminSifre == adminLoginDto.AdminSifre);
        }
    }
}
