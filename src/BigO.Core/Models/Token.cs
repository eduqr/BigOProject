using BigO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigO.Core.Models
{
	public class Token
	{
		public Guid Id { get; set; }
		public TokenType Type { get; set; }
		public string Value { get; set; }
		public int Line { get; set; }
		public int Length { get; set; }
		public bool IsValid { get; set; }
		public bool IsIgnored { get; set; }
	}
}
