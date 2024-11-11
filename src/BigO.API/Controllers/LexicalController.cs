using BigO.Core.DTOs;
using BigO.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigO.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LexicalController : ControllerBase
	{
		private readonly ILexicalAnalyzer _lexicalAnalyzer;

		public LexicalController(ILexicalAnalyzer lexicalAnalyzer)
		{
			_lexicalAnalyzer = lexicalAnalyzer;
		}
		[HttpGet("ping")]
		public IActionResult Ping()
		{
			return Ok("Pong");
		}

		[HttpPost("analyze")]
		public async Task<IActionResult> GetCode([FromBody] CodeRequestDTO request)
		{
			var response = await _lexicalAnalyzer.Analyze(request);
			return Ok(response);
		}

		[HttpPost("upload-file")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			using var reader = new StreamReader(file.OpenReadStream());
			var source = await reader.ReadToEndAsync();
			return Ok(source);
		}

	}
}
