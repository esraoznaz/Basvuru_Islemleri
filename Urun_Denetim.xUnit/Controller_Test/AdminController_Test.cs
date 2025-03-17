using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Urun_Denetim.Controllers;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;
using Basvurular.DataAccess;
using Basvurular.Business;

namespace Urun_Denetim.xUnit.Controller_Test
{
	public class AdminController_Test
	{
		private readonly Mock<IRepositoryAdmin> _mockAdminRepository;
		private readonly AdminService _adminService;
		private readonly AdminController _adminController;
		public AdminController_Test()
		{
			_mockAdminRepository = new Mock<IRepositoryAdmin>();
			_adminService = new AdminService(_mockAdminRepository.Object);
			_adminController = new AdminController(_adminService);
		}
		
		[Fact]
		public async Task AdminController_GetAdmin_ReturnsOk()
		{
			_mockAdminRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Admins>());
			
			var result = await _adminController.GetAdmin();
			
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task AdminController_AdminLogin_ReturnsOk()
		{
			_mockAdminRepository.Setup(r => r.AdminLoginAsync(It.IsAny<AdminLoginDto>())).ReturnsAsync(new Admins());
			
			var result = await _adminController.AdminLogin(new AdminLoginDto());
			
			Assert.IsType<OkObjectResult>(result);
		}


	}
}
