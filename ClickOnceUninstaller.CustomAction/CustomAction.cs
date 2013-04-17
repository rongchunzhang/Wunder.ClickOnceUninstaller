using Microsoft.Deployment.WindowsInstaller;

namespace Wunder.ClickOnceUninstaller
{
    public class CustomActions
    {
        const string AppName = "Wunderlist";

        [CustomAction]
        public static ActionResult UninstallClickOnce(Session session)
        {
            session.Log("Begin to uninstall ClickOnce deployment");

            var uninstallInfo = UninstallInfo.Find(AppName);

            if (uninstallInfo == null)
            {
                session.Log("No uninstall information found for " + AppName);
                return ActionResult.NotExecuted;
            }
            else
            {
                var uninstaller = new Uninstaller();
                uninstaller.Uninstall(uninstallInfo);
            }

            return ActionResult.Success;
        }
    }
}
