using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace RenamerNG.Settings
{
	/// <summary>
	/// Summary description for FrmSettings.
	/// </summary>
	public class FrmSettings : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabSE;
		private System.Windows.Forms.Button btSEAdd;
		private System.Windows.Forms.Button btSERemove;
		private System.Windows.Forms.TabPage tabColours;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label lblFocused;
		private System.Windows.Forms.Label lblSelectedBG;
		private System.Windows.Forms.Button btSelectedBG;
		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.Button btText;
		private System.Windows.Forms.Label lblBackground;
		private System.Windows.Forms.Button btBG;
		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Label lbl2;
		private System.Windows.Forms.Label lbl3;
		private System.Windows.Forms.Label lbl4;
		private System.Windows.Forms.Button btFocused;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.CheckBox chEditExtensions;
		private System.Windows.Forms.CheckBox chImmediateEdits;
		private System.ComponentModel.IContainer components;

		public FrmSettings()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.components = new System.ComponentModel.Container();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabColours = new System.Windows.Forms.TabPage();
			this.btFocused = new System.Windows.Forms.Button();
			this.lbl4 = new System.Windows.Forms.Label();
			this.lbl3 = new System.Windows.Forms.Label();
			this.lbl2 = new System.Windows.Forms.Label();
			this.lbl1 = new System.Windows.Forms.Label();
			this.lblBackground = new System.Windows.Forms.Label();
			this.btBG = new System.Windows.Forms.Button();
			this.lblText = new System.Windows.Forms.Label();
			this.btText = new System.Windows.Forms.Button();
			this.lblSelectedBG = new System.Windows.Forms.Label();
			this.btSelectedBG = new System.Windows.Forms.Button();
			this.lblFocused = new System.Windows.Forms.Label();
			this.tabSE = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btSERemove = new System.Windows.Forms.Button();
			this.btSEAdd = new System.Windows.Forms.Button();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.chEditExtensions = new System.Windows.Forms.CheckBox();
			this.chImmediateEdits = new System.Windows.Forms.CheckBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl.SuspendLayout();
			this.tabColours.SuspendLayout();
			this.tabSE.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabColours);
			this.tabControl.Controls.Add(this.tabSE);
			this.tabControl.Controls.Add(this.tabMisc);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(332, 247);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
			// 
			// tabColours
			// 
			this.tabColours.Controls.Add(this.btFocused);
			this.tabColours.Controls.Add(this.lbl4);
			this.tabColours.Controls.Add(this.lbl3);
			this.tabColours.Controls.Add(this.lbl2);
			this.tabColours.Controls.Add(this.lbl1);
			this.tabColours.Controls.Add(this.lblBackground);
			this.tabColours.Controls.Add(this.btBG);
			this.tabColours.Controls.Add(this.lblText);
			this.tabColours.Controls.Add(this.btText);
			this.tabColours.Controls.Add(this.lblSelectedBG);
			this.tabColours.Controls.Add(this.btSelectedBG);
			this.tabColours.Controls.Add(this.lblFocused);
			this.tabColours.Location = new System.Drawing.Point(4, 22);
			this.tabColours.Name = "tabColours";
			this.tabColours.Size = new System.Drawing.Size(324, 221);
			this.tabColours.TabIndex = 1;
			this.tabColours.Text = "List colours";
			// 
			// btFocused
			// 
			this.btFocused.Location = new System.Drawing.Point(13, 69);
			this.btFocused.Name = "btFocused";
			this.btFocused.Size = new System.Drawing.Size(127, 23);
			this.btFocused.TabIndex = 12;
			this.btFocused.Text = "Focused text";
			this.btFocused.Click += new System.EventHandler(this.btFocused_Click);
			// 
			// lbl4
			// 
			this.lbl4.Location = new System.Drawing.Point(13, 194);
			this.lbl4.Name = "lbl4";
			this.lbl4.Size = new System.Drawing.Size(84, 21);
			this.lbl4.TabIndex = 11;
			this.lbl4.Text = "Item number 4";
			this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl3
			// 
			this.lbl3.Location = new System.Drawing.Point(13, 173);
			this.lbl3.Name = "lbl3";
			this.lbl3.Size = new System.Drawing.Size(84, 21);
			this.lbl3.TabIndex = 10;
			this.lbl3.Text = "Item number 3";
			this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl2
			// 
			this.lbl2.Location = new System.Drawing.Point(13, 153);
			this.lbl2.Name = "lbl2";
			this.lbl2.Size = new System.Drawing.Size(84, 20);
			this.lbl2.TabIndex = 9;
			this.lbl2.Text = "Item number 2";
			this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl1
			// 
			this.lbl1.Location = new System.Drawing.Point(13, 132);
			this.lbl1.Name = "lbl1";
			this.lbl1.Size = new System.Drawing.Size(84, 21);
			this.lbl1.TabIndex = 8;
			this.lbl1.Text = "Item number 1";
			this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBackground
			// 
			this.lblBackground.BackColor = System.Drawing.Color.White;
			this.lblBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblBackground.Location = new System.Drawing.Point(147, 42);
			this.lblBackground.Name = "lblBackground";
			this.lblBackground.Size = new System.Drawing.Size(86, 20);
			this.lblBackground.TabIndex = 7;
			// 
			// btBG
			// 
			this.btBG.Location = new System.Drawing.Point(13, 42);
			this.btBG.Name = "btBG";
			this.btBG.Size = new System.Drawing.Size(127, 23);
			this.btBG.TabIndex = 6;
			this.btBG.Text = "Background";
			this.btBG.Click += new System.EventHandler(this.btBG_Click);
			// 
			// lblText
			// 
			this.lblText.BackColor = System.Drawing.Color.Black;
			this.lblText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblText.Location = new System.Drawing.Point(147, 14);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(86, 21);
			this.lblText.TabIndex = 5;
			// 
			// btText
			// 
			this.btText.Location = new System.Drawing.Point(13, 14);
			this.btText.Name = "btText";
			this.btText.Size = new System.Drawing.Size(127, 23);
			this.btText.TabIndex = 4;
			this.btText.Text = "Text";
			this.btText.Click += new System.EventHandler(this.btText_Click);
			// 
			// lblSelectedBG
			// 
			this.lblSelectedBG.BackColor = System.Drawing.Color.Yellow;
			this.lblSelectedBG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblSelectedBG.Location = new System.Drawing.Point(147, 97);
			this.lblSelectedBG.Name = "lblSelectedBG";
			this.lblSelectedBG.Size = new System.Drawing.Size(86, 21);
			this.lblSelectedBG.TabIndex = 3;
			// 
			// btSelectedBG
			// 
			this.btSelectedBG.Location = new System.Drawing.Point(13, 97);
			this.btSelectedBG.Name = "btSelectedBG";
			this.btSelectedBG.Size = new System.Drawing.Size(127, 23);
			this.btSelectedBG.TabIndex = 2;
			this.btSelectedBG.Text = "Selected background";
			this.btSelectedBG.Click += new System.EventHandler(this.btSelectedBG_Click);
			// 
			// lblFocused
			// 
			this.lblFocused.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblFocused.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFocused.Location = new System.Drawing.Point(147, 69);
			this.lblFocused.Name = "lblFocused";
			this.lblFocused.Size = new System.Drawing.Size(86, 21);
			this.lblFocused.TabIndex = 1;
			// 
			// tabSE
			// 
			this.tabSE.Controls.Add(this.label2);
			this.tabSE.Controls.Add(this.label1);
			this.tabSE.Controls.Add(this.btSERemove);
			this.tabSE.Controls.Add(this.btSEAdd);
			this.tabSE.Location = new System.Drawing.Point(4, 22);
			this.tabSE.Name = "tabSE";
			this.tabSE.Size = new System.Drawing.Size(324, 221);
			this.tabSE.TabIndex = 0;
			this.tabSE.Text = "ShellExtension";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(267, 35);
			this.label2.TabIndex = 3;
			this.label2.Text = "Remove shell extension, remove the shell extension created by the \"Add shell exte" +
				"nsion\" button.";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(267, 35);
			this.label1.TabIndex = 2;
			this.label1.Text = "Add shell extension, allows starting Renamer by right clicking a directory in you" +
				"r file manager";
			// 
			// btSERemove
			// 
			this.btSERemove.Location = new System.Drawing.Point(13, 146);
			this.btSERemove.Name = "btSERemove";
			this.btSERemove.TabIndex = 1;
			this.btSERemove.Text = "Remove";
			this.btSERemove.Click += new System.EventHandler(this.btSERemove_Click);
			// 
			// btSEAdd
			// 
			this.btSEAdd.Location = new System.Drawing.Point(13, 55);
			this.btSEAdd.Name = "btSEAdd";
			this.btSEAdd.TabIndex = 0;
			this.btSEAdd.Text = "Add";
			this.btSEAdd.Click += new System.EventHandler(this.btSEAdd_Click);
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.chEditExtensions);
			this.tabMisc.Controls.Add(this.chImmediateEdits);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Size = new System.Drawing.Size(324, 221);
			this.tabMisc.TabIndex = 2;
			this.tabMisc.Text = "Miscellaneous";
			// 
			// chEditExtensions
			// 
			this.chEditExtensions.Checked = true;
			this.chEditExtensions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chEditExtensions.Location = new System.Drawing.Point(8, 32);
			this.chEditExtensions.Name = "chEditExtensions";
			this.chEditExtensions.Size = new System.Drawing.Size(296, 16);
			this.chEditExtensions.TabIndex = 1;
			this.chEditExtensions.Text = "Edit filename extensions";
			this.toolTip.SetToolTip(this.chEditExtensions, "When enabled filename extensions are affected by changes, if not the extension is" +
				" treated separatly.");
			// 
			// chImmediateEdits
			// 
			this.chImmediateEdits.Location = new System.Drawing.Point(8, 8);
			this.chImmediateEdits.Name = "chImmediateEdits";
			this.chImmediateEdits.Size = new System.Drawing.Size(296, 16);
			this.chImmediateEdits.TabIndex = 0;
			this.chImmediateEdits.Text = "Edits to filenames are reflected in list immediately";
			this.toolTip.SetToolTip(this.chImmediateEdits, "NOT IMPLEMENTED YET!!!");
			// 
			// FrmSettings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(348, 263);
			this.Controls.Add(this.tabControl);
			this.DockPadding.All = 8;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.FrmSettings_Load);
			this.tabControl.ResumeLayout(false);
			this.tabColours.ResumeLayout(false);
			this.tabSE.ResumeLayout(false);
			this.tabMisc.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tabControl_TabIndexChanged(object sender, System.EventArgs e)
		{
			InitTabPage();
		}

		private void InitTabPage()
		{
			switch (tabControl.TabPages[tabControl.SelectedIndex].Name)
			{
				case "tabSE":
					bool exists = ShellExtension.CheckShellExtension();
					btSEAdd.Enabled = !exists;
					btSERemove.Enabled = exists;
					break;
				case "tabColours":
					DisplayColours();
					break;
			}
		}

		#region Open/Save
		const string file = "\\settings.dat";
		const int Version = 1;

		bool successful = false;

		public bool Successful
		{
			get { return successful; }
		}

		public bool EditExtensions
		{
			get { return chEditExtensions.Checked; }
		}

		public bool ImmediateEdits
		{
			get { return chImmediateEdits.Checked; }
		}

		static string ReadLine(StreamReader r)
		{
			string s = "'";

			while (s.Length > 0 && s[0] == '\'')
			{
				s = r.ReadLine();
				if (s == null) throw new EndOfStreamException();
			}

			return s;
		}

		static int ReadInt(StreamReader r)
		{
			return int.Parse(ReadLine(r));
		}

		static bool ReadBool(StreamReader r)
		{
			return bool.Parse(ReadLine(r));
		}

		public void Open()
		{
			try
			{
				StreamReader r = new StreamReader(PathFinder.OpenFile(file));

				using (r)
				{
					string s;
					int count;

					int version = ReadInt(r);


					//FrmMain
					s = ReadLine(r);
					if (s == FormWindowState.Maximized.ToString())
						MainWindowState = FormWindowState.Maximized;
					else if (s == FormWindowState.Minimized.ToString())
						MainWindowState = FormWindowState.Minimized;
					else
						MainWindowState = FormWindowState.Normal;

					MainWindowLocation.X = ReadInt(r);
					MainWindowLocation.Y = ReadInt(r);
					MainWindowSize.Width = ReadInt(r);
					MainWindowSize.Height = ReadInt(r);

					count = ReadInt(r);
					ListColumns = new ListColumns[count];
					for (int i = 0 ; i < count ; i++)
						ListColumns[i] = (ListColumns)ReadInt(r);

					ListColumnWidths = new int[count];
					for (int i = 0 ; i < count ; i++)
						ListColumnWidths[i] = ReadInt(r);

					ViewEdit = ReadBool(r);
					ViewMacros = ReadBool(r);
					ViewNavigation = ReadBool(r);
					ViewOperations = ReadBool(r);
					ViewStatusbar = ReadBool(r);

					count = ReadInt(r);
					ScanPatterns = new string[count];
					for (int i = 0 ; i < count ; i++)
						ScanPatterns[i] = ReadLine(r);

					ScanFiles = ReadBool(r);
					ScanDirs = ReadBool(r);
					ScanRecurse = ReadBool(r);
					ScanPath = ReadLine(r);


					//FrmSettings
					ListColourText = Color.FromArgb(ReadInt(r));
					ListColourBackground = Color.FromArgb(ReadInt(r));
					ListColourFocusedText = Color.FromArgb(ReadInt(r));
					ListColourSelectedBackground = Color.FromArgb(ReadInt(r));

					chImmediateEdits.Checked = ReadBool(r);
					chEditExtensions.Checked = ReadBool(r);
				}

				successful = true;
			}
			catch (FileNotFoundException)
			{
				//Ignore
			}
			catch (Exception ex)
			{
				FrmMain.ErrorMessage(ex.Message);
			}
		}

		public void Save()
		{
			try
			{
				StreamWriter w = new StreamWriter(PathFinder.SaveFile(file));

				using (w)
				{
					//Version
					w.WriteLine("'Perferences file version");
					w.WriteLine(Version);


					//FrmMain
					w.WriteLine("'");
					w.WriteLine("' FrmMain");
					w.WriteLine("'");

					w.WriteLine("'Window (state, x, y, width, height)");
					w.WriteLine(MainWindowState.ToString());
					w.WriteLine(MainWindowLocation.X);
					w.WriteLine(MainWindowLocation.Y);
					w.WriteLine(MainWindowSize.Width);
					w.WriteLine(MainWindowSize.Height);

					w.WriteLine("'ListColumns (count, names, widths)");
					w.WriteLine(ListColumns.Length);
					foreach (ListColumns s in ListColumns)
						w.WriteLine((int)s);

					foreach (int s in ListColumnWidths)
						w.WriteLine(s);

					w.WriteLine("'View (edit, macros, navigation, operations, statusbar)");
					w.WriteLine(ViewEdit);
					w.WriteLine(ViewMacros);
					w.WriteLine(ViewNavigation);
					w.WriteLine(ViewOperations);
					w.WriteLine(ViewStatusbar);

					w.WriteLine("'Scanning (patterncount, patterns, files, dirs, recurse)");
					w.WriteLine(ScanPatterns.Length);
					foreach (string s in ScanPatterns)
						w.WriteLine(s);

					w.WriteLine(ScanFiles);
					w.WriteLine(ScanDirs);
					w.WriteLine(ScanRecurse);
					w.WriteLine(ScanPath);


					//FrmSettings
					w.WriteLine("'");
					w.WriteLine("' FrmSettings");
					w.WriteLine("'");

					w.WriteLine("'Colours in ARGB format, Text, Background, Focused text, Selected background");
					w.WriteLine(ListColourText.ToArgb().ToString());
					w.WriteLine(ListColourBackground.ToArgb().ToString());
					w.WriteLine(ListColourFocusedText.ToArgb().ToString());
					w.WriteLine(ListColourSelectedBackground.ToArgb().ToString());
					
					w.WriteLine(chImmediateEdits.Checked.ToString());
					w.WriteLine(chEditExtensions.Checked.ToString());
				}
			}
			catch (Exception ex)
			{
				FrmMain.ErrorMessage(ex.Message);
			}
		}
		#endregion

		#region ShellExtension
		private void btSEAdd_Click(object sender, System.EventArgs e)
		{
			ShellExtension.SetShellExtension();
			InitTabPage();
		}

		private void btSERemove_Click(object sender, System.EventArgs e)
		{
			ShellExtension.ClearShellExtension();
			InitTabPage();
		}
		#endregion

		#region List colours
		public Color ListColourText
		{
			get { return lblText.BackColor; }
			set { lblText.BackColor = value; }
		}

		public Color ListColourBackground
		{
			get { return lblBackground.BackColor; }
			set { lblBackground.BackColor = value; }
		}

		public Color ListColourFocusedText
		{
			get { return lblFocused.BackColor; }
			set { lblFocused.BackColor = value; }
		}

		public Color ListColourSelectedBackground
		{
			get { return lblSelectedBG.BackColor; }
			set { lblSelectedBG.BackColor = value; }
		}

		private void PickColour(Label label)
		{
			colorDialog1.Color = label.BackColor;

			DialogResult res = colorDialog1.ShowDialog();

			if (res != DialogResult.OK) return;

			label.BackColor = colorDialog1.Color;

			DisplayColours();
		}

		private void DisplayColours()
		{
			lbl1.BackColor = lblBackground.BackColor;
			lbl2.BackColor = lblBackground.BackColor;
			lbl3.BackColor = lblSelectedBG.BackColor;
			lbl4.BackColor = lblSelectedBG.BackColor;

			lbl1.ForeColor = lblText.BackColor;
			lbl2.ForeColor = lblText.BackColor;
			lbl3.ForeColor = lblText.BackColor;
			lbl4.ForeColor = lblFocused.BackColor;
		}

		private void btText_Click(object sender, System.EventArgs e)
		{
			PickColour(lblText);
		}

		private void btBG_Click(object sender, System.EventArgs e)
		{
			PickColour(lblBackground);
		}

		private void btFocused_Click(object sender, System.EventArgs e)
		{
			PickColour(lblFocused);
		}

		private void btSelectedBG_Click(object sender, System.EventArgs e)
		{
			PickColour(lblSelectedBG);
		}
		#endregion

		#region FrmMain
		public FormWindowState MainWindowState = FormWindowState.Normal;
		public Point MainWindowLocation;
		public Size MainWindowSize;
		public ListColumns[] ListColumns = FileName.DefaultColumns;
		public int[] ListColumnWidths = FileName.DefaultColumnWidths;
		public bool ViewEdit = true;
		public bool ViewMacros = true;
		public bool ViewOperations = true;
		public bool ViewNavigation = true;
		public bool ViewStatusbar = false;
		public string[] ScanPatterns = {"*.*"};
		public bool ScanFiles = true;
		public bool ScanDirs = false;
		public bool ScanRecurse = false;
		public string ScanPath = "";
		#endregion

		private void FrmSettings_Load(object sender, System.EventArgs e)
		{
			InitTabPage();
		}
	}
}
