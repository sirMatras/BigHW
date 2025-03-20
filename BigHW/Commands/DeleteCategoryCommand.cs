using System;

namespace BigHW.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public void Execute()
        {
            while (true)
            {
                Console.Write("Введите название категории для удаления: ");
                string catName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(catName))
                {
                    Console.WriteLine("Ошибка: название категории не может быть пустым.");
                    continue;
                }

                var repo = DataRepository.Instance;
                var cat = repo.GetCategoryByName(catName);
                if (cat == null)
                {
                    Console.WriteLine($"Категория '{catName}' не найдена. Повторите ввод.");
                    continue;
                }

                repo.Categories.Remove(cat);
                Console.WriteLine($"Категория '{cat.Name}' удалена.");
                break; 
            }
        }
    }
}