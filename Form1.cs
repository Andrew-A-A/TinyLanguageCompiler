using System;
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
            tokenListGridView.Rows.Clear();
            errorsListGridView.Rows.Clear();
            listBox1.Items.Clear();
            treeView1.Nodes.Clear();

            //Get the Source code from the text box
            var code = codeTextBox.Text;

            //Declare scanner object
            var scanner=new Scanner();

            //Start scanning the code
            scanner.Scan(code);

            /*
             * Show the results of scanning by loop on the tokens list to
             * show all detected tokens in the tokens table
             */
            for (var i = 0; i < scanner.Tokens.Count; i++)
            {
                tokenListGridView.Rows.Add(scanner.Tokens.ElementAt(i).Lexeme, scanner.Tokens.ElementAt(i).TokenType);
            }
            /*
              * loop on the errors list to
              * show all errors in the errors table
              */
            for (var j = 0; j < scanner.ErrorsList.Count; j++)
            {
                if (scanner.ErrorsList.ElementAt(j)!="\n")
                {
                    errorsListGridView.Rows.Add(scanner.ErrorsList.ElementAt(j));
                }
               
            }

            Parser ps = new Parser();
            ps.parsing(scanner.Tokens);
            if (ps.ll.Items.Count > 0) listBox1.Items.Add(ps.ll.Items[0]);
            //this.treeView1  = (TreeNode) treeView1.Clone();
            treeView1.Nodes.Add(ps.root);
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
            errorsListGridView.Rows.Clear();
            listBox1.Items.Clear();
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
    }
}
