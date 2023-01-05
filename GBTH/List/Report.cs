namespace GBTH.List
{
    public partial class PartedIngredientList
    {
        public class Report
        {
            public string? Name { get; set; }
            public string? Standard { get; set; }

            public class ReportRow
            {
                public int Number { get; set; }
                public DateTime Date { get; set; }
                public string? Descryption { get; set; }
                public int Insert { get; set; }
                public int Use { get; set; }
                public int Storege { get; set; }
                public string? Manager { get; set; }
                public string? Other { get; set; }
            }
        }
    }
}
