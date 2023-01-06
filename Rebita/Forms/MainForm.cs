using System.Drawing;
using System.Windows.Forms;

using C = Rebita.Classes;

namespace Rebita
{
    public partial class MainForm : Form
    {
        private RichTextBox data_log;
        private RichTextBox data_input;

        private C.Discord.Manager manager;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "Rebita Forms";
            this.Font = new Font("Mono", 9, FontStyle.Regular);
            this.ClientSize = new Size(400, 600);
            this.MaximizeBox = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.data_log = new RichTextBox()
            {
                ReadOnly = true,
                Visible = true,
                Parent = this,
                Location = new Point(10, 10),
                Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 70),
            };

            this.data_input = new RichTextBox()
            {
                Multiline = false,
                Visible = true,
                Parent = this,
                Location = new Point(10, this.data_log.Location.Y + this.data_log.Height + 10),
                Size = new Size(this.ClientSize.Width - 20, 40),
            };
            this.data_input.KeyDown += ClickKeyboard;
        }

        private async void ClickKeyboard(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.data_input.Text == "/start")
                {
                    this.manager = new C.Discord.Manager(this.data_log, this.data_input);
                    this.manager.StartBot();
                }
                else if (this.data_input.Text == "/stop")
                {
                    this.manager.StopBot();
                }
                else if (this.data_input.Text.StartsWith("/msg"))
                {
                    await this.manager.LastChannel.SendMessageAsync(this.data_input.Text.Replace("/msg", ""));
                }

                ClearDataInput();
            }

        }

        private void ClearDataInput()
        {
            this.data_input.Text = "";
            this.data_input.Focus();
        }
    }
}
