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
    public partial class FormImportImage : Form
    {
        public string translation;
        string fileURL = "";
        Form formtemp;
        List<string> reconResult;
        mp3player mp3 = new mp3player();
        public FormImportImage(Form parentform)
        {
            InitializeComponent();
            formtemp = parentform;
            button3.Visible = false;
            button7.Visible = false;
            button1.Visible = true;
            label5.Visible = false;
        }
        private void InitCombox()
        {
            for(int i = 0; i < reconResult.Count(); i++)
            {
                comboBox1.Items.Add(reconResult[i]);
            }
            if(reconResult.Count()==1)
                comboBox1.SelectedItem = reconResult[0];
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string voc = comboBox1.Text.ToString();
            try
            {
                //performTranslate(comboBox1.SelectedItem.ToString());
                richTextBox2.Text = FormMain.AddWordToMemory(voc);
                if (richTextBox2.Text != "")
                {
                    string webUrl = "http://media.shanbay.com/audio/us/" + voc + ".mp3";
                    bool judgeSuc=GetWebPr.getpron(webUrl, config.mp3_dir);
                    if (judgeSuc == true)
                        button7.Visible = true;
                    button1.Visible = false;
                    button3.Visible = true;
                    label5.Visible = true;
                    button3.Focus();
                }
                else
                    throw new Exception();
            }
            catch (Exception )
            {
                MessageBox.Show("请选择需要翻译的单词");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMain.CommitChangesToDisk();
            this.Close();
            formtemp.Show();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            comboBox1.Text = "";
            button7.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            label5.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";    //打开对话框后的初始目录
            fileDialog.Filter = "image|*.jpg|image|*.png|所有文件|*.*";
            fileDialog.RestoreDirectory = false;    //若为false，则打开对话框后为上次的目录。若为true，则为初始目录
            if (fileDialog.ShowDialog() == DialogResult.OK)
                fileURL = System.IO.Path.GetFullPath(fileDialog.FileName);// + fileDialog.SafeFileName;
            richTextBox1.Text = fileURL;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int flag = 1;
            try
            {
                reconResult = ImageRecon.ReconWordsFromImage(fileURL);
            }
            catch (Exception)
            {
                flag = 0;
                MessageBox.Show("获取图像内容失败");
            }
            if (flag==1)
                InitCombox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormMain.CommitChangesToDisk();
            FormMain.PullRecordFormDisk(FormMain.todayWordsDict);
            FormExam quiz = new FormExam(this);
            this.Hide();
            quiz.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filepath= config.mp3_dir +comboBox1.Text + ".mp3";
            mp3.Play(filepath, 1);
        }
    }
}
