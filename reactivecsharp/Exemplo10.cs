using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace reactivecsharp
{
    class Exemplo10
    {
        private const string FSW_DIRECTORY = "C:\\Renatozz";
        private FileSystemWatcher _fsw;

        public Exemplo10()
        {
            _fsw = new FileSystemWatcher();
            FileSystemWatcherConfigure();
        }

        public void Start() {
            var obsCreate = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => _fsw.Created += x, 
                x => _fsw.Created += x)
                .Select(x=>x.EventArgs);

            var obsChanged = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => _fsw.Changed += x,
                x => _fsw.Changed += x)
                .Select(x => x.EventArgs);

            var obsDeleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => _fsw.Deleted += x,
                x => _fsw.Deleted += x)
                .Select(x => x.EventArgs);

            var obs = obsCreate
                .Merge(obsChanged)
                .Merge(obsDeleted)
                .TimeInterval()
                .Scan((state, item) => state == null
                        || item.Interval - state.Interval > TimeSpan.FromMilliseconds(1)
                        || state.Value.FullPath != item.Value.FullPath
                        || !(state.Value.ChangeType == WatcherChangeTypes.Created && item.Value.ChangeType == WatcherChangeTypes.Changed) ? item : state)
                .Select(x => x.Value)
                .Select(x => new FileWatcherRaisedEvent { Filename = x.FullPath, ChangeTypes = x.ChangeType })
                .DistinctUntilChanged(new FileWatcherRaisedEventEqualityComparer()); 


            obs.Subscribe(x =>
            {
                Console.WriteLine($" {x.ChangeTypes.ToString()} __ {x.Filename}");
            });
        }

        private void FileSystemWatcherConfigure()
        {
            Directory.CreateDirectory(FSW_DIRECTORY);
            _fsw.Path = FSW_DIRECTORY;
            _fsw.IncludeSubdirectories = false;
            _fsw.EnableRaisingEvents = true;

        }



    }

    public class FileWatcherRaisedEvent {
        public string Filename { get; set; }
        public WatcherChangeTypes ChangeTypes { get; set; }
    }

    public class FileWatcherRaisedEventEqualityComparer : IEqualityComparer<FileWatcherRaisedEvent>
    {
        public bool Equals(FileWatcherRaisedEvent x, FileWatcherRaisedEvent y)
        {
            return x.Filename == y.Filename && x.ChangeTypes == y.ChangeTypes;
        }
        public int GetHashCode(FileWatcherRaisedEvent obj)
        {
            return obj.Filename.GetHashCode() ^ obj.ChangeTypes.GetHashCode();
        }
    }
}
