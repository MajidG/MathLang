using MathLib;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            textBox2.Text = "";
            Parser parser = new Parser();
            try
            {
                var syms = treeView1.Nodes.Add("Syms");
                var kw = syms.Nodes.Add("keywords");
                var nums = syms.Nodes.Add("numbers");
                var ids = syms.Nodes.Add("identifiers");
                var strs = syms.Nodes.Add("strings");
                var seps = syms.Nodes.Add("seperators");

                Lexer lex = new Lexer();
                lex.init(textBox1.Text);
                while(lex.Peek() != Token.EOF)
                {
                    switch (lex.Peek())
                    {
                        case Token.LET:
                        case Token.FNC:
                        case Token.PRT:
                            kw.Nodes.Add(lex.Take().lexeme);
                            break;
                        case Token.NUM:
                            nums.Nodes.Add(lex.Take().lexeme);
                            break;
                        case Token.ID:
                            ids.Nodes.Add(lex.Take().lexeme);
                            break;
                        case Token.STR:
                            strs.Nodes.Add(lex.Take().lexeme);
                            break;
                        case Token.EPS:
                            lex.Take();
                            break;
                        case Token.LPN:
                        case Token.RPN:
                        case Token.COM:
                        case Token.EQL:
                        case Token.ADD:
                        case Token.SUB:
                        case Token.MUL:
                        case Token.DIV:
                        case Token.POW:
                            seps.Nodes.Add(lex.Take().lexeme);
                            break;
                        case Token.EOL:
                        case Token.EOF:
                            lex.Take();
                            break;
                        default:
                            lex.Take();
                            break;
                    }
                }


                var prg = parser.Parse(textBox1.Text);
                treeView1.Nodes.Add(prg.ToTreeNode());
                Symantic.Check(prg);
                foreach (var error in Symantic.errors)
                    textBox2.AppendText(error+Environment.NewLine);

            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }
    }
    }
