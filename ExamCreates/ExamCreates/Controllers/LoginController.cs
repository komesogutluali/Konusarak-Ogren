using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExamCreates.Context;
using ExamCreates.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamCreates.Controllers
{
    public class LoginController : Controller
    {
        private ExamContext examContext;
        private SHA1Managed sha1;
        public LoginController(ExamContext examContext)
        {
            this.examContext = examContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ViewLoginModels viewLoginModels)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    string sha_password = Sha_hash(viewLoginModels.Password);

                    object user_id = examContext.Loginmodel.Where(x => x.User == viewLoginModels.User && x.Password == sha_password).FirstOrDefault().ID;
                    if (user_id != null)
                    {

                        HttpContext.Session.SetString("user_id", user_id + "");
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            catch
            {
                ViewBag.Error = "Şifre Yada Kullanıcı Adı Yanlış";

            }
            return View();
        }

        private string Sha_hash(string password)
        {
            sha1 = new SHA1Managed();
            var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder(hashSh1.Length * 2);
            foreach (byte b in hashSh1)
                sb.Append(b.ToString("X2").ToLower());

            return sb.ToString();
        }
    }
}