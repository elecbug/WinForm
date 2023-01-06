using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GBTH.List
{
    public class IngredientList : ListView
    {
        public class Row
        {
            public int Number { get; set; }
            public string? Name { get; set; }
            public string? Standard { get; set; }
            public string? Unit { get; set; }
            public string? Assortment { get; set; }
        }
        public class Report
        {
            public int Number { get; set; }
            public string? Name { get; set; }
            public string? Standard { get; set; }
            public List<ReportRow> Rows { get; set; } = new List<ReportRow>();
        }
        public class ReportRow
        {
            public int Number { get; set; }
            public string? Date { get; set; }
            public string? Descryption { get; set; }
            public int Insert { get; set; }
            public int Use { get; set; }
            public int Storege { get; set; }
            public string? Manager { get; set; }
            public string? Other { get; set; }

            public ReportRow(int number, string? date, string? descryption, int insert,
                int use, int storege, string? manager, string? other)
            {
                this.Number = number;
                this.Date = date;
                this.Descryption = descryption;
                this.Insert = insert;
                this.Use = use;
                this.Storege = storege;
                this.Manager = manager;
                this.Other = other;
            }
        }

        public List<Row> Rows { get; private set; }
        public List<Report> Reports { get; private set; }

        private ReportGrid grid_view;
        private string? path;
        private Report? selected_report;

        public IngredientList(ref ReportGrid grid_view) : base() 
        {
            this.Rows = new List<Row>();
            this.Reports = new List<Report>();

            this.grid_view = grid_view;

            this.Columns.Add("No.");
            this.Columns.Add("이름");
            this.Columns.Add("규격");
            this.Columns.Add("단위");
            this.Columns.Add("분류");

            this.View = View.Details;
            this.FullRowSelect = true;

            this.ItemSelectionChanged += IngredientListSelectionChanged;
            this.ColumnClick += IngredientListColumnClick;
        }

        private void IngredientListColumnClick(object? sender, ColumnClickEventArgs e)
        {
            this.ListViewItemSorter = new ListViewColumnSorter();
            if (e.Column == 0 || e.Column == 3 || e.Column == 4 || e.Column == 5)
            {
                (this.ListViewItemSorter as ListViewColumnSorter)!.SetType(typeof(int));
            }

            if (e.Column == (this.ListViewItemSorter as ListViewColumnSorter)!.SortColumn)
            {
                if ((this.ListViewItemSorter as ListViewColumnSorter)!.Order == SortOrder.Ascending)
                {
                    (this.ListViewItemSorter as ListViewColumnSorter)!.Order = SortOrder.Descending;
                }
                else
                {
                    (this.ListViewItemSorter as ListViewColumnSorter)!.Order = SortOrder.Ascending;
                }
            }
            else
            {
                (this.ListViewItemSorter as ListViewColumnSorter)!.SortColumn = e.Column;
                (this.ListViewItemSorter as ListViewColumnSorter)!.Order = SortOrder.Ascending;
            }

            this.Sort();
        }
        private void IngredientListSelectionChanged(object? sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 0)
            {
                if (this.selected_report != null)
                {
                    SaveData();
                }

                this.selected_report = this.Reports.Find((x) => { return int.Parse(this.SelectedItems[0].Text) == x.Number; });

                if (this.selected_report == null)
                {
                    this.selected_report = new Report()
                    {
                        Number = int.Parse(this.SelectedItems[0].Text),
                        Name = this.SelectedItems[0].SubItems[0].Text,
                        Standard = this.SelectedItems[0].SubItems[1].Text,
                    };

                    this.Reports.Add(this.selected_report);
                }

                LoadData();
            }
        }

        public void Add(Row row)
        {
            ListViewItem item = new ListViewItem(row.Number.ToString());

            item.SubItems.Add(row.Name);
            item.SubItems.Add(row.Standard);
            item.SubItems.Add(row.Unit);
            item.SubItems.Add(row.Assortment);

            this.Rows.Add(row);
            this.Items.Add(item);
        }
        public void Print()
        {

        }

        private void LoadData()
        {
            this.grid_view.Rows.Clear();

            for (int i = 0; i < this.selected_report!.Rows.Count; i++)
            {
                this.grid_view.Rows.Add
                    (
                        this.selected_report.Rows[i].Number,
                        this.selected_report.Rows[i].Date,
                        this.selected_report.Rows[i].Descryption,
                        this.selected_report.Rows[i].Insert,
                        this.selected_report.Rows[i].Use,
                        this.selected_report.Rows[i].Storege,
                        this.selected_report.Rows[i].Manager,
                        this.selected_report.Rows[i].Other
                    );
            }
        }
        public void SaveData()
        {
            if (this.selected_report != null)
            {
                this.selected_report!.Rows.Clear();

                for (int i = 0; i < this.grid_view.RowCount - 1; i++)
                {
                    this.selected_report!.Rows.Add(new ReportRow
                        (
                            int.Parse((this.grid_view.Rows[i].Cells[0].EditedFormattedValue as string == "" ? "0" 
                                : this.grid_view.Rows[i].Cells[0].EditedFormattedValue as string)!),
                            this.grid_view.Rows[i].Cells[1].EditedFormattedValue as string,
                            this.grid_view.Rows[i].Cells[2].EditedFormattedValue as string,
                            int.Parse((this.grid_view.Rows[i].Cells[3].EditedFormattedValue as string == "" ? "0"
                                : this.grid_view.Rows[i].Cells[3].EditedFormattedValue as string)!),
                            int.Parse((this.grid_view.Rows[i].Cells[4].EditedFormattedValue as string == "" ? "0"
                                : this.grid_view.Rows[i].Cells[4].EditedFormattedValue as string)!),
                            int.Parse((this.grid_view.Rows[i].Cells[5].EditedFormattedValue as string == "" ? "0"
                                : this.grid_view.Rows[i].Cells[5].EditedFormattedValue as string)!),
                            this.grid_view.Rows[i].Cells[6].EditedFormattedValue as string,
                            this.grid_view.Rows[i].Cells[1].EditedFormattedValue as string
                        ));
                }

                Serialize();
            }
        }

        public void Serialize()
        {
            string result = JsonSerializer.Serialize(this.Rows) + "\r" + JsonSerializer.Serialize(this.Reports);
            
            if (this.path != null)
            {
                if (!Directory.Exists(this.path))
                {
                    Directory.CreateDirectory(this.path);
                }
            }
            else
            {
                throw new Exception("This instance isn't have path");
            }

            StreamWriter writer = new StreamWriter(this.path + "\\" + Properties.Resources.PartedIngredientPath);
            writer.Write(result);
            writer.Close();
        }
        public void Serialize(string path_of_folder, int year)
        {
            string result = JsonSerializer.Serialize(this.Rows) + "\r" + JsonSerializer.Serialize(this.Reports);

            if (!Directory.Exists(path_of_folder + "\\" + year))
            {
                Directory.CreateDirectory(path_of_folder + "\\" + year);
            }

            StreamWriter writer = new StreamWriter(path_of_folder + "\\" + year + "\\" + Properties.Resources.PartedIngredientPath);
            writer.Write(result);
            writer.Close();
        }

        public void PartedDeserialize()
        {
            EB.Excel.Stream stream = new EB.Excel.Stream(Environment.CurrentDirectory + "\\default.xlsx");
            string[,] cells = stream.Read(stream.UsedRange);

            for (int r = 0; r < cells.GetLength(0); r++)
            {
                this.Rows.Add(new Row()
                {
                    Number = int.Parse("0" + cells[r, 0]),
                    Name = cells[r, 1],
                    Standard = cells[r, 2],
                    Unit = cells[r, 3],
                    Assortment = cells[r, 4],
                });
            }

            string result = JsonSerializer.Serialize(this.Rows) + "\r" + JsonSerializer.Serialize(this.Reports);

            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path!);
            }

            StreamWriter writer = new StreamWriter(this.path + "\\" + Properties.Resources.PartedIngredientPath);
            writer.Write(result);
            writer.Close();
        }
        public static IngredientList Deserialize(ref ReportGrid grid_view, string path_of_folder, int year)
        {
            if (!Directory.Exists(path_of_folder + "\\" + year))
            {
                IngredientList result = new IngredientList(ref grid_view)
                {
                    path = path_of_folder + "\\" + year,
                };
                result.PartedDeserialize();
            }
            StreamReader reader = new StreamReader(path_of_folder + "\\" + year + "\\" + Properties.Resources.PartedIngredientPath);
            string json = reader.ReadToEnd();
            string row_json = json.Split('\r')[0];
            string report_json = json.Split('\r')[1];
            reader.Close();

            List<Row>? rows = JsonSerializer.Deserialize<List<Row>>(row_json);
            List<Report>? reports = JsonSerializer.Deserialize<List<Report>>(report_json);

            if (rows != null && reports != null)
            {
                IngredientList result = new IngredientList(ref grid_view)
                {
                    Rows = rows,
                    path = path_of_folder + "\\" + year,
                    Reports = reports,
                };

                foreach (Row row in rows)
                {
                    ListViewItem item = new ListViewItem(row.Number.ToString());
                    item.SubItems.Add(row.Name);
                    item.SubItems.Add(row.Standard);
                    item.SubItems.Add(row.Unit);
                    item.SubItems.Add(row.Assortment);

                    result.Items.Add(item);
                }

                return result;
            }
            else
            {
                throw new Exception("json rows data is null");
            }
        }
    }
}
