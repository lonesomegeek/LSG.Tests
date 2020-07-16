using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BenchTests.SqlInVersusOr.Models
{
    public class AccountBase
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
    }
}
