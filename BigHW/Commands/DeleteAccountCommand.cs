using System;

namespace BigHW.Commands
{
    public class DeleteAccountCommand : ICommand
    {
        public void Execute()
        {
            while (true)
            {
                Console.Write("Введите название счёта, который хотите удалить: ");
                string accName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(accName))
                {
                    Console.WriteLine("Ошибка: название не может быть пустым. Повторите ввод.");
                    continue;
                }

                var repo = DataRepository.Instance;
                var acc = repo.GetAccountByName(accName);

                if (acc == null)
                {
                    Console.WriteLine($"Счёт '{accName}' не найден. Повторите ввод.");
                }
                else
                {
                    repo.BankAccounts.Remove(acc);
                    Console.WriteLine($"Счёт '{acc.Name}' удалён.");
                    break; 
                }
            }
        }
    }
}