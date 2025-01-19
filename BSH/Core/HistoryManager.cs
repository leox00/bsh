namespace BSH.Core;

public class HistoryManager
{
    static string historyDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".bsh_history");
    
    public static void SaveCommand(string command)
    {
        command = $": {DateTimeOffset.UtcNow.ToUnixTimeSeconds()};{command}";
        File.AppendAllText(historyDirectory, command + Environment.NewLine);
    }
}