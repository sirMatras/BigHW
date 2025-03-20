using System;
using System.Collections.Generic;
using System.Linq;
using BigHW.BankAccount;
using BigHW.Category;
using BigHW.Operation;

namespace BigHW
{
    public sealed class DataRepository
    {
        private static readonly Lazy<DataRepository> _instance
            = new Lazy<DataRepository>(() => new DataRepository());

        public static DataRepository Instance => _instance.Value;

        public List<BankAccount.BankAccount> BankAccounts { get; private set; }
        public List<Category.Category> Categories { get; private set; }
        public List<Operation.Operation> Operations { get; private set; }

        private DataRepository()
        {
            BankAccounts = new List<BankAccount.BankAccount>();
            Categories = new List<Category.Category>();
            Operations = new List<Operation.Operation>();
        }

        public BankAccount.BankAccount GetAccountByName(string name)
        {
            return BankAccounts.FirstOrDefault(acc => 
                acc.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Category.Category GetCategoryByName(string name)
        {
            return Categories.FirstOrDefault(cat =>
                cat.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}