namespace MultiMinesweeper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.ClientSize = new Size(500, 500);
            var panel = new Controls.FieldPanel(20, 20, 60)
            {
                Width = 500,
                Height = 500,
                Visible = true,
                Parent = this,
                BackColor = Color.Black,
            };

            panel.SetField();
        }
    }
}