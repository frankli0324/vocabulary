using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace vocabulary
{
    class OnlineTranslate
    {
        public static string translate(string word)
        {
            string retString = "";
            try
            {
                HttpWebRequest req = WebRequest.Create($@"http://fanyi.youdao.com/translate?&doctype=json&type=AUTO&i={word}") as HttpWebRequest;
                req.Host = "fanyi.youdao.com";

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception e)
            {
                Console.Write("请检查网络连接");
                return retString;
            }

            retString = retString.Substring(retString.IndexOf("\"tgt\":\"") + 7);
            retString = retString.Substring(0, retString.IndexOf("\"}]"));
            return retString;
        }
    }
}
