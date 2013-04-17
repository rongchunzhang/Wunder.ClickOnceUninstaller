using System;
using System.Collections.Generic;
using System.IO;

namespace Wunder.ClickOnceUninstaller
{
    public class RemoveFiles : IUninstallStep
    {
        private string _clickOnceFolder;
        private List<string> _foldersToRemove;
        private List<string> _filesToRemove;

        public void Prepare(List<string> componentsToRemove)
        {
            _clickOnceFolder = FindClickOnceFolder();

            _foldersToRemove = new List<string>();
            foreach (var directory in Directory.GetDirectories(_clickOnceFolder))
            {
                if (componentsToRemove.Contains(Path.GetFileName(directory)))
                {
                    _foldersToRemove.Add(directory);
                }
            }

            _filesToRemove = new List<string>();
            foreach (var file in Directory.GetFiles(Path.Combine(_clickOnceFolder, "manifests")))
            {
                if (componentsToRemove.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    _filesToRemove.Add(file);
                }
            }
        }

        public void PrintDebugInformation()
        {
            if (string.IsNullOrEmpty(_clickOnceFolder) || !Directory.Exists(_clickOnceFolder))
                throw new InvalidOperationException("Call Prepare() first.");

            Console.WriteLine("Remove files from " + _clickOnceFolder);

            foreach (var folder in _foldersToRemove)
            {
                Console.WriteLine("Delete folder " + folder.Substring(_clickOnceFolder.Length + 1));
            }

            foreach (var file in _filesToRemove)
            {
                Console.WriteLine("Delete file " + file.Substring(_clickOnceFolder.Length + 1));
            }

            Console.WriteLine();
        }

        public void Execute()
        {
            if (string.IsNullOrEmpty(_clickOnceFolder) || !Directory.Exists(_clickOnceFolder))
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

        private string FindClickOnceFolder()
        {
            var apps20Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Apps\2.0");
            if (!Directory.Exists(apps20Folder)) throw new ArgumentException("Could not find ClickOnce folder");

            foreach (var subFolder in Directory.GetDirectories(apps20Folder))
            {
                if ((Path.GetFileName(subFolder) ?? string.Empty).Length == 12)
                {
                    foreach (var subSubFolder in Directory.GetDirectories(subFolder))
                    {
                        if ((Path.GetFileName(subSubFolder) ?? string.Empty).Length == 12)
                        {
                            return subSubFolder;
                        }
                    }
                }
            }

            throw new ArgumentException("Could not find ClickOnce folder");
        }

        public void Dispose()
        {
        }
    }
}
