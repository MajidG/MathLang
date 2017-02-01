namespace MathLib
{
    //class Variable{
    //	public string name;
    //	public float value;
    //	public bool constant;
    //	public Variable(string name, float value, bool cnst=false){
    //		this.name = name;
    //		this.value = value;
    //		constant = cnst;
    //	}
    //}

    //public class Evaluator {
    //	Stack<Variable> vars = new Stack<Variable>();
    //	Dictionary<string, FncDecStmtNode> funcs = new Dictionary<string, FncDecStmtNode>();
    //	Dictionary<string, FncNativeStmtNode> nativefuncs = new Dictionary<string, FncNativeStmtNode>();

    //	public Evaluator(){
    //		clean ();
    //	}

    //	public void Evaluate(ProgNode prg){
    //		foreach (var stmt in prg.stmts)
    //			EvaluateStmt (stmt);
    //	}

    //	public void EvaluateStmt(StmtNode stmt){
    //		if (stmt is FncDecStmtNode) {
    //			var func = stmt as FncDecStmtNode;
    //			funcs [func.ID.lexeme] = func;
    //		}else if (stmt is CnstDecStmtNode) {
    //			var cnst = stmt as CnstDecStmtNode;
    //			float value = evaluateExp (cnst.exp);
    //			var vr = getVar (cnst.ID);
    //			if (vr != null)
    //			if (vr.constant)
    //				throw new ParseException(ref cnst.LET, "redefinition of a const");
    //			else
    //				vr.value = value;
    //			else
    //				vars.Push (new Variable(cnst.ID.lexeme, value));
    //		}else if (stmt is PrintStmtNode) {
    //			//var print = stmt as PrintStmtNode;
    //			//foreach (var exp in print.exps)
    //			//	if(exp is StringExpNode)
    //			//		Console.Write ((exp as StringExpNode).STR.lexeme);
    //			//	else
    //			//		Console.Write (evaluateExp (exp));
    //			//Console.WriteLine ();
    //		}else if (stmt is ExpStmtNode) {
    //			var expStmt = stmt as ExpStmtNode;
    //			Console.WriteLine (evaluateExp(expStmt.exp));
    //		}
    //	}

    //	Variable getVar(LexUnit id){
    //		foreach (var vr in vars)
    //			if (vr.name == id.lexeme)
    //				return vr;
    //		return null;
    //	}

    //	private float evaluateExp(ExpNode exp){
    //		if (exp is NumExpNode) {
    //			var nm = exp as NumExpNode;
    //			return nm.Value;
    //		}

    //		if (exp is IdExpNode) {
    //			var id = exp as IdExpNode;
    //			var vr = getVar (id.ID);
    //			if(vr == null)
    //				throw new ParseException(ref id.ID, "is not defined");
    //			return vr.value;
    //		}

    //		if (exp is FncCallExpNode) {
    //			var fncall = exp as FncCallExpNode;
    //			if (funcs.ContainsKey (fncall.ID.lexeme)) {
    //				var fnc = funcs [fncall.ID.lexeme];
    //				if (fnc.paramList.Count != fncall.args.Count)
    //					throw new ParseException (ref fncall.ID, "wrong number of params");
    //				for (int i = 0; i < fnc.paramList.Count; i++) {
    //					float value = evaluateExp (fncall.args [i]);
    //					vars.Push (new Variable (fnc.paramList [i].lexeme, value));
    //				}
    //				float val = evaluateExp (fnc.exp);
    //				for (int i = 0; i < fnc.paramList.Count; i++)
    //					vars.Pop ();
    //				return val;
    //			}else if(nativefuncs.ContainsKey (fncall.ID.lexeme)){
    //				var fnc = nativefuncs [fncall.ID.lexeme];
    //				if (fncall.args.Count != 1)
    //					throw new ParseException (ref fncall.ID, "wrong number of params");
    //				return fnc.Call (evaluateExp (fncall.args [0]));
    //			}
    //			throw new ParseException(ref fncall.ID, "function is not declared");
    //		}



    //		if (exp is CompExpNode) {
    //			var cexp = exp as CompExpNode;
    //			float left = evaluateExp (cexp.left);
    //			float right = evaluateExp (cexp.right);

    //			switch (cexp.OP.token) {
    //			case Token.ADD:
    //				return left + right;
    //			case Token.SUB:
    //				return left - right;
    //			case Token.MUL:
    //				return left * right;
    //			case Token.DIV:
    //				if(right == 0)
    //					throw new ParseException(ref cexp.OP, "division by zero");
    //				return left / right;
    //			case Token.POW:
    //				return (float)Math.Pow(left,  right);
    //			default:
    //				break;
    //			}
    //		}
    //		return 0;
    //	}

    //	public void clean(){
    //		nativefuncs.Clear ();
    //		vars.Clear ();
    //		funcs.Clear ();

    //		nativefuncs ["cos"] = new FncNativeStmtNode(x => (float)Math.Cos(x));
    //		nativefuncs ["sin"] = new FncNativeStmtNode(x => (float)Math.Sin(x));
    //		nativefuncs ["sqrt"] = new FncNativeStmtNode(x => (float)Math.Sqrt(x));
    //		nativefuncs ["tan"] = new FncNativeStmtNode(x => (float)Math.Tan(x));
    //		nativefuncs ["ln"] = new FncNativeStmtNode(x => (float)Math.Log(x));
    //		nativefuncs ["log"] = new FncNativeStmtNode(x => (float)Math.Log10(x));
    //		nativefuncs ["abs"] = new FncNativeStmtNode(x => (float)Math.Abs(x));

    //		vars.Push (new Variable("pi",(float)Math.PI, true));
    //		vars.Push (new Variable("e",(float)Math.E, true));
    //	}
    //}
}

