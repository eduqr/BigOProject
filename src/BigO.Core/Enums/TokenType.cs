using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigO.Core.Enums
{
	public enum TokenType
	{
		Undefined,
		Identifier, // Variable name
		Keyword,    // Reserved word
		Symbol,     // { } [ ] ( ) , ; . ...
		Literal,    // Number, string, boolean...
		Comment
	}
}
