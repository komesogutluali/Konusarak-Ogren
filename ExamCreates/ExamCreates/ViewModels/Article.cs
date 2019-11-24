using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamCreates.ViewModels
{
    public class Article
    {
        public string title;
        public string link;

        public Article(string title, string link)
        {
            this.title = title;
            this.link = link;
        }
        public string detail_get()
        {
            try
            {
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                HtmlNodeCollection collection = document.DocumentNode.SelectNodes("/html/body/div[1]/div/main/article/div[2]/div/div[1]/div[1]/div[1]");
                string detail = "";

                foreach (var col in collection)
                    detail += col.InnerText;
                return detail;
            }
            catch
            {
                return "";

            }
        }
    }
}
