using System;
using System.IO;
using Xunit;
using BigHW.Commands;
using BigHW;
using BigHW.BankAccount;
using BigHW.Category;

namespace BigHW.Tests
{
    public class CommandsTests : IDisposable
    {
        public CommandsTests()
        {
            DataRepository.Instance.BankAccounts.Clear();
            DataRepository.Instance.Categories.Clear();
            DataRepository.Instance.Operations.Clear();
        }
        
        public void Dispose()
        {
            DataRepository.Instance.BankAccounts.Clear();
            DataRepository.Instance.Categories.Clear();
            DataRepository.Instance.Operations.Clear();
        }

        [Fact]
        public void CreateAccountCommand_ShouldCreateAccount()
        {
            string simulatedInput = "TestAccount\n100\n";
            using (var sr = new StringReader(simulatedInput))
            using (var sw = new StringWriter()) 
            {
                Console.SetIn(sr);
                Console.SetOut(sw); 

                var command = new CreateAccountCommand();
                
                command.Execute();
                
                var account = DataRepository.Instance.GetAccountByName("TestAccount");
                Assert.NotNull(account);
                Assert.Equal("TestAccount", account.Name);
                Assert.Equal(100, account.Balance);
            } 
        }


        [Fact]
        public void DeleteAccountCommand_ShouldDeleteAccount()
        {
            var account = Factory.CreateBankAccount("DeleteMe", 50);
            DataRepository.Instance.BankAccounts.Add(account);
            string simulatedInput = "DeleteMe\n";
            var originalOut = Console.Out;
            try
            {
                using (var sr = new StringReader(simulatedInput))
                {
                    Console.SetIn(sr);
                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    var command = new DeleteAccountCommand();
                    command.Execute();
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
            var deleted = DataRepository.Instance.GetAccountByName("DeleteMe");
            Assert.Null(deleted);
        }

        [Fact]
        public void RenameAccountCommand_ShouldRenameAccount()
        {
            var account = Factory.CreateBankAccount("OldName", 100);
            DataRepository.Instance.BankAccounts.Add(account);
            string simulatedInput = "OldName\nNewName\n";
            using (var sr = new StringReader(simulatedInput))
            {
                Console.SetIn(sr);
                var command = new RenameAccountCommand();
                command.Execute();
            }
            var renamed = DataRepository.Instance.GetAccountByName("NewName");
            Assert.NotNull(renamed);
            Assert.Equal("NewName", renamed.Name);
        }

        [Fact]
        public void CreateCategoryCommand_ShouldCreateCategory()
        {
            string simulatedInput = "Доход\nSalary\n";
            var originalOut = Console.Out;
            try
            {
                using (var sr = new StringReader(simulatedInput))
                {
                    Console.SetIn(sr);
                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    var command = new CreateCategoryCommand();
                    command.Execute();
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
            var category = DataRepository.Instance.GetCategoryByName("Salary");
            Assert.NotNull(category);
            Assert.Equal("доход", category.Type.ToLower());
        }


        [Fact]
        public void CreateOperationCommand_ShouldCreateOperationAndAdjustBalance_Deposit()
        {
            var account = Factory.CreateBankAccount("OpAccount", 100);
            DataRepository.Instance.BankAccounts.Add(account);
            string simulatedInput = "OpAccount\nДоход\n50\nTest Operation\n\n";
            var originalOut = Console.Out;
            try
            {
                using (var sr = new StringReader(simulatedInput))
                {
                    Console.SetIn(sr);
                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    var command = new CreateOperationCommand();
                    command.Execute();
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
            Assert.Equal(150, account.Balance);
            var createdOperations = DataRepository.Instance.Operations
                .Where(op => op.Type == "Доход" && op.Amount == 50)
                .ToList();
            Assert.NotEmpty(createdOperations);
            Assert.Contains(createdOperations, op => op.Description == "Test Operation");
        }

        [Fact]
        public void DeleteOperationCommand_ShouldDeleteOperation()
        {
            var account = Factory.CreateBankAccount("OpAccount", 100);
            DataRepository.Instance.BankAccounts.Add(account);
            var op = Factory.CreateOperation("Доход", account, 50, "Test Op", null);
            DataRepository.Instance.Operations.Add(op);
            account._balanceHistory.Add(op);
            string simulatedInput = op.Id.ToString() + "\n";
            using (var sr = new StringReader(simulatedInput))
            {
                Console.SetIn(sr);
                var command = new DeleteOperationCommand();
                command.Execute();
            }
            Assert.Empty(DataRepository.Instance.Operations);
            Assert.Empty(account._balanceHistory);
        }

        [Fact]
        public void MakeTransactionCommand_ShouldTransferFunds()
        {
            var sender = Factory.CreateBankAccount("Sender", 200);
            var receiver = Factory.CreateBankAccount("Receiver", 100);
            DataRepository.Instance.BankAccounts.Add(sender);
            DataRepository.Instance.BankAccounts.Add(receiver);
            string simulatedInput = "Sender\nReceiver\n50\n";
            using (var sr = new StringReader(simulatedInput))
            {
                Console.SetIn(sr);
                var command = new MakeTransactionCommand();
                command.Execute();
            }
            Assert.Equal(150, sender.Balance);
            Assert.Equal(150, receiver.Balance);
        }

        [Fact]
        public void ViewAccountCommand_ShouldOutputAccountInfo()
        {
            var account = Factory.CreateBankAccount("ViewAccount", 100);
            DataRepository.Instance.BankAccounts.Add(account);
            var op = Factory.CreateOperation("Доход", account, 30, "Test", null);
            account._balanceHistory.Add(op);
            string simulatedInput = "ViewAccount\nn\n";
            using (var sr = new StringReader(simulatedInput))
            using (var sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);
                var command = new ViewAccountCommand();
                command.Execute();
                string output = sw.ToString();
                Assert.Contains("ViewAccount", output);
                Assert.Contains("Баланс: 100", output);
                Assert.DoesNotContain("Операция", output);
            }
        }
        
        [Fact]
        public void RenameAccountCommand_ShouldRenameAccount2()
        {
            // Arrange: Создаём аккаунт
            var account = Factory.CreateBankAccount("OldName", 500);
            DataRepository.Instance.BankAccounts.Add(account);
            
            string simulatedInput = "OldName\nNewName\n";
            var originalOut = Console.Out;
            try
            {
                using (var sr = new StringReader(simulatedInput))
                {
                    Console.SetIn(sr);
                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    var command = new RenameAccountCommand();
                    command.Execute();
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
            
            var renamedAccount = DataRepository.Instance.GetAccountByName("NewName");
            Assert.NotNull(renamedAccount);
            Assert.Equal("NewName", renamedAccount.Name);
            Assert.Equal(500, renamedAccount.Balance);
            
            var oldAccount = DataRepository.Instance.GetAccountByName("OldName");
            Assert.Null(oldAccount);
        }

    }
}
