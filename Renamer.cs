using System;
using System.Windows.Forms;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for Renamer.
	/// </summary>
	public class Renamer
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			if (args.Length == 0)
			{
				Application.Run(new FrmMain(""));
			}
			else if (args.Length == 1)
			{
				Application.Run(new FrmMain(args[0]));
			}
			else
			{
				string msg = "Invalid number of parameters, found " + args.Length.ToString() + " expected 0 or 1.\nRefer to manual for parameter specification.";
				FrmMain.ErrorMessage(msg);
			}
		}
	}
}
