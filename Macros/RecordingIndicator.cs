using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RenamerNG.Macros
{
	/// <summary>
	/// Summary description for RecordingIndicator.
	/// </summary>
	public class RecordingIndicator : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblRecording;
		private System.Windows.Forms.Timer timer;
		private System.ComponentModel.IContainer components;

		public RecordingIndicator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblRecording = new System.Windows.Forms.Label();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// lblRecording
			// 
			this.lblRecording.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRecording.Location = new System.Drawing.Point(0, 0);
			this.lblRecording.Name = "lblRecording";
			this.lblRecording.Size = new System.Drawing.Size(104, 24);
			this.lblRecording.TabIndex = 0;
			this.lblRecording.Text = "Not recording";
			this.lblRecording.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// RecordingIndicator
			// 
			this.Controls.Add(this.lblRecording);
			this.Name = "RecordingIndicator";
			this.Size = new System.Drawing.Size(104, 24);
			this.ResumeLayout(false);

		}
		#endregion

		public new bool Enabled
		{
			set 
			{
				timer.Enabled = value;
				if (value)
				{
					lblRecording.Text = "Recording!!!";
				}
				else
				{
					lblRecording.ForeColor = Color.Black;
					lblRecording.BackColor = this.BackColor;
					lblRecording.Text = "Not recording";
				}
			}
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			if (lblRecording.ForeColor == Color.Red)
			{
				lblRecording.ForeColor = Color.Black;
				lblRecording.BackColor = this.BackColor;
			}
			else
			{
				lblRecording.ForeColor = Color.Red;
				lblRecording.BackColor = Color.Yellow;
			}
		}
	}
}
