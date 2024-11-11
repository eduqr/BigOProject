using BigO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigO.Core.DTOs
{
	public class LexicalResponseDTO
	{
		public bool Succeeded { get; set; }
		public int TotalTokens { get; set; }
		public List<Token> Tokens { get; set; }
		public string ErrorMessage { get; set; }

	}
}
