http://associationmanager.codeplex.com/

															  Project Description
Association Manager is a library which makes it a lot easier for .NET developers to associate file extensions with their application.
Support for Windows "old" file associations
Support for "Default Programs" introduced in Windows Vista & Windows 7
Support for previous associated program when removing association


Register the associations used by your application. This step can be performed multiple times and will only maintain the known application associations.:

using (FileAssociationManager mgr = new FileAssociationManager())
{
   using (ApplicationAssociation ext = mgr.RegisterFileAssociation("doc"))
   {
      ext.DefaultIcon = new ApplicationIcon(Application.ExecutablePath);
      ext.ShellOpenCommand = Application.ExecutablePath;
      ext.Associated = true;
   }
   using (ApplicationAssociation ext = mgr.RegisterFileAssociation("txt"))
   {
      ext.DefaultIcon = new ApplicationIcon(Application.ExecutablePath);
      ext.ShellOpenCommand = Application.ExecutablePath;
      ext.Associated = true;
   }
}


UnRegister the associations used by your application when uninstalling.

using (FileAssociationManager mgr = new FileAssociationManager())
{
   mgr.UnregisterApplicationAssociation();
}
Last edited Nov 13, 2010 at 9:42 PM by rfc1459, version 7