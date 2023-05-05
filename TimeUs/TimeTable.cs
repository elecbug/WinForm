using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TimeUs
{
    /// <summary>
    /// Sharing time table class.
    /// </summary>
    internal class TimeTable : SplitContainer
    {
        /// <summary>
        /// One person's time table subclass in sharing time table class.
        /// </summary>
        internal class PersonalTime
        {
            /// <summary>
            /// The struct means a lesson, have start time and lesson term.
            /// </summary>
            internal struct TimeToTime
            {
                /// <summary>
                /// Have only Hour and minute struct.
                /// </summary>
                internal struct HourMinute
                {
                    public int Hour { get; set; }
                    public int Minute { get; set; }

                    /// <summary>
                    /// The normal constructor.
                    /// </summary>
                    /// <param name="hour"> Hour. </param>
                    /// <param name="minute"> Minute. </param>
                    public HourMinute(int hour, int minute)
                    {
                        this.Hour = hour;
                        this.Minute = minute;
                    }
                    /// <summary>
                    /// If insert time value, Parsing time data.
                    /// </summary>
                    /// <param name="time"> text value. </param>
                    public HourMinute(string text)
                    {
                        if (text.IndexOf(':') != -1)
                        {
                            try
                            {
                                string[] split = text.Split(':');
                                this.Hour = int.Parse(split[0]);
                                this.Minute = int.Parse(split[1]);
                            }
                            catch (Exception e)
                            {
                                this.Hour = 0;
                                this.Minute = 0;
                            }
                        }
                        else if (text.IndexOf('.') != -1)
                        {
                            try
                            {
                                string[] split = text.Split('.');
                                this.Hour = int.Parse(split[0]);
                                this.Minute = int.Parse(split[1]);
                            }
                            catch (Exception e)
                            {
                                this.Hour = 0;
                                this.Minute = 0;
                            }
                        }
                        else if (text.IndexOf(' ') != -1)
                        {
                            try
                            {
                                string[] split = text.Split(' ');
                                this.Hour = int.Parse(split[0]);
                                this.Minute = int.Parse(split[1]);
                            }
                            catch (Exception e)
                            {
                                this.Hour = 0;
                                this.Minute = 0;
                            }
                        }
                        else
                        {
                            try
                            {
                                this.Hour = int.Parse(text);
                                this.Minute = 0;
                            }
                            catch (Exception e)
                            {
                                this.Hour = 0;
                                this.Minute = 0;
                            }
                        }
                    }

                    public override string ToString()
                    {
                        return $"{ this.Hour.ToString().PadLeft(2, '0')}:{ this.Minute.ToString().PadLeft(2, '0')}";
                    }
                }

                /// <summary>
                /// Start Time on lesson.
                /// </summary>
                public HourMinute StartTime { get; set; }
                /// <summary>
                /// Start - End time term by lesson.
                /// </summary>
                public HourMinute Term { get; set; }
                /// <summary>
                /// Day of week.
                /// </summary>
                public DayOfWeek DayOfWeek { get; set; }
            }
            /// <summary>
            /// Open field color struct.
            /// </summary>
            internal struct OColor
            {
                public int R { get; set; }
                public int G { get; set; }
                public int B { get; set; }

                public Color ToColor()
                {
                    return Color.FromArgb(R, G, B);
                }
            }

            /// <summary>
            /// Name of time table owner.
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// List of lessons had by this time table object.
            /// </summary>
            public List<TimeToTime> Times { get; set; }
            /// <summary>
            /// The person's drawing color.
            /// </summary>
            public OColor PersonColor { get; set; }

            /// <summary>
            /// Default constructor, Set non-nullable properties. 
            /// </summary>
            public PersonalTime()
            {
                this.Name = string.Empty;
                this.Times = new List<TimeToTime>();
                this.PersonColor = new OColor();
            }

            /// <summary>
            /// Copy constructor.
            /// </summary>
            /// <param name="copy"> src copyed.</param>
            public PersonalTime(PersonalTime copy)
            {
                this.Name = copy.Name;
                this.Times = new List<TimeToTime>();
                this.PersonColor = copy.PersonColor;

                for (int i = 0; i < copy.Times.Count; i++)
                {
                    this.Times.Add(copy.Times[i]);
                }
            }

            /// <summary>
            /// Drawing Time table blook on panel.
            /// </summary>
            /// <param name="box"> Will draw panel. </param>
            /// <param name="graphics"> Graphic interface for draw. </param>
            /// <param name="n"> Line number of all lines. </param>
            /// <param name="weight"> All lines number able to max draw. </param>
            public void Draw(PictureBox box, Graphics graphics, int n, int weight)
            {
                const int START = 8, TERM = 13;
                const float FONT_SIZE = 6.5f;
                int width = box.Width;
                int height = box.Height;

                // View times are am 8 ~ pm 8
                SizeF one_blook = new SizeF(width / 6.75f, height / (TERM + 0.75f));

                Func<DayOfWeek, int> day_to_int = (x) => 
                {
                    switch(x)
                    {
                        case DayOfWeek.Monday: return 0;
                        case DayOfWeek.Tuesday: return 1;
                        case DayOfWeek.Wednesday: return 2;
                        case DayOfWeek.Thursday: return 3;
                        case DayOfWeek.Friday: return 4;
                    }
                    return -1;
                };
                Pen pen = new Pen(this.PersonColor.ToColor(), 7.0f);
                
                for (int i = 0; i < this.Times.Count; i++)
                {
                    TimeToTime time = this.Times[i];

                    Point start = new Point
                    (
                        (int)(day_to_int(time.DayOfWeek) * one_blook.Width
                            + one_blook.Width * (n + 1) / (weight + 1) + one_blook.Width * 0.75),
                        (int)((time.StartTime.Hour - START) * one_blook.Height 
                            + time.StartTime.Minute * one_blook.Height / 60 + one_blook.Height * 0.75)
                    );
                    Point end = new Point
                    (
                        (int)(day_to_int(time.DayOfWeek) * one_blook.Width
                            + one_blook.Width * (n + 1) / (weight + 1) + one_blook.Width * 0.75),
                        (int)((time.StartTime.Hour + time.Term.Hour - START) * one_blook.Height 
                            + (time.StartTime.Minute + time.Term.Minute) * one_blook.Height / 60 
                            + one_blook.Height * 0.75)
                    );

                    // circled on now time
                    Point now_time = new Point
                    (
                        (int)((day_to_int(DateTime.Now.DayOfWeek) + 0.5) * one_blook.Width) - (int)(one_blook.Width / 2 
                            - one_blook.Width * 0.75),
                        Math.Min(Math.Max((int)((DateTime.Now.Hour - START) * one_blook.Height
                            + DateTime.Now.Minute * one_blook.Height / 60 + one_blook.Height * 0.75) - 2, 0), box.Height - 2)
                    );

                    Debug.WriteLine(start.ToString() + " " + end.ToString());
                    graphics.DrawLine(pen, start, end);
                    graphics.DrawEllipse(new Pen(Color.DarkGray, 10f), new Rectangle(now_time, new Size((int)one_blook.Width, 4)));

                    graphics.DrawString(" " + Name + "\r\n " + time.StartTime.ToString(),
                        new Font("굴림체", FONT_SIZE), new SolidBrush(this.PersonColor.ToColor()), start);
                    graphics.DrawString(" " + new TimeToTime.HourMinute(time.StartTime.Hour + time.Term.Hour + (time.StartTime.Minute + time.Term.Minute) / 60,
                        (time.StartTime.Minute + time.Term.Minute) % 60).ToString(),
                        new Font("굴림체", FONT_SIZE), new SolidBrush(this.PersonColor.ToColor()), new Point(end.X, end.Y - (int)FONT_SIZE * 2));
                } 
            }

            /// <summary>
            /// Json Serializing by this personal time table.
            /// </summary>
            public void Serializing()
            {
                string json = JsonSerializer.Serialize<PersonalTime>(this);

                StreamWriter writer = new StreamWriter("tableset." + this.Name + ".json");
                writer.Write(json);
                writer.Close();
            }

            /// <summary>
            /// Json Serializing by this personal time table.
            /// </summary>
            public static List<PersonalTime> Deserializing()
            {
                List<PersonalTime> result = new List<PersonalTime>(); 
                string[] files = Directory.GetFiles(Environment.CurrentDirectory);
                 
                for (int i = 0; i < files.Length; i++)
                {
                    string[] file_name = files[i].Split('\\')[files[i].Split('\\').Length - 1].Split('.');
                    
                    if (file_name.Length == 3 && file_name[0] == "tableset" && file_name[2] == "json")
                    {
                        StreamReader reader = new StreamReader("tableset." + file_name[1] + ".json");
                        result.Add(JsonSerializer.Deserialize<PersonalTime>(reader.ReadToEnd())!);
                        reader.Close();
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Time tables by persons.
        /// </summary>
        public List<PersonalTime> AllTables { get; private set; }
        /// <summary>
        /// Checked viewing time talbes by persons.
        /// </summary>
        public List<PersonalTime> ViewTables { get; private set; }
        /// <summary>
        /// Horizontal containel for checked list.
        /// </summary>
        public SplitContainer Horizontal { get; private set; }
        /// <summary>
        /// View checked list of PersonalTime's list.
        /// </summary>
        public CheckedListBox CheckedList { get; private set; }
        /// <summary>
        /// Time table viewing sheet.
        /// </summary>
        public PictureBox TableBox { get; private set; }
        /// <summary>
        /// Table box's grapihic.
        /// </summary>
        private Graphics graphic;

        /// <summary>
        /// Sharing time table constructor using size.
        /// </summary>
        /// <param name="width"> width of panel </param>
        /// <param name="height">height of panel </param>
        public TimeTable(int width, int height)
        {
            this.Size = new Size(width, height);
            this.IsSplitterFixed = true;
            this.SplitterDistance = 800;
            this.AllTables = PersonalTime.Deserializing();
            this.Horizontal = new SplitContainer()
            {
                Parent = this.Panel2,
                Visible = true,
                Size = this.Panel2.ClientSize,
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
            };
            this.Horizontal.SplitterDistance = 500;
            this.CheckedList = new CheckedListBox()
            {
                Parent = this.Horizontal.Panel1,
                Visible = true,
                Size = this.Horizontal.Panel1.Size,
                Dock = DockStyle.Fill,
                CheckOnClick = true,
            };
            this.ViewTables = new List<PersonalTime>();
            this.TableBox = new PictureBox()
            {
                Location = new Point(5, 5),
                Parent = this.Panel1,
                Visible = true,
                Size = this.Panel1.ClientSize - new Size(10, 10),
                Dock = DockStyle.Fill,
                BackgroundImage = Resource.Table,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            this.graphic = this.TableBox.CreateGraphics();

            for (int i = 0; i < this.AllTables.Count; i++)
            {
                this.CheckedList.Items.Add(this.AllTables[i].Name);
            }

            // check box checking or doesn't, redraw timetable
            this.CheckedList.ItemCheck += (sender, e) =>
            {
                this.graphic.DrawImage(Resource.Table, 0, 0, this.TableBox.Width, this.TableBox.Height);

                if (e.CurrentValue == CheckState.Unchecked)
                {
                    this.ViewTables.Add(this.AllTables[e.Index]);
                }
                else
                {
                    this.ViewTables.Remove(this.AllTables[e.Index]);
                }

                for (int i = 0; i < this.ViewTables.Count; i++)
                {
                    PersonalTime table = this.ViewTables[i];
                    table.Draw(this.TableBox, this.graphic, i, this.ViewTables.Count);
                }

                if (this.CheckedList.SelectedItems.Count != 0)
                {
                    this.Horizontal.Panel2.Controls.Clear();
                }
            };
            this.CheckedList.DoubleClick += (sender, e) =>
            {
                if (this.CheckedList.CheckedItems.Count == 0)
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    strip.Items.Add("새 시간표");
                    strip.Items.Add("수정");
                    strip.Items.Add("복사");
                    this.CheckedList.SetItemCheckState(this.CheckedList.SelectedIndex, CheckState.Checked);

                    strip.Items[0].Click += (sender, e) =>
                    {
                        PersonalTime personal = new PersonalTime();

                        for (int i = 0; i < this.CheckedList.Items.Count; i++)
                        {
                            this.CheckedList.SetItemChecked(i, false);
                        }

                        SplitterPanel panel = this.Horizontal.Panel2;

                        RichTextBox name = new RichTextBox()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6 - 35,
                            Height = 35,
                            Location = new Point(3, 3),
                            Multiline = false,
                        };
                        name.TextChanged += (s, e) =>
                        {
                            personal.Name = name.Text;
                        };

                        Button color = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = 35,
                            Height = 35,
                            Location = new Point(3 + name.Width, 3),
                            BackColor = Color.Gray,
                        };
                        color.Click += (s, e) =>
                        {
                            ColorDialog dialog = new ColorDialog();
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                color.BackColor = dialog.Color;
                                personal.PersonColor = new PersonalTime.OColor()
                                {
                                    R = color.BackColor.R,
                                    G = color.BackColor.G,
                                    B = color.BackColor.B,
                                };
                            }
                        };

                        EditGrid grid = new EditGrid()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = panel.Height - name.Height - name.Location.Y - 9 - 35,
                            Location = new Point(3, name.Height + name.Location.Y + 6),
                        };
                        grid.CellValueChanged += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };
                        grid.RowsRemoved += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };

                        Button save = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = 35,
                            Location = new Point(3, grid.Height + grid.Location.Y + 3),
                            Text = "저장"
                        };
                        save.Click += (s, e) =>
                        {
                            this.AllTables.Add(personal);
                            this.CheckedList.Items.Add(personal.Name);
                            personal.Serializing();

                            this.Horizontal.Panel2.Controls.Clear();
                        };
                    };
                    strip.Items[1].Click += (sender, e) =>
                    {
                        int index = this.CheckedList.SelectedIndex;
                        PersonalTime personal = this.AllTables.Find(x =>
                        {
                            return x.Name == this.AllTables[index].Name;
                        })!;

                        SplitterPanel panel = this.Horizontal.Panel2;

                        RichTextBox name = new RichTextBox()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6 - 35,
                            Height = 35,
                            Location = new Point(3, 3),
                            Text = personal.Name,
                            Multiline = false,
                        };
                        name.TextChanged += (s, e) =>
                        {
                            personal.Name = name.Text;
                        };

                        Button color = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = 35,
                            Height = 35,
                            Location = new Point(3 + name.Width, 3),
                            BackColor = Color.FromArgb(personal.PersonColor.R, personal.PersonColor.G, personal.PersonColor.B),
                        };
                        color.Click += (s, e) =>
                        {
                            ColorDialog dialog = new ColorDialog();
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                color.BackColor = dialog.Color;
                                personal.PersonColor = new PersonalTime.OColor()
                                {
                                    R = color.BackColor.R,
                                    G = color.BackColor.G,
                                    B = color.BackColor.B,
                                };
                            }
                        };

                        EditGrid grid = new EditGrid()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = panel.Height - name.Height - name.Location.Y - 9 - 35,
                            Location = new Point(3, name.Height + name.Location.Y + 6),
                        };
                        for (int i = 0; i < personal.Times.Count; i++)
                        {
                            grid.Rows.Add(personal.Times[i].StartTime, personal.Times[i].Term,
                                personal.Times[i].DayOfWeek);
                        }
                        grid.CellValueChanged += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };
                        grid.RowsRemoved += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };

                        Button save = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = 35,
                            Location = new Point(3, grid.Height + grid.Location.Y + 3),
                            Text = "저장"
                        };
                        save.Click += (s, e) =>
                        {
                            this.CheckedList.Items[index] = personal.Name;
                            personal.Serializing();

                            this.Horizontal.Panel2.Controls.Clear();
                        };
                    };
                    strip.Items[2].Click += (sender, e) =>
                    {
                        int index = this.CheckedList.SelectedIndex;
                        PersonalTime personal = new PersonalTime(this.AllTables.Find(x =>
                        {
                            return x.Name == this.AllTables[index].Name;
                        })!);
                        personal.Name += "(C)";

                        SplitterPanel panel = this.Horizontal.Panel2;

                        RichTextBox name = new RichTextBox()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6 - 35,
                            Height = 35,
                            Location = new Point(3, 3),
                            Text = personal.Name,
                            Multiline = false,
                        };
                        name.TextChanged += (s, e) =>
                        {
                            personal.Name = name.Text;
                        };

                        Button color = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = 35,
                            Height = 35,
                            Location = new Point(3 + name.Width, 3),
                            BackColor = Color.FromArgb(personal.PersonColor.R, personal.PersonColor.G, personal.PersonColor.B),
                        };
                        color.Click += (s, e) =>
                        {
                            ColorDialog dialog = new ColorDialog();
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                color.BackColor = dialog.Color;
                                personal.PersonColor = new PersonalTime.OColor()
                                {
                                    R = color.BackColor.R,
                                    G = color.BackColor.G,
                                    B = color.BackColor.B,
                                };
                            }
                        };

                        EditGrid grid = new EditGrid()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = panel.Height - name.Height - name.Location.Y - 9 - 35,
                            Location = new Point(3, name.Height + name.Location.Y + 6),
                        };
                        for (int i = 0; i < personal.Times.Count; i++)
                        {
                            grid.Rows.Add(personal.Times[i].StartTime, personal.Times[i].Term,
                                personal.Times[i].DayOfWeek);
                        }
                        grid.CellValueChanged += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };
                        grid.RowsRemoved += (s, e) =>
                        {
                            personal.Times.Clear();

                            for (int i = 0; i < grid.Rows.Count; i++)
                            {
                                if (grid.Rows[i].Cells[0].Value != null && grid.Rows[i].Cells[1].Value != null
                                    && grid.Rows[i].Cells[2].Value != null)
                                {
                                    personal.Times.Add(new PersonalTime.TimeToTime()
                                    {
                                        DayOfWeek = (DayOfWeek)grid.Rows[i].Cells[2].Value,
                                        StartTime = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[0].Value.ToString()!),
                                        Term = new PersonalTime.TimeToTime.HourMinute(
                                            grid.Rows[i].Cells[1].Value.ToString()!),
                                    });
                                }
                            }
                        };

                        Button save = new Button()
                        {
                            Parent = panel,
                            Visible = true,
                            Width = panel.Width - 6,
                            Height = 35,
                            Location = new Point(3, grid.Height + grid.Location.Y + 3),
                            Text = "저장"
                        };
                        save.Click += (s, e) =>
                        {
                            this.AllTables.Add(personal);
                            this.CheckedList.Items.Add(personal.Name);
                            personal.Serializing();

                            this.Horizontal.Panel2.Controls.Clear();
                        };
                    };
                    strip.Show(Cursor.Position);
                }
            };
        }
    }
}
