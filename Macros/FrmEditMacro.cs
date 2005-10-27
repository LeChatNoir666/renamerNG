using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RenamerNG.FileNameOperations;

namespace RenamerNG.Macros
{
	/// <summary>
	/// Summary description for FrmSettings.
	/// </summary>
	public class FrmEditMacro : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem miEdit;
		private System.Windows.Forms.MenuItem miDelete;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem miMoveUp;
		private System.Windows.Forms.MenuItem miMoveDown;
		private System.Windows.Forms.TextBox tbDesc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chParameters;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmEditMacro()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			macro = null;
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
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.miEdit = new System.Windows.Forms.MenuItem();
			this.miDelete = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.miMoveUp = new System.Windows.Forms.MenuItem();
			this.miMoveDown = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbDesc = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listView = new System.Windows.Forms.ListView();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chParameters = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(416, 16);
			this.btOk.Name = "btOk";
			this.btOk.TabIndex = 0;
			this.btOk.Text = "Ok";
			this.btOk.Click += new System.EventHandler(this.btOk_Click);
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(416, 48);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 1;
			this.btCancel.Text = "Cancel";
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.miEdit,
																						this.miDelete,
																						this.menuItem3,
																						this.miMoveUp,
																						this.miMoveDown});
			// 
			// miEdit
			// 
			this.miEdit.Index = 0;
			this.miEdit.Text = "Edit";
			this.miEdit.Click += new System.EventHandler(this.btEdit_Click);
			// 
			// miDelete
			// 
			this.miDelete.Index = 1;
			this.miDelete.Text = "Delete";
			this.miDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// miMoveUp
			// 
			this.miMoveUp.Index = 3;
			this.miMoveUp.Text = "Move Up\tCtrl+Up";
			this.miMoveUp.Click += new System.EventHandler(this.miMoveUp_Click);
			// 
			// miMoveDown
			// 
			this.miMoveDown.Index = 4;
			this.miMoveDown.Text = "Move Down\tCtrl+Down";
			this.miMoveDown.Click += new System.EventHandler(this.miMoveDown_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(16, 32);
			this.tbName.MaxLength = 50;
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(126, 20);
			this.tbName.TabIndex = 4;
			this.tbName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 176);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 20);
			this.label2.TabIndex = 8;
			this.label2.Text = "Operations";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbDesc
			// 
			this.tbDesc.Location = new System.Drawing.Point(16, 96);
			this.tbDesc.MaxLength = 500;
			this.tbDesc.Multiline = true;
			this.tbDesc.Name = "tbDesc";
			this.tbDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbDesc.Size = new System.Drawing.Size(376, 64);
			this.tbDesc.TabIndex = 9;
			this.tbDesc.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 20);
			this.label3.TabIndex = 10;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.chName,
																					   this.chParameters});
			this.listView.ContextMenu = this.contextMenu;
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(16, 200);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(376, 160);
			this.listView.TabIndex = 11;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 100;
			// 
			// chParameters
			// 
			this.chParameters.Text = "Parameters";
			this.chParameters.Width = 242;
			// 
			// FrmEditMacro
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(506, 374);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbDesc);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btOk);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmEditMacro";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit macro";
			this.ResumeLayout(false);

		}
		#endregion

		private Macro macro;

		public Macro Macro
		{
			get { return macro; }
		}

		private void InsertItem(int index, Operation o)
		{
			ListViewItem lvi = new ListViewItem(o.ToString());
			string s = "";
			int i;
			for (i = 0 ; i < o.ParameterCount - 1 ; i++)
				s += "\"" + o[i] + "\", ";
			if (o.ParameterCount > 0)
				s += o[i];

			lvi.SubItems.Add(s);
			lvi.Tag = o;
			listView.Items.Insert(index, lvi);
		}

		private void AddItem(Operation o)
		{
			InsertItem(listView.Items.Count, o);
		}

		public DialogResult ShowDialog(Macro m)
		{
			tbName.Text = m.Name;
			tbDesc.Text = m.Description;

			Macro tmp = (Macro)SerializationCopy.Copy(m);
			foreach (Operation o in tmp)
				AddItem(o);

			DialogResult res = ShowDialog();

			if (res == DialogResult.OK)
			{
				macro = new Macro(tbName.Text);
				macro.Description = tbDesc.Text;
				
				foreach (ListViewItem lvi in listView.Items)
				{
					Operation op = (Operation)lvi.Tag;
					macro.AddOperation(op);
				}
			}

			return res;
		}

		private void btOk_Click(object sender, System.EventArgs e)
		{
			if (tbName.Text == "")
			{
				FrmMain.ErrorMessage("You must enter a name.");
				return;
			}

			DialogResult = DialogResult.OK;
		}

		private void btDelete_Click(object sender, System.EventArgs e)
		{
			if (listView.SelectedItems.Count < 1)
			{
				FrmMain.ErrorMessage("Please select an operation to delete.");
				return;
			}

			listView.Items.Remove(listView.SelectedItems[0]);
		}

		private void btEdit_Click(object sender, System.EventArgs e)
		{
			if (listView.SelectedItems.Count < 1)
			{
				FrmMain.ErrorMessage("Please select an operation to edit.");
				return;
			}

			Operation op = (Operation)listView.SelectedItems[0].Tag;

			if (op.Gui == null)
			{
				FrmMain.ErrorMessage("Only operations with parameters can be edited.");
				return;
			}

			ListViewItem tmp = new ListViewItem();
			tmp.Tag = new FileName(@"Z:\tmp.txt");
			FrmDynamic dyn = new FrmDynamic(op, tmp);

			DialogResult res = dyn.ShowDialog();

			if (res == DialogResult.OK)
			{
				int index = listView.SelectedItems[0].Index;
				listView.Items.RemoveAt(index);
				InsertItem(index, op);
				listView.Items[index].Focused = true;
				listView.Items[index].Selected = true;
			}
		}

		private void miMoveUp_Click(object sender, System.EventArgs e)
		{
			if (listView.SelectedItems.Count < 1)
			{
				FrmMain.ErrorMessage("Please select an item to move.");
				return;
			}

			int index = listView.SelectedItems[0].Index;
			if (index > 0)
			{
				ListViewItem lvi = listView.Items[index];
				listView.Items.RemoveAt(index);
				listView.Items.Insert(index - 1, lvi);
				listView.Items[index - 1].Focused = true;
				listView.Items[index - 1].Selected = true;
			}
		}

		private void miMoveDown_Click(object sender, System.EventArgs e)
		{
			if (listView.SelectedItems.Count < 1)
			{
				FrmMain.ErrorMessage("Please select an item to move.");
				return;
			}

			int index = listView.SelectedItems[0].Index;
			if (index < listView.Items.Count - 1)
			{
				ListViewItem lvi = listView.Items[index];
				listView.Items.RemoveAt(index);
				listView.Items.Insert(index + 1, lvi);
				listView.Items[index + 1].Focused = true;
				listView.Items[index + 1].Selected = true;
			}
		}

		private void listView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == 38 && e.Control == true) //Up
			{
				miMoveUp_Click(null, null);
				e.Handled = true;
			}
			if (e.KeyValue == 40 && e.Control == true) //Down
			{
				miMoveDown_Click(null, null);
				e.Handled = true;
			}
		}
	}
}
