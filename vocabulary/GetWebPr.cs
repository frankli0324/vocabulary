using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
namespace vocabulary
{
    class GetWebPr
    {
        public static bool getpron(string mp3url, string localpath)
        {
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(mp3url, localpath + System.IO.Path.GetFileName(mp3url));
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("未找到单词发音");
                return false;
            }
        }
    }
}
