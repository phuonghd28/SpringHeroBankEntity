using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpringHeroBank.Model
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }
        public string SecurityCode { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }

    }
}