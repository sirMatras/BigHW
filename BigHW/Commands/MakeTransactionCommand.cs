using System;

namespace BigHW
{
    public class MakeTransactionCommand : ICommand
    {
        public void Execute()
        {
            var repo = DataRepository.Instance;
            
            var sender = PromptForExistingAccount(repo, "Введите название счёта-отправителя: ");
            
            var receiver = PromptForExistingAccount(repo, "Введите название счёта-получателя: ");
            
            float amount = PromptForPositiveFloat("Введите сумму перевода: ");
            
            var facade = new BankAccountFacade(new BalanceControl());
            
            facade.MakeTransaction(sender, receiver, amount);

            Console.WriteLine($"Успешно переведено {amount} со счёта '{sender.Name}' на счёт '{receiver.Name}'.");
        }

        private BankAccount.BankAccount PromptForExistingAccount(DataRepository repo, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
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

        private float PromptForPositiveFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (float.TryParse(Console.ReadLine(), out float value) && value > 0)
                    return value;

                Console.WriteLine("Ошибка: введите число > 0.");
            }
        }
    }
}
