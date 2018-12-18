using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vocabulary
{
    class ImageRecon
    {
        public static List<String> ReconWordsFromImage(String imageURL)
        {
            List<String> ret = new List<String>();
            var client = new Baidu.Aip.Ocr.Ocr(config.API_KEY, config.SECRET_KEY);
            client.Timeout = 60000;
            var image = File.ReadAllBytes(imageURL);
            JObject result = client.GeneralBasic(image);
            decimal wordsCnt = Convert.ToDecimal(result["words_result_num"]);
            if (wordsCnt < 1)
                throw new Exception("no words found");
            for (int i = 0; i < wordsCnt; i++)
                ret.Add(result["words_result"][i]["words"].ToString());
            return ret;
        }
    }
}
