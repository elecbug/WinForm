using GBTH.List;

namespace GBTH
{
    public partial class MainForm : Form
    {
        private SplitContainer tool_container;
        private SplitContainer container;
        private IngredientList list_view;
        private DataGridView grid_view;
        private NumericUpDown numeric;

        public MainForm()
        {

            this.Load += MainFormLoad;
            this.FormClosed += MainFormFormClosed;

            InitializeComponent();

            this.Text = "GBTH";
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("굴림체", 13, FontStyle.Regular);

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
                Parent = this.tool_container.Panel2,
                Visible = true,
                Size = this.tool_container.Panel1.ClientSize,
                Dock = DockStyle.Fill,
            };

            this.grid_view = new DataGridView()
            {
                Parent = this.container.Panel2,
                Visible = true,
                Size = this.container.Panel2.ClientSize,
                Dock = DockStyle.Fill,
            };
            this.grid_view.Columns.Add("Number", "번호"); //0
            this.grid_view.Columns.Add("Date", "날짜");
            this.grid_view.Columns.Add("Descrytion", "내용");
            this.grid_view.Columns.Add("Insert", "입고"); //3
            this.grid_view.Columns.Add("Use", "사용"); //4
            this.grid_view.Columns.Add("Storege", "재고"); //5
            this.grid_view.Columns.Add("Manager", "담당자");
            this.grid_view.Columns.Add("Other", "비고");

            this.grid_view.Columns[0].ValueType = typeof(int);
            this.grid_view.Columns[3].ValueType = typeof(int);
            this.grid_view.Columns[4].ValueType = typeof(int);
            this.grid_view.Columns[5].ValueType = typeof(int);
            this.grid_view.Columns[5].ReadOnly = true;

            this.grid_view.Click += (sender, e) =>
            {
                this.grid_view.Rows[0].Cells[5].Value 
                    = int.Parse((this.grid_view.Rows[0].Cells[3].EditedFormattedValue as string == "" ? "0"
                        : this.grid_view.Rows[0].Cells[3].EditedFormattedValue as string)!);

                for (int r = 1; r < this.grid_view.RowCount; r++)
                {
                    this.grid_view.Rows[r].Cells[5].Value
                        = int.Parse((this.grid_view.Rows[r - 1].Cells[5].EditedFormattedValue as string == "" ? "0"
                            : this.grid_view.Rows[r - 1].Cells[5].EditedFormattedValue as string)!)
                        - int.Parse((this.grid_view.Rows[r - 1].Cells[4].EditedFormattedValue as string == "" ? "0"
                            : this.grid_view.Rows[r - 1].Cells[4].EditedFormattedValue as string)!)
                        + int.Parse((this.grid_view.Rows[r].Cells[3].EditedFormattedValue as string == "" ? "0"
                            : this.grid_view.Rows[r].Cells[3].EditedFormattedValue as string)!);
                }
            };

            this.numeric = new NumericUpDown()
            {
                Parent = this.tool_container.Panel1,
                Visible = true,
                Minimum = 2000,
                Maximum = 3000,
                Value = DateTime.Now.Year,
                Increment = 1,
            };
            this.numeric.ValueChanged += NumericValueChanged;
            {
                //this.list_view = new PartedIngredientList(ref this.grid)
                //{
                //    Parent = this.container.Panel1,
                //    Visible = true,
                //    Size = this.container.Panel1.ClientSize,
                //    Dock = DockStyle.Fill,
                //};

                //List.IngredientList list = new GBTH.List.IngredientList();
                //list.Rows.Add(new GBTH.List.IngredientList.Row() {Year = 2020, ListPath = "hi", ReportPath = "Hello" });
                //list.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                //IngredientList news = IngredientList.Deserialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                //this.list_view.Add(new PartedIngredientList.Row() { Number = 1, Name = "A" });
                //this.list_view.Add(new PartedIngredientList.Row() { Number = 2, Name = "B" });
                //this.list_view.Add(new PartedIngredientList.Row() { Number = 3, Name = "C" });
                //this.list_view.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 2020);
            }

            this.list_view = IngredientList.Deserialize(ref this.grid_view, Environment.CurrentDirectory, (int)this.numeric.Value);
            this.list_view.Parent = this.container.Panel1;
            this.list_view.Visible = true;
            this.list_view.Size = this.container.Panel1.ClientSize;
            this.list_view.Dock = DockStyle.Fill;
        }

        private void MainFormLoad(object? sender, EventArgs e)
        {
            PropertiesLoad();
        }

        private void NumericValueChanged(object? sender, EventArgs e)
        {
            PropertiesSave();

            this.list_view.Visible = false;
            this.list_view.Dispose();

            this.list_view = IngredientList.Deserialize(ref this.grid_view, Environment.CurrentDirectory, (int)this.numeric.Value);
            this.list_view.Parent = this.container.Panel1;
            this.list_view.Visible = true;
            this.list_view.Size = this.container.Panel1.ClientSize;
            this.list_view.Dock = DockStyle.Fill;

            PropertiesLoad();
        }

        private void MainFormFormClosed(object? sender, FormClosedEventArgs e)
        {
            this.list_view.SaveData();

            PropertiesSave();
        }

        private void PropertiesSave()
        {
            Properties.Settings.Default.AllSettings
                = this.tool_container.SplitterDistance + "\\"
                + this.container.SplitterDistance + "\\";

            for (int c = 0; c < this.list_view.Columns.Count; c++)
            {
                Properties.Settings.Default.AllSettings
                    += this.list_view.Columns[c].Width + "\\";
            }
            for (int c = 0; c < this.grid_view.Columns.Count; c++)
            {
                Properties.Settings.Default.AllSettings
                    += this.grid_view.Columns[c].Width + "\\";
            }

            Properties.Settings.Default.Save();
        }
        private void PropertiesLoad()
        {
            if (Properties.Settings.Default.AllSettings != "")
            {
                int i = 0;
                string[] splits = Properties.Settings.Default.AllSettings.Split('\\');

                this.tool_container.SplitterDistance = int.Parse(splits[i++]);
                this.container.SplitterDistance = int.Parse(splits[i++]);

                for (int c = 0; c < this.list_view.Columns.Count; c++)
                {
                    this.list_view.Columns[c].Width = int.Parse(splits[i++]);
                }
                for (int c = 0; c < this.grid_view.Columns.Count; c++)
                {
                    this.grid_view.Columns[c].Width = int.Parse(splits[i++]);
                }
            }
        }
    }
}