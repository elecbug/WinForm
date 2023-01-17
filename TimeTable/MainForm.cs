namespace TimeTable
{
    public partial class MainForm : Form
    {
        private ListView list_view;

        public MainForm()
        {
            InitializeComponent();

            this.list_view = new ListView()
            {
                Parent = this,
                Size = this.ClientSize,
                Dock = DockStyle.Fill,
            };

            EB.Excel.Stream stream = new EB.Excel.Stream()
            this.list_view.Columns.Add()
        }
    }
}