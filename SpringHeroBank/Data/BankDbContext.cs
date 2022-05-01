using SpringHeroBank.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SpringHeroBank.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext() : base("BankDbContext") {}

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}