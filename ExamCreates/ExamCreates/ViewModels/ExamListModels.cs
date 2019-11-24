using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.ViewModels
{
    public class ExamListModels
    {
        public string title { get; set; }
        public string detail { get; set; }
        public List<ExamInsertModels> insertlist { get; set; }
    }
}
