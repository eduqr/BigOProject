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

		[HttpPost("analyze")]
		public async Task<IActionResult> GetCode([FromBody] string code)
		{
			var tokens = await _lexicalAnalyzer.Analyze(code);
			return Ok(tokens);
		}
	}
}
