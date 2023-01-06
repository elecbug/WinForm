using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Rocio.Forms
{
    public partial class MainForm : Form
    {
        private MenuStrip menu;
        private List<Classes.TextSet> text_sets;
        private TabControl tab_control;
        private RichTextBox now_font_box;
        private List<RichTextBox> fN_boxes;

        private ToolStripMenuItem file_strip;
        private ToolStripMenuItem edit_strip;
        private ToolStripMenuItem view_strip;
        private ToolStripMenuItem text_strip;

        public MainForm(string path)
        {
            //FormSetting.Default.Reset();

            this.ResizeEnd += MainFormResize;
            this.FormClosing += MainFormClosing;
            this.KeyDown += MainFormKeyDown;

            InitializeComponent();

            this.Text = Resources.TextENG.Title;
            this.Visible = true;
            this.Font = Resources.FormSetting.Default.Font;
            this.ForeColor = Resources.FormSetting.Default.ForeColor;
            this.BackColor = Resources.FormSetting.Default.FormColor;
            this.Location = Resources.FormSetting.Default.StartLocation;
            this.Size = Resources.FormSetting.Default.StartSize;
            if (Resources.FormSetting.Default.MaximumStart)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            this.menu = new MenuStrip()
            {
                Parent = this,
                Visible = true,
                Font = Resources.FormSetting.Default.Font,
                ForeColor = Resources.FormSetting.Default.ForeColor,
                BackColor = Resources.FormSetting.Default.ControlColor,
            };
            this.file_strip = this.menu.Items.Add(Resources.TextENG.Menu.Split('\\')[0]) as ToolStripMenuItem;
            this.edit_strip = this.menu.Items.Add(Resources.TextENG.Menu.Split('\\')[1]) as ToolStripMenuItem;
            this.view_strip = this.menu.Items.Add(Resources.TextENG.Menu.Split('\\')[2]) as ToolStripMenuItem;
            this.text_strip = this.menu.Items.Add(Resources.TextENG.Menu.Split('\\')[3]) as ToolStripMenuItem;

            this.file_strip.DropDownItems.Add(Resources.TextENG.File.Split('\\')[0]).Click += MenuClick;
            this.file_strip.DropDownItems.Add(Resources.TextENG.File.Split('\\')[1]).Click += MenuClick;
            this.file_strip.DropDownItems.Add(Resources.TextENG.File.Split('\\')[2]).Click += MenuClick;
            this.file_strip.DropDownItems.Add(Resources.TextENG.File.Split('\\')[3]).Click += MenuClick;
            this.file_strip.DropDownItems.Add(Resources.TextENG.File.Split('\\')[4]).Click += MenuClick;

            this.edit_strip.DropDownItems.Add(Resources.TextENG.Edit.Split('\\')[0]).Click += MenuClick;
            this.edit_strip.DropDownItems.Add(Resources.TextENG.Edit.Split('\\')[1]).Click += MenuClick;
            this.edit_strip.DropDownItems.Add(Resources.TextENG.Edit.Split('\\')[2]).Click += MenuClick;
            this.edit_strip.DropDownItems.Add(Resources.TextENG.Edit.Split('\\')[3]).Click += MenuClick;

            this.view_strip.DropDownItems.Add(Resources.TextENG.View.Split('\\')[0]).Click += MenuClick;
            this.view_strip.DropDownItems.Add(Resources.TextENG.View.Split('\\')[1]).Click += MenuClick;

            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[0]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[1]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[2]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[3]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[4]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[5]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[6]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[7]).Click += MenuClick;
            this.text_strip.DropDownItems.Add(Resources.TextENG.Text.Split('\\')[8]).Click += MenuClick;

            this.tab_control = new TabControl()
            {
                Parent = this,
                Visible = true,
                Font = Resources.FormSetting.Default.Font,
                ForeColor = Resources.FormSetting.Default.ForeColor,
                BackColor = Resources.FormSetting.Default.ControlColor,
            };

            this.text_sets = new List<Classes.TextSet>();

            this.now_font_box = new RichTextBox()
            {
                Parent = this,
                Visible = true,
                Text = "가나다ABC",
                BackColor = Resources.FormSetting.Default.ControlColor,
                ReadOnly = true,
                SelectionAlignment = HorizontalAlignment.Center,
            };
            this.now_font_box.KeyDown += MainFormKeyDown;

            this.fN_boxes = new List<RichTextBox>();

            for (int i = 0; i < 12; i++)
            {
                this.fN_boxes.Add(new RichTextBox()
                {
                    Parent = this,
                    Visible = true,
                    Text = "F" + (i + 1),
                    BackColor = Resources.FormSetting.Default.ControlColor,
                    ReadOnly = true,
                    SelectionAlignment = HorizontalAlignment.Center,
                });
            }

            this.fN_boxes[0].Font = Resources.FontList.Default.F1;
            this.fN_boxes[0].ForeColor = Resources.FontList.Default.F1Color;
            this.fN_boxes[1].Font = Resources.FontList.Default.F2;
            this.fN_boxes[1].ForeColor = Resources.FontList.Default.F2Color;
            this.fN_boxes[2].Font = Resources.FontList.Default.F3;
            this.fN_boxes[2].ForeColor = Resources.FontList.Default.F3Color;
            this.fN_boxes[3].Font = Resources.FontList.Default.F4;
            this.fN_boxes[3].ForeColor = Resources.FontList.Default.F4Color;
            this.fN_boxes[4].Font = Resources.FontList.Default.F5;
            this.fN_boxes[4].ForeColor = Resources.FontList.Default.F5Color;
            this.fN_boxes[5].Font = Resources.FontList.Default.F6;
            this.fN_boxes[5].ForeColor = Resources.FontList.Default.F6Color;
            this.fN_boxes[6].Font = Resources.FontList.Default.F7;
            this.fN_boxes[6].ForeColor = Resources.FontList.Default.F7Color;
            this.fN_boxes[7].Font = Resources.FontList.Default.F8;
            this.fN_boxes[7].ForeColor = Resources.FontList.Default.F8Color;
            this.fN_boxes[8].Font = Resources.FontList.Default.F9;
            this.fN_boxes[8].ForeColor = Resources.FontList.Default.F9Color;
            this.fN_boxes[9].Font = Resources.FontList.Default.F10;
            this.fN_boxes[9].ForeColor = Resources.FontList.Default.F10Color;
            this.fN_boxes[10].Font = Resources.FontList.Default.F11;
            this.fN_boxes[10].ForeColor = Resources.FontList.Default.F11Color;
            this.fN_boxes[11].Font = Resources.FontList.Default.F12;
            this.fN_boxes[11].ForeColor = Resources.FontList.Default.F12Color;

            if (path == null)
            {
                MenuClick(this.file_strip.DropDownItems[0], new EventArgs());
            }
            else
            {
                this.tab_control.TabPages.Add(path.Split('\\')[path.Split('\\').Length - 1]);
                this.text_sets.Add(new Classes.TextSet()
                {
                    Path = path,
                    TextBox = new RichTextBox()
                    {
                        Parent = this.tab_control.TabPages[this.tab_control.TabCount - 1],
                        Visible = true,
                        Font = Resources.FormSetting.Default.Font,
                        ForeColor = Resources.FormSetting.Default.ForeColor,
                        BackColor = Resources.FormSetting.Default.ControlColor,
                        SelectionFont = Resources.FormSetting.Default.Font,
                        SelectionColor = Resources.FormSetting.Default.ForeColor,
                        LanguageOption = RichTextBoxLanguageOptions.UIFonts,
                    }
                });
                this.text_sets[this.text_sets.Count - 1].TextBox.KeyDown += MainFormKeyDown;
                this.text_sets[this.text_sets.Count - 1].TextBox.LoadFile(path);

                this.tab_control.SelectedIndex = this.tab_control.TabCount - 1;
                this.text_sets[this.tab_control.SelectedIndex].TextBox.Focus();
            }

            MainFormResize(this, new EventArgs());
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Resources.TextENG.ClosingMessage, Resources.TextENG.WarningMessage, MessageBoxButtons.OKCancel)
                == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                Resources.FormSetting.Default.Font = this.Font;
                Resources.FormSetting.Default.ForeColor = this.ForeColor;
                Resources.FormSetting.Default.FormColor = this.BackColor;
                Resources.FormSetting.Default.StartLocation = this.Location;
                Resources.FormSetting.Default.StartSize = this.Size;
                Resources.FormSetting.Default.Save();

                Resources.FontList.Default.F1 = this.fN_boxes[0].Font;
                Resources.FontList.Default.F2 = this.fN_boxes[1].Font;
                Resources.FontList.Default.F3 = this.fN_boxes[2].Font;
                Resources.FontList.Default.F4 = this.fN_boxes[3].Font;
                Resources.FontList.Default.F5 = this.fN_boxes[4].Font;
                Resources.FontList.Default.F6 = this.fN_boxes[5].Font;
                Resources.FontList.Default.F7 = this.fN_boxes[6].Font;
                Resources.FontList.Default.F8 = this.fN_boxes[7].Font;
                Resources.FontList.Default.F9 = this.fN_boxes[8].Font;
                Resources.FontList.Default.F10 = this.fN_boxes[9].Font;
                Resources.FontList.Default.F11 = this.fN_boxes[10].Font;
                Resources.FontList.Default.F12 = this.fN_boxes[11].Font;

                Resources.FontList.Default.F1Color = this.fN_boxes[0].ForeColor;
                Resources.FontList.Default.F2Color = this.fN_boxes[1].ForeColor;
                Resources.FontList.Default.F3Color = this.fN_boxes[2].ForeColor;
                Resources.FontList.Default.F4Color = this.fN_boxes[3].ForeColor;
                Resources.FontList.Default.F5Color = this.fN_boxes[4].ForeColor;
                Resources.FontList.Default.F6Color = this.fN_boxes[5].ForeColor;
                Resources.FontList.Default.F7Color = this.fN_boxes[6].ForeColor;
                Resources.FontList.Default.F8Color = this.fN_boxes[7].ForeColor;
                Resources.FontList.Default.F9Color = this.fN_boxes[8].ForeColor;
                Resources.FontList.Default.F10Color = this.fN_boxes[9].ForeColor;
                Resources.FontList.Default.F11Color = this.fN_boxes[10].ForeColor;
                Resources.FontList.Default.F12Color = this.fN_boxes[11].ForeColor;

                Resources.FontList.Default.Save();

                for (int i = 0; i < 12; i++)
                {
                    Debug.WriteLine($"this.fN_boxes[{i}].Font = Resources.FontList.Default.F{i + 1};");
                    Debug.WriteLine($"this.fN_boxes[{i}].ForeColor = Resources.FontList.Default.F{i + 1}Color");

                }
            }
        }

        private void MainFormResize(object sender, EventArgs e)
        {
            this.now_font_box.Location = new Point(this.ClientSize.Width - 105, this.ClientSize.Height - 30);
            this.now_font_box.Size = new Size(100, 25);

            for (int i = 0; i < 12; i++)
            {
                this.fN_boxes[i].Location = new Point(5 + i * 30, this.ClientSize.Height - 30);
                this.fN_boxes[i].Size = new Size(30, 25);
            }

            this.tab_control.Location = new Point(5, this.menu.Height + 5);
            this.tab_control.Size = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - this.tab_control.Location.Y - this.now_font_box.Height - 10);

            for (int i = 0; i < this.text_sets.Count; i++)
            {
                this.text_sets[i].TextBox.Location = new Point(0, 0);
                this.text_sets[i].TextBox.Size = this.tab_control.TabPages[i].Size;
            }

        }

        private void MainFormKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.F)
                {
                    MenuClick(this.text_strip.DropDownItems[0], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.C)
                {
                    MenuClick(this.text_strip.DropDownItems[1], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.B)
                {
                    MenuClick(this.text_strip.DropDownItems[2], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.I)
                {
                    MenuClick(this.text_strip.DropDownItems[3], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.U)
                {
                    MenuClick(this.text_strip.DropDownItems[4], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.S)
                {
                    MenuClick(this.text_strip.DropDownItems[5], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.A)
                {
                    MenuClick(this.text_strip.DropDownItems[6], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.S)
                {
                    MenuClick(this.text_strip.DropDownItems[7], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.D)
                {
                    MenuClick(this.text_strip.DropDownItems[8], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.P)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.FontFamily,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Size + 0.5f,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style);
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Alt) && e.KeyCode == Keys.M)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.FontFamily,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Size - 0.5f,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style);
                }

                else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.N)
                {
                    MenuClick(this.file_strip.DropDownItems[0], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.O)
                {
                    MenuClick(this.file_strip.DropDownItems[1], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control | Keys.Shift) && e.KeyCode == Keys.S)
                {
                    MenuClick(this.file_strip.DropDownItems[4], new EventArgs());
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.S)
                {
                    MenuClick(this.file_strip.DropDownItems[3], new EventArgs());
                }

                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F1)
                {
                    this.fN_boxes[0].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[0].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F2)
                {
                    this.fN_boxes[1].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[1].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F3)
                {
                    this.fN_boxes[2].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[2].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F4)
                {
                    this.fN_boxes[3].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[3].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F5)
                {
                    this.fN_boxes[4].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[4].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F6)
                {
                    this.fN_boxes[5].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[5].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F7)
                {
                    this.fN_boxes[6].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[6].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F8)
                {
                    this.fN_boxes[7].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[7].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F9)
                {
                    this.fN_boxes[8].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[8].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F10)
                {
                    this.fN_boxes[9].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[9].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F11)
                {
                    this.fN_boxes[10].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[10].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Shift) && e.KeyCode == Keys.F12)
                {
                    this.fN_boxes[11].Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                    this.fN_boxes[11].ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                }

                else if (e.KeyCode == Keys.F1)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[0].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[0].ForeColor;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[1].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[1].ForeColor;
                }
                else if (e.KeyCode == Keys.F3)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[2].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[2].ForeColor;
                }
                else if (e.KeyCode == Keys.F4)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[3].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[3].ForeColor;
                }
                else if (e.KeyCode == Keys.F5)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[4].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[4].ForeColor;
                }
                else if (e.KeyCode == Keys.F6)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[5].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[5].ForeColor;
                }
                else if (e.KeyCode == Keys.F7)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[6].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[6].ForeColor;
                }
                else if (e.KeyCode == Keys.F8)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[7].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[7].ForeColor;
                }
                else if (e.KeyCode == Keys.F9)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[8].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[8].ForeColor;
                }
                else if (e.KeyCode == Keys.F10)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[9].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[9].ForeColor;
                }
                else if (e.KeyCode == Keys.F11)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[10].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[10].ForeColor;
                }
                else if (e.KeyCode == Keys.F12)
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[11].Font;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[11].ForeColor;
                }

                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F1)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[0].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[0].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F2)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[1].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[1].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F3)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[2].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[2].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F4)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[3].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[3].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F5)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[4].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[4].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F6)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[5].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[5].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F7)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[6].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[6].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F8)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[7].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[7].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F9)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[8].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[8].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F10)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[9].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[9].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F11)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[10].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[10].ForeColor;
                //}
                //else if (Control.ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F12)
                //{
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = this.fN_boxes[11].Font;
                //    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = this.fN_boxes[11].ForeColor;
                //}

                this.now_font_box.Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                this.now_font_box.ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                this.now_font_box.Font = new Font(this.now_font_box.Font.FontFamily, 9, this.now_font_box.Font.Style);
            }
            catch (ArgumentOutOfRangeException _e)
            {
                Debug.WriteLine(_e);
            }
        }

        private void MenuClick(object sender, EventArgs e)
        {
            try
            {
                if (sender == this.file_strip.DropDownItems[0])
                {
                    this.tab_control.TabPages.Add("New page");
                    this.text_sets.Add(new Classes.TextSet()
                    {
                        Path = "",
                        TextBox = new RichTextBox()
                        {
                            Parent = this.tab_control.TabPages[this.tab_control.TabCount - 1],
                            Visible = true,
                            Font = Resources.FormSetting.Default.Font,
                            ForeColor = Resources.FormSetting.Default.ForeColor,
                            BackColor = Resources.FormSetting.Default.ControlColor,
                            SelectionFont = Resources.FormSetting.Default.Font,
                            SelectionColor = Resources.FormSetting.Default.ForeColor,
                            LanguageOption = RichTextBoxLanguageOptions.UIFonts,
                        }
                    });
                    this.text_sets[this.text_sets.Count - 1].TextBox.KeyDown += MainFormKeyDown;

                    this.tab_control.SelectedIndex = this.tab_control.TabCount - 1;
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.Focus();
                }
                else if (sender == this.file_strip.DropDownItems[1])
                {
                    OpenFileDialog dialog = new OpenFileDialog()
                    {
                        Filter = Resources.TextENG.DialogFilter,
                    };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.tab_control.TabPages.Add(dialog.FileName.Split('\\')[dialog.FileName.Split('\\').Length - 1]);
                        this.text_sets.Add(new Classes.TextSet()
                        {
                            Path = dialog.FileName,
                            TextBox = new RichTextBox()
                            {
                                Parent = this.tab_control.TabPages[this.tab_control.TabCount - 1],
                                Visible = true,
                                Font = Resources.FormSetting.Default.Font,
                                ForeColor = Resources.FormSetting.Default.ForeColor,
                                BackColor = Resources.FormSetting.Default.ControlColor,
                                SelectionFont = Resources.FormSetting.Default.Font,
                                SelectionColor = Resources.FormSetting.Default.ForeColor,
                                LanguageOption = RichTextBoxLanguageOptions.UIFonts,
                            }
                        });
                        this.text_sets[this.text_sets.Count - 1].TextBox.KeyDown += MainFormKeyDown;
                        this.text_sets[this.text_sets.Count - 1].TextBox.LoadFile(dialog.FileName);

                        this.tab_control.SelectedIndex = this.tab_control.TabCount - 1;
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.Focus();
                    }
                }
                else if (sender == this.file_strip.DropDownItems[2])
                {
                    if (MessageBox.Show(Resources.TextENG.ClosingMessage, Resources.TextENG.WarningMessage, MessageBoxButtons.OKCancel)
                        == DialogResult.OK)
                    {
                        this.text_sets.RemoveAt(this.tab_control.SelectedIndex);
                        this.tab_control.TabPages.RemoveAt(this.tab_control.SelectedIndex);

                        if (this.tab_control.TabCount == 0)
                        {
                            MenuClick(this.file_strip.DropDownItems[0], e);
                        }
                    }
                }
                else if (sender == this.file_strip.DropDownItems[3])
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = Resources.TextENG.DialogFilter,
                    };

                    if (this.text_sets[this.tab_control.SelectedIndex].Path == "")
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            this.text_sets[this.tab_control.SelectedIndex].Path = dialog.FileName;
                            this.text_sets[this.tab_control.SelectedIndex].TextBox.SaveFile(this.text_sets[this.tab_control.SelectedIndex].Path);

                            this.tab_control.TabPages[this.tab_control.SelectedIndex].Text
                                = this.text_sets[this.tab_control.SelectedIndex].Path.Split('\\')[this.text_sets[this.tab_control.SelectedIndex].Path.Split('\\').Length - 1];
                        }
                    }
                    else
                    {
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SaveFile(this.text_sets[this.tab_control.SelectedIndex].Path);
                    }

                }
                else if (sender == this.file_strip.DropDownItems[4])
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = Resources.TextENG.DialogFilter,
                    };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.text_sets[this.tab_control.SelectedIndex].Path = dialog.FileName;
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SaveFile(this.text_sets[this.tab_control.SelectedIndex].Path);

                        this.tab_control.TabPages[this.tab_control.SelectedIndex].Text
                                = this.text_sets[this.tab_control.SelectedIndex].Path.Split('\\')[this.text_sets[this.tab_control.SelectedIndex].Path.Split('\\').Length - 1];
                    }
                }

                else if (sender == this.edit_strip.DropDownItems[0])
                {
                    SendKeys.Send("^x");
                }
                else if (sender == this.edit_strip.DropDownItems[1])
                {
                    SendKeys.Send("^c");
                }
                else if (sender == this.edit_strip.DropDownItems[2])
                {
                    SendKeys.Send("^v");
                }
                else if (sender == this.edit_strip.DropDownItems[3])
                {
                    SendKeys.Send("^a");
                }

                else if (sender == this.view_strip.DropDownItems[0])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.ZoomFactor += 0.5f;
                }
                else if (sender == this.view_strip.DropDownItems[1])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.ZoomFactor -= 0.5f;
                }

                else if (sender == this.text_strip.DropDownItems[0])
                {
                    FontDialog dialog = new FontDialog()
                    {
                        Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont,
                    };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont = new Font(dialog.Font, dialog.Font.Style);
                    }
                }
                else if (sender == this.text_strip.DropDownItems[1])
                {
                    ColorDialog dialog = new ColorDialog()
                    {
                        Color = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor,
                    };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor = dialog.Color;
                    }
                }
                else if (sender == this.text_strip.DropDownItems[2])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style ^ FontStyle.Bold);
                }
                else if (sender == this.text_strip.DropDownItems[3])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style ^ FontStyle.Italic);
                }
                else if (sender == this.text_strip.DropDownItems[4])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style ^ FontStyle.Underline);
                }
                else if (sender == this.text_strip.DropDownItems[5])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont
                        = new Font(this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont,
                        this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont.Style ^ FontStyle.Strikeout);
                }
                else if (sender == this.text_strip.DropDownItems[6])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionAlignment = HorizontalAlignment.Left;
                }
                else if (sender == this.text_strip.DropDownItems[7])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionAlignment = HorizontalAlignment.Center;
                }
                else if (sender == this.text_strip.DropDownItems[8])
                {
                    this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionAlignment = HorizontalAlignment.Right;
                }

                MainFormResize(this, e);

                this.now_font_box.Font = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionFont;
                this.now_font_box.ForeColor = this.text_sets[this.tab_control.SelectedIndex].TextBox.SelectionColor;
                this.now_font_box.Font = new Font(this.now_font_box.Font.FontFamily, 9, this.now_font_box.Font.Style);
            }
            catch (ArgumentOutOfRangeException _e)
            {
                Debug.WriteLine(_e);
            }
        }
    }
}
