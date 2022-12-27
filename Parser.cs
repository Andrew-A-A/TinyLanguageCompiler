using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyLanguageCompilerProject;

namespace TinyLanguageComilerProject
{
    internal class Parser
    {
        public TreeNode root = new TreeNode("Parse Tree");
        public TreeNode statements = new TreeNode("Statements");
        public TreeNode functions = new TreeNode("Functions");
        public TreeNode cc1;
        Stack<char> parentheses = new Stack<char>();
        public List<TreeNode> children = new List<TreeNode>();
        public List<Token> list;
        public string myStart;
        public int temp = 0;
        public ListBox ll = new ListBox();

        public int ind = 0;
        public Parser() { } //constructor
        public void parsing(List<Token> tokenslist)
        {
            list = tokenslist;
            children.Clear();
            bool flag = false;
            for (int i = 0; i < tokenslist.Count; i++)
            {
                myStart = "";
                ind = i;
                if (tokenslist[i].TokenType == TokenClass.ReservedWordRead) {
                    flag = true;
                    read();
                }
                else if (tokenslist[i].TokenType == TokenClass.ReservedWordRepeat) {
                    repeat();
                    flag = true;
                }
                else if (tokenslist[i].TokenType == TokenClass.ReservedWordIf) { 
                    ifSatament();
                    flag = true; 
                }
                else if (tokenslist[i].TokenType == TokenClass.DataTypeInt && tokenslist[i+1].TokenType == TokenClass.ReservedWordMain) {
                    mainFunction();
                    flag = true;
                }
                else if (tokenslist[i].TokenType == TokenClass.LCurlyBraces) { 
                    functionBody();
                    flag = true;
                }
                else if (tokenslist[i].TokenType == TokenClass.ReservedWordWrite) { 
                    flag = true;
                    write();
                }
                else if (tokenslist[i].TokenType == TokenClass.ReservedWordReturn) {
                    flag = true; 
                    ritorno(); 
                }
                else if (tokenslist[i].TokenType == TokenClass.Identifier) { 
                    bool s = assignment();
                    if (!s) { children.Clear(); ind = temp; functionCall(); } 
                    else { treeprinter(root, children, "Assignment Statement"); ind--; flag = true; } }
                else if (tokenslist[i].TokenType == TokenClass.Number || tokenslist[i].TokenType == TokenClass.LeftParentheses) { flag = true; expression(); }
                else if (((tokenslist[i].TokenType == TokenClass.DataTypeFloat) || (tokenslist[i].TokenType == TokenClass.DataTypeInt) || (tokenslist[i].TokenType == TokenClass.DataTypeString)) && tokenslist[i + 1].TokenType == TokenClass.Identifier && tokenslist[i + 2].TokenType == TokenClass.LeftParentheses) {
                    flag = true; functionDec();
                    ind--;
                }
                else if (((tokenslist[i].TokenType == TokenClass.DataTypeFloat) || (tokenslist[i].TokenType == TokenClass.DataTypeInt) || (tokenslist[i].TokenType == TokenClass.DataTypeString))) { 
                    flag = true;
                    decStatment();
                }
                if (flag) i = ind;
                children.Clear();
            }

        }

        public bool assignment()
        {
            temp = ind;
            if (myStart == "") myStart = "assignment";
            bool c1 = match(TokenClass.Identifier);
            bool c2 = match(TokenClass.AssignOp);
            if (!c2 || !c1) { return false; }
            bool c3 = expression();

            bool c4 = match(TokenClass.Semicolon);
            return c1 && c2 && c3 && c4;
        }
        public bool match(TokenClass x)
        {
            if (ind < list.Count && list[ind].TokenType == x) { 
                children.Add(new TreeNode((list[ind].Lexeme.ToString() + " → " + list[ind].TokenType.ToString())));
                ind++; 
                return true; }
            return false;
        }

        public bool read()
        {

            if (myStart == "") myStart = "read";
            bool c1 = match(TokenClass.ReservedWordRead);
            bool c2 = match(TokenClass.Identifier);
            bool c3 = match(TokenClass.Semicolon);
            if (c1 && c2 && c3) {
                if (myStart != "read") return true; else { treeprinter(statements, children, "Read Statement"); ind--; return true; }}
            return false;
        }

        public bool ritorno()
        {
            if (myStart == "") myStart = "return";
            bool c1 = match(TokenClass.ReservedWordReturn);
            bool c2 = expression();
            bool c3 = match(TokenClass.Semicolon);
            if (c1 && c2 && c3) { if (myStart == "return") { treeprinter(statements, children, "Return Statement"); ind--; return true; } else return true; }
            return false;
        }

        public bool expression()
        {
            bool c1 = term();
            bool c2 = espressione();
            if (c1 || c2)
            {
                if (myStart == "assignment")
                { return true; }
                else if (myStart == "read") { treeprinter(statements, children, "Read Statement"); return true; }
                else if (myStart == "return") { return true; }
                else if (myStart == "repeat") return true;
                else if (myStart == "write") { return true; }
                else if (myStart == "if") { return true; }
                else
                { return true; }
            }
            return c1 || c2;
        }

        public bool espressione()
        {
            bool c1 = match(TokenClass.PlusOp) || match(TokenClass.MinusOp);
            bool c2 = term();
            bool c3 = false;
            if (c1 && c2)
            { c3 = espressione(); }
            return c1 && c2 && c3;
        }

        public bool write()
        {
            if (myStart == "") myStart = "write";
            bool c1 = match(TokenClass.ReservedWordWrite);
            bool c2 = expression();
            bool c3 = match(TokenClass.ReservedWordEndl);
            bool c4 = match(TokenClass.Semicolon);
            if ((c1 && c2 && c4) || (c1 && c3 && c4)) {
                if (myStart != "write")
                {
                    return true;
                }
                else
                {
                    treeprinter(statements, children, "Write Statement");
                    ind--;
                    return true;
                }
            }
            return ((c1 && c2 && c3) || (c3));
        }

        public bool factor()
        {
            bool c1 = match(TokenClass.LeftParentheses), c2 = false, c3 = false;
            if (parentheses.Count >= 1 && match(TokenClass.RightParentheses)) { parentheses.Pop(); c3 = true; }
            else if (c1)
            {
                parentheses.Push('(');
                c2 = expression();
                c3 = match(TokenClass.RightParentheses);
            }
            bool c4 = match(TokenClass.Number);
            bool c5 = match(TokenClass.DataTypeString);
            bool c6 = match(TokenClass.Identifier);
            bool c7 = functionCall();
            return (c1 && c2 && c3) || c4 || c5 || c6 || c7;
        }

        public bool functionDec()
        {
            bool c1 = match(TokenClass.DataTypeFloat) || match(TokenClass.DataTypeInt) || match(TokenClass.DataTypeString);
            bool c2 = match(TokenClass.Identifier);
            bool c3 = match(TokenClass.LeftParentheses);
            bool c4 = parameter();
            bool c6 = false;
            if (c4 == false) c6 = match(TokenClass.RightParentheses);
            while (c4)
            {
                bool c5 = match(TokenClass.Comma);
                if (c5) c4 = parameter();
                else { c6 = match(TokenClass.RightParentheses); c4 = false; }
            }
            //if(!c4 && c5) error
            if (c1 && c2 && c3 && c6) { treeprinter(functions, children, "Function Declaration"); return true; }
            else return false;

        }

        public bool parameter()
        {
            bool c1 = match(TokenClass.DataTypeFloat) || match(TokenClass.DataTypeInt) || match(TokenClass.DataTypeString);
            if (!c1) return false;
            bool c2 = match(TokenClass.Identifier);
            return c1 && c2;
        }
        public bool term()
        {
            bool c1 = factor();
            if (c1) return true;
            bool c2 = termine();
            return c1 || c2;
        }
        public bool termine()
        {
            bool c1 = match(TokenClass.MultiplyOp) || match(TokenClass.DivideOp) || match(TokenClass.PlusOp) || match(TokenClass.MinusOp);
            if (!c1) return false;
            bool c2 = factor();
            if (!c2) return false;
            bool c3 = false;
            if (c1 && c2) c3 = termine();
            return c1 && c2 && c3;
        }

        public bool functionCall()
        {
            bool c1 = match(TokenClass.Identifier);
            bool c2 = functionPart();
            if (c1 && c2) { treeprinter(functions, children, "Function Call"); return true; }
            else return false;


        }

        public bool functionPart()
        {
            bool c1 = match(TokenClass.LeftParentheses);
            bool c2 = match(TokenClass.Identifier);
            bool c4 = false;
            if (c2) while (c2)
                {
                    bool c3 = match(TokenClass.Comma);
                    if (c3) c2 = match(TokenClass.Identifier);
                    else { c4 = match(TokenClass.RightParentheses); c2 = false; }
                }
            else c4 = match(TokenClass.RightParentheses);

            return (c1 && c4);

        }

        public bool decStatment()
        {
            if (myStart == "") myStart = "declarationstatement";
            bool c1 = match(TokenClass.DataTypeFloat) || match(TokenClass.DataTypeInt) || match(TokenClass.DataTypeString);
            bool c2 = true;
            bool c3 = true;
            bool c4 = true;
            bool c9 = true;
            if (c2) while (c4)
                {
                    c2 = assignment();
                    c3 = match(TokenClass.Comma);
                    if (c3) c4 = true;
                    else c4 = false;
                }
            bool c5 = match(TokenClass.Semicolon);
            if (list[ind - 1].TokenType == TokenClass.Semicolon) c5 = true;
            if (c1 && !c3 && c5) { treeprinter(statements, children, "Declaration_Statement"); ind--; return true; }
            return c1 && !c3 && c5;

        }

        public bool ifSatament()
        {
            int temp = ind;
            if (myStart == "") myStart = "if";
            bool c1 = match(TokenClass.ReservedWordIf);
            bool c2 = conditionStatement();
            bool c3 = match(TokenClass.ReservedWordThen);
            bool c4 = true;
            int checker = 0;
            while (c4)
            {
                int tmp = ind;
                c4 = assignment();
                if (!c4) { 
                    ind = tmp; 
                    tmp = ind; 
                    c4 = read();
                }
                if (!c4) {
                    ind = tmp;
                    tmp = ind;
                    c4 = ritorno(); 
                }
                if (!c4) { 
                    ind = tmp;
                    tmp = ind;
                    c4 =write();
                }
                if (c4) 
                    checker++;
                //tmp++;
            }




            bool c5 = elseIf();
            bool c6 = elseStatement();
           bool c7 = match(TokenClass.ReservedWordEnd);
           // bool c7 = true;
            if ((c1 && c2 && c3 && checker>0) && (c5 || c6 || c7)) { treeprinter(statements, children, "If Statement"); ind--; return true; }
            return false;
        }

        public bool functionBody()
        {
            if (myStart == "") myStart = "functionBody";
            bool c1 = match(TokenClass.LCurlyBraces);
            int check = 0;
            bool c2 = true;
            int x = 0;
            while (c2)
            {
                int tmp = ind;
                c2 = write();
                if (!c2) { ind = tmp; tmp = ind; c2 = assignment(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = read(); }
                //if (!c2) { ind = tmp; tmp = ind; c2 = ifSatament(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = decStatment(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = ritorno(); if (c2) x++; }
                if (c2) check++;
            }
            bool c4 = match(TokenClass.RCurlyBraces);
            if (c1 && check > 0 && c4 && x < 2) { if (myStart == "functionBody") { treeprinter(functions, children, "Function Body"); ind--; return true; } else return true; }
            if (x >= 2) { ll.Items.Add("two return statements in same functionBody"); }
            if (x == 0) { ll.Items.Add("No return statements in functionBody"); }
            return c1 && check > 0 && c4 && x < 2;
        }

        public bool functionStatement()
        {
            bool c1 = functionDec();
            bool c2 = functionBody();
            if (c1 && c2) { treeprinter(statements, children, "Function Statement"); ind--; return true; }
            return c1 && c2;
        }

        public bool mainFunction()
        {
            if (myStart == "") myStart = "mainFunction";
            bool c0= match(TokenClass.DataTypeInt);
            bool c1 = match(TokenClass.ReservedWordMain);
            bool c2 = match(TokenClass.LeftParentheses);
            bool c3 = match(TokenClass.RightParentheses);
            bool c4 = functionBody();
            if (c0 &&c1 && c2 && c3 && c4) { treeprinter(root, children, "Main Function"); ind--; return true; }
            return c0 &&c1 && c2 && c3 && c4;
        }

        public bool elseIf()
        {
            bool c1 = match(TokenClass.ReservedWordElseIf);
            if (!c1) return false;
            bool c2 = conditionStatement();
            bool c3 = match(TokenClass.ReservedWordThen);
            bool c4 = write() || read() || ritorno() || assignment();
            bool c5 = true;// elseIf();
            bool c6 = elseStatement();
            bool c7 = match(TokenClass.ReservedWordEnd);
            return ((c1 && c2 && c3 && c4) && (c5 || c6 || c7));
        }

        public bool elseStatement()
        {
            bool c1 = match(TokenClass.ReservedWordElse);
            if (!c1) return false;
            bool c2 = true;
            int check = 0;
            while (c2)
            {
                int tmp = ind;
                c2 = write();
                if (!c2) { ind = tmp; tmp = ind; c2 = assignment(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = read(); }
                //if (!c2) { ind = tmp; tmp = ind; c2 = ifSatament(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = decStatment(); }
                if (c2) check++;
            }
            //bool c3 = match(Type.END);
            return c1 && check > 0;// && c3;
        }

        public bool repeat()
        {
            if (myStart == "") myStart = "repeat";
            bool c1 = match(TokenClass.ReservedWordRepeat);
            int check = 1;
            bool c2 = true;
            while (c2)
            {
                int tmp = ind;
                c2 = assignment();
                if (!c2) { ind = tmp; tmp = ind; c2 = write();  }
                if (!c2) { ind = tmp; tmp = ind; c2 = read(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = ifSatament(); }
                if (!c2) { ind = tmp; tmp = ind; c2 = decStatment(); }
                if (c2) check++;
            }
            bool c3 = match(TokenClass.ReservedWordUntil);
            bool c4 = conditionStatement();
            if (c1 && check > 0 && c3 && c4) { treeprinter(statements, children, "Repeat Statement"); ind--; return true; }
            return c1 && check > 0 && c3 && c4;
        }

        public bool conditionStatement()
        {
            bool c3 = false;
            bool c1 = condition();
            bool c2 = match(TokenClass.OrOp);
            if (c2) c3 = conditionTerm();
            else c3 = false;

            return ((c1 && c2 && c3) || c1);
        }
        public bool condition()
        {
            bool c1 = expression();
            bool c2 = match(TokenClass.GreaterThanOp) || match(TokenClass.LessThanOp) || match(TokenClass.Equal) || match(TokenClass.NotEqualOp);
            bool c3 = expression();
            return c1 && c2 && c3;
        }

        public bool conditionTerm()
        {
            bool c1 = conditionStatement();
            bool c2 = conditionTermine();
            return c1 && c2;

        }

        public bool conditionTermine()
        {
            bool c1 = match(TokenClass.AndOp);
            bool c2 = condition();
            return c1 && c2;
        }

        public bool statement()
        {
           bool c1= assignment();

            bool c2 = write();

            bool c3 = read();

            //bool c4 = ifSatament();

            return c1||c2||c3;

        }

        public void treeprinter(TreeNode rooter, List<TreeNode> tn, string child)
        {
            //statements.Remove();
            rooter = root;
            cc1 = new TreeNode(child);
            for (int i = 0; i < tn.Count; i++)
            {
                cc1.Nodes.Add(tn[i]);
            }
            rooter.Nodes.Add(cc1);
            //root.Nodes.Add(rooter);
            tn.Clear();
            children.Clear();
        }

    }
}
