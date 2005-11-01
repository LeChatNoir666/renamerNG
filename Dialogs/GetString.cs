using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RenamerNG;

namespace Dialogs
{
	/// <summary>
	/// Summary description for GetString.
	/// </summary>
	public class GetString : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.TextBox textBoxConfirm;
		private System.Windows.Forms.Label labelConfirm;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public static string Show(string title, string message, int minLength, bool secret, bool confirm)
		{
			GetString g = new GetString(title, message, minLength, secret, confirm);
			DialogResult res = g.ShowDialog();
			if (res == DialogResult.OK)
				return g.Value;
			else      
				return null;
		}

		public static string Show(string title, string message, int minLength, bool secret)
		{
			return Show(title, message, minLength, secret, false);
		}

		public static string Show(string title, string message, int minLength)
		{
			return Show(title, message, minLength, false, false);
		}

		public static string Show(string title, string message)
		{
			return Show(title, message, 0, false, false);
		}

		public static string Show(string title)
		{
			return Show(title, "Enter a string.", 0, false, false);
		}

		private GetString(string title, string message, int minLength, bool secret, bool confirm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

						Text = title;
			label.Text = message;
			this.minLength = minLength;

			if (secret)
				textBox.PasswordChar = '*';
			else
				textBox.PasswordChar = '\0';

			if (!(secret && confirm))
			{
				textBoxConfirm.Visible = false;
				labelConfirm.Visible = false;
				btOk.Top -= 60;
				Height -= 60;
			}
		}

		int minLength = 1;

		private string Value
		{
			get { return textBox.Text; }
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
			this.label = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.btOk = new System.Windows.Forms.Button();
			this.textBoxConfirm = new System.Windows.Forms.TextBox();
			this.labelConfirm = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(8, 8);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(304, 24);
			this.label.TabIndex = 0;
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(8, 32);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(304, 20);
			this.textBox.TabIndex = 0;
			this.textBox.Text = "";
			this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(240, 120);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(72, 23);
			this.btOk.TabIndex = 2;
			this.btOk.Text = "Ok";
			this.btOk.Click += new System.EventHandler(this.btOk_Click);
			// 
			// textBoxConfirm
			// 
			this.textBoxConfirm.Location = new System.Drawing.Point(9, 88);
			this.textBoxConfirm.Name = "textBoxConfirm";
			this.textBoxConfirm.PasswordChar = '*';
			this.textBoxConfirm.Size = new System.Drawing.Size(304, 20);
			this.textBoxConfirm.TabIndex = 1;
			this.textBoxConfirm.Text = "";
			this.textBoxConfirm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
			// 
			// labelConfirm
			// 
			this.labelConfirm.Location = new System.Drawing.Point(8, 64);
			this.labelConfirm.Name = "labelConfirm";
			this.labelConfirm.Size = new System.Drawing.Size(120, 23);
			this.labelConfirm.TabIndex = 4;
			this.labelConfirm.Text = "Confirm";
			this.labelConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GetString
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(322, 151);
			this.Controls.Add(this.labelConfirm);
			this.Controls.Add(this.textBoxConfirm);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.btOk);
			this.Controls.Add(this.label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GetString";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion

		private void btOk_Click(object sender, System.EventArgs e)
		{
			if (textBox.Text.Length < minLength)
			{
				FrmMain.ErrorMessage("Please enter at least " + minLength.ToString() + " characters.");
				return;
			}

			if (textBoxConfirm.Visible && textBox.Text != textBoxConfirm.Text)
			{
				FrmMain.ErrorMessage("String and confirmation string does not match.");
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void textBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((int)e.KeyChar == 13)
				btOk_Click(null, null);
		}
	}
}
