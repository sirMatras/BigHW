using System;

namespace BigHW
{
    public class BalanceChangeLogger : IBalanceObserver
    {
        public void OnBalanceChanged(BankAccount.BankAccount account, float oldBalance, float newBalance)
        {
            Console.WriteLine($"[LOG] Баланс счета '{account.Name}' изменился с {oldBalance} на {newBalance}.");
        }
    }
}