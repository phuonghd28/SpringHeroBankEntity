using SpringHeroBank.Data;
using SpringHeroBank.Model;
using SpringHeroBank.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SpringHeroBank
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private BankDbContext db;


        private AccountService accountService;
        private TransactionService tsnService;

        public Service1()
        {
            accountService = new AccountService();
            tsnService = new TransactionService();
            db = new BankDbContext();
        }
        public Transaction Deposit(string Token, double amount)
        {
            /*Debug.WriteLine(Token);
            Debug.WriteLine(amount);
            var account = db.Accounts.Where(s => s.Token.Equals(Token)).FirstOrDefault();
            Debug.WriteLine(account.Balance);
            if (account != null)
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Tất cả bắt đầu từ đây
                        // Account
                        account.Balance = account.Balance + amount;
                        db.Accounts.AddOrUpdate(account);

                        Transaction transactionHistory = new Transaction
                        {
                            SenderAccountNumber = account.AccountNumber,
                            ReceiverAccountNumber = account.AccountNumber,
                            Type = 1,
                            Amount = amount,
                            Message = "Deposit"
                        };
                        db.Transactions.AddOrUpdate(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return transactionHistory;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }
            }
            return null;*/
            return tsnService.Deposit(Token, amount);
        }

        public Account FindAccount(string Token)
        {
            return accountService.FindAccount(Token);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string Login(int AccountNumber, string password)
        {
            return accountService.Login(AccountNumber, password);
        }

        public bool Register(Account account)
        {
            var item = accountService.Register(account);
            if (item != null)
            {
                return true;
            }
            return false;
        }

        public Transaction Transfer(string Token, double amount, int receiverAccountNumber)
        {
            return tsnService.Transfer(Token, receiverAccountNumber, amount);
        }

        public Transaction WithDraw(string Token, double amount)
        {
            return tsnService.Withdrawal(Token, amount);
        }
    }
}
