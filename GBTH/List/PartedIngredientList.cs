using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GBTH.List
{
    public partial class PartedIngredientList : ListView
    {
        public class Row
        {
            public int Number { get; set; }
            public string? Name { get; set; }
            public string? Standard { get; set; }
            public string? Unit { get; set; }
            public string? Assortment { get; set; }
        }

        public List<Row> Rows { get; private set; }

        public PartedIngredientList() : base() 
        {
            this.Rows = new List<Row>();

            this.Columns.Add("No.");
            this.Columns.Add("이름");
            this.Columns.Add("규격");
            this.Columns.Add("단위");
            this.Columns.Add("분류");

            this.View = View.Details;
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

        public void Serialize(string path_of_folder, int year)
        {
            string result = JsonSerializer.Serialize(this.Rows);

            if (!Directory.Exists(path_of_folder))
            {
                Directory.CreateDirectory(path_of_folder);
            }

            StreamWriter writer = new StreamWriter(path_of_folder + "\\" + year + "-" + Properties.Resources.PartedIngredientPath);
            writer.Write(result);
            writer.Close();
        }

        public static PartedIngredientList Deserialize(string path_of_folder, int year)
        {
            if (!Directory.Exists(path_of_folder))
            {
                throw new Exception("Not found path: (" + path_of_folder + ")");
            }

            StreamReader reader = new StreamReader(path_of_folder + "\\" + year + "-" + Properties.Resources.PartedIngredientPath);
            string json = reader.ReadToEnd();
            reader.Close();

            List<Row>? rows = JsonSerializer.Deserialize<List<Row>>(json);

            if (rows != null)
            {
                PartedIngredientList result = new PartedIngredientList()
                {
                    Rows = rows,
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
