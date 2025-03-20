using System;

namespace BigHW.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        public void Execute()
        {
            string type = PromptForCategoryType("Введите тип категории (доход или расход): ");
            
            string name = PromptForNonEmptyString("Введите название категории: ");
            
            var cat = Factory.CreateCategory(name, type);
            DataRepository.Instance.Categories.Add(cat);
            Console.WriteLine($"Категория '{cat.Name}' (тип {cat.Type}) успешно создана.");
        }

        private string PromptForCategoryType(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim().ToLower();
                
                if (input == "доход" || input == "income")
                    return "доход";
                if (input == "расход" || input == "expense")
                    return "расход";

                Console.WriteLine("Ошибка: введите \"доход\" или \"расход\".");
            }
        }

        private string PromptForNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        }
    }
}