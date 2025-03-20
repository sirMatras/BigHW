using System;
using Xunit;
using BigHW.BankAccount;
using BigHW.Category;
using BigHW;

namespace BigHW.Tests
{
    public class DataRepositoryTests
    {
        [Fact]
        public void Instance_ShouldBeSingleton()
        {
            var repo1 = DataRepository.Instance;
            var repo2 = DataRepository.Instance;
            Assert.Same(repo1, repo2);
        }
        
        [Fact]
        public void GetAccountByName_ShouldReturnCorrectAccount()
        {
            var repo = DataRepository.Instance;
            repo.BankAccounts.Clear();
            var account = new BankAccount.BankAccount("Test", 100);
            repo.BankAccounts.Add(account);
            
            var found = repo.GetAccountByName("Test");
            Assert.NotNull(found);
            Assert.Equal(account.Id, found.Id);
        }
        
        [Fact]
        public void GetCategoryByName_ShouldReturnCorrectCategory()
        {
            var repo = DataRepository.Instance;
            repo.Categories.Clear();
            var category = new Category.Category("TestCat", "Доход");
            repo.Categories.Add(category);
            
            var found = repo.GetCategoryByName("TestCat");
            Assert.NotNull(found);
            Assert.Equal(category.Id, found.Id);
        }
    }
}