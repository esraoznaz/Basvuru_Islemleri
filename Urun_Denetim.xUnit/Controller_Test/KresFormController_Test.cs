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
	public class KresFormController_Test
	{
		private readonly Mock<IRepository> _mockKresFormRepository;
		private readonly KresFormService _kresFormService;
		private readonly KresFormController _kresFormController;
		public KresFormController_Test()
		{
			_mockKresFormRepository = new Mock<IRepository>();
			_kresFormService = new KresFormService(_mockKresFormRepository.Object);
			_kresFormController = new KresFormController(_kresFormService);
		}

		[Fact]
		public async Task KresFormController_GetAllForms_ReturnsOk()
		{
			_mockKresFormRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<KresForm>());

			var result = await _kresFormController.GetAllForms();

			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task KresFormController_GetFormById_ReturnsOk()
		{
			int formId = 1;
			var form = new KresForm { KresFormId = formId};

			_mockKresFormRepository.Setup(r => r.GetByIdAsync(formId)).ReturnsAsync(form);

			var result = await _kresFormController.GetFormById(formId);

			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task KresFormController_GetFormById_ReturnsNotFound()
		{
			int formId = 1;
			KresForm form = null;

			_mockKresFormRepository.Setup(r => r.GetByIdAsync(formId)).ReturnsAsync(form);

			var result = await _kresFormController.GetFormById(formId);

			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task KresFormController_GetByFilterAsync_ReturnsOk()
		{
			string? isim = "isim";
			string? soyisim = "soyisim";
			string? tcNo = "tcNo";

			var forms = new List<KresForm> { new KresForm { isim = isim, soyisim = soyisim, tc = tcNo } };

			_mockKresFormRepository
				.Setup(r => r.GetByFilterAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
				.ReturnsAsync(forms);

			var result = await _kresFormController.GetByFilterAsync(isim, soyisim, tcNo);
			var result_1 = await _kresFormController.GetByFilterAsync(isim, soyisim, null);
			var result_2 = await _kresFormController.GetByFilterAsync(isim, null, tcNo);
			var result_3 = await _kresFormController.GetByFilterAsync(null, soyisim, tcNo);
			var result_4 = await _kresFormController.GetByFilterAsync(null, soyisim, null);
			var result_5 = await _kresFormController.GetByFilterAsync(null, null, tcNo);

			Assert.IsType<OkObjectResult>(result);
			Assert.IsType<OkObjectResult>(result_1);
			Assert.IsType<OkObjectResult>(result_2);
			Assert.IsType<OkObjectResult>(result_3);
			Assert.IsType<OkObjectResult>(result_4);
			Assert.IsType<OkObjectResult>(result_5);
		}

		[Fact]
		public async Task KresFormController_AddForm_ReturnsOk()
		{
			var formDto = new KresFormEkleDto { isim = "isim", soyisim = "soyisim", tc = "tc" };
			var form = new KresForm { isim = formDto.isim, soyisim = formDto.soyisim, tc = formDto.tc };

			_mockKresFormRepository.Setup(r => r.AddAsync(formDto)).ReturnsAsync(form);

			var result = await _kresFormController.AddForm(formDto);

			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task KresFormController_UpdateForm_ReturnsOk()
		{
			int formId = 1;
			var formDto = new FormGuncelleDto { isim = "isim", soyisim = "soyisim", tc = "tc" };
			var form = new KresForm { KresFormId = formId, isim = formDto.isim, soyisim = formDto.soyisim, tc = formDto.tc };

			_mockKresFormRepository.Setup(r => r.UpdateAsync(formId, formDto)).ReturnsAsync(form);

			var result = await _kresFormController.UpdateForm(formId, formDto);

			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async Task KresFormController_ToggleAktif_ReturnsOk()
		{
			int formId = 1;
			int formId_1 = 1;
			var form = new KresForm { KresFormId = formId, Aktif = true };
			var form_1 = new KresForm { KresFormId = formId_1, Aktif = false };

			_mockKresFormRepository.Setup(r => r.ToggleAktifAsync(formId)).ReturnsAsync(form);
			_mockKresFormRepository.Setup(r => r.ToggleAktifAsync(formId_1)).ReturnsAsync(form_1);

			var result = await _kresFormController.ToggleAktif(formId);
			var result_1 = await _kresFormController.ToggleAktif(formId_1);

			Assert.IsType<OkObjectResult>(result);
			Assert.IsType<OkObjectResult>(result_1);
		}

		[Fact]
		public async Task KresFormController_ToggleAktif_ReturnsNotFound()
		{
			int formId = 1;
			KresForm form = null;

			_mockKresFormRepository.Setup(r => r.ToggleAktifAsync(formId)).ReturnsAsync(form);

			var result = await _kresFormController.ToggleAktif(formId);

			Assert.IsType<NotFoundResult>(result);
		}




	}
}
