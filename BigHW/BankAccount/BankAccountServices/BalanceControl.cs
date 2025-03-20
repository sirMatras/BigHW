using System;
using BigHW.BankAccount;
using BigHW.BankAccount;
using BigHW.Category;
using BigHW.Operation;

namespace BigHW
{
    public interface IBalanceControl
    {
        void Deposit(BankAccount.BankAccount receipter, float money);
        void Withdraw(BankAccount.BankAccount sender, float money);
    }

    public class BalanceControl : IBalanceControl
    {
        public void Deposit(BankAccount.BankAccount receipter, float money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Сумма пополнения должна быть положительной.");
            }

            float oldBalance = receipter.Balance;
            receipter.Balance += money;
            
            receipter.Notify(oldBalance, receipter.Balance);

            var history = Factory.CreateOperation("Income", receipter, money, "");
            receipter._balanceHistory.Add(history);
            Console.WriteLine($"Баланс аккаунта {receipter.Name} пополнен на сумму {money}.");
        }

        public void Withdraw(BankAccount.BankAccount sender, float money)
        {
            if (sender.Balance < money)
            {
                throw new ArgumentException("На аккаунте недостаточно средств для вывода.");
            }

            float oldBalance = sender.Balance;
            sender.Balance -= money;
            sender.Notify(oldBalance, sender.Balance);
            
            var history = new Operation.Operation()
            {
                Type = "Expense",
                BankAccountId = sender.Id,
                Amount = money,
                Date = DateTime.Now
            };
            sender._balanceHistory.Add(history);
            
            Console.WriteLine($"Списана сумма {money} из банковского аккаунта {sender.Name}.");
        }
    }
}