using SpringHeroBank.Data;
using SpringHeroBank.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SpringHeroBank.Service
{
    public class TransactionService
    {
        private BankDbContext db;
        public TransactionService()
        {
            db = new BankDbContext();
        }
        public Transaction Transfer(string Token, int ReceiverAccountNumber, double TransferAmount) {
            var account = db.Accounts.Where(s => s.Token.Equals(Token)).FirstOrDefault();
            var accountReceiver = db.Accounts.Where(s => s.AccountNumber.Equals(ReceiverAccountNumber)).FirstOrDefault();
            if(accountReceiver != null)
            {
                if(account.Balance < TransferAmount)
                {
                    return null;
                } else
                {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try {
                            account.Balance -= TransferAmount;
                            accountReceiver.Balance += TransferAmount;

                            var tsn = new Transaction() {
                                TransactionCode = Guid.NewGuid().ToString(),
                                Amount = TransferAmount,
                                SenderAccountNumber = account.AccountNumber,
                                ReceiverAccountNumber = accountReceiver.AccountNumber,
                                Message = "Transfer",
                                CreatedAt = DateTime.Now,
                                Type = 3
                            };

                            db.Entry(account).State = EntityState.Modified;
                            db.Entry(accountReceiver).State = EntityState.Modified;
                            db.Transactions.Add(tsn);
                            db.SaveChanges();
                            transaction.Commit();
                            return new Transaction()
                            {
                                TransactionCode = tsn.TransactionCode,
                                Amount = tsn.Amount,
                                SenderAccountNumber = tsn.SenderAccountNumber,
                                ReceiverAccountNumber = tsn.ReceiverAccountNumber,
                                Message = tsn.Message,
                                CreatedAt = tsn.CreatedAt,
                                Type = tsn.Type,
                            };
                        } catch(Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            return null;
        }
        public Transaction Withdrawal(string Token, double Amount) {
            var account = db.Accounts.Where(s => s.Token.Equals(Token)).FirstOrDefault();
            if (account != null)
            {
                if(account.Balance >= Amount) {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            account.Balance -= Amount;
                            var tsn = new Transaction()
                            {
                                TransactionCode = Guid.NewGuid().ToString(),
                                Amount = Amount,
                                SenderAccountNumber = account.AccountNumber,
                                ReceiverAccountNumber = account.AccountNumber,
                                Message = "WithDraw",
                                CreatedAt = DateTime.Now,
                                Type = 1,
                            };
                            db.Entry(account).State = EntityState.Modified;
                            db.Transactions.Add(tsn);
                            db.SaveChanges();
                            transaction.Commit();
                            return new Transaction()
                            {
                                TransactionCode = tsn.TransactionCode,
                                Amount = tsn.Amount,
                                SenderAccountNumber = tsn.SenderAccountNumber,
                                ReceiverAccountNumber = tsn.ReceiverAccountNumber,
                                Message = tsn.Message,
                                CreatedAt = tsn.CreatedAt,
                                Type = tsn.Type,
                            };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
                return null;
            }
            return null;

        }
        public Transaction Deposit(string Token, double Amount) {
            Debug.WriteLine(Token);
            Debug.WriteLine(Amount);
            var account = db.Accounts.Where(s => s.Token.Equals(Token)).FirstOrDefault();
            Debug.WriteLine(account.Balance);       
            if (account != null)
            {
               /* using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {*/
                    try
                    {
                        // Tất cả bắt đầu từ đây
                        // Account
                        account.Balance = account.Balance + Amount;
                        db.Accounts.AddOrUpdate(account);

                        Transaction transactionHistory = new Transaction
                        {
                            SenderAccountNumber = account.AccountNumber,
                            ReceiverAccountNumber = account.AccountNumber,
                            Type = 1,
                            Amount = Amount,
                            Message = "Deposit"
                        };
                        db.Transactions.AddOrUpdate(transactionHistory);
                        db.SaveChanges();
                        /*transaction.Commit();*/
                        return transactionHistory;
                    }
                    catch (Exception ex)
                    {
                        /*transaction.Rollback();*/
                        Console.WriteLine("Error occurred.");
                    }
               /* }*/
            }
            return null;
        }
    }
}