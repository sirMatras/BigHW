using System.Diagnostics;

namespace BigHW
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
            
            _innerCommand.Execute();
            
            sw.Stop();
            System.Console.WriteLine($"Время выполнения команды: {sw.ElapsedMilliseconds} мс");
        }
    }
}