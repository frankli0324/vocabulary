using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Windows;
using System.Threading;
namespace vocabulary
{
    public partial class FormManInput : Form
    {
        public static FormManInput newform2;
        Form formtemp = new Form();
        mp3player mp3 = new mp3player();
        public FormManInput(Form parentform)
        {
            InitializeComponent();
            newform2 = this;
            formtemp = parentform;
            button4.Visible = false;
            button2.Visible = true;
            button6.Visible = false;
            label3.Visible = false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private static void Threadsleep()
        {
            string def = (newform2.richTextBox1.Text);
            newform2.richTextBox2.Text = def;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMain.CommitChangesToDisk();
            this.Close();
            formtemp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string voc;
            if (richTextBox1.Text == "") MessageBox.Show("请输入单词");
            else
            {
                voc = richTextBox1.Text;
                richTextBox2.Text = FormMain.AddWordToMemory(voc);
                string webUrl = "http://media.shanbay.com/audio/us/" + voc + ".mp3";
                bool judgeSuc=GetWebPr.getpron(webUrl, config.mp3_dir);
                if(judgeSuc==true)
                    button6.Visible = true;
                button4.Visible = true;
                button2.Visible = false;
                label3.Visible = true;
                button4.Focus();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FormMain.CommitChangesToDisk();
            FormMain.PullRecordFormDisk(FormMain.todayWordsDict);
            FormExam quiz = new FormExam(this);//memory
            this.Hide();
            quiz.Show(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;
            button6.Visible = false;
            richTextBox2.Clear();
            richTextBox1.Clear();
            richTextBox1.Focus();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filepath = config.mp3_dir + richTextBox1.Text + ".mp3";
            mp3.Play(filepath, 1);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
