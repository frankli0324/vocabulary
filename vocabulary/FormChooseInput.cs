using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vocabulary
{
    public partial class FormChooseInputMethod : Form
    {
        Form formtemp;
        public FormChooseInputMethod(Form parentform)
        {
            InitializeComponent();
            formtemp = parentform;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormImportImage newformIR = new FormImportImage(this);
            this.Hide();
            newformIR.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormManInput newform1 = new FormManInput(this);
            this.Hide();
            newform1.Show();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            formtemp.Show();
        }
    }
}
