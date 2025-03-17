using Basvurular.Business;
using Basvurular.DataAccess;
using Moq;
using Urun_Denetim.Controllers;
using Basvurular.Entities;
using Microsoft.AspNetCore.Mvc;
using Basvurular.Entities.DTOs;


namespace Urun_Denetim.xUnit.Controller_Test
{
	public class DagitimController_Test
	{
		private readonly Mock<IRepositoryDagitim> _mockDagitimRepository;
		private readonly DagitimService _dagitimService;
		private readonly DagitimController _dagitimController;
		public DagitimController_Test()
		{
			_mockDagitimRepository = new Mock<IRepositoryDagitim>();
			_dagitimService = new DagitimService(_mockDagitimRepository.Object);
			_dagitimController = new DagitimController(_dagitimService);
		}

		[Fact]
		public async Task DagitimControllers_GetAllForms_ReturnsOk()
		{
			// Arrange
			_mockDagitimRepository.Setup(r => r.GetDagitimFormsAsync()).ReturnsAsync(new List<KresForm>());

			// Act
			var result = await _dagitimController.GetAllForms();

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task DagitimControllers_UpdateForm_ReturnsOk()
		{
			// Arrange
			int formıd = 1;
			var updateDto = new DagitimGuncelleDto { dagıtım = "Beklemede" };
			var updatedForm= new KresForm { KresFormId = formıd, dagıtım = "Beklemede" };

			_mockDagitimRepository.Setup(r => r.UpdateDagitimFormAsync(formıd, updateDto)).ReturnsAsync(updatedForm);
			
			// Act
			 var result = await _dagitimController.UpdateForm(formıd, updateDto);
			
			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedForm = Assert.IsType<KresForm>(okResult.Value);
			Assert.Equal("Beklemede", returnedForm.dagıtım);
			
		}

		[Fact]
		public async Task DagitimControllers_UpdateForm_ReturnsNotFound_WhenFormNotExists()
		{
			// Arrange
			int formıd = 1;
			var updateDto = new DagitimGuncelleDto { dagıtım = "Beklemede" };

			_mockDagitimRepository.Setup(r => r.UpdateDagitimFormAsync(formıd, updateDto)).ReturnsAsync((KresForm)null);

			// Act
			var result = await _dagitimController.UpdateForm(formıd, updateDto);

			// Assert
			Assert.IsType<NotFoundObjectResult>(result);

		}



	}
}
