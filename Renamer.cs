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
				Application.Run(new FrmMain(""));
			else
				Application.Run(new FrmMain(args[0]));
		}
	}
}
