using ExamCreates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCreates.Context
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {

        }
        public DbSet<LoginModels> Loginmodel { get; set; }
        public DbSet<ExamModels> Exammodel { get; set; }
        public DbSet<QuestionsModels> Questionsmodels { get; set; }
    }
}
