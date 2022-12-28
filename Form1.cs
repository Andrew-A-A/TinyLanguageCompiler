using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TinyLanguageComilerProject;

namespace TinyLanguageCompilerProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
       
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            //Clear the tables 
            textBox1.Text = "";
            TinyLanguageCompilerProject.TokenStream.Clear();
            TinyLanguageCompilerProject.Tiny_Scanner=new Scanner();
            TinyLanguageCompilerProject.Tiny_Parser=new Parser();

            treeView1.Nodes.Clear();
            tokenListGridView.Rows.Clear();

            //Get the Source code from the text box
            var code = codeTextBox.Text;

             TinyLanguageCompilerProject.Start_Compiling(code);

           

            



            /*
             * Show the results of scanning by loop on the tokens list to
             * show all detected tokens in the tokens table
             */
            for (var i = 0; i < TinyLanguageCompilerProject.Tiny_Scanner.Tokens.Count; i++)
            {
                tokenListGridView.Rows.Add(TinyLanguageCompilerProject.Tiny_Scanner.Tokens.ElementAt(i).Lex, TinyLanguageCompilerProject.Tiny_Scanner.Tokens.ElementAt(i).TokenType);
            }

            treeView1.Nodes.Add(Parser.PrintParseTree(TinyLanguageCompilerProject.treeroot));
            /*
              * loop on the errors list to
              * show all errors in the errors table
              */
            for (int i = 0; i < Errors.Errors_List.Count; i++)
            {
                textBox1.Text += Errors.Errors_List[i];
                textBox1.Text += "\r\n";
            }


            treeView1.ExpandAll();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void errorsListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Clear the tables 
            tokenListGridView.Rows.Clear();
            TinyLanguageCompilerProject.TokenStream.Clear();
            treeView1.Nodes.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
