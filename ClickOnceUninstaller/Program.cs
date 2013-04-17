using System;

namespace Wunder.ClickOnceUninstaller
{
    class Program
    {

        static void Main(string[] args)
        {
            var uninstallInfo = UninstallInfo.Find("Wunderlist");

            if (uninstallInfo == null)
            {
                Console.WriteLine("Application not found");
            }
            else
            {
                var uninstaller = new Uninstaller();
                uninstaller.Uninstall(uninstallInfo);
                Console.WriteLine("Uninstall complete");
            }
            
            Console.ReadLine();
        }
    }
}
