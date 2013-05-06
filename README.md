An uninstaller for ClickOnce applications
===========================

### Why?

Apparently, ClickOnce installations can't be removed silently. The ClickOnce uninstaller always shows a "Maintainance" dialog, requiring user interaction. 

For [Wunderlist](http://www.6wunderkinder.com/wunderlist) 2.1 we wanted to switch from a ClickOnce deployment to a Windows Installer package using the [WiX Toolset](http://wixtoolset.org/). We wanted this switch to be integrated into our new installer, invisible to the user.

### What?

The Wunder.ClickOnceUninstaller uninstaller imitates the actions performed by the ClickOnce uninstaller, removing files, registry entries, start menu and desktop links for a given application. 

It automatically resolves dependencies between installed components and removes all of the applications's components which are not required by other installed ClickOnce applications.

The uninstaller can be used programmatically as .NET library, through a command line interface and as custom action for a WiX setup package. 

The included custom action for WiX is based on the .NET Framework 3.0 which is already shipped with Windows Vista or higher. 
