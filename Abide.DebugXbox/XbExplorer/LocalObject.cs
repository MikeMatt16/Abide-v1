using System;
using System.Collections.Generic;
using System.IO;

namespace XbExplorer
{
    public sealed class LocalObject
    {
        public string[] NestedDirectories => nestedDirectories.ToArray();
        public string[] NestedFiles => nestedFiles.ToArray();
        public string Name { get; }
        public string Root { get; }
        public string Path { get; }
        public bool IsDirectory { get; }
        public bool IsFile { get; }

        private readonly List<string> nestedDirectories = new List<string>();
        private readonly List<string> nestedFiles = new List<string>();
        
        public LocalObject(string path)
        {
            //Initialize
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Name = System.IO.Path.GetFileName(path);
            Root = System.IO.Path.GetDirectoryName(path);

            //Setup
            IsDirectory = Directory.Exists(path);
            IsFile = File.Exists(path);

            //Check
            if (IsDirectory)
                RecursiveSearch(path);
        }

        private void RecursiveSearch(string path)
        {
            //Add current directory
            nestedDirectories.Add(path);

            //Add directories
            foreach (var directory in Directory.GetDirectories(path))
                RecursiveSearch(directory);

            //Add files
            foreach (string file in Directory.GetFiles(path))
                nestedFiles.Add(file);
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
