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
		Identifier,  // Nombre de variable o función
		Number,      // Constantes numéricas
		LiteralString, // Constantes de cadena
		LiteralChar, // Constantes de carácter
		Exclamation, // Operador !
		Quotation,   // Comillas "
		NumberSign,  // Numeral #
		LeftParen,   // Paréntesis izquierdo (
		RightParen,  // Paréntesis derecho )
		LeftBracket, // Corchete izquierdo [
		RightBracket,// Corchete derecho ]
		LeftBrace,   // Llave izquierda {
		RightBrace,  // Llave derecha }
		Underscore,  // Guión bajo _
		Dot,         // Punto .
		Comma,       // Coma ,
		At,          // Arroba @
		Dollar,      // Signo de dólar $
		Plus,        // Operador +
		Minus,       // Operador -
		Divide,      // Operador /
		Multiply,    // Operador *
		Modulus,     // Operador %
		Exponent,    // Operador ^
		Assignment,  // Operador de asignación =
		MoreThan,    // Operador mayor que >
		LessThan,    // Operador menor que <
		MoreThanOrEqual, // Operador mayor o igual que >=
		LessThanOrEqual, // Operador menor o igual que <=
		Equality,    // Operador de igualdad ==
		Inequality,  // Operador de desigualdad !=
		LogicalAnd,  // Operador &&
		LogicalOr,   // Operador ||
		Increment,   // Operador ++
		Decrement,   // Operador --
		Question,    // Operador ?
		Colon,       // Dos puntos :
		Ampersand,   // Operador &
		Pipe,        // Operador |
		Semicolon,   // Punto y coma ;
		Keyword,     // Palabras clave (if, else, etc.)
		DataType,   // Tipos de datos (int, real, etc.)
		StartOfFile, // ((
		EndOfFile,   // ))
		Whitespace,  // Espacios en blanco
		Comment,     // Comentarios
	}
}
