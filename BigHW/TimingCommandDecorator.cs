using System.Diagnostics;

namespace BigHW.Commands
{
    public class TimingCommandDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        public TimingCommandDecorator(ICommand command)
        {
            _innerCommand = command;
        }

        public void Execute()
        {
            var sw = Stopwatch.StartNew();
            
            // Выполняем команду, которую «декорируем».
            _innerCommand.Execute();
            
            sw.Stop();
            System.Console.WriteLine($"Время выполнения команды: {sw.ElapsedMilliseconds} мс");
        }
    }
}