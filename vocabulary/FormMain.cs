using System;
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

namespace vocabulary
{
    public partial class FormMain : Form
    {
        public string address;

        public static XmlDocument todayWordsDict;
        public static XmlDocument allWordsDict;

        public static List<string> words = new List<string>();
        public static List<string> definitions = new List<string>();
        public static int cnt = 0;

        public FormMain() {
            InitializeComponent();
            allWordsDict = new XmlDocument();
            todayWordsDict = new XmlDocument();
            string propath = @"C:\vocabulary\pronuncation";
            if (!Directory.Exists(config.root_dir))//如果路径不存在
            {
                Directory.CreateDirectory(config.root_dir);//创建一个路径的文件夹
            }
            if (!Directory.Exists(propath))
            {
                Directory.CreateDirectory(propath);
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }
        public static void PullRecordFormDisk(XmlDocument dictionary)
        {
            words.Clear(); definitions.Clear();
            try
            {
                XmlNode root = dictionary.SelectSingleNode("Dictionary");
                XmlNodeList entries = root.ChildNodes;
                foreach (XmlNode entry in entries)
                {
                    XmlAttributeCollection attributeCol = entry.Attributes;
                    foreach (XmlAttribute attribute in attributeCol)
                    {
                        if (attribute.Name == @"word")
                            words.Add(attribute.Value);
                        else if (attribute.Name == @"definition")
                            definitions.Add(attribute.Value);
                    }
                    if (words.Count != definitions.Count || entry.HasChildNodes)
                        throw new DataException("not valid dictionary");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
        }
        public static string AddWordToMemory(string word)//向内存中缓存将要输入的单词
        {
            string def = OnlineTranslate.translate(word);
            XmlElement entry = FormMain.todayWordsDict.CreateElement($@"entry{++cnt}");
            entry.SetAttribute(@"word", word);
            entry.SetAttribute(@"definition", def);
            FormMain.todayWordsDict.FirstChild.AppendChild(entry);
            entry = FormMain.allWordsDict.CreateElement($@"entry{cnt}");
            entry.SetAttribute(@"word", word);
            entry.SetAttribute(@"definition", def);
            FormMain.allWordsDict.FirstChild.AppendChild(entry);
            return def;
        }
        public static void CommitChangesToDisk()
        {
            FormMain.allWordsDict.Save($@"{config.root_dir}\Dict.xml");
            FormMain.todayWordsDict.Save($@"{config.root_dir}\{DateTime.Now.ToString("yyyy-MM-dd")}.xml");
        }

        private void button1_Click(object sender, EventArgs e)      //背诵今日单词
        {
            string date_str = DateTime.Now.ToString("yyyy-MM-dd");
            if (File.Exists($@"{config.root_dir}\{date_str}.xml"))
                todayWordsDict.Load($@"{config.root_dir}/{date_str}.xml");
            else
                todayWordsDict.AppendChild(todayWordsDict.CreateElement("Dictionary"));

            if (!File.Exists($@"{config.root_dir}\Dict.xml"))
                allWordsDict.AppendChild(allWordsDict.CreateElement("Dictionary"));
            else
            {
                allWordsDict.Load($@"{config.root_dir}/Dict.xml");
                cnt = allWordsDict.FirstChild.ChildNodes.Count;
            }
          

            FormChooseInputMethod newform = new FormChooseInputMethod(this);
            this.Hide();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)     //复习单日单词
        {
            FormDayPicker newform2 = new FormDayPicker(this);
            this.Hide();
            newform2.Show();
        }

        private void button3_Click(object sender, EventArgs e)     //复习所有单词
        {
            allWordsDict.Load($@"{config.root_dir}/Dict.xml");
            allWordsDict.Save($@"{config.root_dir}\Dict.xml");
            PullRecordFormDisk(allWordsDict);
            FormExam newform = new FormExam(this);
            this.Hide();
            newform.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
