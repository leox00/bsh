namespace BSH.Core
{
    public class CommandHandler
    {
        public void ExecuteCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            string[] parts = command.Split(' ', 2);
            string baseCommand = parts[0];
            string argument = parts.Length > 1 ? parts[1] : null;

            switch (baseCommand)
            {
                case "cd":
                    ChangeDirectory(argument);
                    break;

                case "exit":
                    ExitShell();
                    break;

                default:
                    ExecuteSystemCommand(command);
                    break;
            }
        }

        private void ChangeDirectory(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "~")
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }

            string fullPath = Path.IsPathRooted(path)
                ? path
                : Path.Combine(Environment.CurrentDirectory, path);

            if (Directory.Exists(fullPath))
            {
                Environment.CurrentDirectory = fullPath;
            }
            else
            {
                Console.WriteLine($"Error: Directory '{path}' does not exist.");
            }
        }

        private void ExitShell()
        {
            Console.WriteLine("Exiting BSH. Goodbye!");
            Environment.Exit(0);
        }

        private void ExecuteSystemCommand(string command)
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine(output);
                }

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Console.Error.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to execute command '{command}'. Details: {ex.Message}");
            }
        }
    }
}
