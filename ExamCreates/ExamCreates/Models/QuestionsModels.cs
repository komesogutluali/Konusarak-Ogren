using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.Models
{
    public class QuestionsModels
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100), Required]
        public string question { get; set; }
        [StringLength(100), Required]
        public string A { get; set; }
        [StringLength(100), Required]
        public string B { get; set; }
        [StringLength(100), Required]
        public string C { get; set; }
        [StringLength(100), Required]
        public string D { get; set; }
        [StringLength(100), Required]
        public string DC { get; set; }
        [Required]
        public int ExamID { get; set; }
        [ForeignKey("ExamID")]
        public virtual ExamModels exammodel { get; set; }
    }
}
