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
	public class IlceController_Test
	{
		private readonly Mock<IilceRepository> _mockIlceRepository;
		private readonly IlceService _ilceService;
		private readonly IlceController _ilceController;
		public IlceController_Test()
		{
			_mockIlceRepository = new Mock<IilceRepository>();
			_ilceService = new IlceService(_mockIlceRepository.Object);
			_ilceController = new IlceController(_ilceService);
		}

		[Fact]
		public async Task IlceController_GetAllForms_ReturnsOk()
		{
			_mockIlceRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Ilce>());

			var result = await _ilceController.GetAllForms();

			Assert.IsType<OkObjectResult>(result);
		}

	}
}
