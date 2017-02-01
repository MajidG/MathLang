using System;
using System.Collections.Generic;

namespace MathLib
{
    //abstract class BaseNode
    //{
    //    public List<BaseNode> subNodes = new List<BaseNode>();
    //}
    //class TerminalNode:BaseNode
    //{
    //    LexUnit unit;

    //    public TerminalNode()
    //    {
    //        unit = new LexUnit() { token = Token.EPS };
    //    }

    //    public TerminalNode(LexUnit unit)
    //    {
    //        this.unit = unit;
    //    }
    //}
    //class RuleNode : BaseNode
    //{
    //    string name;
    //    public RuleNode(string name)
    //    {
    //        this.name = name;
    //    }
    //}
    //class Rule
    //{
    //    public string name;
    //    public object[] prods;
    //    public Rule(string name, params object[] prods)
    //    {
    //        this.name = name;
    //        this.prods = prods;
    //    }        
    //}
    //class ParsingTable
    //{
    //    Dictionary<string, Dictionary<Token, Rule>> table = new Dictionary<string, Dictionary<Token, Rule>>();
    //    public Rule this[string rule, Token token]
    //    {
    //        get
    //        {
    //            return table[rule][token];
    //        }
    //        set
    //        {
    //            table[rule][token] = value;
    //        }
    //    }
    //}
    //class TableParser
    //{
    //    ParsingTable table = new ParsingTable();
    //    Lexer lex;
    //    public TableParser(string code)
    //    {
    //        AddRule("funcDec", Token.FNC, Token.FNC, Token.ID, Token.LPN, "params", Token.RPN
    //            , Token.EQL, "exp");
    //        AddRule("exp", Token.FNC, Token.FNC, Token.ID, Token.LPN, "params", Token.RPN
    //            , Token.EQL, "exp");
    //        lex = new Lexer();
    //        lex.init(code);
    //    }
    //    void AddRule(string name, Token first, params object[] prods)
    //    {
    //        table[name, first] = new Rule(name, prods);
    //    }
    //    public RuleNode BuildAST(Rule rule)
    //    {
    //        RuleNode node = new RuleNode(rule.name);
    //        foreach (var prod in rule.prods)
    //        {
    //            if (prod is Token)
    //            {
    //                if ((Token)prod == Token.EPS)
    //                    node.subNodes.Add(new TerminalNode());
    //                else
    //                {
    //                    Match((Token)prod, rule.name);
    //                    node.subNodes.Add(new TerminalNode(lex.Take()));
    //                }
    //            }
    //            else if (prod is string)
    //            {
    //                var subRule = table[prod as string, lex.Peek()];
    //                node.subNodes.Add(BuildAST(subRule));
    //            }
    //            else
    //                throw new Exception("not a token nor a rule");
    //        }
    //        return node;
    //    }
    //    void Match(Token expected, string msg)
    //    {
    //        if (lex.Peek() != expected)
    //            throw new Exception(string.Format("{0},{1}: Syntaxe error : expected '{2}' found '{3}' in " + msg,
    //                lex.unit.line, lex.unit.colmn, expected, lex.Peek()));
    //    }
    //}
}
