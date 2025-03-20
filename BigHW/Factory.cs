using System;
using BigHW.BankAccount;
using BigHW.Category;
using BigHW.Operation;

namespace BigHW
{
    public static class Factory
    {
        public static BankAccount.BankAccount CreateBankAccount(string name, float initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя не может быть пустым", nameof(name));
            }
            if (initialBalance < 0)
            {
                throw new ArgumentException("Начальный баланс не может быть отрицательным", nameof(initialBalance));
            }

            return new BankAccount.BankAccount(name, initialBalance);
        }

        public static Category.Category CreateCategory(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя категории не может быть пустым", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Тип категории не может быть пустым", nameof(type));
            }

            return new Category.Category(name, type);
        }

        public static Operation.Operation CreateOperation(string type,
                                                          BankAccount.BankAccount account,
                                                          float amount,
                                                          string description,
                                                          Category.Category category)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Тип операции не может быть пустым", nameof(type));
            }
            if (account == null)
            {
                throw new ArgumentException("Аккаунт не может быть null", nameof(account));
            }
            if (amount < 0)
            {
                throw new ArgumentException("Сумма не может быть меньше нуля", nameof(amount));
            }

            return new Operation.Operation(type, account, amount, description, category);
        }
        
        public static Operation.Operation CreateOperation(string type,
                                                          BankAccount.BankAccount account,
                                                          float amount,
                                                          string description)
        {
            return new Operation.Operation(type, account, amount, description, null);
        }
    }
}
