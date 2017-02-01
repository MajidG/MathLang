using System;
using System.Collections.Generic;

namespace MathLib
{



    class ParseException : Exception {
		public ParseException(ref LexUnit unexpected, string msg=null):
		base(string.Format("{0},{1}: Syntaxe error : unexpected '{2}'"+(msg!=null?(", "+msg):""), unexpected.line,unexpected.colmn,unexpected.lexeme)){
		}
	}

	public class Parser {
		Lexer lex = new Lexer();

		public Parser () {
		}

        LexUnit Match(Token to, string err=null)
        {
            if(lex.Peek() != to)
                throw new ParseException(ref lex.unit, err);
            return lex.Take();
        }

		public ProgNode Parse(string code){
			lex.init (code);
			ProgNode prg = new ProgNode ();
            StmtNode stmt = parseStmt();
            while (stmt != null)
            {
                prg.stmts.Add(stmt);
                stmt = parseStmt();
            }
			return prg;
		}

		private StmtNode parseStmt(){
            while (lex.Peek() == Token.EOL)
                lex.Take();
            StmtNode stmt = null;
			if (lex.Peek () == Token.LET)
				stmt = parseCnstDec ();
			else if (lex.Peek () == Token.FNC)
                stmt = parseFncDec();
            else if (lex.Peek() == Token.PRT)
                stmt = parsePrintExp();
            else if (lex.Peek() != Token.EOL && lex.Peek() != Token.EOF)
            {
				stmt = new ExpStmtNode ();
				((ExpStmtNode)stmt).exp = parseExp ();
			}
			if (lex.Peek () != Token.EOL && lex.Peek () != Token.EOF)
				throw new ParseException(ref lex.unit, "invalid in expression");
			lex.Take ();
			return stmt;
		}

        private StmtNode parsePrintExp()
        {
            var stmt = new PrintStmtNode();
            stmt.PRT = Match(Token.PRT, "");
            while(true)
            {
                if (lex.Peek() == Token.STR)
                    stmt.exps.Add(new StringExpNode(lex.Take()));
                else
                    stmt.exps.Add(parseExp());
                if (lex.Peek() != Token.COM)
                    break;
                lex.Take();
            } 
            return stmt;
        }

        private StmtNode parseFncDec()
        {
            var stmt = new FncDecStmtNode();
            stmt.FNC = Match(Token.FNC);
            stmt.ID = Match(Token.ID, "func name expected");
            stmt.LP = Match(Token.LPN, "'(' expected");
            stmt.paramList = parseParams();
            stmt.RP = Match(Token.RPN, "')' expected");
            stmt.EQ = Match(Token.EQL, "'=' expected");
            stmt.exp = parseExp();
            return stmt;
        }

        private List<LexUnit> parseParams()
        {
            var parms = new List<LexUnit>();
            if (lex.Peek() == Token.RPN)
                return parms;
            parms.Add(Match(Token.ID, "param name expected"));
            while (lex.Peek() == Token.COM)
            {
                lex.Take();
                parms.Add(Match(Token.ID, "param name expected"));
            }
            return parms;
        }

        private CnstDecStmtNode parseCnstDec(){
			var dec = new CnstDecStmtNode ();
			dec.LET = lex.Take ();
            dec.ID = Match(Token.ID, "expected ident in const declaration");
			dec.EQ = Match(Token.EQL, "expected '=' in const declaration");
			dec.exp = parseExp ();
			return dec;
		}

		public ExpNode parseExp()
        {
            OpExpNode exp = null;
            ExpNode fact = null;
            if (lex.Peek() == Token.SUB)
            {
                exp = new OpExpNode();
                exp.left = null;
                exp.OP = lex.Take();
                exp.right = parseFact();
            }else
                fact = parseFact ();
			while (lex.Peek() == Token.ADD || lex.Peek() == Token.SUB) {
				OpExpNode tmp = new OpExpNode ();
				tmp.left = exp == null ? fact : exp;					
				tmp.OP = lex.Take ();
				tmp.right = parseFact ();
				exp = tmp;
			}
			return exp == null ? fact : exp;
		}

		private ExpNode parseFact(){
			ExpNode expn = parseExpnt ();
			OpExpNode exp = null;
			while (lex.Peek() == Token.MUL || lex.Peek() == Token.DIV) {
				OpExpNode tmp = new OpExpNode ();
				tmp.left = exp == null ? expn : exp;					
				tmp.OP = lex.Take ();
				tmp.right = parseExpnt ();
				exp = tmp;
			}
			return exp == null ? expn : exp;
		}

		private ExpNode parseExpnt(){
			ExpNode elm = parseElmnt ();
			if (lex.Peek () != Token.POW)
				return elm;
			OpExpNode exp = new OpExpNode ();
			exp.left = elm;
			exp.OP = lex.Take ();
			exp.right = parseExpnt ();
			return exp;
		}

		private ExpNode parseElmnt(){
			if (lex.Peek () == Token.NUM)
				return new NumExpNode (lex.Take());
			if (lex.Peek () == Token.ID) {
				LexUnit id = lex.Take ();
				if (lex.Peek () == Token.LPN) {
					lex.Take ();
                    FncCallExpNode fnc = new FncCallExpNode();
                    fnc.ID = id;
                    fnc.args = parseArgs (); 
                    Match(Token.RPN, "expected ')' for valid function call");
					return fnc;
				} else {
					return new IdExpNode (id);
				}
			}
				
			if (lex.Peek () == Token.LPN) {
				lex.Take ();
				ExpNode exp = parseExp ();
                Match(Token.RPN, "expected ')' for valid expression");
				return exp;
			}
			throw new ParseException(ref lex.unit, "invalid in expression");
		}

		private List<ExpNode> parseArgs(){
			List<ExpNode> args = new List<ExpNode>();
			args.Add (parseExp());
			while (lex.Peek () == Token.COM) {
				lex.Take ();
				args.Add (parseExp ());
			}
			return args;
		}
	}
}

