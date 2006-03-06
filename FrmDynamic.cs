using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using RenamerNG.FileNameOperations;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for FrmDynamic.
	/// </summary>
	public class FrmDynamic : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panelOkCancel;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Panel panelPreview;
		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.TextBox tbBefore;
		private System.Windows.Forms.TextBox tbAfter;
		private System.Windows.Forms.Button btHelp;
		private System.Windows.Forms.Label lblAfter;
		private System.Windows.Forms.Label lblBefore;
		private System.ComponentModel.IContainer components;

		public FrmDynamic(Operation operation, ListViewItem sample)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.operation = operation;
			this.sample = (ListViewItem)SerializationCopy.Copy(sample);
			this.sample.Tag = ((FileName)sample.Tag).Clone();
			Text = operation.Name;
			tbBefore.Text = ((FileName)this.sample.Tag).NewName;
			if (sample.Selected)
				tbBefore.SelectAll();
			else
				tbBefore.Select(0, 0);

			if (operation.Gui != null)
			{
				controls = new Control[operation.Gui.Length];

				CreateGUI();
			}
			else
			{
				HideContentPanel();
			}

			UpdatePreview();
		}


		Operation operation;
		Control[] controls = null;
		ListViewItem sample;


		private void HideContentPanel()
		{
			this.SuspendLayout();

			this.ClientSize = new Size(ClientSize.Width, DockPadding.Top + DockPadding.Bottom + panelOkCancel.Height + panelPreview.Height);

			this.ResumeLayout(false);
		}

		private void CreateGUI()
		{
			int width = panelContent.ClientSize.Width;
			int height = 0;

			this.SuspendLayout();

			for (int i = 0 ; i < operation.Gui.Length ; i++)
			{
				GUIElement g = operation.Gui[i];

				if (g.Type != GUIElement.Types.CHECKBOX && g.Type != GUIElement.Types.RADIOBUTTONS)
				{
					Label l = new Label();
					l.Text = g.Name;
					l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					l.Height = 20;
					l.Width = width;
					l.Top = height;
					l.Left = 0;
					panelContent.Controls.Add(l);

					if (g.Type == GUIElement.Types.LABEL)
						toolTip.SetToolTip(l, g.Info);
				}

				switch (g.Type)
				{
					case GUIElement.Types.LABEL:
						height += 40;
						break;
					case GUIElement.Types.TEXTBOX:
						TextBox t = new TextBox();
						controls[i] = t;

						t.Text = operation[i];
						t.TextChanged += new EventHandler(AnythingChanged);
						t.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

						t.Width = width;
						t.Top = height + 20;
						t.Left = 0;

						panelContent.Controls.Add(t);
						toolTip.SetToolTip(t, g.Info);
						height += 60;
						break;
					case GUIElement.Types.NUMERICUPDOWN:
						NumericUpDown n = new NumericUpDown();
						controls[i] = n;

						string[] minmax = g.Options.Split(',');
						n.Minimum = int.Parse(minmax[0]);
						n.Maximum = int.Parse(minmax[1]);
						n.Value = int.Parse(operation[i]);
						n.ValueChanged += new EventHandler(AnythingChanged);
						n.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

						n.Width = 100;
						n.Top = height + 20;
						n.Left = 0;

						panelContent.Controls.Add(n);
						foreach (Control c in n.Controls)
							toolTip.SetToolTip(c, g.Info);
						height += 60;
						break;
					case GUIElement.Types.CHECKBOX:
						CheckBox ch = new CheckBox();
						controls[i] = ch;

						ch.Checked = bool.Parse(operation[i]);
						ch.Text = g.Name;
						ch.CheckedChanged += new EventHandler(AnythingChanged);
						ch.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

						ch.Width = width;
						ch.Top = height;
						ch.Left = 0;

						panelContent.Controls.Add(ch);
						toolTip.SetToolTip(ch, g.Info);
						height += 40;
						break;
					case GUIElement.Types.POSITION:
						TextBox p = new TextBox();
						controls[i] = p;

						p.Text = tbBefore.Text;
						p.ReadOnly = true;
						p.MouseUp += new MouseEventHandler(position_MouseUp);
						p.MouseDown += new MouseEventHandler(position_MouseUp);
						p.MouseMove += new MouseEventHandler(position_MouseUp);
						p.KeyUp += new KeyEventHandler(position_KeyDown);
						p.KeyDown += new KeyEventHandler(position_KeyDown);
						p.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

						p.Width = width - 60;
						p.Top = height + 20;
						p.Left = 0;

						panelContent.Controls.Add(p);
						toolTip.SetToolTip(p, g.Info);


						Label pl = new Label();
						p.Tag = pl;

						pl.Text = operation[i];
						pl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
						pl.Height = 20;
						pl.TextChanged +=new EventHandler(AnythingChanged);

						pl.Width = 55;
						pl.Top = height + 20;
						pl.Left = width - 55;

						panelContent.Controls.Add(pl);
						toolTip.SetToolTip(pl, "Currently selected position");


						CheckBox pch = new CheckBox();
						pl.Tag = pch;

						pch.Checked = bool.Parse(operation[i].Split(',')[1]);
						pch.Text = "Use end of string as reference point";
						pch.CheckedChanged += new EventHandler(position_CheckedChanged);

						pch.Width = width;
						pch.Top = height + 40;
						pch.Left = 0;

						panelContent.Controls.Add(pch);


						if (pch.Checked)
							p.SelectionStart = p.Text.Length - int.Parse(pl.Text.Split(',')[0]);
						else
							p.SelectionStart = int.Parse(pl.Text.Split(',')[0]);


						height += 80;
						break;
					case GUIElement.Types.SELECTION:
						TextBox s = new TextBox();
						controls[i] = s;

						s.Text = tbBefore.Text;
						s.ReadOnly = true;
						s.HideSelection = false;
						s.MouseUp += new MouseEventHandler(selection_MouseUp);
						s.MouseDown += new MouseEventHandler(selection_MouseUp);
						s.MouseMove += new MouseEventHandler(selection_MouseUp);
						s.KeyUp += new KeyEventHandler(selection_KeyDown);
						s.KeyDown += new KeyEventHandler(selection_KeyDown);
						s.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

						s.Width = width - 110;
						s.Top = height + 20;
						s.Left = 0;

						panelContent.Controls.Add(s);
						toolTip.SetToolTip(s, g.Info);


						Label sl = new Label();
						s.Tag = sl;

						//sl.Text = operation[i].Split(',')[0] + "," + operation[i].Split(',')[1];
						sl.Text = operation[i];
						sl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
						sl.Height = 20;
						sl.TextChanged +=new EventHandler(AnythingChanged);

						sl.Width = 105;
						sl.Top = height + 20;
						sl.Left = width - 105;

						panelContent.Controls.Add(sl);
						toolTip.SetToolTip(sl, "Currently selected position");


						CheckBox sch = new CheckBox();
						sl.Tag = sch;

						sch.Tag = new int[]{0,0};
						sch.Checked = bool.Parse(operation[i].Split(',')[2]);
						sch.Text = "Use end of string as reference point";
						sch.CheckedChanged += new EventHandler(selection_CheckedChanged);

						sch.Width = width;
						sch.Top = height + 40;
						sch.Left = 0;
						

						panelContent.Controls.Add(sch);

						


						int length = int.Parse(sl.Text.Split(',')[1]);
						if (sch.Checked)
						{
							int start = s.Text.Length - int.Parse(sl.Text.Split(',')[0]) - length;
							if (start < 0) start = 0;
							s.SelectionStart = start;
						}
						else
						{
							s.SelectionStart = int.Parse(sl.Text.Split(',')[0]);
						}
						s.SelectionLength = length;


						height += 80;
						break;
					case GUIElement.Types.RADIOBUTTONS:
						GroupBox gb = new GroupBox();

						int selected = int.Parse(operation[i]);
						string[] buttons = g.Options.Split(',');
						Control prev = null;
						for (int j = 0 ; j < buttons.Length ; j++)
						{
							RadioButton r = new RadioButton();
							r.Width = width - 20;
							r.Top = 20 + 22 * j;
							r.Left = 10;
							r.Text = buttons[j];
							r.CheckedChanged += new EventHandler(AnythingChanged);
							r.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

							if (prev == null)
								controls[i] = r;
							else
								prev.Tag = r;

							if (selected == j) r.Checked = true;

							gb.Controls.Add(r);
							toolTip.SetToolTip(r, g.Info);
							prev = r;
						}

						gb.Text = g.Name;
						gb.Height = buttons.Length * 22 + 25;
						gb.Width = width;
						gb.Top = height;
						gb.Left = 0;
						toolTip.SetToolTip(gb, g.Info);
						panelContent.Controls.Add(gb);
						height += gb.Height + 20;
						break;
				}
			}


			this.ClientSize = new Size(ClientSize.Width, height + DockPadding.Top + DockPadding.Bottom + panelOkCancel.Height + panelPreview.Height);

			this.ResumeLayout(false);
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
			this.panelOkCancel = new System.Windows.Forms.Panel();
			this.btHelp = new System.Windows.Forms.Button();
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panelPreview = new System.Windows.Forms.Panel();
			this.lblAfter = new System.Windows.Forms.Label();
			this.lblBefore = new System.Windows.Forms.Label();
			this.tbAfter = new System.Windows.Forms.TextBox();
			this.tbBefore = new System.Windows.Forms.TextBox();
			this.panelContent = new System.Windows.Forms.Panel();
			this.panelOkCancel.SuspendLayout();
			this.panelPreview.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelOkCancel
			// 
			this.panelOkCancel.Controls.Add(this.btHelp);
			this.panelOkCancel.Controls.Add(this.btOk);
			this.panelOkCancel.Controls.Add(this.btCancel);
			this.panelOkCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelOkCancel.Location = new System.Drawing.Point(8, 255);
			this.panelOkCancel.Name = "panelOkCancel";
			this.panelOkCancel.Size = new System.Drawing.Size(345, 28);
			this.panelOkCancel.TabIndex = 1;
			// 
			// btHelp
			// 
			this.btHelp.Location = new System.Drawing.Point(0, 0);
			this.btHelp.Name = "btHelp";
			this.btHelp.TabIndex = 2;
			this.btHelp.Text = "Help";
			this.btHelp.Click += new System.EventHandler(this.btHelp_Click);
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(168, 0);
			this.btOk.Name = "btOk";
			this.btOk.TabIndex = 0;
			this.btOk.Text = "Ok";
			this.btOk.Click += new System.EventHandler(this.btOk_Click);
			// 
			// btCancel
			// 
			this.btCancel.Location = new System.Drawing.Point(264, 0);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 1;
			this.btCancel.Text = "Cancel";
			this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
			// 
			// panelPreview
			// 
			this.panelPreview.Controls.Add(this.lblAfter);
			this.panelPreview.Controls.Add(this.lblBefore);
			this.panelPreview.Controls.Add(this.tbAfter);
			this.panelPreview.Controls.Add(this.tbBefore);
			this.panelPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelPreview.Location = new System.Drawing.Point(8, 175);
			this.panelPreview.Name = "panelPreview";
			this.panelPreview.Size = new System.Drawing.Size(345, 80);
			this.panelPreview.TabIndex = 1;
			// 
			// lblAfter
			// 
			this.lblAfter.Location = new System.Drawing.Point(7, 42);
			this.lblAfter.Name = "lblAfter";
			this.lblAfter.Size = new System.Drawing.Size(60, 20);
			this.lblAfter.TabIndex = 3;
			this.lblAfter.Text = "After";
			this.lblAfter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBefore
			// 
			this.lblBefore.Location = new System.Drawing.Point(7, 14);
			this.lblBefore.Name = "lblBefore";
			this.lblBefore.Size = new System.Drawing.Size(60, 20);
			this.lblBefore.TabIndex = 2;
			this.lblBefore.Text = "Before";
			this.lblBefore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbAfter
			// 
			this.tbAfter.Location = new System.Drawing.Point(73, 42);
			this.tbAfter.Name = "tbAfter";
			this.tbAfter.ReadOnly = true;
			this.tbAfter.Size = new System.Drawing.Size(260, 20);
			this.tbAfter.TabIndex = 1;
			this.tbAfter.TabStop = false;
			this.tbAfter.Text = "";
			// 
			// tbBefore
			// 
			this.tbBefore.Location = new System.Drawing.Point(73, 14);
			this.tbBefore.Name = "tbBefore";
			this.tbBefore.ReadOnly = true;
			this.tbBefore.Size = new System.Drawing.Size(260, 20);
			this.tbBefore.TabIndex = 0;
			this.tbBefore.TabStop = false;
			this.tbBefore.Text = "";
			// 
			// panelContent
			// 
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(8, 8);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(345, 167);
			this.panelContent.TabIndex = 0;
			// 
			// FrmDynamic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(361, 291);
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.panelPreview);
			this.Controls.Add(this.panelOkCancel);
			this.DockPadding.All = 8;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmDynamic";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmDynamic";
			this.panelOkCancel.ResumeLayout(false);
			this.panelPreview.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void position_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Position((TextBox)sender);
		}

		private void position_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Position((TextBox)sender);
		}

		private void position_CheckedChanged(object sender, System.EventArgs e)
		{
			Control c1 = FindParentTo((Control)sender);
			Control c2 = FindParentTo(c1);
			Position((TextBox)c2);
		}

		private void selection_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Selection((TextBox)sender);
		}

		private void selection_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Selection((TextBox)sender);
		}

		private void selection_CheckedChanged(object sender, System.EventArgs e)
		{
			Control c1 = FindParentTo((Control)sender);
			Control c2 = FindParentTo(c1);
			Selection((TextBox)c2);
		}

		void Position(TextBox tb)
		{
			tb.SelectionLength = 0;

			Label l = (Label)tb.Tag;
			CheckBox ch = (CheckBox)l.Tag;

			int position;
			if (ch.Checked)
				position = tb.Text.Length - tb.SelectionStart;
			else
				position = tb.SelectionStart;

			l.Text = position.ToString() + "," + ch.Checked.ToString();
		}

		void Selection(TextBox tb)
		{
			Label l = (Label)tb.Tag;
			CheckBox ch = (CheckBox)l.Tag;

			int[] selPrevs = (int[])ch.Tag;
			int selPrevSta = 0;
			int selPrevLen = 1;

			int position, length;
			length = tb.SelectionLength;
			if (length > selPrevs[selPrevLen] && tb.SelectionStart == selPrevs[selPrevSta]) //we're going rightwards
				ch.Checked = false;
			else if (length > selPrevs[selPrevLen] && tb.SelectionStart < selPrevs[selPrevSta])
				ch.Checked = true;

			if (ch.Checked)
				position = tb.Text.Length - tb.SelectionStart - length;
			else
				position = tb.SelectionStart;  

			selPrevs[selPrevSta] = tb.SelectionStart;
			selPrevs[selPrevLen] = tb.SelectionLength;
			ch.Tag = selPrevs;

			l.Text = position.ToString() + "," + length.ToString() + "," + ch.Checked.ToString().ToLower();
		}

		Control FindParentTo(Control control)
		{
			if (control == null)
				throw new ArgumentNullException();

			foreach (Control c in panelContent.Controls)
			{
				if (c.Tag == control) return c;
			}

			throw new ArgumentException("FindParentTo: Parent not found");
		}

		private void btHelp_Click(object sender, System.EventArgs e)
		{
			FrmMain.InformationMessage(operation.Help);
		}

		private string[] GetData()
		{
			if (operation.Gui == null) return null;

			string[] data = new string[operation.Gui.Length];

			for (int i = 0 ; i < data.Length ; i++)
			{
				GUIElement g = operation.Gui[i];

				switch (g.Type)
				{
					case GUIElement.Types.LABEL:
						data[i] = null;
						break;
					case GUIElement.Types.TEXTBOX:
						data[i] = controls[i].Text;
						if (g.Options != "ALLOWEMPTY" && data[i] == "")
							throw new ArgumentException(g.Name + " must not be empty.");
						break;
					case GUIElement.Types.NUMERICUPDOWN:
						data[i] = ((NumericUpDown)controls[i]).Value.ToString();
						break;
					case GUIElement.Types.CHECKBOX:
						data[i] = ((CheckBox)controls[i]).Checked.ToString();
						break;
					case GUIElement.Types.POSITION:
						data[i] = ((Label)controls[i].Tag).Text;
						break;
					case GUIElement.Types.SELECTION:
						data[i] = ((Label)controls[i].Tag).Text;

						if (data[i].Split(',')[1] == "0")
							throw new ArgumentException(g.Name + " length must not be 0.");
						break;
					case GUIElement.Types.RADIOBUTTONS:
						RadioButton r = (RadioButton)controls[i].Tag;
						int p;
						for (p = 0 ; r.Tag != null ; p++)
						{
							if (r.Checked) break;
		
							r = (RadioButton)r.Tag;
						}
						data[i] = p.ToString();
						break;
				}
			}

			return data;
		}

		private void btOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				string[] data = GetData();

				operation.SetParameters(data);

				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				FrmMain.ErrorMessage(ex.Message);
			}
		}

		private void btCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void AnythingChanged(object sender, System.EventArgs e)
		{
			UpdatePreview();
		}

		private void UpdatePreview()
		{
			try
			{
				string[] data = GetData();

				operation.SetParameters(data);
				ListViewItem work = (ListViewItem)SerializationCopy.Copy(sample);
				work.Tag = ((FileName)sample.Tag).Clone();
				operation.Init();
				operation.Perform(work);
				tbAfter.Text = ((FileName)work.Tag).NewName;
				if (work.Selected)
					tbAfter.SelectAll();
				else
					tbAfter.Select(0, 0);
			}
			catch (ArgumentException argEx)
			{
				tbAfter.Text = argEx.Message;
			}
			catch (Exception ex)
			{
				tbAfter.Text = ex.Message;
			}
		}

		private void textBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((int)e.KeyChar == 13)
				btOk_Click(null, null);

            if ((int)e.KeyChar == 27)
                btCancel_Click(null, null);
        }
	}
}
