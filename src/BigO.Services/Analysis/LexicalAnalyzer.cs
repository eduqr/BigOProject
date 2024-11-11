using BigO.Core.DTOs;
using BigO.Core.Enums;
using BigO.Core.Interfaces;
using BigO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigO.Services.Analysis
{
	public class LexicalAnalyzer : ILexicalAnalyzer
	{
		private static readonly Dictionary<string, TokenType> TokenPatterns = new()
		{
			{ @"//.*", TokenType.Comment },
			{@"\s+", TokenType.Whitespace},

			{@"^\(\(", TokenType.StartOfFile},
			{@"\)\)$", TokenType.EndOfFile},

			{@"[a-zA-Z_][a-zA-Z0-9_]*", TokenType.Identifier},
			{@"\d+(\.\d+)?", TokenType.Number},
			{@"""[^""]*""", TokenType.LiteralString},
			{@"'[^']*'", TokenType.LiteralChar},

			{@"!", TokenType.Exclamation},
			{@"""", TokenType.Quotation},
			{@"#", TokenType.NumberSign},
			{@"\(", TokenType.LeftParen},
			{@"\)", TokenType.RightParen},
			{@"\[", TokenType.LeftBracket},
			{@"\]", TokenType.RightBracket},
			{@"\{", TokenType.LeftBrace},
			{@"\}", TokenType.RightBrace},
			{@"_", TokenType.Underscore},
			{@"\.", TokenType.Dot},
			{@",", TokenType.Comma},
			{@"@", TokenType.At},
			{@"\$", TokenType.Dollar},
			{@"\+", TokenType.Plus},
			{@"-", TokenType.Minus},
			{@"/", TokenType.Divide},
			{@"\*", TokenType.Multiply},
			{@"%", TokenType.Modulus},
			{@"\^", TokenType.Exponent},
			{@"=", TokenType.Assignment},
			{@">", TokenType.MoreThan},
			{@"<", TokenType.LessThan},
			{@">=", TokenType.MoreThanOrEqual},
			{@"<=", TokenType.LessThanOrEqual},
			{@"==", TokenType.Equality},
			{@"!=", TokenType.Inequality},
			{@"&&", TokenType.LogicalAnd},
			{@"\|\|", TokenType.LogicalOr},
			{@"\+\+", TokenType.Increment},
			{@"--", TokenType.Decrement},
			{@"\?", TokenType.Question},
			{@":", TokenType.Colon},
			{@"&", TokenType.Ampersand},
			{@"\|", TokenType.Pipe},
			{@";", TokenType.Semicolon},
		};

		private static readonly Dictionary<string, TokenType> Keywords = new()
		{
			{"if", TokenType.Keyword},
			{"be", TokenType.Keyword},
			{"until", TokenType.Keyword},
			{"but", TokenType.Keyword},
			{"while", TokenType.Keyword},
			{"in", TokenType.Keyword},
			{"out", TokenType.Keyword},
			{"cap", TokenType.DataType},
			{"nocap", TokenType.DataType},
			{"maybe", TokenType.DataType},
		};

		public static readonly Dictionary<string, TokenType> DataTypes = new()
		{
			{"int8", TokenType.DataType},
			{"int", TokenType.DataType},
			{"int16", TokenType.DataType},
			{"real", TokenType.DataType},
			{"int32", TokenType.DataType},
			{"atom", TokenType.DataType},
			{"text", TokenType.DataType},
			{"flag", TokenType.DataType},
		};
		public async Task<LexicalResponseDTO> Analyze(CodeRequestDTO sourceCode)
		{
			var response = new LexicalResponseDTO
			{
				Tokens = new List<Token>(),
				Succeeded = true
			};

			try
			{
				if (string.IsNullOrWhiteSpace(sourceCode.Code))
				{
					return InvalidResponse("Code is required");
				}

				if (!Regex.IsMatch(sourceCode.Code, @"^\(\(") || !Regex.IsMatch(sourceCode.Code, @"\)\)$"))
				{
					return InvalidResponse("Code must start and end with '((' and '))'");
				}

				var content = sourceCode.Code.Substring(2, sourceCode.Code.Length - 4); // quitamos (())

				var lines = content.Split('\n');
				var currentPosition = 2;
				var lineNumber = 1;

				foreach (var line in lines)
				{
					var position = 0;
					while (position < line.Length)
					{
						var token = GetNextToken(line, ref position, lineNumber, currentPosition);
						if (token != null)
						{
							response.Tokens.Add(token);
							if (!token.IsValid)
							{
								response.Succeeded = false;
								response.ErrorMessage = $"Invalid token '{token.Value}' at line {token.Line}";
								// aquí podría retornar
							}
						}
					}
					currentPosition += line.Length + 1; // +1 por el \n
					lineNumber++;
				}

				// SOF y EOF
				response.Tokens.Insert(0, new Token
				{
					Id = Guid.NewGuid(),
					Type = TokenType.StartOfFile,
					Value = "((",
					Line = 1,
					Length = 2,
					IsValid = true,
					IsIgnored = false
				});

				response.Tokens.Add(new Token
				{
					Id = Guid.NewGuid(),
					Type = TokenType.EndOfFile,
					Value = "))",
					Line = lineNumber - 1,
					Length = 2,
					IsValid = true,
					IsIgnored = false
				});

				response.TotalTokens = response.Tokens.Count(t => !t.IsIgnored);

				return response;
			}
			catch (Exception ex)
			{
				return InvalidResponse($"Error analyzing code: {ex.Message}");
			}
		}

		private Token GetNextToken(string line, ref int position, int lineNumber, int absolutePosition)
		{
			var remainingText = line.Substring(position);

			foreach (var pattern in TokenPatterns)
			{
				var match = Regex.Match(remainingText, "^" + pattern.Key);
				if (match.Success)
				{
					var value = match.Value;
					var length = match.Length;
					position += length;

					var type = pattern.Value;

					// ver si es keyword o datatype
					if (type == TokenType.Identifier && Keywords.ContainsKey(value))
					{
						type = TokenType.Keyword;
					}

					if (type == TokenType.Identifier && DataTypes.ContainsKey(value))
					{
						type = TokenType.DataType;
					}

					var token = new Token
					{
						Id = Guid.NewGuid(),
						Type = type,
						Value = value,
						Line = lineNumber,
						Length = length,
						IsValid = true,
						IsIgnored = IsIgnoredToken(pattern.Value)
					};

					return token;
				}
			}

			// crear token inválido si no hay patrón
			if (position < line.Length)
			{
				var invalidToken = new Token
				{
					Id = Guid.NewGuid(),
					Type = TokenType.Undefined,
					Value = line[position].ToString(),
					Line = lineNumber,
					Length = 1,
					IsValid = false,
					IsIgnored = false
				};
				position++;
				return invalidToken;
			}

			return null;
		}

		private bool IsIgnoredToken(TokenType type)
		{
			return type == TokenType.Whitespace || type == TokenType.Comment;
		}

		private LexicalResponseDTO InvalidResponse(string errorMessage)
		{
			return new LexicalResponseDTO
			{
				Succeeded = false,
				Tokens = new List<Token>(),
				TotalTokens = -1,
				ErrorMessage = errorMessage
			};
		}
	}
}
