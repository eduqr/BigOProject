using BigO.Core.Enums;
using BigO.Core.Interfaces;
using BigO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigO.Services.Analysis
{
	public class LexicalAnalyzer : ILexicalAnalyzer
	{
		public async Task<List<Token>> Analyze(string sourceCode)
		{
			var tokens = new List<Token>();
			tokens.Add(new Token
			{
				Id = Guid.NewGuid(),
				Type = TokenType.Undefined,
				Value = sourceCode,
				Line = 1,
				Length = 9,
				IsValid = false,
				IsIgnored = false
			});

			return tokens;
		}
	}
}
