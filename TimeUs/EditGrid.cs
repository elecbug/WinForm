using static TimeUs.TimeTable.PersonalTime.TimeToTime;

namespace TimeUs
{
    /// <summary>
    /// The custom data grid view for personal time table.
    /// </summary>
    internal class EditGrid : DataGridView
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditGrid()
        {
            this.Columns.Add("hh", "시작 시각");
            this.Columns.Add("mm)", "수업 시간");
            this.Columns.Add("w", "요일");

            for (int i = 0; i < 2; i++)
            {
                this.Columns[i].ValueType = typeof(string);
                this.Columns[i].Width = 90;
            }
            this.Columns[2].ValueType = typeof(DayOfWeek);
            this.Columns[2].Width = 90;
        }

        /// <summary>
        /// Set table by time_table's owner.
        /// </summary>
        /// <param name="time_table"> time talbe </param>
        public void Set(TimeTable.PersonalTime time_table)
        {
            this.Rows.Clear();

            for (int i = 0; i < time_table.Times.Count; i++)
            {
                this.Rows.Add(time_table.Times[i].StartTime.Hour, time_table.Times[i].StartTime.Minute,
                    time_table.Times[i].Term.Hour, time_table.Times[i].Term.Minute);
            }
        }
    }
}
