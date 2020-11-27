using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceAnalysis.Db
{
    class SettingsContext : DbContext
    {
        public DbSet<Parser> Parsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TraceAnalysisModule");
        }
        public void InitDb()
        {
            var parsers = Parsers.ToList();
            if (!parsers.Any())
            {
                var parser = new Parser { Name = "Demo Parser", Pattern = "Access denied", Type = "MyClass" };
                Parsers.Add(parser);
                SaveChanges();
            }
        }
    }
}
