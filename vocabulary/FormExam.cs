using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace vocabulary
{
    public partial class FormExam : Form
    {
        Form formtemp = new Form();
        int current=0;
        List<int> index;
        mp3player mp3 = new mp3player();
        bool mp3set = true;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void _initialize()
        {
            index = new List<int>();
            for (int i = 0; i < FormMain.words.Count; i++)
                index.Add(i);
            index.Shuffle();
        }
        private void output()
        {
            //System.Threading.Thread.Sleep(1000);
            richTextBox1.Clear();
            richTextBox2.Clear();
            
            if (current == FormMain.words.Count)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                MessageBox.Show("所有单词已听写");
                Thread.Sleep(1000);
           
            }
            else
            {
                richTextBox2.Focus();
                richTextBox1.Text = FormMain.definitions[index[current]];
                string filepath = config.mp3_dir + FormMain.words[index[current]] + ".mp3";
                if(mp3set)
                    mp3.Play(filepath,1);
                current++;
            }
        }
        private void judge_input()
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            string word_input = richTextBox2.Text;
            if (word_input.ToLower() == FormMain.words[index[current - 1]].ToLower())//仅比较小写
                pictureBox1.Visible = true;
            else
            {
                label3.Visible = true;
                richTextBox3.Visible = true;
                richTextBox3.Text = FormMain.words[index[current - 1]];
                pictureBox2.Visible = true;
            }
        }
        public FormExam(Form form)
        {
            InitializeComponent();
            _initialize();
            formtemp = form;
            button4.Visible = false;
            label3.Visible = false;
            this.richTextBox3.Visible = false;
            this.richTextBox2.Focus();
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.button3.Visible = true;
            this.button5.Visible = false;
            output();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            judge_input();
            button2.Visible = false;
            button5.Visible = true;
            button5.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            formtemp.Show();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void FormExam_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            button3.Visible = false;
            mp3set = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button4.Visible = false;
            mp3set = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            button2.Visible = true;
            button5.Visible = false;
            label3.Visible = false;
            richTextBox3.Visible = false;
            richTextBox3.Clear();
            output();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
