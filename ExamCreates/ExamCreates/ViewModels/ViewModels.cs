using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.ViewModels
{
    public class ViewLoginModels
    {
        [StringLength(100), Required(ErrorMessage = "Kullanıcı Adını Giriniz.")]
        public string User { get; set; }
        [StringLength(100), Required(ErrorMessage = "Şifreyi Giriniz.")]
        public string Password { get; set; }
    }
}
