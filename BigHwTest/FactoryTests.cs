using System;
using Xunit;
using BigHW.BankAccount;
using BigHW.Category;
using BigHW.Operation;

namespace BigHW.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void CreateBankAccount_ShouldCreateAccount_WhenDataIsValid()
        {
            var account = Factory.CreateBankAccount("ValidAccount", 100);
            Assert.Equal("ValidAccount", account.Name);
            Assert.Equal(100, account.Balance);
            Assert.NotEqual(Guid.Empty, account.Id);
        }
        
        [Fact]
        public void CreateBankAccount_ShouldThrow_WhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => Factory.CreateBankAccount("", 100));
        }
        
        [Fact]
        public void CreateBankAccount_ShouldThrow_WhenBalanceIsNegative()
        {
            Assert.Throws<ArgumentException>(() => Factory.CreateBankAccount("Test", -10));
        }
        
        [Fact]
        public void CreateCategory_ShouldCreateCategory_WhenDataIsValid()
        {
            var category = Factory.CreateCategory("Salary", "Доход");
            Assert.Equal("Salary", category.Name);
            Assert.Equal("Доход", category.Type);
            Assert.NotEqual(Guid.Empty, category.Id);
        }
        
        [Fact]
        public void CreateCategory_ShouldThrow_WhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => Factory.CreateCategory("", "Доход"));
        }
        
        [Fact]
        public void CreateOperation_ShouldCreateOperation_WhenDataIsValid()
        {
            var account = Factory.CreateBankAccount("TestAccount", 100);
            var category = Factory.CreateCategory("Food", "Расход");
            var op = Factory.CreateOperation("Расход", account, 20, "Dinner", category);
            Assert.Equal("Расход", op.Type);
            Assert.Equal(20, op.Amount);
            Assert.Equal(account.Id, op.BankAccountId);
            Assert.Equal(category.Id, op.CategoryId);
            Assert.NotEqual(Guid.Empty, op.Id);
        }
        
        [Fact]
        public void CreateOperation_ShouldThrow_WhenAccountIsNull()
        {
            Assert.Throws<ArgumentException>(() => Factory.CreateOperation("Доход", null, 20, "Test", null));
        }
        
        [Fact]
        public void CreateOperation_ShouldCreateValidOperation()
        {
            var account = Factory.CreateBankAccount("TestAccount", 1000);
            var category = Factory.CreateCategory("Salary", "Доход");
            
            var operation = Factory.CreateOperation("Доход", account, 500, "Зарплата", category);
            
            Assert.NotNull(operation);
            Assert.NotEqual(Guid.Empty, operation.Id);
            Assert.Equal("Доход", operation.Type);
            Assert.Equal(account.Id, operation.BankAccountId);
            Assert.Equal(500, operation.Amount);
            Assert.Equal("Зарплата", operation.Description);
            Assert.Equal(category.Id, operation.CategoryId);
        }
    }
}
