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
            dataGridView1 = new DataGridView();
            exit = new Button();
            backToMenuButton = new Button();
            label1 = new Label();
            Imie = new DataGridViewTextBoxColumn();
            Telefon = new DataGridViewTextBoxColumn();
            cena = new DataGridViewTextBoxColumn();
            data_zamowienia = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Imie, Telefon, cena, data_zamowienia, status });
            dataGridView1.GridColor = SystemColors.ActiveBorder;
            dataGridView1.Location = new Point(12, 75);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(895, 564);
            dataGridView1.TabIndex = 1;
            // 
            // exit
            // 
            exit.Location = new Point(951, 75);
            exit.Name = "exit";
            exit.Size = new Size(107, 46);
            exit.TabIndex = 2;
            exit.Text = "Wyjdz";
            exit.UseVisualStyleBackColor = true;
            exit.Click += exit_Click;
            // 
            // backToMenuButton
            // 
            backToMenuButton.Location = new Point(951, 156);
            backToMenuButton.Name = "backToMenuButton";
            backToMenuButton.Size = new Size(108, 50);
            backToMenuButton.TabIndex = 4;
            backToMenuButton.Text = "Menu programu";
            backToMenuButton.UseVisualStyleBackColor = true;
            backToMenuButton.Click += backToMenuButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 18F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            label1.Location = new Point(60, 25);
            label1.Name = "label1";
            label1.Size = new Size(219, 34);
            label1.TabIndex = 5;
            label1.Text = "Edycja zamówień ";
            // 
            // Imie
            // 
            Imie.DataPropertyName = "Imie";
            Imie.FillWeight = 200F;
            Imie.HeaderText = "Imie";
            Imie.Name = "Imie";
            Imie.Width = 200;
            // 
            // Telefon
            // 
            Telefon.DataPropertyName = "telefon";
            Telefon.FillWeight = 200F;
            Telefon.HeaderText = "Telefon";
            Telefon.Name = "Telefon";
            Telefon.Width = 200;
            // 
            // cena
            // 
            cena.DataPropertyName = "cena";
            cena.FillWeight = 200F;
            cena.HeaderText = "Cena";
            cena.Name = "cena";
            // 
            // data_zamowienia
            // 
            data_zamowienia.FillWeight = 250F;
            data_zamowienia.HeaderText = "data zamówienia";
            data_zamowienia.Name = "data_zamowienia";
            data_zamowienia.Width = 250;
            // 
            // status
            // 
            status.HeaderText = "status ";
            status.Name = "status";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 700);
            Controls.Add(label1);
            Controls.Add(backToMenuButton);
            Controls.Add(exit);
            Controls.Add(dataGridView1);
            Name = "Form3";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button exit;
        private Button backToMenuButton;
        private Label label1;
        private DataGridViewTextBoxColumn Imie;
        private DataGridViewTextBoxColumn Telefon;
        private DataGridViewTextBoxColumn cena;
        private DataGridViewTextBoxColumn data_zamowienia;
        private DataGridViewTextBoxColumn status;
    }
}