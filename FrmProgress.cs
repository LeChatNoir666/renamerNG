using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for FrmProgress.
	/// </summary>
	public class FrmProgress : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btCancel;
		public System.Windows.Forms.ProgressBar progress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmProgress()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		bool cancel = false;

		public bool Cancel
		{
			get { return cancel; }
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
			this.btCancel = new System.Windows.Forms.Button();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// btCancel
			// 
			this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.btCancel.Location = new System.Drawing.Point(260, 7);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 0;
			this.btCancel.Text = "Cancel";
			this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(7, 7);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(246, 23);
			this.progress.TabIndex = 1;
			// 
			// FrmProgress
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 38);
			this.ControlBox = false;
			this.Controls.Add(this.progress);
			this.Controls.Add(this.btCancel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmProgress";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Progress";
			this.ResumeLayout(false);

		}
		#endregion

		private void btCancel_Click(object sender, System.EventArgs e)
		{
			cancel = true;
		}
	}
}
