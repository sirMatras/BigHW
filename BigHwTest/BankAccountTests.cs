using System;
using Xunit;

namespace BigHW.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            var account = new BankAccount.BankAccount("TestAccount", 100);
            
            // Assert
            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.Equal("TestAccount", account.Name);
            Assert.Equal(100, account.Balance);
            Assert.NotNull(account._balanceHistory);
        }

        [Fact]
        public void Notify_ShouldCallAttachedObserver()
        {
            var account = new BankAccount.BankAccount("TestAccount", 100);
            bool observerCalled = false;
            var observer = new TestObserver(() => observerCalled = true);
            account.Attach(observer);
            
            account.Notify(100, 150);
            
            Assert.True(observerCalled);
        }
    }
    
    class TestObserver : IBalanceObserver
    {
        private readonly Action _onNotified;
        public TestObserver(Action onNotified)
        {
            _onNotified = onNotified;
        }
        public void OnBalanceChanged(BankAccount.BankAccount account, float oldBalance, float newBalance)
        {
            _onNotified();
        }
    }
}