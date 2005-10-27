using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RenamerNG.Settings
{
	/// <summary>
	/// Summary description for ShellExtension.
	/// </summary>
	public class ShellExtension
	{
		static string filetype = "Directory";

		public static bool CheckShellExtension()
		{
			try
			{
				RegistryKey k = Registry.ClassesRoot.OpenSubKey(filetype + "\\shell\\" + Application.ProductName);

				using (k)
				{
					if (k.SubKeyCount == 1)
						return true;
					else
						return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public static void SetShellExtension()
		{
			if (CheckShellExtension()) return;

			try
			{
				RegistryKey k = Registry.ClassesRoot.CreateSubKey(filetype + "\\shell\\" + Application.ProductName + "\\command");

				using (k)
				{
					string command = "\"" + Application.ExecutablePath + "\"" + " \"%0\"";
					k.SetValue("", command);
				}
			}
			catch (Exception ex)
			{
				FrmMain.ErrorMessage(ex.Message);
			}
		}

		public static void ClearShellExtension()
		{
			if (!CheckShellExtension()) return;

			try
			{
				Registry.ClassesRoot.DeleteSubKey(filetype + "\\shell\\" + Application.ProductName + "\\command");
				Registry.ClassesRoot.DeleteSubKey(filetype + "\\shell\\" + Application.ProductName);
			}
			catch (Exception ex)
			{
				FrmMain.ErrorMessage(ex.Message);
			}
		}
	}
}
