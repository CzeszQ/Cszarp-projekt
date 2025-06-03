namespace WinFormsApp1
{
    partial class Form2
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
            label1 = new Label();
            zamowieniaButton = new Button();
            asortymentButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            label1.Location = new Point(223, 65);
            label1.Name = "label1";
            label1.Size = new Size(670, 29);
            label1.TabIndex = 0;
            label1.Text = "Witamy w programie edycji zamówień oraz oferty pizzeri ";
            // 
            // zamowieniaButton
            // 
            zamowieniaButton.Location = new Point(287, 283);
            zamowieniaButton.Name = "zamowieniaButton";
            zamowieniaButton.Size = new Size(165, 78);
            zamowieniaButton.TabIndex = 1;
            zamowieniaButton.Text = "Edycja zamowien";
            zamowieniaButton.UseVisualStyleBackColor = true;
            zamowieniaButton.Click += zamowieniaButton_Click;
            // 
            // asortymentButton
            // 
            asortymentButton.Location = new Point(617, 284);
            asortymentButton.Name = "asortymentButton";
            asortymentButton.Size = new Size(142, 77);
            asortymentButton.TabIndex = 2;
            asortymentButton.Text = "Edycja asortymentu pizz";
            asortymentButton.UseVisualStyleBackColor = true;
            asortymentButton.Click += asortymentButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 664);
            Controls.Add(asortymentButton);
            Controls.Add(zamowieniaButton);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button zamowieniaButton;
        private Button asortymentButton;
    }
}