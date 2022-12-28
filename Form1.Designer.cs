namespace TinyLanguageCompilerProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.codeTextBox = new System.Windows.Forms.RichTextBox();
            this.tokenListGridView = new System.Windows.Forms.DataGridView();
            this.Lexeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorsListGridView = new System.Windows.Forms.DataGridView();
            this.Errors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.scanBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.tokenListGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorsListGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // codeTextBox
            // 
            this.codeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextBox.Location = new System.Drawing.Point(2, 36);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(444, 482);
            this.codeTextBox.TabIndex = 0;
            this.codeTextBox.Text = "";
            this.codeTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // tokenListGridView
            // 
            this.tokenListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tokenListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lexeme,
            this.Token});
            this.tokenListGridView.Location = new System.Drawing.Point(465, 36);
            this.tokenListGridView.MultiSelect = false;
            this.tokenListGridView.Name = "tokenListGridView";
            this.tokenListGridView.ReadOnly = true;
            this.tokenListGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tokenListGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tokenListGridView.RowTemplate.Height = 24;
            this.tokenListGridView.Size = new System.Drawing.Size(474, 482);
            this.tokenListGridView.TabIndex = 1;
            this.tokenListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Lexeme
            // 
            this.Lexeme.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Lexeme.HeaderText = "Lexeme";
            this.Lexeme.MinimumWidth = 6;
            this.Lexeme.Name = "Lexeme";
            this.Lexeme.ReadOnly = true;
            // 
            // Token
            // 
            this.Token.HeaderText = "Token";
            this.Token.MinimumWidth = 6;
            this.Token.Name = "Token";
            this.Token.ReadOnly = true;
            this.Token.Width = 250;
            // 
            // errorsListGridView
            // 
            this.errorsListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorsListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Errors});
            this.errorsListGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.errorsListGridView.Location = new System.Drawing.Point(1024, 615);
            this.errorsListGridView.Name = "errorsListGridView";
            this.errorsListGridView.ReadOnly = true;
            this.errorsListGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red;
            this.errorsListGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.errorsListGridView.RowTemplate.Height = 24;
            this.errorsListGridView.Size = new System.Drawing.Size(345, 179);
            this.errorsListGridView.TabIndex = 2;
            this.errorsListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.errorsListGridView_CellContentClick);
            // 
            // Errors
            // 
            this.Errors.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Errors.DefaultCellStyle = dataGridViewCellStyle2;
            this.Errors.HeaderText = "Scanner errors";
            this.Errors.MinimumWidth = 6;
            this.Errors.Name = "Errors";
            this.Errors.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Code area :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(577, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tokens list :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 580);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Errors list :";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // scanBtn
            // 
            this.scanBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scanBtn.Location = new System.Drawing.Point(203, 524);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(243, 42);
            this.scanBtn.TabIndex = 6;
            this.scanBtn.Text = "Scan !";
            this.scanBtn.UseVisualStyleBackColor = true;
            this.scanBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(747, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 44);
            this.button1.TabIndex = 7;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TinyLanguageComilerProject.Properties.Resources.SL_112520_38300_04;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 567);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(945, 36);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(458, 558);
            this.treeView1.TabIndex = 9;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.Red;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(12, 612);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(976, 179);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 818);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.errorsListGridView);
            this.Controls.Add(this.tokenListGridView);
            this.Controls.Add(this.codeTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Tiny language scanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tokenListGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorsListGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox codeTextBox;
        private System.Windows.Forms.DataGridView tokenListGridView;
        private System.Windows.Forms.DataGridView errorsListGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button scanBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Errors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Token;
    }
}

