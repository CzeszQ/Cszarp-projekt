using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }


        private void zamowieniaButton_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide(); 
            form3.ShowDialog(); 
            this.Show(); 
        }

        private void asortymentButton_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }
    }
}
