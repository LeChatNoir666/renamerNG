using System;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for GUIElement.
	/// </summary>
	[Serializable()]
	public class GUIElement
	{
		public enum Types {LABEL, TEXTBOX, NUMERICUPDOWN, CHECKBOX, POSITION, SELECTION, RADIOBUTTONS};

		private Types type;
		private string name;
		private string options;
		private string info;

		public Types Type
		{
			get { return type; }
		}

		public string Name
		{
			get { return name; }
		}

		public string Options
		{
			get { return options; }
		}

		public string Info
		{
			get { return info; }
		}

		public GUIElement(Types type, string name, string options, string info)
		{
			this.type = type;

			this.name = name;

			this.options = options;

			this.info = info;
		}

	}
}
