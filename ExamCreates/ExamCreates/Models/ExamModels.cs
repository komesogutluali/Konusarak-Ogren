using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.Models
{
    public class ExamModels
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100), Required]
        public string Title { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public string Dater { get; set; }
        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual LoginModels Loginmodels { get; set; }

        public virtual List<QuestionsModels> Questions { get; set; }
    }
}
