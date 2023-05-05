using static TimeUs.TimeTable;
using static TimeUs.TimeTable.PersonalTime;
using static TimeUs.TimeTable.PersonalTime.TimeToTime;

namespace TimeUs
{
    class Setter
    {
        public Setter(TimeTable table)
        {

            {
                int i = 0;
                table.AllTables.Add(new PersonalTime()
                {
                    PersonColor = new OColor { R = 0, G = 0, B = 0 },
                    Name = "Temp",
                });
                table.CheckedList.Items.Add("Temp");

                table.AllTables[i].Times.Add(new PersonalTime.TimeToTime()
                {
                    StartTime = new HourMinute(8, 0),
                    Term = new HourMinute(2, 0),
                    DayOfWeek = DayOfWeek.Monday,
                });
            }
        }
    }
}
