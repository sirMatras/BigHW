using System;
using System.Linq;

namespace BigHW.Commands
{
    public class DeleteOperationCommand : ICommand
    {
        public void Execute()
        {
            while (true)
            {
                Console.Write("Введите ID операции (GUID) для удаления: ");
                string opIdStr = Console.ReadLine();

                if (!Guid.TryParse(opIdStr, out Guid opId))
                {
                    Console.WriteLine("Ошибка: некорректный GUID. Повторите ввод.");
                    continue;
                }

                var repo = DataRepository.Instance;
                var operation = repo.Operations.FirstOrDefault(o => o.Id == opId);
                if (operation == null)
                {
                    Console.WriteLine($"Операция с ID {opId} не найдена. Повторите ввод.");
                    continue;
                }
                
                repo.Operations.Remove(operation);
                
                var account = repo.BankAccounts.FirstOrDefault(a => a.Id == operation.BankAccountId);
                if (account != null)
                {
                    account._balanceHistory.Remove(operation);
                    Console.WriteLine($"Операция {opId} удалена из счёта {account.Name}.");
                }
                else
                {
                    Console.WriteLine($"Операция {opId} удалена (счёт не найден).");
                }

                break; 
            }
        }
    }
}