using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OnSolveAutomationProject.Model
{
    public class FormSiteAPI
    {
        private XDocument _xmlDocResult;

        private string _apiUrl = "https://fs28.formsite.com/api/users/ecnvietnam/forms/form1/";
        private string _apiKey = "Qm8nO3h6auh7";

        public FormSiteAPI()
        {
           
        }

        public List<FormData> GetResultDataByMinDateTime(string dateTimeMin)
        {
            List<FormData> fDatas = new List<FormData>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_apiUrl  + "/results" + "?fs_api_key=" + _apiKey + "&fs_min_date=" + dateTimeMin);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string content = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }

            _xmlDocResult = XDocument.Parse(content);

            List<string> datas = (from i in _xmlDocResult.Descendants("item")
                                            select i.Value).ToList();
            
            for (int i = 0; i < datas.Count; i++)
            {
                FormData data = new FormData();
                data.FirstName = datas[i];
                data.LastName = datas[++i];
                data.StreetAddress = datas[++i];
                data.AddressLine2 = datas[++i];
                data.City = datas[++i];
                data.State = datas[++i];
                data.ZipCode = datas[++i];
                data.PhoneNumber = datas[++i];
                data.EmailAddress = datas[++i];
                data.Date = datas[++i];
                fDatas.Add(data);
            }

            return fDatas;
        }

    }
}
