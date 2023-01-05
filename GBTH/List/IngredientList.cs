using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GBTH.List
{
    public class IngredientList
    {
        public class Row
        {
            public int Year { get; set; }
            public string? ListPath { get; set; }
            public string? ReportPath { get; set; }
        }

        public List<Row> Rows { get; private set; }

        public IngredientList()
        {
            this.Rows = new List<Row>();
        }

        public void Serialize(string path_of_folder)
        {
            string result = JsonSerializer.Serialize(this.Rows);

            if (!Directory.Exists(path_of_folder))
            {
                Directory.CreateDirectory(path_of_folder);
            }

            StreamWriter writer = new StreamWriter(path_of_folder + "\\" + Properties.Resources.IngredientPath);
            writer.Write(result);
            writer.Close();
        }

        public static IngredientList Deserialize(string path_of_folder)
        {
            if (!Directory.Exists(path_of_folder))
            {
                throw new Exception("Not found path: (" + path_of_folder + ")");
            }

            StreamReader reader = new StreamReader(path_of_folder + "\\" + Properties.Resources.IngredientPath);
            string json = reader.ReadToEnd();
            reader.Close();

            List<Row>? rows = JsonSerializer.Deserialize<List<Row>>(json);

            if (rows != null)
            {
                IngredientList result = new IngredientList()
                {
                    Rows = rows,
                };

                return result;
            }
            else
            {
                throw new Exception("json rows data is null");
            }    
        }
    }
}
