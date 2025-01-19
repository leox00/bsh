using BSH.Core;

namespace BSH.UI
{
    public class ShellPrompt
    {
        private readonly CommandHandler _commandHandler;

        public ShellPrompt()
        {
            _commandHandler = new CommandHandler();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to BSH!");
            while (true)
            {
                Console.WriteLine($"bsh: {Environment.CurrentDirectory}");
                Console.Write("=> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    HistoryManager.SaveCommand(input);
                    _commandHandler.ExecuteCommand(input);
                }
            }
        }
    }
}