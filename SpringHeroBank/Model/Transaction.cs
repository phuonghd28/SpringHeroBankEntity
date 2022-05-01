using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpringHeroBank.Model
{
    public class Transaction
    {
        [Key]
        public string TransactionCode { get; set; }
        public double Amount { get; set; }
        public int SenderAccountNumber { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}