using System;
using Xunit;
using BigHW.BankAccount;

namespace BigHW.Tests
{
    public class BankAccountFacadeTests
    {
        [Fact]
        public void MakeTransaction_ShouldTransferFundsCorrectly()
        {
            var sender = Factory.CreateBankAccount("Sender", 200);
            var receiver = Factory.CreateBankAccount("Receiver", 100);
            var facade = new BankAccountFacade(new BalanceControl());
            
            facade.MakeTransaction(sender, receiver, 50);
            
            Assert.Equal(150, sender.Balance);
            Assert.Equal(150, receiver.Balance);
        }
    }
}