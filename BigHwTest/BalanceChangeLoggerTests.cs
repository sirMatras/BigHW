using System;
using System.IO;
using BigHW;
using BigHW.BankAccount;
using Xunit;

namespace BigHW.Tests
{
    public class BalanceChangeLoggerTests
    {
        [Fact]
        public void OnBalanceChanged_ShouldLogBalanceChange()
        {
            var account = new BankAccount.BankAccount("TestAccount", 1000);
            var logger = new BalanceChangeLogger();

            float oldBalance = 1000;
            float newBalance = 1500;
            
            var originalOut = Console.Out;
            try
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    
                    logger.OnBalanceChanged(account, oldBalance, newBalance);
                    
                    sw.Flush();
                    var consoleOutput = sw.ToString().Trim();
                    
                    string expectedMessage = $"[LOG] Баланс счета 'TestAccount' изменился с 1000 на 1500.";
                    Assert.Equal(expectedMessage, consoleOutput);
                }
            }
            finally
            {
                Console.SetOut(originalOut); 
            }
        }
    }
}