using Bogus;
using LSG.BenchTests.SqlInVersusOr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;

namespace LSG.BenchTests.SqlInVersusOr
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfIterations = 1000;
            var numberOfElementsByIteration = 1000;

            var accountFaker = new Faker<AccountWithoutIndex>()
                .RuleFor(_ => _.Id, new Guid())
                .RuleFor(_ => _.FirstName, (f, a) => f.Name.FirstName())
                .RuleFor(_ => _.LastName, (f, a) => f.Name.LastName())
                .RuleFor(_ => _.Country, (f, a) => f.Address.Country());

            Console.WriteLine("Clearing all tables");
            using (var context = new BenchmarkContext())
            {
                context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.AccountsWithoutIndex");
                context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.AccountsWithIndex");
            }

            int n = 0;
            while (n++ < numberOfIterations)
            {
                using (var context = new BenchmarkContext())
                {
                    var accounts = accountFaker.Generate(numberOfElementsByIteration);
                    context.Set<AccountWithoutIndex>().AddRange(accounts);
                    context.SaveChanges();
                    Console.WriteLine($"Iteration {n}/{numberOfIterations}");
                }
            }

            Console.WriteLine("Copying data from table without index to table with an index");
            using (var context = new BenchmarkContext())
            {
                context.Database.ExecuteSqlRaw("INSERT INTO dbo.AccountsWithIndex SELECT * FROM dbo.AccountsWithoutIndex");
            }
        }
    }
}
