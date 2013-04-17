using System;
using System.Collections.Generic;
using System.IO;

namespace Wunder.ClickOnceUninstaller
{
    public class RemoveFiles : IUninstallStep
    {
        private readonly ClickOnceRegistry _registry;
        private List<string> _foldersToRemove;
        private List<string> _filesToRemove;

        public RemoveFiles(ClickOnceRegistry registry)
        {
            _registry = registry;
        }

        public void Prepare(List<string> componentsToRemove)
        {
            // sanity check
            if (string.IsNullOrEmpty(_registry.ClickOnceFolder) ||
                !_registry.ClickOnceFolder.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)))
                throw new ArgumentException("Invalid ClickOnce folder: " + _registry.ClickOnceFolder);

            _foldersToRemove = new List<string>();
            foreach (var directory in Directory.GetDirectories(_registry.ClickOnceFolder))
            {
                if (componentsToRemove.Contains(Path.GetFileName(directory)))
                {
                    _foldersToRemove.Add(directory);
                }
            }

            _filesToRemove = new List<string>();
            foreach (var file in Directory.GetFiles(Path.Combine(_registry.ClickOnceFolder, "manifests")))
            {
                if (componentsToRemove.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    _filesToRemove.Add(file);
                }
            }
        }

        public void PrintDebugInformation()
        {
            if (_foldersToRemove == null)
                throw new InvalidOperationException("Call Prepare() first.");

            Console.WriteLine("Remove files from " + _registry.ClickOnceFolder);

            foreach (var folder in _foldersToRemove)
            {
                Console.WriteLine("Delete folder " + folder.Substring(_registry.ClickOnceFolder.Length + 1));
            }

            foreach (var file in _filesToRemove)
            {
                Console.WriteLine("Delete file " + file.Substring(_registry.ClickOnceFolder.Length + 1));
            }

            Console.WriteLine();
        }

        public void Execute()
        {
            if (_foldersToRemove == null)
                throw new InvalidOperationException("Call Prepare() first.");

            foreach (var folder in _foldersToRemove)
            {
                try
                {
                    Directory.Delete(folder, true);
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            foreach (var file in _filesToRemove)
            {
                File.Delete(file);
            }
        }

        public void Dispose()
        {
        }
    }
}
