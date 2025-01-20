using BSH.Core;

//TODO Fix blank line without '=>' when browsing history

namespace BSH.UI
{
    public class ShellPrompt
    {
        private readonly CommandHandler _commandHandler;
        private readonly HistoryManager _historyManager;
        
        public ShellPrompt()
        {
            var historyFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
                ".bsh_history"
            );
            _historyManager = new HistoryManager(historyFilePath);
            _commandHandler = new CommandHandler();
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine($"bsh: {Environment.CurrentDirectory}");
                Console.Write("=> ");
                var input = ReadLineWithHistory();

                if (string.IsNullOrWhiteSpace(input)) continue;
                
                _historyManager.SaveCommand(input);
                ExecuteCommand(input);
            }
        }
        
        private string ReadLineWithHistory()
        {
            string input = string.Empty;
            ConsoleKeyInfo key;

            while ((key = Console.ReadKey(intercept: true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.UpArrow)
                {
                    input = _historyManager.NavigateHistory(isUpArrow: true);
                    ClearLine();
                    Console.Write(input);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    input = _historyManager.NavigateHistory(isUpArrow: false);
                    ClearLine();
                    Console.Write(input);
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input[..^1];
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }

            Console.WriteLine(); // Move to the next line
            return input;
        }

        private void ClearLine()
        {
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        }

        private void ExecuteCommand(string command)
        {
            _commandHandler.ExecuteCommand(command);
        }
    }
}