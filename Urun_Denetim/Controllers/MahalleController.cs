using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basvurular.Business;

namespace Urun_Denetim.Controllers
{
	[Route("api/Mahalle")]
	[ApiController]
	public class MahalleController : ControllerBase
	{

		private readonly MahalleService _service;
		public MahalleController(MahalleService service)
		{
			_service = service;
		}

		[HttpGet("ilceId/{ilceId:int}")]
		public async Task<IActionResult> GetMahalleByIlceId(int ilceId)
		{
			var mahalleler = await _service.GetAllAsyncByIlceId(ilceId);
			if (mahalleler == null) return NotFound("Mahalle Bulunamadı");
			return Ok(mahalleler);
		}
	}
}