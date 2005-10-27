using System;
using System.Collections;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for OperationList.
	/// </summary>
	public class OperationList : SortedList
	{
		public void Add(Operation op)
		{
			base.Add (op.Name, op);
		}
	}
}
