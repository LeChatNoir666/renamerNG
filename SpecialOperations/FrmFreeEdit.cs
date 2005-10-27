using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RenamerNG.SpecialOperations
{
	/// <summary>
	/// Summary description for FrmDefault.
	/// </summary>
	public class FrmFreeEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox tbNewNames;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Button btCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmFreeEdit()
		{
			InitializeComponent();
		}

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

		public String[] Lines
		{
			get { return tbNewNames.Lines; }
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbNewNames = new System.Windows.Forms.TextBox();
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbNewNames
			// 
			this.tbNewNames.Location = new System.Drawing.Point(8, 8);
			this.tbNewNames.MaxLength = 1000000;
			this.tbNewNames.Multiline = true;
			this.tbNewNames.Name = "tbNewNames";
			this.tbNewNames.Size = new System.Drawing.Size(384, 272);
			this.tbNewNames.TabIndex = 0;
			this.tbNewNames.Text = "";
			// 
			// btOk
			// 
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOk.Location = new System.Drawing.Point(216, 288);
			this.btOk.Name = "btOk";
			this.btOk.TabIndex = 1;
			this.btOk.Text = "Ok";
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(320, 288);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 2;
			this.btCancel.Text = "Cancel";
			// 
			// FrmFreeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(402, 318);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btOk);
			this.Controls.Add(this.tbNewNames);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmFreeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Free edit";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
