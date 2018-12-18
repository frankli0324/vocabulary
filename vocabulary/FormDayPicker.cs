using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace vocabulary
{ 
    public partial class FormDayPicker : Form
    {
        Form formtemp = new Form();
        public string[] getdate = new string[100000];
        public string[] getname = new string[100000];
        int num = 0;
        public void getaddress()
        {
            getdate= Directory.GetFiles(config.root_dir);
        }
        public void add()
        {
            for(int i = 0; i < num; i++)
            {
                this.comboBox3.Items.Add(getname[i]);
            }
            this.comboBox3.SelectedItem = getname[num - 1];

        }
        public void getnames()
        {
            for(int i = 0;i< getdate.Length ; i++)
            {
                getname[i] = System.IO.Path.GetFileNameWithoutExtension(getdate[i]);
                num = i;
            }
            num++;
        }
        public FormDayPicker(Form parentform)
        {
            InitializeComponent();
            formtemp = parentform;
            getaddress();
            getnames();
            add();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Form1.allWordsStream.Close();
            //string address = config.root_dir + comboBox3.SelectedItem + ".txt";
            //StreamReader sr = new StreamReader(address, Encoding.GetEncoding("GB2312"));
            //Form2.readtext(sr);
            XmlDocument thatDay = new XmlDocument();
            thatDay.Load($@"{config.root_dir}\{comboBox3.SelectedItem}.xml");
            FormMain.PullRecordFormDisk(thatDay);
            FormExam newform = new FormExam(formtemp);
            this.Close();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            formtemp.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
