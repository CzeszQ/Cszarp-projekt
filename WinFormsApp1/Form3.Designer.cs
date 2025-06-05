namespace WinFormsApp1
{
    partial class Form3
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            usuń = new ToolStripMenuItem();
            backToMenuButton = new Button();
            exit = new Button();
            imie = new DataGridViewTextBoxColumn();
            numertelefonu = new DataGridViewTextBoxColumn();
            datazamowienia = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            nazwapizzy = new DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 18F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            label1.Location = new Point(72, 25);
            label1.Name = "label1";
            label1.Size = new Size(219, 34);
            label1.TabIndex = 5;
            label1.Text = "Edycja zamówień ";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { imie, numertelefonu, datazamowienia, status, nazwapizzy });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Location = new Point(12, 105);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1093, 489);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.RowValidating += dataGridView1_RowValidating;
            dataGridView1.ContextMenuStripChanged += usunToolStripMenuItem_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { usuń });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(101, 26);
            contextMenuStrip1.Text = "usun";
            // 
            // usuń
            // 
            usuń.Name = "usuń";
            usuń.Size = new Size(100, 22);
            usuń.Text = "usuń";
            usuń.Click += usunToolStripMenuItem_Click;
            // 
            // backToMenuButton
            // 
            backToMenuButton.Location = new Point(1196, 105);
            backToMenuButton.Name = "backToMenuButton";
            backToMenuButton.Size = new Size(121, 55);
            backToMenuButton.TabIndex = 7;
            backToMenuButton.Text = "Menu";
            backToMenuButton.UseVisualStyleBackColor = true;
            backToMenuButton.Click += backToMenuButton_Click;
            // 
            // exit
            // 
            exit.Location = new Point(1196, 190);
            exit.Name = "exit";
            exit.Size = new Size(121, 48);
            exit.TabIndex = 8;
            exit.Text = "wyjdź";
            exit.UseVisualStyleBackColor = true;
            exit.Click += exit_Click_Click;
            // 
            // imie
            // 
            imie.DataPropertyName = "imie";
            imie.HeaderText = "imie";
            imie.Name = "imie";
            imie.Width = 200;
            // 
            // numertelefonu
            // 
            numertelefonu.DataPropertyName = "numertelefonu";
            numertelefonu.HeaderText = "numertelefonu";
            numertelefonu.Name = "numertelefonu";
            numertelefonu.Width = 200;
            // 
            // datazamowienia
            // 
            datazamowienia.DataPropertyName = "datazamowienia";
            datazamowienia.HeaderText = "datazamowienia";
            datazamowienia.Name = "datazamowienia";
            datazamowienia.Width = 200;
            // 
            // status
            // 
            status.DataPropertyName = "status";
            status.HeaderText = "status";
            status.Name = "status";
            status.Width = 200;
            // 
            // nazwapizzy
            // 
            nazwapizzy.DataPropertyName = "nazwapizzy";
            nazwapizzy.HeaderText = "nazwapizzy";
            nazwapizzy.Name = "nazwapizzy";
            nazwapizzy.Resizable = DataGridViewTriState.True;
            nazwapizzy.SortMode = DataGridViewColumnSortMode.Automatic;
            nazwapizzy.Width = 250;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 700);
            Controls.Add(exit);
            Controls.Add(backToMenuButton);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private DataGridView dataGridView1;
        private Button backToMenuButton;
        private Button exit;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem usuń;
        private DataGridViewTextBoxColumn imie;
        private DataGridViewTextBoxColumn numertelefonu;
        private DataGridViewTextBoxColumn datazamowienia;
        private DataGridViewTextBoxColumn status;
        private DataGridViewComboBoxColumn nazwapizzy;
    }
}