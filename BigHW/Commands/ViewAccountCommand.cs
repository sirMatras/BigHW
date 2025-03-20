using System;

namespace BigHW
{
    public class ViewAccountCommand : ICommand
    {
        public void Execute()
        {
            var repo = DataRepository.Instance;

            while (true)
            {
                Console.Write("Введите название счёта для просмотра: ");
                string accName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(accName))
                {
                    Console.WriteLine("Ошибка: имя счёта не может быть пустым. Повторите ввод.");
                    continue;
                }
                
                var account = repo.GetAccountByName(accName);
                if (account == null)
                {
                    Console.WriteLine($"Счёт '{accName}' не найден. Повторите ввод.");
                    continue;
                }
                
                Console.WriteLine("\n=== Информация о счёте ===");
                Console.WriteLine($"ID: {account.Id}");
                Console.WriteLine($"Имя счёта: {account.Name}");
                Console.WriteLine($"Баланс: {account.Balance}");
                
                Console.Write("\nПоказать список операций? (Y/N): ");
                string ans = Console.ReadLine()?.Trim().ToLower();
                if (ans == "y" || ans == "yes")
                {
                    if (account._balanceHistory.Count == 0)
                    {
                        Console.WriteLine("На этом счёте пока нет операций.");
                    }
                    else
                    {
                        Console.WriteLine("\n=== История операций ===");
                        foreach (var op in account._balanceHistory)
                        {
                            Console.WriteLine($"- Операция {op.Id}, Тип: {op.Type}, Сумма: {op.Amount}, Дата: {op.Date}");
                        }
                    }
                }
                
                break;
            }
        }
    }
}
