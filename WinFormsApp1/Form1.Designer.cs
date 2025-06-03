namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            Nazwa = new DataGridViewTextBoxColumn();
            Skladniki = new DataGridViewTextBoxColumn();
            cena = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            usun = new ToolStripMenuItem();
            exit = new Button();
            label1 = new Label();
            backToMenuButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nazwa, Skladniki, cena });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.GridColor = SystemColors.ActiveBorder;
            dataGridView1.Location = new Point(36, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(843, 577);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            
            dataGridView1.RowValidating += dataGridView1_RowValidating;
            dataGridView1.ContextMenuStripChanged += usunToolStripMenuItem_Click;
            // 
            // Nazwa
            // 
            Nazwa.DataPropertyName = "nazwa";
            Nazwa.FillWeight = 200F;
            Nazwa.HeaderText = "Nazwa";
            Nazwa.Name = "Nazwa";
            Nazwa.Width = 200;
            // 
            // Skladniki
            // 
            Skladniki.DataPropertyName = "skladniki";
            Skladniki.FillWeight = 200F;
            Skladniki.HeaderText = "Skladniki";
            Skladniki.Name = "Skladniki";
            Skladniki.Width = 500;
            // 
            // cena
            // 
            cena.DataPropertyName = "cena";
            cena.FillWeight = 200F;
            cena.HeaderText = "Cena";
            cena.Name = "cena";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { usun });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 26);
            // 
            // usun
            // 
            usun.Name = "usun";
            usun.Size = new Size(180, 22);
            usun.Text = "toolStripMenuItem1";
            usun.Click += usunToolStripMenuItem_Click;
            // 
            // exit
            // 
            exit.Location = new Point(961, 81);
            exit.Name = "exit";
            exit.Size = new Size(130, 46);
            exit.TabIndex = 1;
            exit.Text = "Wyjdz";
            exit.UseVisualStyleBackColor = true;
            exit.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 18F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            label1.Location = new Point(81, 34);
            label1.Name = "label1";
            label1.Size = new Size(249, 34);
            label1.TabIndex = 2;
            label1.Text = "Edycja Menu Pizzeri";
            // 
            // backToMenuButton
            // 
            backToMenuButton.Location = new Point(961, 160);
            backToMenuButton.Name = "backToMenuButton";
            backToMenuButton.Size = new Size(130, 40);
            backToMenuButton.TabIndex = 3;
            backToMenuButton.Text = "Menu programu";
            backToMenuButton.UseVisualStyleBackColor = true;
            backToMenuButton.Click += backToMenuButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1198, 670);
            Controls.Add(backToMenuButton);
            Controls.Add(label1);
            Controls.Add(exit);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button exit;
        private DataGridViewTextBoxColumn Nazwa;
        private DataGridViewTextBoxColumn Skladniki;
        private DataGridViewTextBoxColumn cena;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem usun;
        private Label label1;
        private Button backToMenuButton;
    }
}
