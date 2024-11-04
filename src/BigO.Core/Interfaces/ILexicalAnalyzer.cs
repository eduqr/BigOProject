using BigO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigO.Core.Interfaces
{
	public interface ILexicalAnalyzer
	{
		Task<List<Token>> Analyze(string sourceCode);
	}
}
