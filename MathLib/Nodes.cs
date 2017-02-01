using System.Collections.Generic;
using System.Windows.Forms;

namespace MathLib
{
    public abstract class Node
    {
        public abstract TreeNode ToTreeNode();
    }

    public class ProgNode : Node
    {
        public List<StmtNode> stmts = new List<StmtNode>();

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Prog :");
            foreach (var stmt in stmts)
                tree.Nodes.Add(stmt.ToTreeNode());
            return tree;
        }
    }

    public abstract class StmtNode : Node
    {
    }

    public class ExpStmtNode : StmtNode
    {
        public ExpNode exp;

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Exp :");
            tree.Nodes.Add(exp.ToTreeNode());
            return tree;
        }
    }

    public class CnstDecStmtNode : StmtNode
    {
        public LexUnit LET, ID, EQ;
        public ExpNode exp;

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Const :");
            tree.Nodes.Add("let");
            tree.Nodes.Add(ID.lexeme);
            tree.Nodes.Add("=");
            var expn =tree.Nodes.Add("exp :");
            expn.Nodes.Add(exp.ToTreeNode());
            return tree;
        }
    }

    public class FncDecStmtNode : StmtNode
    {
        public LexUnit FNC, ID, LP, RP, EQ;
        public List<LexUnit> paramList;
        public ExpNode exp;

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Func :");
            tree.Nodes.Add("fnc");
            tree.Nodes.Add("id = "+ID.lexeme);
            tree.Nodes.Add("(");
            var prms = tree.Nodes.Add("Prms :");
            foreach (var prm in paramList)
                prms.Nodes.Add("id = "+prm.lexeme);
            tree.Nodes.Add(")");
            tree.Nodes.Add("=");
            var expn = tree.Nodes.Add("exp :");
            expn.Nodes.Add(exp.ToTreeNode());            
            return tree;
        }
    }

    //public class FncNativeStmtNode : StmtNode
    //{
    //    public LexUnit ID;
    //    public delegate float NativeFunc(float x);
    //    NativeFunc native;

    //    public FncNativeStmtNode(NativeFunc func)
    //    {
    //        native = func;
    //    }
    //    public float Call(float value)
    //    {
    //        return native(value);
    //    }

    //    public override TreeNode ToTreeNode()
    //    {
    //        TreeNode tree = new TreeNode("function intern");
    //        tree.Nodes.Add(ID.lexeme);
    //        return tree;
    //    }
    //}

    public class PrintStmtNode : StmtNode
    {
        public LexUnit PRT;
        public List<ExpNode> exps = new List<ExpNode>();

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Print :");
            tree.Nodes.Add("print");
            var prms = tree.Nodes.Add("Exps :");
            foreach (var exp in exps)
                prms.Nodes.Add(exp.ToTreeNode());
            return tree;
        }
    }

    // expressions

    public abstract class ExpNode : Node
    {
    }

    public class NumExpNode : ExpNode
    {
        public LexUnit NUM;

        public NumExpNode(LexUnit num)
        {
            NUM = num;
        }

        public override TreeNode ToTreeNode()
        {
            return new TreeNode(NUM.lexeme);
        }
    }

    public class IdExpNode : ExpNode
    {
        public LexUnit ID;

        public IdExpNode(LexUnit id)
        {
            ID = id;
        }

        public override TreeNode ToTreeNode()
        {
            return new TreeNode(ID.lexeme);
        }
    }

    public class OpExpNode : ExpNode
    {
        public ExpNode left, right;
        public LexUnit OP;

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode(OP.lexeme);
            if (left != null)
                tree.Nodes.Add(left.ToTreeNode());
            if (right != null)
                tree.Nodes.Add(right.ToTreeNode());
            return tree;
        }
    }

    public class FncCallExpNode : ExpNode
    {
        public LexUnit ID;
        public List<ExpNode> args;

        public override TreeNode ToTreeNode()
        {
            TreeNode tree = new TreeNode("Call :");
            tree.Nodes.Add(ID.lexeme);
            tree.Nodes.Add("(");
            var argTree = tree.Nodes.Add("Args :");
            foreach (var arg in args)
            {
                var argn = argTree.Nodes.Add("exp :");
                argn.Nodes.Add(arg.ToTreeNode());
            }
            tree.Nodes.Add(")");
            return tree;
        }
    }
    public class StringExpNode : ExpNode
    {
        public LexUnit STR;
        public StringExpNode(LexUnit unit)
        {
            STR = unit;
        }

        public override TreeNode ToTreeNode()
        {
            return new TreeNode("\"" + STR.lexeme + "\"");
        }
    }
}
