using BigHW.BankAccount;

namespace BigHW
{
    public class BankAccountFacade
    {
        private readonly IBalanceControl _balanceControl;

        public BankAccountFacade(IBalanceControl balanceControl)
        {
            _balanceControl = balanceControl;
        }
        
        public void MakeTransaction(BankAccount.BankAccount sender, BankAccount.BankAccount receiver, float amount)
        {
            _balanceControl.Withdraw(sender, amount);
            _balanceControl.Deposit(receiver, amount);
        }
    }
}