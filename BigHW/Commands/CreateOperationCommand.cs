using System;
namespace BigHW
{
    public class CreateOperationCommand : ICommand
    {
        public void Execute()
        {
            var repo = DataRepository.Instance;
            
            var account = PromptForExistingAccount(repo);
            
            string opType = PromptForOperationType();
            
            float amount = PromptForFloat("Введите сумму операции: ");
            
            Console.Write("Введите описание операции (необязательно): ");
            string desc = Console.ReadLine();
            
            Console.Write("Введите название категории (или оставьте пустым): ");
            string catName = Console.ReadLine();
            var category = (string.IsNullOrWhiteSpace(catName))
                ? null
                : repo.GetCategoryByName(catName);
            
            var operation = Factory.CreateOperation(opType, account, amount, desc, category);
            repo.Operations.Add(operation);
            account._balanceHistory.Add(operation);
            
            var balanceControl = new BalanceControl();
            if (opType.Equals("Доход", StringComparison.OrdinalIgnoreCase))
            {
                balanceControl.Deposit(account, amount);
            }
            else
            {
                balanceControl.Withdraw(account, amount);
            }

            Console.WriteLine($"Операция '{opType}' добавлена в счёт '{account.Name}'.");
        }

        private BankAccount.BankAccount PromptForExistingAccount(DataRepository repo)
        {
            while (true)
            {
                Console.Write("Введите название счёта: ");
                string accName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(accName))
                {
                    Console.WriteLine("Ошибка: название не может быть пустым.");
                    continue;
                }

                var account = repo.GetAccountByName(accName);
                if (account == null)
                {
                    Console.WriteLine($"Счёт '{accName}' не найден. Повторите ввод.");
                    continue;
                }

                return account;
            }
        }

        private string PromptForOperationType()
        {
            while (true)
            {
                Console.Write("Введите тип операции (Доход/Расход): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "доход") return "Доход";
                if (input == "расход") return "Расход";

                Console.WriteLine("Ошибка: введите 'Доход' или 'Расход'. Повторите ввод.");
            }
        }

        private float PromptForFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (float.TryParse(Console.ReadLine(), out float value))
                {
                    return value;
                }
                Console.WriteLine("Ошибка: введите корректное число.");
            }
        }
    }
}
