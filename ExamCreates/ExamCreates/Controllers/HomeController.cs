using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamCreates.Models;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using ExamCreates.ViewModels;
using Newtonsoft.Json;
using ExamCreates.Context;

namespace ExamCreates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ExamContext context;
        private Uri url;
        private WebClient client;
        private HtmlDocument document;
        private string html;
        public HomeController(ILogger<HomeController> logger, ExamContext context)
        {
            _logger = logger;
            this.context = context;
        }
        private string user_id;
        [HttpGet]
        public ActionResult ShowExamList()
        {
            List<ExamModels> examModels = new List<ExamModels>();
            user_id = HttpContext.Session.GetString("user_id");
            int id = -1;
            if (Int32.TryParse(user_id, out id))
               examModels=context.Exammodel.Where(x => x.UserID == id).ToList();
            return View(examModels);
        }
        [HttpGet]
        public ActionResult ExamDelete(int id)
        {
            context.Questionsmodels.Remove(context.Questionsmodels.Where(x => x.ExamID == id).FirstOrDefault());
            context.SaveChanges();
            context.Exammodel.Remove(context.Exammodel.Where(x => x.ID == id).FirstOrDefault());
            context.SaveChanges();
            return RedirectToAction("ShowExamList", "Home");
        }

        private List<Article> Get_Articles()
        {
            List<Article> articles = new List<Article>();
            try
            {
                url = new Uri("https://www.wired.com/");
                client = new WebClient();
                client.Encoding = Encoding.UTF8;
                html = client.DownloadString(url);
                document = new HtmlDocument();
                document.LoadHtml(html);
                HtmlNode node = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[3]/div/div/div[2]/div[1]/div/div[2]/aside/div[2]/div/div/ul");
                for (int i = 0; i < 5; i++)
                {
                    string title = node.SelectSingleNode("/html/body/div[3]/div/div[3]/div/div/div[2]/div[1]/div/div[2]/aside/div[2]/div/div/ul/li[" + (i + 1) + "]/a/div[2]/h5").InnerText;
                    string link = node.SelectSingleNode("/html/body/div[3]/div/div[3]/div/div/div[2]/div[1]/div/div[2]/aside/div[2]/div/div/ul/li[" + (i + 1) + "]/a").GetAttributeValue("href", string.Empty);
                    articles.Add(new Article(title, link));
                }
                return articles;
            }
            catch
            {
                return articles;
            }

        }
        
            [HttpPost]
        public string[] ExamCheck([FromBody]string question_id)
        {
            try
            {
                string[] question = JsonConvert.DeserializeObject<string[]>(question_id);
                string[] answers = new string[question.Length];

                for (int i = 0; i < question.Length; i++)
                {
                    int answer_id = Convert.ToInt32(question[i]);
                    answers[i] = context.Questionsmodels.Where(x => x.ID == answer_id).FirstOrDefault().DC;
                }
                return answers;
            }

            catch
            {
                return new string[] {"" };

            }
        }
        [HttpPost]
        public string ExamCreate(string link)
        {
            try
            {
                url = new Uri(link);
                client = new WebClient();
                client.Encoding = Encoding.UTF8;
                html = client.DownloadString(url);
                document = new HtmlDocument();
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
        [HttpPost]

        public ActionResult ExamSave(IFormCollection form)
        {
            ExamShowModels examShowModels = new ExamShowModels();
            examShowModels.question_id = new List<string>();
          try {
               
                string[] sık = { "A", "B", "C", "D", "DC" };
                ExamListModels exam = new ExamListModels();
                exam.insertlist = new List<ExamInsertModels>();
                for (int i = 0; i < 4; i++)
                {
                    exam.title = form["title"];
                    exam.detail = form["detail"];
                    exam.insertlist.Add(new ExamInsertModels() { question = form["question" + (i + 1)], A = form[sık[0] + (i + 1)], B = form[sık[1] + (i + 1)], C = form[sık[2] + (i + 1)], D = form[sık[3] + (i + 1)], DC = form[sık[4] + (i + 1)] });
                }
                user_id = HttpContext.Session.GetString("user_id");
                int validation_check = 0;
                foreach (ExamInsertModels data in exam.insertlist)
                    if (data.question.Length > 0 && data.A.Length > 0 && data.B.Length > 0 && data.C.Length > 0 && data.D.Length > 0 && data.DC.Length > 0)
                        validation_check++;
                if (validation_check == exam.insertlist.Count)
                {
                    int newexam_id = -1;
                    ExamModels newexam = new ExamModels() { Title = exam.title, Detail = exam.detail, Dater = DateTime.Now.Date.ToShortDateString(), UserID = Convert.ToInt32(user_id) };
                    context.Exammodel.Add(newexam);
                    if (context.SaveChanges() > 0)
                    { 
                        newexam_id = newexam.ID;
                        foreach (ExamInsertModels data in exam.insertlist)
                        {
                            QuestionsModels qm = new QuestionsModels() { question = data.question, A = data.A, B = data.B, C = data.C, D = data.D, DC = data.DC, ExamID = newexam_id };
                            context.Questionsmodels.Add(qm);
                            context.SaveChanges();
                            examShowModels.question_id.Add(qm.ID+"");
                        }
                       
                        examShowModels.ExamListModels = exam;

                        return View(examShowModels);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
           
            return View(examShowModels);
        }
      
         
        public IActionResult Index()
        {
            user_id = HttpContext.Session.GetString("user_id");
            if (user_id == null)
                return RedirectToAction("Index", "Login");
           

            return View(Get_Articles());
        }
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
