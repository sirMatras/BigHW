using System;

namespace BigHW
{
    public class RenameAccountCommand : ICommand
    {
        public void Execute()
        {
            var repo = DataRepository.Instance;
            
            var account = PromptForExistingAccount(repo);
            
            string newName = PromptForNonEmptyString("Введите новое название счёта: ");

            account.Name = newName;
            Console.WriteLine($"Счёт переименован в '{newName}'.");
        }

        private BankAccount.BankAccount PromptForExistingAccount(DataRepository repo)
        {
            while (true)
            {
                Console.Write("Введите текущее название счёта: ");
                string oldName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(oldName))
                {
                    Console.WriteLine("Ошибка: имя не может быть пустым. Повторите ввод.");
                    continue;
                }

                var account = repo.GetAccountByName(oldName);
                if (account == null)
                {
                    Console.WriteLine("Счёт не найден. Повторите ввод.");
                    continue;
                }

                return account; 
            }
        }

        private string PromptForNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        }
    }
}