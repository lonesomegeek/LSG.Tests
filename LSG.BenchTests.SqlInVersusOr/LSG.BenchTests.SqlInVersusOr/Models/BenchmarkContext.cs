using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BenchTests.SqlInVersusOr.Models
{
    public class BenchmarkContext : DbContext
    {
        //public BenchmarkContext(DbContextOptions<BenchmarkContext> options) : base(options) {}
        public DbSet<AccountWithoutIndex> AccountsWithoutIndex { get; set; }
        public DbSet<AccountWithIndex> AccountsWithIndex { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=LSG.BenchTests.SqlInVersusOr;Trusted_Connection=True;");
        }
    }
}
