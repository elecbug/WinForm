namespace TimeUs
{
    public partial class TimeUs : Form
    {
        public TimeUs()
        {
            InitializeComponent();
            this.Text = "Time Us";
            this.ResizeRedraw = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 900);
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            new TimeTable(this.ClientSize.Width, this.ClientSize.Height)
            {
                Parent = this,
                Visible = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
            };
        }
    }
}