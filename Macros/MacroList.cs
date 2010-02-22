using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

using RenamerNG.FileNameOperations;
using Dialogs;

namespace RenamerNG.Macros
{
    /// <summary>
    /// Summary description for MacroList.
    /// </summary>
    public class MacroList : System.Windows.Forms.UserControl
    {
        public delegate void ExecuteEventHandler(object sender, Macro macro);
        public event ExecuteEventHandler Execute;
        public delegate void RecordingEventHandler(object sender);
        public event RecordingEventHandler RecordingStarted;
        public event RecordingEventHandler RecordingStopped;

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem miRecord;
        private System.Windows.Forms.MenuItem miExecute;
        private System.Windows.Forms.MenuItem miAdd;
        private System.Windows.Forms.MenuItem miEndRecording;
        private System.Windows.Forms.MenuItem miEdit;
        private System.Windows.Forms.MenuItem miCopy;
        private System.Windows.Forms.MenuItem miDelete;
        private System.Windows.Forms.MenuItem miMoveUp;
        private System.Windows.Forms.MenuItem miMoveDown;
        private System.Windows.Forms.MenuItem miSep1;
        private System.Windows.Forms.MenuItem miSep2;
        private System.Windows.Forms.MenuItem miSep3;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miImport;
        private System.Windows.Forms.MenuItem miExport;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MacroList(FrmMain parent)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.parent = parent;
        }

        private Macro recorder = null;
        private bool changed = false;
        private FrmMain parent = null;

        public bool Recording
        {
            get
            {
                return recorder != null;
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.miExecute = new System.Windows.Forms.MenuItem();
            this.miSep1 = new System.Windows.Forms.MenuItem();
            this.miRecord = new System.Windows.Forms.MenuItem();
            this.miAdd = new System.Windows.Forms.MenuItem();
            this.miEndRecording = new System.Windows.Forms.MenuItem();
            this.miSep2 = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miCopy = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.miSep3 = new System.Windows.Forms.MenuItem();
            this.miMoveUp = new System.Windows.Forms.MenuItem();
            this.miMoveDown = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miImport = new System.Windows.Forms.MenuItem();
            this.miExport = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.ContextMenu = this.contextMenu;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(72, 95);
            this.listBox.TabIndex = 0;
            this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            this.listBox.DoubleClick += new System.EventHandler(this.MacroList_DoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                            this.miExecute,
                                            this.miSep1,
                                            this.miRecord,
                                            this.miAdd,
                                            this.miEndRecording,
                                            this.miSep2,
                                            this.miEdit,
                                            this.miCopy,
                                            this.miDelete,
                                            this.miSep3,
                                            this.miMoveUp,
                                            this.miMoveDown,
                                            this.menuItem1,
                                            this.miImport,
                                            this.miExport});
            // 
            // miExecute
            // 
            this.miExecute.Index = 0;
            this.miExecute.Text = "Execute";
            this.miExecute.Click += new System.EventHandler(this.miExecute_Click);
            // 
            // miSep1
            // 
            this.miSep1.Index = 1;
            this.miSep1.Text = "-";
            // 
            // miRecord
            // 
            this.miRecord.Index = 2;
            this.miRecord.Text = "Record macro";
            this.miRecord.Click += new System.EventHandler(this.miRecord_Click);
            // 
            // miAdd
            // 
            this.miAdd.Index = 3;
            this.miAdd.Text = "Add to macro";
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miEndRecording
            // 
            this.miEndRecording.Enabled = false;
            this.miEndRecording.Index = 4;
            this.miEndRecording.Text = "End recording";
            this.miEndRecording.Click += new System.EventHandler(this.miEndRecording_Click);
            // 
            // miSep2
            // 
            this.miSep2.Index = 5;
            this.miSep2.Text = "-";
            // 
            // miEdit
            // 
            this.miEdit.Index = 6;
            this.miEdit.Text = "Edit";
            this.miEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // miCopy
            // 
            this.miCopy.Index = 7;
            this.miCopy.Text = "Copy";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miDelete
            // 
            this.miDelete.Index = 8;
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miSep3
            // 
            this.miSep3.Index = 9;
            this.miSep3.Text = "-";
            // 
            // miMoveUp
            // 
            this.miMoveUp.Index = 10;
            this.miMoveUp.Text = "Move up\tCtrl+Up";
            this.miMoveUp.Click += new System.EventHandler(this.miMoveUp_Click);
            // 
            // miMoveDown
            // 
            this.miMoveDown.Index = 11;
            this.miMoveDown.Text = "Move down\tCtrl+Down";
            this.miMoveDown.Click += new System.EventHandler(this.miMoveDown_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 12;
            this.menuItem1.Text = "-";
            // 
            // miImport
            // 
            this.miImport.Index = 13;
            this.miImport.Text = "Import";
            this.miImport.Click += new System.EventHandler(this.miImport_Click);
            // 
            // miExport
            // 
            this.miExport.Index = 14;
            this.miExport.Text = "Export";
            this.miExport.Click += new System.EventHandler(this.miExport_Click);
            // 
            // MacroList
            // 
            this.Controls.Add(this.listBox);
            this.Name = "MacroList";
            this.Size = new System.Drawing.Size(72, 96);
            this.ResumeLayout(false);

        }
        #endregion

        private void UpdateMenuStatus()
        {
            miRecord.Enabled = !Recording;
            miAdd.Enabled = !Recording;
            miEndRecording.Enabled = Recording;
        }

        private void miExecute_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to execute.");
                return;
            }

            if (Execute != null) Execute(this, (Macro)listBox.SelectedItem);
        }

        public string[] Macros()
        {
            ArrayList al = new ArrayList();
            foreach (Macro m in listBox.Items)
            {
                al.Add(m.Name);
            }

            return (string[])al.ToArray(typeof(String));
        }

        private bool ValidMacroName(string name)
        {
            if (name == null) return false;

            if (name.IndexOfAny(new char[] { '|', '\"', '\n', '\t' }) >= 0) return false;

            if (!parent.ValidMacroName(name)) return false;

            foreach (Macro m in listBox.Items)
                if (m.Name.ToLower() == name.ToLower())
                    return false;

            return true;
        }

        public Macro GetMacroFromName(string name)
        {
            if (name == null) return null;

            foreach (Macro m in listBox.Items)
                if (m.Name.ToLower() == name.ToLower())
                    return m;

            return null;
        }

        private void miRecord_Click(object sender, System.EventArgs e)
        {
            string name = GetString.Show("Record new macro", "Please enter macro name", 1);

            if (name == null) return;

            if (!ValidMacroName(name))
            {
                FrmMain.ErrorMessage("Not a valid macro name.\n\nMacros names may not...\n* Contain character | or \"\n* Equal another macro name\n* Equal an operation name");
                return;
            }

            recorder = new Macro(name);
            if (RecordingStarted != null) RecordingStarted(this);

            UpdateMenuStatus();
        }

        private void miAdd_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to add operations to.");
                return;
            }

            recorder = (Macro)listBox.SelectedItem;
            if (RecordingStarted != null) RecordingStarted(this);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            UpdateMenuStatus();
        }

        private void miEndRecording_Click(object sender, System.EventArgs e)
        {
            EndRecording();
        }

        private void EndRecording()
        {
            listBox.Items.Add(recorder);
            recorder = null;
            if (RecordingStopped != null) RecordingStopped(this);

            UpdateMenuStatus();
        }

        public void AbortRecording()
        {
            if (!Recording) return;
            EndRecording();
        }

        public void RecordOperation(Operation op)
        {
            if (!Recording) throw new InvalidOperationException("RecordOperation may only be used when recording.");
            changed = true;
            recorder.AddOperation(op);
        }

        private void listBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

        private void miMoveUp_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select an item to move.");
                return;
            }

            if (listBox.SelectedIndex > 0)
            {
                int index = listBox.SelectedIndex;
                object o = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index - 1, o);
                listBox.SelectedIndex = index - 1;
                changed = true;
            }
        }

        private void miMoveDown_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select an item to move.");
                return;
            }

            if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex < listBox.Items.Count - 1)
            {
                int index = listBox.SelectedIndex;
                object o = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index + 1, o);
                listBox.SelectedIndex = index + 1;
                changed = true;
            }
        }

        private void MacroList_DoubleClick(object sender, System.EventArgs e)
        {
            miExecute_Click(sender, e);
        }

        private void miEdit_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to edit.");
                return;
            }

            FrmEditMacro dlg = new FrmEditMacro();
            DialogResult res = dlg.ShowDialog((Macro)listBox.SelectedItem);

            if (res == DialogResult.OK)
            {
                changed = true;
                int index = listBox.SelectedIndex;
                listBox.Items.RemoveAt(index);

                while (!ValidMacroName(dlg.Macro.Name))
                {
                    FrmMain.ErrorMessage("A macro with that name already exists.");
                    string name = GetString.Show("Macro name", "Please enter macro name", 1);
                    if (name == null) continue;
                    dlg.Macro.Name = name;
                }

                listBox.Items.Insert(index, dlg.Macro);
            }
        }

        private void miCopy_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to copy.");
                return;
            }

            Macro n = (Macro)SerializationCopy.Copy(listBox.SelectedItem);
            if (ValidMacroName("Copy of " + n.Name))
            {
                n.Name = "Copy of " + n.Name;
            }
            else
            {
                int i = 2;

                while (!ValidMacroName("Copy " + i.ToString() + " of " + n.Name))
                    i++;

                n.Name = "Copy " + i.ToString() + " of " + n.Name;
            }
            changed = true;
            listBox.Items.Add(n);
        }

        private void miDelete_Click(object sender, System.EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to delete.");
                return;
            }

            DialogResult res = FrmMain.YesNoQuestion("The macro will be deleted!\nContinue?");
            if (res != DialogResult.Yes) return;

            changed = true;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        #region Open/Save
        const string extension = "renamermacro.xml";
        const string file = "\\macros." + extension;

        public void Open()
        {
            try
            {
                StreamReader r = new StreamReader(Settings.PathFinder.OpenFile(file));

                using (r)
                {
                    SoapFormatter f = new SoapFormatter();

                    MacroFile mf = (MacroFile)f.Deserialize(r.BaseStream);
                    foreach (Macro m in mf)
                        listBox.Items.Add(m);
                    changed = false;
                }
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
            if (!changed) return;

            try
            {
                StreamWriter w = new StreamWriter(Settings.PathFinder.SaveFile(file));

                using (w)
                {
                    SoapFormatter f = new SoapFormatter();

                    MacroFile mf = new MacroFile();
                    foreach (Macro m in listBox.Items)
                        mf.Add(m);

                    f.Serialize(w.BaseStream, mf);
                    changed = false;
                }
            }
            catch (Exception ex)
            {
                FrmMain.ErrorMessage(ex.Message);
            }
        }
        #endregion

        #region Import/Export
        public void Import()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = true;
                ofd.Filter = "Renamer macro files|*." + extension;
                DialogResult res = ofd.ShowDialog();
                if (res != DialogResult.OK) return;

                StreamReader r = new StreamReader(ofd.FileName);

                using (r)
                {
                    SoapFormatter f = new SoapFormatter();

                    MacroFile mf = (MacroFile)f.Deserialize(r.BaseStream);

                    Macro m = mf[0];
                    if (!ValidMacroName(m.Name))
                    {
                        if (ValidMacroName("Imported " + m.Name))
                        {
                            m.Name = "Imported " + m.Name;
                        }
                        else
                        {
                            int i = 2;

                            while (!ValidMacroName("Imported " + i.ToString() + " " + m.Name))
                                i++;

                            m.Name = "Imported " + i.ToString() + " " + m.Name;
                        }
                    }

                    listBox.Items.Add(m);
                    changed = true;
                }
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

        public void Export()
        {
            //Active when exporting selected macro only
            if (listBox.SelectedIndex < 0)
            {
                FrmMain.ErrorMessage("Please select a macro to export.");
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = extension;
                DialogResult res = sfd.ShowDialog();
                if (res != DialogResult.OK) return;

                StreamWriter w = new StreamWriter(sfd.FileName);

                using (w)
                {
                    MacroFile mf = new MacroFile();
                    mf.Add((Macro)listBox.SelectedItem);

                    IFormatter f = new SoapFormatter();

                    f.Serialize(w.BaseStream, mf);
                }
            }
            catch (Exception ex)
            {
                FrmMain.ErrorMessage(ex.Message);
            }
        }

        private void miImport_Click(object sender, System.EventArgs e)
        {
            Import();
        }

        private void miExport_Click(object sender, System.EventArgs e)
        {
            Export();
        }
        #endregion
    }
}
