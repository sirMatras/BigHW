using System;

namespace BigHW
{
    public class CreateAccountCommand : ICommand
    {
        public void Execute()
        {
            string name = PromptForNonEmptyString("Введите название счёта: ");
            
            float balance = PromptForPositiveFloat("Введите стартовый баланс: ");

            var newAcc = Factory.CreateBankAccount(name, balance);
            DataRepository.Instance.BankAccounts.Add(newAcc);

            Console.WriteLine($"Счёт '{newAcc.Name}' успешно создан. Баланс = {newAcc.Balance}");
        }
        
        private string PromptForNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                
                Console.WriteLine("Ошибка: строка не должна быть пустой. Повторите ввод.");
            }
        }
        
        private float PromptForPositiveFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (float.TryParse(Console.ReadLine(), out float value) && value >= 0)
                    return value;

                Console.WriteLine("Ошибка: введите корректное число (неотрицательное). Повторите ввод.");
            }
        }
    }
}