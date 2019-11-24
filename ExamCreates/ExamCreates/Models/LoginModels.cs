using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.Models
{
    public class LoginModels
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100), Required]
        public string User { get; set; }
        [StringLength(100), Required]
        public string Password { get; set; }
        public virtual List<ExamModels> Exams { get; set; }

    }
}
