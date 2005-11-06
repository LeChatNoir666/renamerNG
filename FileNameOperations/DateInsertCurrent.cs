using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for DateInsertCurrent.
	/// </summary>
	[Serializable()]
	public class DateInsertCurrent : Operation
	{
		[NonSerialized()]DateTime date;

		public DateInsertCurrent()
		{
			this[0] = "{10} {11} - ";
			this[1] = "0,false";
		}

		public override string Name
		{
			get { return "Insert current date";}
		}

		public override string Group
		{
			get { return "Date";}
		}

		public override string Help
		{
			get { return "Insert the current date and time.";}
		}

		public override int ParameterCount
		{
			get { return 2;}
		}

		public override Shortcut Key
		{
			get { return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Format", "", "Format of the date to insert, any combination of specifier {0} to {13} (year, month, day of month, day of week, day of year, hour, minute, second, millisecond, ticks, long date, long time, short date, short time"),
					new GUIElement(GUIElement.Types.POSITION, "Position", "", "Select position to insert at")
				};
			}
		}

		/// <summary>
		/// Override Init because this operation needs to
		/// initialize the number value that holds the current
		/// number to insert.
		/// </summary>
		public override void Init()
		{
			date = DateTime.Now;
		}

		protected void Perform(ListViewItem lvi, DateTime date)
		{
			FileName f = (FileName)lvi.Tag;

			string text = String.Format(this[0],
				date.Year, date.Month, date.Day, date.DayOfWeek, date.DayOfYear,
				date.Hour, date.Minute, date.Second, date.Millisecond,
				date.Ticks,
				date.ToLongDateString(), date.ToLongTimeString(), date.ToShortDateString(), date.ToShortTimeString());

			string[] s = this[1].Split(',');
			int start = int.Parse(s[0]);
			bool fromEnd = bool.Parse(s[1]);

			string res = "", data = f.NewName;

			if (!fromEnd)
			{
				if (start > data.Length)
					res = data.Insert(data.Length, text);
				else
					res = data.Insert(start, text);
			}
			else
			{
				if (start > data.Length)
					res = data.Insert(0, text);
				else
					res = data.Insert(data.Length - start, text);
			}

			f.NewName = res;
		}

		public override void Perform(ListViewItem lvi)
		{
			Perform(lvi, this.date);
		}
	}
}
