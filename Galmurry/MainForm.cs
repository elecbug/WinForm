using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;

namespace Galmurry
{
    public partial class MainForm : Form
    {
        private RichTextBox path;
        private WebView2 view;
        private SplitContainer splitter;
        private Button read;
        private RichTextBox read_text;
        private RichTextBox write_text;
        private int index;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainFormLoad;

            this.Text = "Galmurry";
            this.Font = new Font("±¼¸²Ã¼", 11, FontStyle.Regular);
        }

        private void MainFormLoad(object? sender, EventArgs e)
        {
            this.splitter = new SplitContainer()
            {
                Parent = this,
                Visible = true,
                Location = new Point(),
                Size = this.ClientSize,
                BorderStyle = BorderStyle.FixedSingle,
            };
            this.splitter.SplitterMoved += MainFormResize;

            this.path = new RichTextBox()
            {
                Parent = this.splitter.Panel1,
                Visible = true,
                Location = new Point(5, 5),
                Size = new Size(this.splitter.Panel1.Width - 10, 35),
                LanguageOption = RichTextBoxLanguageOptions.UIFonts,
                Multiline = false,
            };
            this.path.KeyDown += PathKeyDown;

            this.view = new WebView2()
            {
                Parent = this.splitter.Panel1,
                Visible = true,
                Location = new Point(5, 45),
                Size = new Size(this.splitter.Panel1.Width - 10, this.splitter.Height - 55),
                Source = new Uri("https://www.google.com"),
            };
            this.view.SourceChanged += WebViewSourceChanged;

            this.read = new Button()
            {
                Parent = this.splitter.Panel2,
                Visible = true,
                Location = new Point(5, 5),
                Size = new Size(this.splitter.Panel2.Width - 10, 35),
                Text = "Galmurry",
            };
            this.read.Click += ReadClick;

            this.write_text = new RichTextBox()
            {
                Parent = this.splitter.Panel2,
                Visible = true,
                Location = new Point(5, 45),
                Size = new Size(this.splitter.Panel2.Width - 10, 35),
                LanguageOption = RichTextBoxLanguageOptions.UIFonts,
                Multiline = false,
            };
            this.write_text.KeyDown += WriteTextKeyPress;
            this.write_text.TextChanged += WriteTextChanged;

            this.read_text = new RichTextBox()
            {
                Parent = this.splitter.Panel2,
                Visible = true,
                Location = new Point(5, 85),
                Size = new Size(this.splitter.Panel2.Width - 10, this.splitter.Panel2.Height - 90),
                ReadOnly = true,
            };

            this.WindowState = FormWindowState.Maximized;
            this.SizeChanged += MainFormResize;

            MainFormResize(this, e);
        }

        private void WriteTextKeyPress(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.read_text.Select(0, this.read_text.Text.Length);
                    this.read_text.SelectionColor = Color.Black;

                    e.Handled = true;

                    this.index = this.read_text.Find(this.write_text.Text, this.index, RichTextBoxFinds.None)
                        + this.write_text.Text.Length;

                    this.read_text.Select(this.read_text.Find(this.write_text.Text, this.index, RichTextBoxFinds.None), this.write_text.Text.Length);
                    this.read_text.SelectionColor = Color.Red;

                    this.read_text.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private void WriteTextChanged(object? sender, EventArgs e)
        {
            try
            {
                this.read_text.Select(0, this.read_text.Text.Length);
                this.read_text.SelectionColor = Color.Black;

                this.read_text.Select(this.read_text.Find(this.write_text.Text, 0, RichTextBoxFinds.None), this.write_text.Text.Length);
                this.read_text.SelectionColor = Color.Red;

                this.index = this.read_text.Find(this.write_text.Text, 0, RichTextBoxFinds.None);

                this.read_text.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void ReadClick(object? sender, EventArgs e)
        {
            this.read_text.Text = await view.ExecuteScriptAsync("document.documentElement.outerHTML");
        }

        private void WebViewSourceChanged(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            this.path.Text = this.view.Source.ToString();
        }

        private void PathKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                try
                {
                    this.view.Source
                        = new Uri((sender as RichTextBox)?.Text ?? "");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    try
                    {
                        if (!this.path.Text.StartsWith("https://"))
                        {
                            this.path.Text = "https://" + this.path.Text;
                        }

                        PathKeyDown(sender, e);
                    }
                    catch (Exception exx)
                    {
                        Debug.WriteLine(exx);

                        this.path.Text = "Not Found";
                    }
                }
            }
        }

        private void MainFormResize(object? sender, EventArgs e)
        {
            this.splitter.Size = this.ClientSize;
            this.path.Size = new Size(this.path.Parent.Width - 10, 35);
            this.view.Size = new Size(this.view.Parent.Width - 10, this.view.Parent.Height - 55);

            this.read.Size = new Size(this.read.Parent.Width - 10, 35);

            this.write_text.Size = new Size(this.write_text.Parent.Width - 10, 35);
            this.read_text.Size = new Size(this.read_text.Parent.Width - 10, this.read_text.Parent.Height - 90);
        }
    }
}