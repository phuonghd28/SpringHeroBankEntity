using SpringHeroBank.Data;
using SpringHeroBank.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SpringHeroBank.Service
{
    public class AccountService
    {
        private BankDbContext db = new BankDbContext();
        public Account Register(Account account) {
            db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }
        public string Login(int AccountNumber, string Password) {
            var account = db.Accounts.Where(s => s.AccountNumber == AccountNumber && s.SecurityCode.Equals(Password)).FirstOrDefault();
            if(account != null)
            {
                var token = RandomToken();
                account.Token = token;
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();       
                return token;
            } else
            {
                return null;
            }
        
        }

        public Account FindAccount(string Token)
        {
            var account = db.Accounts.Where(s => s.Token.Equals(Token)).FirstOrDefault();
            return account;
        }

        public string RandomToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var token = new String(stringChars);
            return token;
        }
    }
}