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
	public class MahalleController_Test
	{
		private readonly Mock<IMahalleRepository> _mockMahalleRepository;
		private readonly MahalleService _mahalleService;
		private readonly MahalleController _mahalleController;
		public MahalleController_Test()
		{
			_mockMahalleRepository = new Mock<IMahalleRepository>();
			_mahalleService = new MahalleService(_mockMahalleRepository.Object);
			_mahalleController = new MahalleController(_mahalleService);
		}

		[Fact]
		public async Task MahalleController_GetMahalleByIlceId_ReturnsOk()
		{
			int ilceId = 1;
			_mockMahalleRepository.Setup(r => r.GetAllAsyncByIlceId(ilceId)).ReturnsAsync(new List<Mahalle>());

			var result = await _mahalleController.GetMahalleByIlceId(ilceId);

			Assert.IsType<OkObjectResult>(result);
		}


	}
}
