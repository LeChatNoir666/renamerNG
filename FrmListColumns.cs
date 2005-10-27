using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for FrmSettings.
	/// </summary>
	public enum ListColumns {NewName, Size, OldName, Path, Changed, Updated, Success, Created, Modified, Accessed};

	public class FrmListColumns : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listSelected;
		private System.Windows.Forms.ListBox listAvailable;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmListColumns()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		string drop = null;
		ListColumns[] selected = null;

		public ListColumns[] Selected
		{
			get { return selected; }
		}

		public DialogResult ShowDialog(ListColumns[] columns)
		{
			for (int i = 0 ; ((ListColumns)i).ToString() != i.ToString() ; i++)
			{
				listAvailable.Items.Add(((ListColumns)i).ToString());
			}

			foreach (ListColumns lc in columns)
			{
				listSelected.Items.Add(lc.ToString());
				listAvailable.Items.Remove(lc.ToString());
			}

			return base.ShowDialog();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listSelected = new System.Windows.Forms.ListBox();
			this.listAvailable = new System.Windows.Forms.ListBox();
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listSelected
			// 
			this.listSelected.AllowDrop = true;
			this.listSelected.Location = new System.Drawing.Point(13, 35);
			this.listSelected.Name = "listSelected";
			this.listSelected.Size = new System.Drawing.Size(100, 134);
			this.listSelected.TabIndex = 0;
			this.listSelected.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listAvailable_MouseDown);
			this.listSelected.DragDrop += new System.Windows.Forms.DragEventHandler(this.listAvailable_DragDrop);
			this.listSelected.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listAvailable_MouseMove);
			this.listSelected.DragEnter += new System.Windows.Forms.DragEventHandler(this.listAvailable_DragEnter);
			// 
			// listAvailable
			// 
			this.listAvailable.AllowDrop = true;
			this.listAvailable.Location = new System.Drawing.Point(127, 35);
			this.listAvailable.Name = "listAvailable";
			this.listAvailable.Size = new System.Drawing.Size(100, 134);
			this.listAvailable.TabIndex = 1;
			this.listAvailable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listAvailable_MouseDown);
			this.listAvailable.DragDrop += new System.Windows.Forms.DragEventHandler(this.listAvailable_DragDrop);
			this.listAvailable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listAvailable_MouseMove);
			this.listAvailable.DragEnter += new System.Windows.Forms.DragEventHandler(this.listAvailable_DragEnter);
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(56, 194);
			this.btOk.Name = "btOk";
			this.btOk.TabIndex = 4;
			this.btOk.Text = "Ok";
			this.btOk.Click += new System.EventHandler(this.btOk_Click);
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(152, 194);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 5;
			this.btCancel.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Selected";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(127, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Available";
			// 
			// FrmListColumns
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(241, 228);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btOk);
			this.Controls.Add(this.listAvailable);
			this.Controls.Add(this.listSelected);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmListColumns";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit list columns";
			this.ResumeLayout(false);

		}
		#endregion

		private void listAvailable_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			drop = e.Data.GetData(DataFormats.Text).ToString();
		}

		private void listAvailable_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.None; //Default to accept nothing

			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				string data = e.Data.GetData(DataFormats.Text).ToString();

				string[] info = data.Split(',');

				if (info.Length == 2)
				{
					if (info[0] == listSelected.Name && listSelected.Items.Contains(info[1]) ||
						info[0] == listAvailable.Name && listAvailable.Items.Contains(info[1]))
					{
						e.Effect = DragDropEffects.Move;
					}
				}
			}
		}

		private void listAvailable_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListBox lb = (ListBox)sender;
			int i = lb.IndexFromPoint(e.X, e.Y) ;
			
			if(i >= 0)
			{
				string s = lb.Name + "," + lb.Items[i].ToString();
				lb.DoDragDrop(s, DragDropEffects.Move);
			}
		}

		private void listAvailable_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (drop != null)
			{
				ListBox lb = (ListBox)sender;

				string[] info = drop.Split(',');

				if (listSelected.Name == info[0])
					listSelected.Items.Remove(info[1]);
				else
					listAvailable.Items.Remove(info[1]);

				int i = lb.IndexFromPoint(e.X, e.Y);
				if (i >= 0)
					lb.Items.Insert(i, info[1]);
				else
					lb.Items.Add(info[1]);

				drop = null;
			}
		}

		private void btOk_Click(object sender, System.EventArgs e)
		{
			int count = listSelected.Items.Count;
			if (count <= 0)
			{
				FrmMain.ErrorMessage("You must select at least one column to display.");
				return;
			}

			selected = new ListColumns[count];

			for (int i = 0 ; i < count ; i++)
			{
				for (int l = 0 ; ((ListColumns)l).ToString() != l.ToString() ; l++)
				{
					if (listSelected.Items[i].ToString() == ((ListColumns)l).ToString())
						selected[i] = (ListColumns)l;
				}
			}

			DialogResult = DialogResult.OK;
		}
	}
}
