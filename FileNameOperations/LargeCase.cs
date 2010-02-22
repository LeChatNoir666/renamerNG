using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for LargeCase.
	/// </summary>
	[Serializable()]
	public class LargeCase : Operation
	{
		public LargeCase()
		{
			this[0] = " -_()[]{}|";
		}

		public override string Name
		{
			get{ return "Large Case";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "First letter of every word is large, the rest are small.";}
		}

		public override int ParameterCount
		{
			get{ return 1;}
		}

        public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Word separators", "", "Characters that separate one word from another.")
				};
			}
		}

/*
		public override string Name
		{
			get{ return "";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return ;}
		}

		public override int ParameterCount
		{
			get{ return ;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return ;
			}
		}
*/
		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;
			string separators = this[0];
			char[] name = f.NewName.ToLower().ToCharArray();

			if (name.Length > 0)
				name[0] = char.ToUpper(name[0]);

			for (int i = 1 ; i < name.Length ; i++)
				if (separators.IndexOf(name[i-1]) >= 0)
					name[i] = char.ToUpper(name[i]);

			string n = "";
			foreach (char c in name)
				n += c;

			f.NewName = n;
		}
	}
}
