using System;
using Xunit;
using BigHW.BankAccount;

namespace BigHW
{
    public class BalanceControlTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalanceAndAddHistory()
        {
            var account = Factory.CreateBankAccount("TestAccount", 100);
            var balanceControl = new BalanceControl();
            int initialHistoryCount = account._balanceHistory.Count;
            
            balanceControl.Deposit(account, 50);
            
            Assert.Equal(150, account.Balance);
            Assert.Equal(initialHistoryCount + 1, account._balanceHistory.Count);
            var op = account._balanceHistory[account._balanceHistory.Count - 1];
            Assert.Equal("Income", op.Type);
        }
        
        [Fact]
        public void Deposit_ShouldThrow_ForNonPositiveAmount()
        {
            var account = Factory.CreateBankAccount("TestAccount", 100);
            var balanceControl = new BalanceControl();
            Assert.Throws<ArgumentException>(() => balanceControl.Deposit(account, 0));
            Assert.Throws<ArgumentException>(() => balanceControl.Deposit(account, -10));
        }
        
        [Fact]
        public void Withdraw_ShouldDecreaseBalanceAndAddHistory()
        {
            var account = Factory.CreateBankAccount("TestAccount", 100);
            var balanceControl = new BalanceControl();
            int initialHistoryCount = account._balanceHistory.Count;
            
            balanceControl.Withdraw(account, 40);
            
            Assert.Equal(60, account.Balance);
            Assert.Equal(initialHistoryCount + 1, account._balanceHistory.Count);
            var op = account._balanceHistory[account._balanceHistory.Count - 1];
            Assert.Equal("Expense", op.Type);
        }
        
        [Fact]
        public void Withdraw_ShouldThrow_ForInsufficientFunds()
        {
            var account = Factory.CreateBankAccount("TestAccount", 50);
            var balanceControl = new BalanceControl();
            Assert.Throws<ArgumentException>(() => balanceControl.Withdraw(account, 60));
        }
    }
}
