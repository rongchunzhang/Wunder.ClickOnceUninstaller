using System;
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

            try
            {
                var uninstallInfo = UninstallInfo.Find(AppName);
                if (uninstallInfo == null)
                {
                    session.Log("No uninstall information found for " + AppName);
                    return ActionResult.NotExecuted;
                }

                var uninstaller = new Uninstaller();
                uninstaller.Uninstall(uninstallInfo);
            }
            catch (Exception ex)
            {
                session.Log("ERROR in ClickOnceUninstaller custom action:\n {0}", ex.ToString());
                return ActionResult.Failure;
            }

            return ActionResult.Success;
        }
    }
}
