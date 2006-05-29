using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using RenamerNG.FileNameOperations;
using RenamerNG.SpecialOperations;
using RenamerNG.ListOperations;
using RenamerNG.Macros;

namespace RenamerNG
{
	public class FrmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panelNavigation;
		private System.Windows.Forms.Panel panelCommandLine;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panelMacro;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem miViewMacro;
		private System.Windows.Forms.MenuItem miViewNavigation;
		private System.Windows.Forms.MenuItem miViewEdit;
		private System.Windows.Forms.GroupBox grpMacro;
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.MenuItem miEdit;
		private System.Windows.Forms.MenuItem miView;
		private System.Windows.Forms.Panel panelList;
		private System.Windows.Forms.ListView listMain;
		private System.Windows.Forms.Panel panelEdit;
		private System.Windows.Forms.Panel panelPath;
		private System.Windows.Forms.Button btPickDir;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.CheckBox chDirs;
		private System.Windows.Forms.CheckBox chFiles;
		private System.Windows.Forms.CheckBox chRecurse;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.TextBox tbPath;
		private System.Windows.Forms.Panel panelStatusbar;
		private System.Windows.Forms.MenuItem miEditMacros;
		private System.Windows.Forms.Panel panelPathButtons;
		private System.Windows.Forms.Panel panelEditButtons;
		private System.Windows.Forms.Label lblLength;
		private System.Windows.Forms.TextBox tbEdit;
		private System.Windows.Forms.Label lblItems;
		private System.Windows.Forms.Button btUp;
		private System.Windows.Forms.MenuItem miViewStatusbar;
		private System.Windows.Forms.MenuItem miEditListColumns;
		private System.Windows.Forms.MenuItem miHelp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboPattern;
		private System.Windows.Forms.MenuItem miFileRename;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem miFileExit;
		private System.Windows.Forms.MenuItem miHelpAbout;
		private System.Windows.Forms.Button btScan;
		private System.Windows.Forms.MenuItem miEditSettings;
		private System.Windows.Forms.Panel panelMacroInternal;
		private System.Windows.Forms.MenuItem miFileOperations;
		private System.Windows.Forms.MenuItem miListOperations;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem miOperationsWorkOnAllItems;
		private System.Windows.Forms.MenuItem miOperationsWorkOnSelectedItems;
		private System.Windows.Forms.MenuItem miOperationsSpecial;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem miOperationsFreeEdit;
		private System.Windows.Forms.MenuItem miFileRepeat;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.Panel panelCommandLineEdit;
		private System.Windows.Forms.ListBox listboxCommandHistory;
		private System.Windows.Forms.TextBox tbCommand;
		private System.Windows.Forms.MenuItem miViewCommandLine;
		private System.ComponentModel.IContainer components;

		public FrmMain(string dir)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//Add version number to title bar
			Text += Application.ProductVersion;

			//Add macro event handlers and fix macro GUI
			macroList1 = new RenamerNG.Macros.MacroList(this);
			macroList1.Dock = DockStyle.Fill;
			macroList1.Execute += new RenamerNG.Macros.MacroList.ExecuteEventHandler(macroList_Execute);
			macroList1.RecordingStarted += new RenamerNG.Macros.MacroList.RecordingEventHandler(macroList1_RecordingStarted);
			macroList1.RecordingStopped += new RenamerNG.Macros.MacroList.RecordingEventHandler(macroList1_RecordingStopped);
			panelMacroInternal.Controls.Add(macroList1);

			recordingIndicator1 = new RenamerNG.Macros.RecordingIndicator();
			recordingIndicator1.Dock = DockStyle.Bottom;
			panelMacroInternal.Controls.Add(recordingIndicator1);

			//Create operation menus
			AddFileNameOperations();
			AddListOperations();
			CreateOperationsMenu(fileOperations, miFileOperations, new EventHandler(miOperation_Click));
			CreateOperationsMenu(listOperations, miEdit, new EventHandler(miOperation_Click));

			//Load settings
			settings = new Settings.FrmSettings();
			settings.Open();
			macroList1.Open();

			//Update GUI according to settings
			if (!settings.ViewEdit) miViewEdit_Click(null, null);
			if (!settings.ViewMacros) miViewMacro_Click(null, null);
			if (!settings.ViewNavigation) miViewNavigation_Click(null, null);
			//if (!settings.ViewStatusbar) miViewStatusbar_Click(null, null);
			if (settings.ViewCommandLine) miViewCommandLine_Click(null, null);

			//Restore scan patterns
			foreach (string s in settings.ScanPatterns)
				comboPattern.Items.Add(s);
			comboPattern.Text = settings.ScanPatterns[0];

			chFiles.Checked = settings.ScanFiles;
			chDirs.Checked = settings.ScanDirs;
			chRecurse.Checked = settings.ScanRecurse;

			tbPath.Text = dir;
			if (dir == "")
			{
#if DEBUG
				CheckDuplicateShortcuts();
				#region Create a set of test file names
				//const string pathBase = @"Z:\";
				const string pathBase = @"C:\temp\RenamerNGTest\";
				Directory.CreateDirectory(pathBase);
				File.Create(pathBase + @"1").Close();
				File.Create(pathBase + @"2").Close();
				File.Create(pathBase + @"3").Close();
				File.Create(pathBase + @".ext").Close();
				File.Create(pathBase + @"test.extension").Close();
				File.Create(pathBase + @"test.ext").Close();
				File.Create(pathBase + @"Tyna Turner.txt").Close();
				File.Create(pathBase + @"tyna turner.nfo").Close();
				File.Create(pathBase + @"Tyna Turner - Simply the Best.mp3").Close();
				File.Create(pathBase + @"Tyna Turner - Nutbush city limits.mp3").Close();
				File.Create(pathBase + @"Metallica - Wherever I may roam.mp3").Close();
				File.Create(pathBase + @"metallica - enter the sandman.mp3").Close();
				File.Create(pathBase + @"Metallica - Master of Puppets.mp3").Close();
				Directory.CreateDirectory(pathBase + @"Metallica");
				Directory.CreateDirectory(pathBase + @"Metallica\Metallica");
				Directory.CreateDirectory(pathBase + @"Metallica\... and Justice for All");
				File.Create(pathBase + @"Metallica\Metallica - Wherever I may roam.mp3").Close();
				File.Create(pathBase + @"Metallica\Metallica - Enter the sandman.mp3").Close();
				Directory.CreateDirectory(pathBase + @"DateTime");
				File.Create(pathBase + @"DateTime\Long 060505.doc").Close(); //Long 2005-06-05.doc
				File.Create(pathBase + @"DateTime\Long 020502.doc").Close(); //Long 2002-02-05.doc
				File.Create(pathBase + @"DateTime\Long 103185.doc").Close(); //Long 1985-10-31.doc
				File.Create(pathBase + @"DateTime\Long 010100.doc").Close(); //Long 2000-01-01.doc
				File.Create(pathBase + @"DateTime\Long 090899 v2.doc").Close(); //Long 1999-09-08 v2.doc
				File.Create(pathBase + @"DateTime\Long 101288 s.doc").Close(); //Long 1988-10-12 s.doc
				File.Create(pathBase + @"DateTime\Long 082099 v2.doc").Close(); //Long 1999-08-20 v2.doc
				File.Create(pathBase + @"DateTime\Long8 05041996.doc").Close(); //Long8 1996-05-04.doc
				File.Create(pathBase + @"DateTime\Short 0504.doc").Close(); //Short 2004-05.doc
				File.Create(pathBase + @"DateTime\Short 0599.doc").Close(); //Short 1999-05.doc
				File.Create(pathBase + @"DateTime\Invalid 082504v2.doc").Close(); //Unchanged because 6 integers not followed by space or “.”)
				File.Create(pathBase + @"DateTime\Invalid 200899 v2.doc").Close(); //Not a valid date
				File.Create(pathBase + @"DateTime\Invalid Last Sample Jan 8 2004.doc").Close(); //Unhandled date format
				File.Create(pathBase + @"DateTime\Invalid 8-15-02.doc").Close(); //Unchanged not six characters
				File.Create(pathBase + @"DateTime\Invalid-101288 s.doc").Close(); //
				File.Create(pathBase + @"DateTime\Invalid101288 s.doc").Close(); //
				File.Create(pathBase + @"DateTime\Invalid082099 v2.doc").Close(); //
				File.Create(pathBase + @"DateTime\Valid (don't touch) 2002-02-05.doc").Close(); //Correct should be untouched
				File.Create(pathBase + @"DateTime\Valid (don't touch) 2002-05.doc").Close(); //Correct should be untouched
				File.Create(pathBase + @"DateTime\1104 short.doc").Close(); //2004-11 short.doc
				File.Create(pathBase + @"DateTime\1199 short.doc").Close(); //1999-11 short.doc
				File.Create(pathBase + @"DateTime\103185 long.doc").Close(); //1985-10-31 long.doc
				File.Create(pathBase + @"DateTime\020502 long.doc").Close(); //2002-02-05 long.doc
				//File.Create(pathBase + @"DateTime\").Close(); //
				#endregion
				tbPath.Text = pathBase;
#else
				tbPath.Text = settings.ScanPath;
#endif
			}

			UpdateListColumns();

            //Remove this eventhandler since default is n"work on all items".
            listMain.ItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(listMain_ItemSelectionChanged);
		}

		RenamerNG.Macros.RecordingIndicator recordingIndicator1;
		RenamerNG.Macros.MacroList macroList1;

		OperationList fileOperations = new OperationList();
		OperationList listOperations = new OperationList();
		int editPosition = 0;
		Settings.FrmSettings settings;
		object previousAction = null; //Used for repeating last operation/macro

		private void AddFileNameOperations()
		{
			fileOperations.Add(new FileNameOperations.Replace());
			fileOperations.Add(new FileNameOperations.ReplaceCharacters());
			fileOperations.Add(new FileNameOperations.Insert());
			fileOperations.Add(new FileNameOperations.InsertNumber());
			fileOperations.Add(new FileNameOperations.RegExp());
			fileOperations.Add(new FileNameOperations.Undo());

			//Date operations
			fileOperations.Add(new FileNameOperations.DateInsertCurrent());
			fileOperations.Add(new FileNameOperations.DateInsertFileCreated());
			fileOperations.Add(new FileNameOperations.DateInsertFileLastWritten());
			fileOperations.Add(new FileNameOperations.DateInsertFileLastAccessed());

			//Trimming operations
			fileOperations.Add(new FileNameOperations.Trim());
			fileOperations.Add(new FileNameOperations.TrimMultipleSpaces());
			fileOperations.Add(new FileNameOperations.TrimLeadingSpaces());
			fileOperations.Add(new FileNameOperations.TrimTailingSpaces());

			//Case operations
			fileOperations.Add(new FileNameOperations.LowerCase());
			fileOperations.Add(new FileNameOperations.UpperCase());
			fileOperations.Add(new FileNameOperations.LargeCase());
			fileOperations.Add(new FileNameOperations.SentenceCase());
			fileOperations.Add(new FileNameOperations.InvertCase());
			fileOperations.Add(new FileNameOperations.RandomCase());
		}

		private void AddListOperations()
		{
			listOperations.Add(new ListOperations.SelectAll());
			listOperations.Add(new ListOperations.SelectNone());
			listOperations.Add(new ListOperations.SelectMatching());
            listOperations.Add(new ListOperations.SelectRegExp());
            listOperations.Add(new ListOperations.DeselectMatching());
			listOperations.Add(new ListOperations.InvertSelection());
			listOperations.Add(new ListOperations.FilterChanged());
			listOperations.Add(new ListOperations.FilterSelected());
		}

		public bool ValidMacroName(string name)
		{
			foreach (Operation op in fileOperations.Values)
				if (op.Name.ToLower() == name.ToLower()) return false;

			foreach (Operation op in listOperations.Values)
				if (op.Name.ToLower() == name.ToLower()) return false;

			return true;
		}

		#region Build operations menu
		private void CreateOperationsMenu(OperationList operations, MenuItem miOperations, EventHandler onClick)
		{
			SortedList groups = new SortedList();
			int groupCount = 0;
			int operationCount = 0;

			//Create all non grouped operations
			foreach (Operation o in operations.Values)
			{
				if (o.Group == "")
				{
					operationCount++;
					MenuItem m = new MenuItem(o.Name, onClick, o.Key);
					miOperations.MenuItems.Add(m);
				}
				else
				{
					groupCount++;
				}
			}

			//If no grouped operations exist, exit (avoids adding separator).
			if (groupCount == 0) return;

			//Insert separator
			if (operationCount > 0)
			{
				MenuItem separator = new MenuItem("-");
				miOperations.MenuItems.Add(separator);
			}

			//Create all groups
			foreach (Operation o in operations.Values)
			{
				if (o.Group != "")
				{
					MenuItem group = (MenuItem)groups[o.Group];

					if (group == null)
					{
						MenuItem m = new MenuItem(o.Group);
						miOperations.MenuItems.Add(m);
						groups.Add(o.Group, m);
					}
				}
			}

			//Create all grouped operations
			foreach (Operation o in operations.Values)
			{
				if (o.Group != "")
				{
					MenuItem m = new MenuItem(o.Name, onClick, o.Key);
					((MenuItem)groups[o.Group]).MenuItems.Add(m);
				}
			}
		}

#if DEBUG
		private void CheckDuplicateShortcuts()
		{
			SortedList list = new SortedList();

			foreach (MenuItem m in mainMenu.MenuItems)
				AddShortcuts(list, m);
		}


		private void AddShortcuts(SortedList list, MenuItem mi)
		{
			try
			{
				if (mi.Shortcut != Shortcut.None)
					list.Add(mi.Shortcut, mi);
			}
			catch (ArgumentException)
			{
				string msg = "";
				msg += "Duplicate menu shortcut found:\n";
				msg += GetMenuItemPath(mi) + "\n";
				msg += "Equal to:\n";
				msg += GetMenuItemPath((MenuItem)list[mi.Shortcut]);

				FrmMain.ErrorMessage(msg);
			}

			foreach (MenuItem m in mi.MenuItems)
				AddShortcuts(list, m);
		}

		private string GetMenuItemPath(MenuItem find)
		{
			foreach (MenuItem m in mainMenu.MenuItems)
			{
				string s = GetMenuItemPathSearch(m, find);
				if (s != "")
					return m.Text + "->" + s;
			}

			return "";
		}

		private string GetMenuItemPathSearch(MenuItem mi, MenuItem find)
		{
			if (mi == find) return mi.Shortcut.ToString();

			foreach (MenuItem m in mi.MenuItems)
			{
				string s = GetMenuItemPathSearch(m, find);
				if (s != "")
					return m.Text + "->" + s;
			}

			return "";
		}
#endif
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panelStatusbar = new System.Windows.Forms.Panel();
            this.lblItems = new System.Windows.Forms.Label();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.comboPattern = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chRecurse = new System.Windows.Forms.CheckBox();
            this.chFiles = new System.Windows.Forms.CheckBox();
            this.chDirs = new System.Windows.Forms.CheckBox();
            this.panelPath = new System.Windows.Forms.Panel();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.panelPathButtons = new System.Windows.Forms.Panel();
            this.btScan = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btPickDir = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.panelCommandLine = new System.Windows.Forms.Panel();
            this.listboxCommandHistory = new System.Windows.Forms.ListBox();
            this.panelCommandLineEdit = new System.Windows.Forms.Panel();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.listMain = new System.Windows.Forms.ListView();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.tbEdit = new System.Windows.Forms.TextBox();
            this.panelEditButtons = new System.Windows.Forms.Panel();
            this.lblLength = new System.Windows.Forms.Label();
            this.panelMacro = new System.Windows.Forms.Panel();
            this.grpMacro = new System.Windows.Forms.GroupBox();
            this.panelMacroInternal = new System.Windows.Forms.Panel();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miFileRename = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miFileRepeat = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miFileExit = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miView = new System.Windows.Forms.MenuItem();
            this.miViewEdit = new System.Windows.Forms.MenuItem();
            this.miViewMacro = new System.Windows.Forms.MenuItem();
            this.miViewNavigation = new System.Windows.Forms.MenuItem();
            this.miViewStatusbar = new System.Windows.Forms.MenuItem();
            this.miViewCommandLine = new System.Windows.Forms.MenuItem();
            this.miFileOperations = new System.Windows.Forms.MenuItem();
            this.miOperationsWorkOnAllItems = new System.Windows.Forms.MenuItem();
            this.miOperationsWorkOnSelectedItems = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miOperationsSpecial = new System.Windows.Forms.MenuItem();
            this.miOperationsFreeEdit = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miListOperations = new System.Windows.Forms.MenuItem();
            this.miEditListColumns = new System.Windows.Forms.MenuItem();
            this.miEditMacros = new System.Windows.Forms.MenuItem();
            this.miEditSettings = new System.Windows.Forms.MenuItem();
            this.miHelp = new System.Windows.Forms.MenuItem();
            this.miHelpAbout = new System.Windows.Forms.MenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panelNavigation.SuspendLayout();
            this.panelPath.SuspendLayout();
            this.panelPathButtons.SuspendLayout();
            this.panelCommandLine.SuspendLayout();
            this.panelCommandLineEdit.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panelEdit.SuspendLayout();
            this.panelEditButtons.SuspendLayout();
            this.panelMacro.SuspendLayout();
            this.grpMacro.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStatusbar
            // 
            this.panelStatusbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusbar.Location = new System.Drawing.Point(8, 424);
            this.panelStatusbar.Name = "panelStatusbar";
            this.panelStatusbar.Size = new System.Drawing.Size(596, 0);
            this.panelStatusbar.TabIndex = 0;
            this.panelStatusbar.Visible = false;
            // 
            // lblItems
            // 
            this.lblItems.Location = new System.Drawing.Point(0, 35);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(47, 19);
            this.lblItems.TabIndex = 1;
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.toolTip.SetToolTip(this.lblItems, "Number of items in list");
            // 
            // panelNavigation
            // 
            this.panelNavigation.Controls.Add(this.comboPattern);
            this.panelNavigation.Controls.Add(this.label1);
            this.panelNavigation.Controls.Add(this.chRecurse);
            this.panelNavigation.Controls.Add(this.chFiles);
            this.panelNavigation.Controls.Add(this.chDirs);
            this.panelNavigation.Controls.Add(this.panelPath);
            this.panelNavigation.Controls.Add(this.lblItems);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigation.Location = new System.Drawing.Point(8, 8);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(596, 53);
            this.panelNavigation.TabIndex = 4;
            // 
            // comboPattern
            // 
            this.comboPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPattern.Location = new System.Drawing.Point(313, 28);
            this.comboPattern.Name = "comboPattern";
            this.comboPattern.Size = new System.Drawing.Size(74, 21);
            this.comboPattern.TabIndex = 4;
            this.toolTip.SetToolTip(this.comboPattern, "File pattern to match (files only)");
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(387, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pattern";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chRecurse
            // 
            this.chRecurse.Location = new System.Drawing.Point(233, 28);
            this.chRecurse.Name = "chRecurse";
            this.chRecurse.Size = new System.Drawing.Size(67, 21);
            this.chRecurse.TabIndex = 2;
            this.chRecurse.Text = "Recurse";
            this.toolTip.SetToolTip(this.chRecurse, "Recurse into sub directories");
            // 
            // chFiles
            // 
            this.chFiles.Checked = true;
            this.chFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chFiles.Location = new System.Drawing.Point(160, 28);
            this.chFiles.Name = "chFiles";
            this.chFiles.Size = new System.Drawing.Size(67, 21);
            this.chFiles.TabIndex = 1;
            this.chFiles.Text = "Add files";
            this.toolTip.SetToolTip(this.chFiles, "Add files");
            this.chFiles.CheckedChanged += new System.EventHandler(this.chDirs_CheckedChanged);
            // 
            // chDirs
            // 
            this.chDirs.Location = new System.Drawing.Point(87, 28);
            this.chDirs.Name = "chDirs";
            this.chDirs.Size = new System.Drawing.Size(66, 21);
            this.chDirs.TabIndex = 0;
            this.chDirs.Text = "Add dirs";
            this.toolTip.SetToolTip(this.chDirs, "Add directories");
            this.chDirs.CheckedChanged += new System.EventHandler(this.chDirs_CheckedChanged);
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.tbPath);
            this.panelPath.Controls.Add(this.panelPathButtons);
            this.panelPath.Controls.Add(this.lblPath);
            this.panelPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPath.Location = new System.Drawing.Point(0, 0);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(596, 21);
            this.panelPath.TabIndex = 0;
            // 
            // tbPath
            // 
            this.tbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPath.Location = new System.Drawing.Point(53, 0);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(410, 20);
            this.tbPath.TabIndex = 1;
            this.toolTip.SetToolTip(this.tbPath, "Current path");
            this.tbPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPath_KeyPress);
            // 
            // panelPathButtons
            // 
            this.panelPathButtons.Controls.Add(this.btScan);
            this.panelPathButtons.Controls.Add(this.btUp);
            this.panelPathButtons.Controls.Add(this.btPickDir);
            this.panelPathButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelPathButtons.Location = new System.Drawing.Point(463, 0);
            this.panelPathButtons.Name = "panelPathButtons";
            this.panelPathButtons.Size = new System.Drawing.Size(133, 21);
            this.panelPathButtons.TabIndex = 3;
            // 
            // btScan
            // 
            this.btScan.Location = new System.Drawing.Point(93, 0);
            this.btScan.Name = "btScan";
            this.btScan.Size = new System.Drawing.Size(40, 21);
            this.btScan.TabIndex = 2;
            this.btScan.Text = "Scan";
            this.toolTip.SetToolTip(this.btScan, "Move up a directory");
            this.btScan.Click += new System.EventHandler(this.btScan_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(50, 0);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(40, 21);
            this.btUp.TabIndex = 1;
            this.btUp.Text = "Up";
            this.toolTip.SetToolTip(this.btUp, "Move up a directory");
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btPickDir
            // 
            this.btPickDir.Location = new System.Drawing.Point(7, 0);
            this.btPickDir.Name = "btPickDir";
            this.btPickDir.Size = new System.Drawing.Size(40, 21);
            this.btPickDir.TabIndex = 0;
            this.btPickDir.Text = "Pick";
            this.toolTip.SetToolTip(this.btPickDir, "Select directory");
            this.btPickDir.Click += new System.EventHandler(this.btPickDir_Click);
            // 
            // lblPath
            // 
            this.lblPath.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPath.Location = new System.Drawing.Point(0, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(53, 21);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Directory";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCommandLine
            // 
            this.panelCommandLine.Controls.Add(this.listboxCommandHistory);
            this.panelCommandLine.Controls.Add(this.panelCommandLineEdit);
            this.panelCommandLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCommandLine.Location = new System.Drawing.Point(8, 312);
            this.panelCommandLine.Name = "panelCommandLine";
            this.panelCommandLine.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelCommandLine.Size = new System.Drawing.Size(596, 112);
            this.panelCommandLine.TabIndex = 5;
            this.panelCommandLine.Visible = false;
            // 
            // listboxCommandHistory
            // 
            this.listboxCommandHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxCommandHistory.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxCommandHistory.ItemHeight = 14;
            this.listboxCommandHistory.Location = new System.Drawing.Point(0, 10);
            this.listboxCommandHistory.Name = "listboxCommandHistory";
            this.listboxCommandHistory.Size = new System.Drawing.Size(596, 74);
            this.listboxCommandHistory.TabIndex = 2;
            // 
            // panelCommandLineEdit
            // 
            this.panelCommandLineEdit.Controls.Add(this.tbCommand);
            this.panelCommandLineEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCommandLineEdit.Location = new System.Drawing.Point(0, 88);
            this.panelCommandLineEdit.Name = "panelCommandLineEdit";
            this.panelCommandLineEdit.Size = new System.Drawing.Size(596, 24);
            this.panelCommandLineEdit.TabIndex = 1;
            // 
            // tbCommand
            // 
            this.tbCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCommand.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCommand.Location = new System.Drawing.Point(0, 0);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(596, 20);
            this.tbCommand.TabIndex = 0;
            this.tbCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommand_KeyPress);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelList);
            this.panelMain.Controls.Add(this.panelMacro);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(8, 61);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(596, 251);
            this.panelMain.TabIndex = 3;
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.listMain);
            this.panelList.Controls.Add(this.panelEdit);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Name = "panelList";
            this.panelList.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.panelList.Size = new System.Drawing.Size(436, 251);
            this.panelList.TabIndex = 3;
            // 
            // listMain
            // 
            this.listMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMain.ForeColor = System.Drawing.Color.Black;
            this.listMain.FullRowSelect = true;
            this.listMain.LabelEdit = true;
            this.listMain.LabelWrap = false;
            this.listMain.Location = new System.Drawing.Point(0, 0);
            this.listMain.Name = "listMain";
            this.listMain.Size = new System.Drawing.Size(436, 227);
            this.listMain.TabIndex = 0;
            this.listMain.UseCompatibleStateImageBehavior = false;
            this.listMain.View = System.Windows.Forms.View.Details;
            this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
            this.listMain.SelectedIndexChanged += new System.EventHandler(this.listMain_SelectedIndexChanged);
            this.listMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listMain_MouseDown);
            this.listMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listMain_KeyDown);
            this.listMain.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listMain_ItemSelectionChanged);
            this.listMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listMain_KeyDown);
            this.listMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listMain_MouseDown);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.tbEdit);
            this.panelEdit.Controls.Add(this.panelEditButtons);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEdit.Location = new System.Drawing.Point(0, 227);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panelEdit.Size = new System.Drawing.Size(436, 22);
            this.panelEdit.TabIndex = 0;
            // 
            // tbEdit
            // 
            this.tbEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEdit.Location = new System.Drawing.Point(0, 2);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(390, 20);
            this.tbEdit.TabIndex = 0;
            this.tbEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbEdit_KeyUp);
            this.tbEdit.TextChanged += new System.EventHandler(this.tbEdit_TextChanged);
            this.tbEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEdit_KeyDown);
            // 
            // panelEditButtons
            // 
            this.panelEditButtons.Controls.Add(this.lblLength);
            this.panelEditButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEditButtons.Location = new System.Drawing.Point(390, 2);
            this.panelEditButtons.Name = "panelEditButtons";
            this.panelEditButtons.Size = new System.Drawing.Size(46, 20);
            this.panelEditButtons.TabIndex = 0;
            // 
            // lblLength
            // 
            this.lblLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLength.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLength.Location = new System.Drawing.Point(6, 0);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 20);
            this.lblLength.TabIndex = 0;
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.lblLength, "Length of edited text");
            // 
            // panelMacro
            // 
            this.panelMacro.Controls.Add(this.grpMacro);
            this.panelMacro.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMacro.Location = new System.Drawing.Point(436, 0);
            this.panelMacro.Name = "panelMacro";
            this.panelMacro.Padding = new System.Windows.Forms.Padding(8, 0, 0, 2);
            this.panelMacro.Size = new System.Drawing.Size(160, 251);
            this.panelMacro.TabIndex = 0;
            // 
            // grpMacro
            // 
            this.grpMacro.Controls.Add(this.panelMacroInternal);
            this.grpMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMacro.Location = new System.Drawing.Point(8, 0);
            this.grpMacro.Name = "grpMacro";
            this.grpMacro.Size = new System.Drawing.Size(152, 249);
            this.grpMacro.TabIndex = 0;
            this.grpMacro.TabStop = false;
            this.grpMacro.Text = "Macro";
            // 
            // panelMacroInternal
            // 
            this.panelMacroInternal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMacroInternal.Location = new System.Drawing.Point(3, 16);
            this.panelMacroInternal.Name = "panelMacroInternal";
            this.panelMacroInternal.Padding = new System.Windows.Forms.Padding(4);
            this.panelMacroInternal.Size = new System.Drawing.Size(146, 230);
            this.panelMacroInternal.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miEdit,
            this.miView,
            this.miFileOperations,
            this.miListOperations,
            this.miHelp});
            // 
            // miFile
            // 
            this.miFile.Index = 0;
            this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFileRename,
            this.menuItem5,
            this.miFileRepeat,
            this.menuItem2,
            this.miFileExit});
            this.miFile.Text = "&File";
            // 
            // miFileRename
            // 
            this.miFileRename.Index = 0;
            this.miFileRename.Text = "&Rename";
            this.miFileRename.Click += new System.EventHandler(this.miFileRename_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "-";
            // 
            // miFileRepeat
            // 
            this.miFileRepeat.Index = 2;
            this.miFileRepeat.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftR;
            this.miFileRepeat.Text = "Repeat previous operation/macro";
            this.miFileRepeat.Click += new System.EventHandler(this.miFileRepeat_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "-";
            // 
            // miFileExit
            // 
            this.miFileExit.Index = 4;
            this.miFileExit.Text = "E&xit";
            this.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
            // 
            // miEdit
            // 
            this.miEdit.Index = 1;
            this.miEdit.Text = "&Edit";
            // 
            // miView
            // 
            this.miView.Index = 2;
            this.miView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miViewEdit,
            this.miViewMacro,
            this.miViewNavigation,
            this.miViewStatusbar,
            this.miViewCommandLine});
            this.miView.Text = "&View";
            // 
            // miViewEdit
            // 
            this.miViewEdit.Checked = true;
            this.miViewEdit.Index = 0;
            this.miViewEdit.Text = "&Edit";
            this.miViewEdit.Click += new System.EventHandler(this.miViewEdit_Click);
            // 
            // miViewMacro
            // 
            this.miViewMacro.Checked = true;
            this.miViewMacro.Index = 1;
            this.miViewMacro.Text = "&Macro";
            this.miViewMacro.Click += new System.EventHandler(this.miViewMacro_Click);
            // 
            // miViewNavigation
            // 
            this.miViewNavigation.Checked = true;
            this.miViewNavigation.Index = 2;
            this.miViewNavigation.Text = "&Navigation";
            this.miViewNavigation.Click += new System.EventHandler(this.miViewNavigation_Click);
            // 
            // miViewStatusbar
            // 
            this.miViewStatusbar.Index = 3;
            this.miViewStatusbar.Text = "&Statusbar";
            this.miViewStatusbar.Visible = false;
            this.miViewStatusbar.Click += new System.EventHandler(this.miViewStatusbar_Click);
            // 
            // miViewCommandLine
            // 
            this.miViewCommandLine.Index = 4;
            this.miViewCommandLine.Text = "Command line";
            this.miViewCommandLine.Click += new System.EventHandler(this.miViewCommandLine_Click);
            // 
            // miFileOperations
            // 
            this.miFileOperations.Index = 3;
            this.miFileOperations.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miOperationsWorkOnAllItems,
            this.miOperationsWorkOnSelectedItems,
            this.menuItem1,
            this.miOperationsSpecial,
            this.menuItem4});
            this.miFileOperations.Text = "&Operations";
            // 
            // miOperationsWorkOnAllItems
            // 
            this.miOperationsWorkOnAllItems.Checked = true;
            this.miOperationsWorkOnAllItems.Index = 0;
            this.miOperationsWorkOnAllItems.Text = "Work on all items";
            this.miOperationsWorkOnAllItems.Click += new System.EventHandler(this.miOperationsWorkOnAllItems_Click);
            // 
            // miOperationsWorkOnSelectedItems
            // 
            this.miOperationsWorkOnSelectedItems.Index = 1;
            this.miOperationsWorkOnSelectedItems.Text = "Work on selected items";
            this.miOperationsWorkOnSelectedItems.Click += new System.EventHandler(this.miOperationsWorkOnSelectedItems_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // miOperationsSpecial
            // 
            this.miOperationsSpecial.Index = 3;
            this.miOperationsSpecial.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miOperationsFreeEdit});
            this.miOperationsSpecial.Text = "Special";
            // 
            // miOperationsFreeEdit
            // 
            this.miOperationsFreeEdit.Index = 0;
            this.miOperationsFreeEdit.Text = "Free edit";
            this.miOperationsFreeEdit.Click += new System.EventHandler(this.miOperationsFreeEdit_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "-";
            // 
            // miListOperations
            // 
            this.miListOperations.Index = 4;
            this.miListOperations.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miEditListColumns,
            this.miEditMacros,
            this.miEditSettings});
            this.miListOperations.Text = "&Tools";
            // 
            // miEditListColumns
            // 
            this.miEditListColumns.Index = 0;
            this.miEditListColumns.Text = "&List columns";
            this.miEditListColumns.Click += new System.EventHandler(this.miEditListColumns_Click);
            // 
            // miEditMacros
            // 
            this.miEditMacros.Index = 1;
            this.miEditMacros.Text = "&Macros";
            // 
            // miEditSettings
            // 
            this.miEditSettings.Index = 2;
            this.miEditSettings.Text = "&Options";
            this.miEditSettings.Click += new System.EventHandler(this.miEditSettings_Click);
            // 
            // miHelp
            // 
            this.miHelp.Index = 5;
            this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miHelpAbout});
            this.miHelp.Text = "&Help";
            // 
            // miHelpAbout
            // 
            this.miHelpAbout.Index = 0;
            this.miHelpAbout.Text = "&About";
            this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // FrmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(612, 432);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelNavigation);
            this.Controls.Add(this.panelCommandLine);
            this.Controls.Add(this.panelStatusbar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(620, 460);
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Renamer NG -= BETA =- by Albert Bertilsson, version ";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmMain_Closing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelNavigation.ResumeLayout(false);
            this.panelPath.ResumeLayout(false);
            this.panelPath.PerformLayout();
            this.panelPathButtons.ResumeLayout(false);
            this.panelCommandLine.ResumeLayout(false);
            this.panelCommandLineEdit.ResumeLayout(false);
            this.panelCommandLineEdit.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            this.panelEditButtons.ResumeLayout(false);
            this.panelMacro.ResumeLayout(false);
            this.grpMacro.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region View menu
		private void miViewStatusbar_Click(object sender, System.EventArgs e)
		{
			miViewStatusbar.Checked = !miViewStatusbar.Checked;
			panelStatusbar.Visible = miViewStatusbar.Checked;
		}

		private void miViewMacro_Click(object sender, System.EventArgs e)
		{
			miViewMacro.Checked = !miViewMacro.Checked;
			panelMacro.Visible = miViewMacro.Checked;
		}

		private void miViewNavigation_Click(object sender, System.EventArgs e)
		{
			miViewNavigation.Checked = !miViewNavigation.Checked;
			panelNavigation.Visible = miViewNavigation.Checked;
		}

		private void miViewCommandLine_Click(object sender, System.EventArgs e)
		{
			miViewCommandLine.Checked = !miViewCommandLine.Checked;
			panelCommandLine.Visible = miViewCommandLine.Checked;
		}

		private void miViewEdit_Click(object sender, System.EventArgs e)
		{
			miViewEdit.Checked = !miViewEdit.Checked;
			panelEdit.Visible = miViewEdit.Checked;
		}
		#endregion

		private int AffectedFileNames(Operation op)
		{
			if (miOperationsWorkOnAllItems.Checked || op.AffectAllItems)
				return listMain.Items.Count;
			else
				return listMain.SelectedItems.Count;
		}

		private bool AffectedFileName(ListViewItem lvi, Operation op)
		{
			if (miOperationsWorkOnAllItems.Checked || op.AffectAllItems)
				return true;

			return lvi.Selected;
		}

		private void miOperation_Click(object sender, System.EventArgs e)
		{
			MenuItem m = (MenuItem)sender;

			Operation op;
			op = (Operation)fileOperations[m.Text];
			if (op == null)
				op = (Operation)listOperations[m.Text];

			if (!OperationRequirementsValid(op))
				return;

			if (op.Gui != null)
			{
				FrmDynamic d = new FrmDynamic(op, listMain.FocusedItem);
				DialogResult res = d.ShowDialog();

				if (res != DialogResult.OK) return;
			}

			previousAction = op;
			PerformOperation(op);
		}


		private bool OperationRequirementsValid(Operation op)
		{
			if (op.RequiresWorkModeSelected && miOperationsWorkOnAllItems.Checked)
			{
				ErrorMessage("This operation requires that \"" + miOperationsWorkOnSelectedItems.Text + "\" is selected.");
				return false;
			}

			if (AffectedFileNames(op) <= 0)
			{
				ErrorMessage("No items to perform operation on.");
				return false;
			}

			return true;
		}

		private void Perform(object obj)
		{
			if (obj is Operation)
				PerformOperation((Operation)obj);
			else if (obj is Macro)
				PerformMacro((Macro)obj);
			else
				throw new ArgumentException("Invalide type, can only perform Operations or Macros.");
		}

		private void PerformOperation(Operation op)
		{
			DisplayCommandLineOperation(op);

			if (!OperationRequirementsValid(op))
				return;

			if (macroList1.Recording)
			{
				if (op.Recordable)
				{
					Operation op2 = (Operation)SerializationCopy.Copy(op);

					macroList1.RecordOperation(op2);
				}
				else
				{
					WarningMessage("This operation is not recordable,\nit has not been recorded.");
				}
			}

			int failed = 0;
			op.Init();
            //listMain.SelectedIndexChanged -= new System.EventHandler(listMain_SelectedIndexChanged);
            foreach (ListViewItem lvi in listMain.Items)
			{
				if (AffectedFileName(lvi, op))
				{
					FileName f = (FileName)lvi.Tag;
					f.Updated = false;
					op.Perform(lvi);
					if (!f.Success) failed++;
      			}
			}
            //listMain.SelectedIndexChanged += new System.EventHandler(listMain_SelectedIndexChanged);

			PostOperationWork(failed);
		}

		private void PostOperationWork(int failed)
		{
			foreach (ListViewItem lvi in listMain.Items) UpdateItem(lvi);

			if (listMain.FocusedItem != null)
				tbEdit.Text = ((FileName)listMain.FocusedItem.Tag).NewName;
			else
				tbEdit.Text = "";

			if (failed > 0)
				ErrorMessage(failed.ToString() + " changes failed.");
		}

		private void btPickDir_Click(object sender, System.EventArgs e)
		{
			DialogResult res = folderBrowserDialog.ShowDialog();
			if (res == DialogResult.OK)
			{
				string s = folderBrowserDialog.SelectedPath;
				tbPath.Text = Path.GetFullPath(s);
				Scan();
			}
		}

		private bool Changed()
		{
			foreach (ListViewItem lvi in listMain.Items)
			{
				if (((FileName)lvi.Tag).Changed) return true;
			}

			return false;
		}

		private bool DontLoseChanges()
		{
			if (Changed())
			{
				DialogResult res = YesNoQuestion("Lose changes?");
				if (res == DialogResult.No) return true;
			}

			return false;
		}

		private void Scan()
		{
			if (!Directory.Exists(tbPath.Text))
			{
				ErrorMessage("Directory doesn't exist.");
				return;
			}

			if (DontLoseChanges()) return;

			listMain.Items.Clear();
			listMain.SuspendLayout();
			listMain.BeginUpdate();

			string pattern = comboPattern.Text;
			AddItemDelegate d = new AddItemDelegate(AddItem);
			FileScanner scanner = new FileScanner(tbPath.Text, chFiles.Checked, chDirs.Checked, chRecurse.Checked, pattern, settings.EditExtensions, d);
			bool suc = scanner.Scan();
			listMain.EndUpdate();

			if (pattern != "")
			{
				comboPattern.Items.Remove(pattern);
				comboPattern.Items.Insert(0, pattern);

				comboPattern.Text = pattern;

				const int maxPatterns = 10;
				if (comboPattern.Items.Count > maxPatterns)
					comboPattern.Items.RemoveAt(maxPatterns);
			}

			if (suc)
			{
				lblItems.Text = listMain.Items.Count.ToString();
				listMain_SelectedIndexChanged(null, null);
			}
			else
			{
				listMain.Items.Clear();
			}

			listMain.ResumeLayout(true);

            //Execute OnScan macro if present...
            Macro m = macroList1.GetMacroFromName("OnScan");
            if (m != null) PerformMacro(m);
		}

        delegate void AddItemCallback(ListViewItem lvi);

        private void AddItem(ListViewItem lvi)
        {
            if (listMain.InvokeRequired)
            {
                AddItemCallback cb = new AddItemCallback(AddItem);
                Invoke(cb, new object[] { lvi });
            }
            else
            {
                listMain.Items.Add(lvi);
            }
        }

		private void AddItem(FileName f)
		{
			ListViewItem lvi = new ListViewItem(f[settings.ListColumns[0]]);

			for (int i = 1 ; i < settings.ListColumns.Length ; i++)
			{
				lvi.SubItems.Add(f[settings.ListColumns[i]]);
			}

			lvi.Tag = f;
			AddItem(lvi);
		}

		private void UpdateItem(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			for (int i = 0 ; i < settings.ListColumns.Length ; i++)
			{
				lvi.SubItems[i].Text = f[settings.ListColumns[i]];
			}
		}

		private void btUp_Click(object sender, System.EventArgs e)
		{
			DirectoryInfo d = Directory.GetParent(tbPath.Text);

			if (d == null)
			{
				ErrorMessage("Already at root directory.");
				return;
			}

			if (DontLoseChanges()) return;

			tbPath.Text = d.FullName;
			Scan();
		}

        private void listMain_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem lvi = e.Item;

            if (lvi.Selected)
                lvi.BackColor = settings.ListColourSelectedBackground;
            else
                lvi.BackColor = settings.ListColourBackground;
        }

        static int focusedItem = -1;
        private void listMain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (listMain.Items.Count == 0)
            {
                tbEdit.Text = "";
                return;
            }

            if (listMain.FocusedItem == null)
                listMain.Items[0].Focused = true;

            if (focusedItem >= 0 && focusedItem < listMain.Items.Count)
                listMain.Items[focusedItem].ForeColor = settings.ListColourText;

            ListViewItem lvi = listMain.FocusedItem;
            if (lvi.Focused)
            {
                lvi.ForeColor = settings.ListColourFocusedText;
                focusedItem = lvi.Index;
            }
            else
            {
                lvi.ForeColor = settings.ListColourText;
            }

            tbEdit.Text = ((FileName)lvi.Tag).NewName;
            tbEdit.Focus();
            tbEdit.SelectionStart = editPosition;
            tbEdit.SelectionLength = 0;
        }

		private void listMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			listMain_SelectedIndexChanged(sender, null);
		}

		private void listMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			listMain_SelectedIndexChanged(sender, null);
		}

		private void tbEdit_TextChanged(object sender, System.EventArgs e)
		{
			lblLength.Text = tbEdit.Text.Length.ToString();
		}

		private void tbEdit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			int i = -1;

			editPosition = tbEdit.SelectionStart;

			switch ((int)e.KeyCode)
			{
				case 38: //Up arrow
					e.Handled = true;
					i = listMain.FocusedItem.Index;

					if (i == 0) return;
					listMain.Items[i-1].Focused = true;
					listMain.Items[i-1].EnsureVisible();
					listMain_SelectedIndexChanged(listMain, null);
					break;
				case 40: //Down arrow
					e.Handled = true;
					i = listMain.FocusedItem.Index;

					if (i == listMain.Items.Count - 1) return;
					listMain.Items[i+1].Focused = true;
					listMain.Items[i+1].EnsureVisible();
					listMain_SelectedIndexChanged(listMain, null);
					break;
				case 13: //Enter
					e.Handled = true;
					i = listMain.FocusedItem.Index;

					((FileName)listMain.Items[i].Tag).NewName = tbEdit.Text;
					((FileName)listMain.Items[i].Tag).NewName = tbEdit.Text; //Perform twice to clear C-flag
					UpdateItem(listMain.Items[i]);

					if (i == listMain.Items.Count - 1) return;
					listMain.Items[i+1].Focused = true;
					listMain.Items[i+1].EnsureVisible();
					listMain_SelectedIndexChanged(listMain, null);
					break;
			}
		}

		private void tbEdit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			editPosition = tbEdit.SelectionStart;
		}

		private void tbPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				e.Handled = true;
				Scan();
			}
		}

		private void FrmMain_Load(object sender, System.EventArgs e)
		{
			if (settings.Successful)
			{
				WindowState = settings.MainWindowState;
				DesktopBounds = new Rectangle(settings.MainWindowLocation, settings.MainWindowSize);
			}

			if (tbPath.Text != "")
				Scan();
		}

		private void miEditListColumns_Click(object sender, System.EventArgs e)
		{
			FrmListColumns form = new FrmListColumns();

			DialogResult res = form.ShowDialog(settings.ListColumns);

			if (res != DialogResult.OK) return;

			settings.ListColumns = form.Selected;
			settings.ListColumnWidths = new int[form.Selected.Length];
			for (int i = 0 ; i < settings.ListColumnWidths.Length ; i++)
				settings.ListColumnWidths[i] = 100;
			
			UpdateListColumns();
		}

		private void FrmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (DontLoseChanges())
			{
				e.Cancel = true;
				return;
			}

			settings.MainWindowState = WindowState;
			settings.MainWindowLocation = DesktopLocation;
			settings.MainWindowSize = Size;

			int[] widths = new int[listMain.Columns.Count];
			int i = 0;
			foreach (ColumnHeader ch in listMain.Columns)
			{
				widths[i++] = ch.Width;
			}
			settings.ListColumnWidths = widths;

			settings.ViewEdit = miViewEdit.Checked;
			settings.ViewMacros = miViewMacro.Checked;
			settings.ViewNavigation = miViewNavigation.Checked;
			settings.ViewStatusbar = miViewStatusbar.Checked;
			settings.ViewCommandLine = miViewCommandLine.Checked;

			//Save scan patterns
			int pos = 0;
			string[] patterns = new string[comboPattern.Items.Count];
			foreach (string s in comboPattern.Items)
				patterns[pos++] = s;
			settings.ScanPatterns = patterns;

			settings.ScanFiles = chFiles.Checked;
			settings.ScanDirs = chDirs.Checked;
			settings.ScanRecurse = chRecurse.Checked;
			settings.ScanPath = tbPath.Text;

			settings.Save();
			macroList1.Save();
		}

		private void UpdateListColumns()
		{
			int count = listMain.Items.Count;

			//Save all existing file names
			FileName[] items = new FileName[count];
			for (int i = 0 ; i < count ; i++)
			{
				items[i] = (FileName)listMain.Items[i].Tag;
			}
			listMain.Items.Clear();

			//Change columns
			listMain.Columns.Clear();
			int pos = 0;
			foreach (ListColumns lc in settings.ListColumns)
			{
				listMain.Columns.Add(lc.ToString(), settings.ListColumnWidths[pos++], HorizontalAlignment.Left);
			}

			//Restore all file names
			for (int i = 0 ; i < count ; i++)
			{
				AddItem(items[i]);
			}

			listMain_SelectedIndexChanged(null, null);
		}

		private void chDirs_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!chDirs.Checked && !chFiles.Checked)
				chFiles.Checked = true;
		}

		private void miFileExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void miFileRename_Click(object sender, System.EventArgs e)
		{
			Rename();
		}

		public void Rename()
		{
			if (miOperationsWorkOnSelectedItems.Checked)
			{
				DialogResult res = YesNoQuestion("Renaming files works on all items, not just the selected ones.\nIf you want to exclude some files from renaming please use\nthe Edit->Filter operations to filter your list.\n\nRename ALL changed files?");
				if (res != DialogResult.Yes) return;
			}

			ArrayList list = new ArrayList(listMain.Items.Count);

			//Fill ArrayList with all items to rename.
			foreach (ListViewItem lvi in listMain.Items)
			{
				FileName f = (FileName)lvi.Tag;
				if (f.Changed) //We only need to process files that have been changed.
					list.Add(f);
			}

			//Reverse sort on pathname to avoid renaming problems
			list.Sort(new ReversePathComparer());

			//Rename all files
			foreach (FileName f in list)
				f.Rename();

			//Update all items.
			int failed = 0;
			foreach (ListViewItem lvi in listMain.Items)
			{
				if (!((FileName)lvi.Tag).Success) failed++;
				UpdateItem(lvi);
			}

			if (failed > 0)
				ErrorMessage(failed.ToString() + " operations failed.");

			//Scan dir
			Scan();
		}

		private void miHelpAbout_Click(object sender, System.EventArgs e)
		{
			FrmAbout a = new FrmAbout();
			a.ShowDialog();
		}

		private void btScan_Click(object sender, System.EventArgs e)
		{
			Scan();
		}

		private void miEditSettings_Click(object sender, System.EventArgs e)
		{
			settings.ShowDialog();
		}


		#region Macro list event handlers
		private void macroList_Execute(object sender, Macro macro)
		{
			if (listMain.Items.Count <= 0)
			{
				ErrorMessage("No items to perform operation on.");
				return;
			}

			previousAction = macro;
			PerformMacro(macro);

			foreach (ListViewItem lvi in listMain.Items)
			{
				UpdateItem(lvi);
			}
		}

		private void PerformMacro(Macro macro)
		{
			foreach (Operation op in macro)
			{
				PerformOperation(op);
			}
		}

		private void macroList1_RecordingStarted(object sender)
		{
			recordingIndicator1.Enabled = true;
		}

		private void macroList1_RecordingStopped(object sender)
		{
			recordingIndicator1.Enabled = false;
		}
		#endregion

        private void ClearSelected()
        {
            foreach (ListViewItem lvi in listMain.Items)
                lvi.Selected = false;
        }

		private void miOperationsWorkOnAllItems_Click(object sender, System.EventArgs e)
		{
            ClearSelected();
            listMain.ItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(listMain_ItemSelectionChanged);
            miOperationsWorkOnAllItems.Checked = true;
			miOperationsWorkOnSelectedItems.Checked = false;
		}

		private void miOperationsWorkOnSelectedItems_Click(object sender, System.EventArgs e)
		{
            ClearSelected();
            listMain.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(listMain_ItemSelectionChanged);
            miOperationsWorkOnAllItems.Checked = false;
			miOperationsWorkOnSelectedItems.Checked = true;
		}

		#region Dialogs
		public static void WarningMessage(String msg)
		{
			MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		public static void InformationMessage(String msg)
		{
			MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void ErrorMessage(String msg)
		{
			MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static DialogResult YesNoQuestion(String question)
		{
			return MessageBox.Show(question, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		#endregion

		#region SpecialOperations
		private void miOperationsFreeEdit_Click(object sender, System.EventArgs e)
		{
			FrmFreeEdit freeEdit = new FrmFreeEdit();
			DialogResult res = freeEdit.ShowDialog();
			if (res != DialogResult.OK) return;

			FreeEdit fe = new FreeEdit();

			if (freeEdit.Lines.Length < AffectedFileNames(fe))
			{
				ErrorMessage("Too few lines entered, please supply one line for each affected file name.");
				return;
			}

			fe.Lines = freeEdit.Lines;
			PerformOperation(fe);
		}
		#endregion

		private void miFileRepeat_Click(object sender, System.EventArgs e)
		{
			if (previousAction == null)
			{
				ErrorMessage("No previous operation or macro found.");
				return;
			}

			if (previousAction is Operation)
				PerformOperation((Operation)previousAction);
			else
				macroList_Execute(null, (Macro)previousAction);
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e)
		{
			FileName fn = ((FileName)listMain.FocusedItem.Tag);
			if(fn.IsDirectory)
			{
				tbPath.Text = fn.Path + (fn.Path.EndsWith("\\")?"":"\\") + fn.Name;
				Scan();
			}
		}

		#region CommandLine
		private void tbCommand_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				e.Handled = true;
				ExecuteCommand(tbCommand.Text);
			}
		}

		private void ExecuteCommand(string text)
		{
			object o = ParseCommandLine(text);
			if (o != null)
				Perform(o);
		}

		private void DisplayCommandLineOperation(Operation op)
		{
			int index = listboxCommandHistory.Items.Add(op.ToString());
			listboxCommandHistory.TopIndex = index;
			listboxCommandHistory.SelectedIndex = index;

			while (listboxCommandHistory.Items.Count > 100)
				listboxCommandHistory.Items.RemoveAt(0);
		}

		private object ParseCommandLine(string text)
		{
			//Check escaping sequences
			bool escape = false;
			for (int i = 0 ; i < text.Length ; i++)
			{
				if (escape)
				{
					string c = text[i].ToString();
					if (c.IndexOfAny(new char[]{'|', '\\', 'n', '\"'}) < 0)
					{
						ErrorMessage("Invalid escape sequence at character index " + i.ToString() + ".");
						return null;
					}
					escape = false;
					continue;
				}

				if (text[i] == '\\')
				{
					escape = true;
					continue;
				}
			}

			if (escape)
			{
				ErrorMessage("Non terminated escape sequence at end of command.");
				return null;
			}

			//Find dividers
			text = '|' + text + '|';
			ArrayList dividers = new ArrayList();
			escape = false;
			for (int i = 0 ; i < text.Length ; i++)
			{
				if (escape)
				{
					escape = false;
					continue;
				}

				if (text[i] == '\\')
				{
					escape = true;
					continue;
				}
				
				if (text[i] == '|')
					dividers.Add(i);
			}

			//Divide into parts
			string[] parts = new string[dividers.Count - 1];
			for (int i = 0 ; i < dividers.Count - 1 ; i++)
			{
				int start = (int)dividers[i] + 1;
				int end = (int) dividers[i + 1];
				parts[i] = text.Substring(start, end - start);
			}

			//Unescape escape sequences
			for (int i = 0 ; i < parts.Length ; i++)
			{
				parts[i] = parts[i].Replace("\\\\", "\\");
				parts[i] = parts[i].Replace("\\\"", "\"");
				parts[i] = parts[i].Replace("\\|", "|");
				parts[i] = parts[i].Replace("\\n", "\n");
			}

			//Get the name
			string name = parts[0];

			//Is it a macro?
			Macro m = macroList1.GetMacroFromName(name);
			if (m != null) return m;

			//Is it an operation?
			Operation o = null;
			foreach (Operation op in fileOperations.Values)
				if (op.Name.ToLower() == name.ToLower())
					o = op;

			foreach (Operation op in listOperations.Values)
				if (op.Name.ToLower() == name.ToLower())
					o = op;

			//Not an op, invalid name
			if (o == null)
			{
				ErrorMessage("Not a valid Operation or Macro name.");
				return null;
			}

			//Verify number of parameters
			if (parts.Length - 1 != o.ParameterCount)
			{
				int count = parts.Length - 1;
				ErrorMessage("Invalid number of parameters, found " + count.ToString() + " parameters, should find " + o.ParameterCount.ToString() + ".");
				return null;
			}
			
			//Set the parameters
			string[] parameters = new string[parts.Length - 1];
			Array.Copy(parts, 1, parameters, 0, parts.Length - 1);
			o.SetParameters(parameters);

			return o;
		}
		#endregion
	}
}
