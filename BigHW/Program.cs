using System;
using BigHW.Commands;

namespace BigHW
{
    class Program
    {
        static void Main()
        {

            ShowInteractiveMenu();
        }

        public static void ShowInteractiveMenu()
        {
            var menuItems = new string[]
            {
                "1) Создать счёт",
                "2) Удалить счёт",
                "3) Переименовать счёт",
                "4) Создать категорию",
                "5) Удалить категорию",
                "6) Создать операцию (доход/расход)",
                "7) Удалить операцию",
                "8) Сделать транзакцию между счетами",
                "9) Посмотреть счёт", 
                "Выход"
            };


            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Учет финансов (Навигация стрелками ↑/↓, Enter для выбора) ====\n");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(menuItems[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex >= menuItems.Length) selectedIndex = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedIndex == menuItems.Length - 1)
                        {
                            Console.WriteLine("Выход из программы...");
                            return;
                        }
                        else
                        {
                            HandleMenuAction(selectedIndex);
                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
        }

        static void HandleMenuAction(int menuIndex)
        {
            ICommand command = null;

            switch (menuIndex)
            {
                case 0: command = new CreateAccountCommand(); break;
                case 1: command = new DeleteAccountCommand(); break;
                case 2: command = new RenameAccountCommand(); break;
                case 3: command = new CreateCategoryCommand(); break;
                case 4: command = new DeleteCategoryCommand(); break;
                case 5: command = new CreateOperationCommand(); break;
                case 6: command = new DeleteOperationCommand(); break;
                case 7: command = new MakeTransactionCommand(); break;
                case 8: command = new ViewAccountCommand(); break;
                default:
                    Console.WriteLine("Неизвестный пункт меню.");
                    return;
            }
            
            ICommand decorated = new TimingCommandDecorator(command);
            decorated.Execute();
        }

    }
}
