using System.IO;

namespace DashBoard.Utils
{
    public class FilesWatcher
    {
        private readonly List<FileSystemWatcher> _fileWatchers;
        private Timer? _debounceTimer;
        private const int DebounceDelay = 1000; 
        private FileSystemEventHandler? _handler;


        public FilesWatcher(string basePath, List<string> fileNames)
        {
            _fileWatchers = new List<FileSystemWatcher>();

            foreach (var fildName in fileNames)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(basePath, fildName)
                {
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = false
                };

                watcher.Changed += HandleEvent;
                watcher.Created += HandleEvent;
                watcher.Renamed += HandleEvent;
                watcher.Deleted += HandleEvent;

                _fileWatchers.Add(watcher);
            }
        }
        public void OnEvent(FileSystemEventHandler fileSystemEventHandler)
        {
            _handler = fileSystemEventHandler;
        }

        private void HandleEvent(object sender, FileSystemEventArgs e)
        {
            _debounceTimer?.Dispose(); 

            _debounceTimer = new Timer(_ =>
            {
                _handler?.Invoke(sender, e);
            }, null, DebounceDelay, Timeout.Infinite);
        }
    }
}
