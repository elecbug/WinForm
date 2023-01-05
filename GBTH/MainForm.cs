using GBTH.List;

namespace GBTH
{
    public partial class MainForm : Form
    {
        private SplitContainer tool_container;
        private SplitContainer container;
        private PartedIngredientList list_view;
        private DataGridView grid;

        public MainForm()
        {
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();

            this.tool_container = new SplitContainer()
            {
                Parent = this,
                Visible = true,
                Size = this.ClientSize,
                Orientation = Orientation.Horizontal,
                Dock = DockStyle.Fill,
            };
            this.container = new SplitContainer()
            {
                Parent = this.tool_container.Panel1,
                Visible = true,
                Size = this.tool_container.Panel1.ClientSize,
                Dock = DockStyle.Fill,
            };
            this.list_view = new PartedIngredientList()
            {
                Parent = this.container.Panel1,
                Visible = true,
                Size = this.container.Panel1.ClientSize,
                Dock = DockStyle.Fill,
            };
            this.grid = new DataGridView()
            {
                Parent = this.container.Panel2,
                Visible = true,
                Size = this.container.Panel2.ClientSize,
                Dock = DockStyle.Fill,
            };

            //List.IngredientList list = new GBTH.List.IngredientList();
            //list.Rows.Add(new GBTH.List.IngredientList.Row() {Year = 2020, ListPath = "hi", ReportPath = "Hello" });
            //list.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            //IngredientList news = IngredientList.Deserialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            //this.list_view.Add(new PartedIngredientList.Row() { Number = 1, Name = "A" });
            //this.list_view.Add(new PartedIngredientList.Row() { Number = 2, Name = "B" });
            //this.list_view.Add(new PartedIngredientList.Row() { Number = 3, Name = "C" });
            //this.list_view.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 2020);

            //this.list_view = PartedIngredientList.Deserialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 2020);
            //this.list_view.Parent = this.container.Panel1;
            //this.list_view.Visible = true;
            //this.list_view.Size = this.container.Panel1.ClientSize;
            //this.list_view.Dock = DockStyle.Fill;
        }

        private void ResizeAll(object? sender, EventArgs e)
        {

        }
    }
}