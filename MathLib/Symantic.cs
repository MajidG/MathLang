using System.Collections.Generic;
using System.Linq;

namespace MathLib
{
    public static class Symantic
    {
        public static List<string> errors = new List<string>();
        static List<FncDecStmtNode> funcs = new List<FncDecStmtNode>();
        static Stack<string> cnsts = new Stack<string>();

        static void error(LexUnit line, string msg)
        {
            errors.Add(string.Format("{0}: Symantic error : " + msg, line.line));
        }
        public static void Check(ProgNode tree)
        {
            errors.Clear();
            funcs.Clear();
            cnsts.Clear();
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "cos" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "sin" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "tan" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "sqrt" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "ln" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "log" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            funcs.Add(new FncDecStmtNode() { ID = new LexUnit() { lexeme = "abs" }, paramList = new List<LexUnit>() { new LexUnit() { lexeme = "x" } } });
            cnsts.Push("pi");
            cnsts.Push("e");
            foreach (var stmt in tree.stmts)
                Check(stmt);
        }

        private static void Check(StmtNode stmt)
        {
            if(stmt is FncDecStmtNode)
            {
                var fnc = stmt as FncDecStmtNode;
                var ff = funcs.Find(f => f.ID.lexeme == fnc.ID.lexeme);
                if (ff == null)
                    funcs.Add(fnc);
                else
                    error(fnc.FNC, "'"+fnc.ID.lexeme+"' already declared");

                var prms = new List<string>();
                foreach (var prm in fnc.paramList)
                    if (prms.Contains(prm.lexeme))
                        error(fnc.FNC, " parameter " + prm.lexeme + " already declared");
                    else
                        prms.Add(prm.lexeme);
                foreach (var prm in prms)
                    cnsts.Push(prm);
                Check(fnc.exp);
                foreach (var prm in prms)
                    cnsts.Pop();
            }
            if (stmt is PrintStmtNode)
            {
                var print = stmt as PrintStmtNode;
                foreach (var exp in print.exps)
                    Check(exp);
            }
            if (stmt is CnstDecStmtNode)
            {
                var cnst = stmt as CnstDecStmtNode;
                if (!cnsts.Contains(cnst.ID.lexeme))
                    cnsts.Push(cnst.ID.lexeme);
                else
                    error(cnst.LET, "'" + cnst.ID.lexeme + "' already defined");
                Check(cnst.exp);
            }
        }

        private static void Check(ExpNode exp)
        {
            if(exp is OpExpNode)
            {
                var op = exp as OpExpNode;
                if (op.OP.token == Token.DIV && op.right is NumExpNode && float.Parse((op.right as NumExpNode).NUM.lexeme) == 0)
                    error(op.OP, "division by zero");
                Check(op.left);
                Check(op.right);
            }
            if(exp is FncCallExpNode)
            {
                var call = exp as FncCallExpNode;
                var fncnames = from fnc in funcs select fnc.ID.lexeme;
                if(!fncnames.Contains(call.ID.lexeme))
                    error(call.ID, "function '"+call.ID.lexeme+"' is undefined");
                else
                {
                    var fnc = funcs.Find(f => f.ID.lexeme == call.ID.lexeme);
                    if (fnc.paramList.Count != call.args.Count)
                        error(call.ID, "'"+call.ID.lexeme + "' takes "+ fnc.paramList.Count+" parameters not "+call.args.Count);
                    foreach (var arg in call.args)
                        Check(arg);
                }
            }
            if(exp is IdExpNode)
            {
                var id = exp as IdExpNode;
                if(!cnsts.Contains(id.ID.lexeme))
                    error(id.ID, " " + id.ID.lexeme + " is undefined");
            }
        }
    }
}
