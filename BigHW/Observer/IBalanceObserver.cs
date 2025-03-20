namespace BigHW
{
    public interface IBalanceObserver
    {
        void OnBalanceChanged(BankAccount.BankAccount account, float oldBalance, float newBalance);
    }
}