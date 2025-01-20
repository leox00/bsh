namespace BSH.Core
{
    public class HistoryManager
    {
        private readonly string _historyFilePath;
        private readonly List<string> _historyCache = new();
        private int _historyIndex = -1;
        private int _loadedCount = 0; // Tracks how many lines are loaded

        private const int LoadChunkSize = 100; // Number of lines to load per chunk

        public HistoryManager(string historyFilePath)
        {
            _historyFilePath = historyFilePath;

            if (!File.Exists(_historyFilePath))
            {
                File.Create(_historyFilePath).Dispose(); // Ensure the file exists
            }
        }

        public string NavigateHistory(bool isUpArrow)
        {
            if (_historyCache.Count == 0 && _loadedCount == 0)
            {
                LoadHistoryChunk(); // Load the first chunk
                _historyIndex = _historyCache.Count; // Start at the end
            }

            if (isUpArrow)
            {
                if (_historyIndex > 0)
                {
                    _historyIndex--;
                }
                else if (LoadHistoryChunk()) // Try loading more history
                {
                    _historyIndex = 0;
                }
            }
            else
            {
                if (_historyIndex < _historyCache.Count - 1)
                {
                    _historyIndex++;
                }
                else
                {
                    return string.Empty; // No more recent commands
                }
            }

            return _historyCache.ElementAtOrDefault(_historyIndex) ?? string.Empty;
        }

        public void SaveCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            // Save to file
            using (var writer = new StreamWriter(_historyFilePath, append: true))
            {
                long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                writer.WriteLine($": {timestamp};{command}");
            }

            // Add to in-memory history
            _historyCache.Add(command);
            _historyIndex = _historyCache.Count;
        }

        private bool LoadHistoryChunk()
        {
            try
            {
                var lines = File.ReadLines(_historyFilePath).Reverse()
                                .Skip(_loadedCount)
                                .Take(LoadChunkSize)
                                .Reverse()
                                .ToList();

                if (lines.Count > 0)
                {
                    _historyCache.InsertRange(0, lines.Select(ParseCommand).Where(cmd => cmd != null));
                    _loadedCount += lines.Count;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading history: {ex.Message}");
            }

            return false; // No more history to load
        }

        private static string ParseCommand(string line)
        {
            int semicolonIndex = line.IndexOf(';');
            return semicolonIndex >= 0 ? line.Substring(semicolonIndex + 1) : null;
        }
    }
}

