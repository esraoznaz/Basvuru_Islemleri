using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.DataAccess;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;

namespace Basvurular.Business
{
    public class AdminService
    {
        private readonly IRepositoryAdmin _repository;

        public AdminService(IRepositoryAdmin repositoryAdmin)
        {
            _repository = repositoryAdmin;
        }

        public async Task<IEnumerable<Admins>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Admins> AdminLoginAsync(AdminLoginDto adminLoginDto)
        {
            return await _repository.AdminLoginAsync(adminLoginDto);
        }
    }
}
