using Moq;
using Urun_Denetim.Controllers;
using Basvurular.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basvurular.Business;
using Basvurular.Entities;
using Basvurular.Entities.DTOs;

namespace Urun_Denetim.xUnit.Controller_Test
{
	public class OnayControllerTest
	{

		private readonly Mock<IRepositoryOnay> _mockRepository;
		private readonly OnayService _onayService;
		private readonly OnayController _controller;
		public OnayControllerTest() 
		{
			_mockRepository = new Mock<IRepositoryOnay>(); // IRepositoryOnay mock'lanıyor
			_onayService = new OnayService(_mockRepository.Object); // Mock edilen repository servise veriliyor
			_controller = new OnayController(_onayService); // Servis controller'a veriliyor
		}

		[Fact]
		public async Task OnayController_GetAllForms_ReturnsOk()
		{
			// Arrange: Boş bir liste döndürülmesini sağla
			_mockRepository.Setup(r => r.GetActiveFormsAsync()).ReturnsAsync(new List<KresForm>()); //Mock edilen repository'nin GetActiveFormsAsync metodunu çağırınca boş bir liste döndürmesini sağlıyorum

			// Act
			var result = await _controller.GetAllForms(); // OnayController içindeki GetAllForms metodunu çağırıyorum ve sonuç tipini alıyorum

			// Assert
			Assert.IsType<OkObjectResult>(result); // normalde dönen sonuç tipi result ta kayıtlı oluğu için  testen dönen OkObjectResult tipi aynı mı diye kontrol ediyorum aynı ise "OK 200" döndüğünü anlarım
			
			//Bu testte veri tabanı bağlantıları değil kod yapısının doğru çalışıp çalışmadığını test ediyorum.
		
		}

		[Fact]
		public async Task OnayController_UpdateForm_ReturnsOk()
		{
			// Arrange: Güncellenen formu taklit et
			int formId = 1;
			var updateDto = new OnayGuncelleDto { durumu = "Onaylandı", SonucAciklama = "Başarılı" };
			var updatedForm = new KresForm { KresFormId = formId, durumu = "Onaylandı", SonucAciklama = "Başarılı" };

			_mockRepository.Setup(r => r.UpdateFormStatusAsync(formId, updateDto)).ReturnsAsync(updatedForm);

			// Act
			var result = await _controller.UpdateForm(formId, updateDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedForm = Assert.IsType<KresForm>(okResult.Value);
			Assert.Equal("Onaylandı", returnedForm.durumu);
			Assert.Equal("Başarılı", returnedForm.SonucAciklama);
		}


		[Fact]
		public async Task OnayController_UpdateForm_ReturnsNotFound_WhenFormNotExists()
		{
			// Arrange: 
			int formId = 99; // formda olmayan bir id gibi davranıcak 
			var updateDto = new OnayGuncelleDto { durumu = "Onaylandı", SonucAciklama = "Başarılı" }; //olasydı öyle biri bu bilgileri istiyorum

			_mockRepository.Setup(r => r.UpdateFormStatusAsync(formId, updateDto)).ReturnsAsync((KresForm)null); //en sonunda böyle bir id olmadı için boş dönmesi gerekiyor

			// Act
			var result = await _controller.UpdateForm(formId, updateDto); //burda bu bilgileri kullanark çağırdığımızda UpdateForm içindeki  fonksiyonlara girerek en son UpdateFormStatusAsync fonksiyonuna ulaşacak onuda yukarda null döndürüdüğümüz için böyle bir id yok gibi davanıyor
		    // Assert															  
			Assert.IsType<NotFoundObjectResult>(result); //burda dönen sonuç tipi NotFoundObjectResult olmalı çünkü böyle bir id yok değeri result ile karşılaştırıyo  veold azaten böş döndüğü için test doğru
		}


	}
}
