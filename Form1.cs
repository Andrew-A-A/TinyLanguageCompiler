using System;
using System.Linq;
using System.Windows.Forms;

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

            //Get the Source code from the text box
            var code = codeTextBox.Text;

            //Declare scanner object
            var scanner=new Scanner(code);

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
            tokenListGridView.Rows.Clear();
            errorsListGridView.Rows.Clear();
            codeTextBox.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
