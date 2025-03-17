using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basvurular.Business;

namespace Urun_Denetim.Controllers
{
	[Route("api/ilce")]
	[ApiController]
	public class IlceController : ControllerBase
	{

		private readonly IlceService _service;

		public IlceController(IlceService service)
		{
			_service = service;
		}



		[HttpGet]
		public async Task<IActionResult> GetAllForms()
		{
			var ilceler = await _service.GetAllFormsAsync();
			if (ilceler == null) return NotFound("Ilçe Bulunamadı");
			return Ok(ilceler);
		}

		
	}
}
