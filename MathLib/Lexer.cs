using System;
using System.Collections.Generic;

namespace MathLib
{
	public enum Token {
		LET=0x101,
		FNC,
		PRT,
		NUM,
		ID,
		STR,
        EPS,
        LPN = '(',
		RPN = ')',
		COM = ',',
		EQL = '=',
		ADD = '+',
		SUB = '-',
		MUL = '*',
		DIV = '/',
		POW = '^',
		EOL = '\n',
		EOF = '\0'
	}

	public class LexUnit{
		public Token token;
		public string lexeme;
		public int line;
		public int colmn;
	}
		

	public class Lexer
	{
		public LexUnit unit;

		public Lexer ()
		{
		}

		public void init(string code)
		{
			this.code = code;
			pos = 0;
			ln = 1;
			lastnl = 0;
            unit = null;
		}

		public void resume(string code)
		{
			this.code = code;
			pos = 0;
			lastnl = 0;
            unit = null;
        }

		string code;
		int pos, ln, lastnl;

		private bool isDigit(){
			return !end() && (code[pos] >= '0' && code[pos] <= '9');
		}

		private bool isAlpha(){
			return !end() && (code[pos] >= 'a' && code[pos] <= 'z' || code[pos] >= 'A' && code[pos] <= 'Z');
		}

		private bool isOneOf(string set){
			return !end() && set.IndexOf(code[pos])>=0;
		}

		private void start(){
			unit.lexeme = "";
			unit.line = ln;
			unit.colmn = pos - lastnl;
			if (code [pos] == '\n') {
				ln++;
				lastnl = pos;
			}
			append ();
		}

		private void append(){
			unit.lexeme += code[pos++];
		}

		private void skipSpace(){
			while (!end() && isOneOf(" \t\r")) {
				pos++;
			}
		}

		private bool end(){
			return pos >= code.Length;
		}

		public LexUnit Take() {
			Peek ();
			var tmp = unit;
            unit = null;
			return tmp;		
		}

		public Token Peek()
		{
			if (unit != null)
				return unit.token;
            unit = new LexUnit();
			skipSpace ();

			if (isDigit ()) {
				start ();
				while (isDigit ())
					append ();
				if (isOneOf (".")) {
					append ();
					while (isDigit ())
						append ();
				}
				unit.token = Token.NUM;
				return Token.NUM;
			}

			if (isOneOf ("()=+-*/^,\n")) {
				unit.token = (Token)code [pos];
				start ();
				if (unit.token == Token.EOL)
					unit.lexeme = "EOL";
				return  unit.token;
			}

			if (isAlpha ()) {
				start ();
				while (isAlpha () || isDigit ())
					append ();
				if (unit.lexeme == "let")
					unit.token = Token.LET;
				else if (unit.lexeme == "fnc")
					unit.token = Token.FNC;
				else if (unit.lexeme == "print")
					unit.token = Token.PRT;
				else
					unit.token = Token.ID;
				return unit.token;
			}	

			if (!end() && code [pos] == '"') {
				pos++;
				unit.lexeme = "";
				while (code [pos] != '"' && code [pos] != '\n')
					append ();
				if(code [pos] == '\n')
					throw new Exception (string.Format("{0},{1}: Lexical error : Expected (\") to end a string", ln, pos - lastnl, code[pos]));
				pos++;
				unit.token = Token.STR;
				return unit.token;
			}

			if (end ()) {
				unit.token = Token.EOF;
				unit.lexeme = "EOF";
				return unit.token;
			}
			throw new Exception (string.Format("{0},{1}: Lexical error : Unrecognized character '{2}'", ln, pos - lastnl, code[pos]));
		}
	}
}

