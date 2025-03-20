using BigHW.BankAccountServices;
using BigHW.BankAccount;

namespace BigHW.UI
{
    public class BankAccountFacade
    {
        private IBalanceControl _balanceControl;

        public BankAccountFacade(IBalanceControl balanceControl)
        {
            _balanceControl = balanceControl;
        }

        public void MakeTransaction(BankAccount.BankAccount sender, BankAccount.BankAccount receipter, float money)
        {
            _balanceControl.Withdraw(sender, money);
            _balanceControl.Deposit(receipter, money);
        }
    }
}