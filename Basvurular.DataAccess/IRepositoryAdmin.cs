using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
    public interface IRepositoryAdmin
    {
        Task<IEnumerable<Admins>> GetAllAsync();


        Task<Admins> AdminLoginAsync(AdminLoginDto adminLoginDto);
    }
}
