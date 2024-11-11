using BigO.Core.DTOs;
using BigO.Core.Models;

namespace BigO.Core.Interfaces
{
	public interface ILexicalAnalyzer
	{
		Task<LexicalResponseDTO> Analyze(CodeRequestDTO sourceCode);
	}
}
