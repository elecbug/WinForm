using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private int size_x, size_y, hide_num;
        private MapData[,] map_data;
        private bool[,] map_maybe;
        private bool[,] map_click;
        private Button[,] map_buttons;
        private readonly int HIDE = -1;
        private int time_secs;
        private bool not_click_yet;
        private DateTime game_start;

        public MainForm()
        {
            InitializeComponent();
        }

        public DateTime GameStart { get => game_start; set => game_start = value; }

        public int SizeX { get => size_x; set => size_x = value; }
        public int SizeY { get => size_y; set => size_y = value; }
        public int Bombs { get => hide_num; set => hide_num = value; }

        private void Load_Minesweeper(object sender, EventArgs e)
        {
            DataManager.main = this;

            game_start = DateTime.Now;

            new DB().ReadLine();

            Set_map_data();

            ResizeEnd_MainForm(this, e);
        }

        private void FormClosed_MainForm(object sender, FormClosedEventArgs e)
        {
            new DB().WriteLine(false, false);
        }

        private void Click_menu_new(object sender, EventArgs e)
        {
            Set_map_data();
        }

        private void Click_menu_rule(object sender, EventArgs e)
        {
            RuleForm set = new RuleForm();
            set.Show();
        }

        private void Click_menu_log_rank(object sender, EventArgs e)
        {
            List<Rank> ranks = new DB().Create_RankList();

            for (int i = 0; i < ranks.Count; i++)
            {
                if ((!ranks[i].IsWin)
                   || ranks[i].Width != size_x
                   || ranks[i].Height != size_y
                   || ranks[i].Bombs != hide_num)
                {
                    ranks.RemoveAt(i--);
                }
            }

            ranks.Sort((x, y) => x.Sec.CompareTo(y.Sec));

            label_ranks.Text = $"Rank: ({size_x}, {size_y}) {hide_num} bombs\r\n";
            for (int i = 0; i < Math.Min(20, ranks.Count); i++)
            {
                label_ranks.Text += $"{i+1,2}: {To_String(ranks[i].Sec)} on {ranks[i].Day}\r\n";
            } 
        }

        private void Click_menu_log_playtime(object sender, EventArgs e)
        {
            label_playtime.Text = $"Erenow: {To_String(new DB().GetPlayTime())}";
        }

        private void Click_map_buttons(object sender, MouseEventArgs e)
        {
            int x = 0, y = 0;
            for (x = 0; x < size_x; x++)
            {
                for (y = 0; y < size_y; y++)
                {
                    if ((Button)sender == map_buttons[x, y])
                    {
                        goto Break;
                    }
                }
            }

        Break:
            if (e.Button == MouseButtons.Left && !map_maybe[x, y])
            {
                Click_mouse(x, y);
            }
            else if (e.Button == MouseButtons.Right && !map_click[x, y])
            {
                if (!map_maybe[x, y])
                {
                    map_maybe[x, y] = true;
                    map_buttons[x, y].Text = "B";
                    map_buttons[x, y].BackColor = Color.Yellow;
                }
                else
                {
                    map_maybe[x, y] = false;
                    map_buttons[x, y].Text = "";
                    map_buttons[x, y].BackColor = Color.White;
                }
            }

            Cheak_Clear();
        }

        private void Click_mouse(int x, int y)
        {
            if (map_data[x, y].Data == HIDE)
            {
                if (not_click_yet)
                {
                    Set_map_data();

                    Click_mouse(x, y);

                    return;
                }

                new DB().WriteLine(false, true, time_secs);

                Show_Hide();
                timer.Stop();

                MessageBox.Show("You touched bomb.\nTry again?", "Oops...", MessageBoxButtons.OK);

                Set_map_data();
            }
            else if (map_data[x, y].Data == 0)
            {
                not_click_yet = false;

                Erase_Empty(x, y);
            }
            else
            {
                not_click_yet = false;

                map_buttons[x, y].Text = map_data[x, y].Data.ToString();
                map_buttons[x, y].BackColor = Color.YellowGreen;
                map_click[x, y] = true;
            }
        }

        private bool Is_EqualArray(int[,] a, int[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int Is_Hide(int x, int y)
        {
            if (map_data[x, y].Data == HIDE)
            {
                return 1;
            }
            else
            {
                return 0;
            } 
        }

        private int Get_NearHide(int x, int y)
        {
            bool N = y != 0, S = y != size_y - 1, E = x != size_x - 1, W = x != 0;
            int sum = 0;

            if (!N)
            {
                if (!E)
                {
                    sum = Is_Hide(x - 1, y) + Is_Hide(x - 1, y + 1) + Is_Hide(x, y + 1);
                }
                else if (!W)
                {
                    sum = Is_Hide(x + 1, y) + Is_Hide(x + 1, y + 1) + Is_Hide(x, y + 1);
                }
                else
                {
                    sum = Is_Hide(x - 1, y) + Is_Hide(x - 1, y + 1) + Is_Hide(x, y + 1) + Is_Hide(x + 1, y) + Is_Hide(x + 1, y + 1);
                }
            }
            else if (!E)
            {
                if (!S)
                {
                    sum = Is_Hide(x, y - 1) + Is_Hide(x - 1, y - 1) + Is_Hide(x - 1, y);
                }
                else
                {
                    sum = Is_Hide(x, y - 1) + Is_Hide(x - 1, y - 1) + Is_Hide(x - 1, y) + Is_Hide(x - 1, y + 1) + Is_Hide(x, y + 1);
                }
            }
            else if (!W)
            {
                if (!S)
                {
                    sum = Is_Hide(x, y - 1) + Is_Hide(x + 1, y - 1) + Is_Hide(x + 1, y);
                }
                else
                {
                    sum = Is_Hide(x, y - 1) + Is_Hide(x + 1, y - 1) + Is_Hide(x + 1, y) + Is_Hide(x + 1, y + 1) + Is_Hide(x, y + 1);
                }
            }
            else if (!S)
            {
                sum = Is_Hide(x - 1, y) + Is_Hide(x - 1, y - 1) + Is_Hide(x, y - 1) + Is_Hide(x + 1, y - 1) + Is_Hide(x + 1, y);
            }
            else
            {
                sum = Is_Hide(x, y - 1) + Is_Hide(x + 1, y - 1) + Is_Hide(x + 1, y) + Is_Hide(x + 1, y + 1)
                    + Is_Hide(x, y + 1) + Is_Hide(x - 1, y + 1) + Is_Hide(x - 1, y) + Is_Hide(x - 1, y - 1);
            }
            return sum;
        }

        private void Set_map_buttons()
        {
            int button_size = this.panel_gameboard.Width / size_x;

            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    map_buttons[x, y] = new Button
                    {
                        Parent = this.panel_gameboard,
                        Width = button_size,
                        Height = button_size,
                        Location = new Point(button_size * x, button_size * y),
                        Text = "",
                        BackColor = Color.White
                    };
                    map_buttons[x, y].MouseDown += Click_map_buttons;
                    map_buttons[x, y].Show();
                }
            }
        }

        private void Set_map_data()
        {
            this.panel_gameboard.Controls.Clear();

            time_secs = 0;
            not_click_yet = true;

            map_buttons = new Button[size_x, size_y];
            Set_map_buttons();

            map_data = new MapData[size_x, size_y];
            map_maybe = new bool[size_x, size_y];
            map_click = new bool[size_x, size_y];

            List<MapData> list_map = new List<MapData>();

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    map_data[x, y] = new MapData();
                    list_map.Add(map_data[x, y]);
                }
            }

            int hide_point = (int)DateTime.UtcNow.ToBinary();
            for (int i = 0; i < hide_num; i++)
            {
                hide_point = new Random(hide_point).Next(0, list_map.Count);

                list_map[hide_point].Data = HIDE;
                list_map.RemoveAt(hide_point);
            }

            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    if (map_data[x, y].Data != HIDE)
                    {
                        map_data[x, y].Data = Get_NearHide(x, y);
                    }
                    string str = map_data[x, y].Data.ToString();
                    if (str[0] != '-')
                    {
                        str = $"+{map_data[x, y].Data} ";
                    }
                    else
                    {
                        str += " ";
                    }
                    Debug.Write(str);
                }
                Debug.Write("\n");
            }

            timer.Start();
        }

        public void Set_Values(int width, int height, int bombs)
        {
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    map_buttons[x, y].Hide();
                }
            }

            size_x = width;
            size_y = height;
            hide_num = bombs;

            Set_map_data();
        }

        private void Show_Hide()
        {
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    if (map_data[x, y].Data == HIDE)
                    {
                        map_buttons[x, y].Text = "B";
                        map_buttons[x, y].BackColor = Color.Red;
                    }
                }
            }
        }

        private string To_String(int time_secs)
        {
            if (time_secs < 60)
            {
                return $"{time_secs} s";
            }
            else if (time_secs / 60 < 60)
            {
                return $"{time_secs / 60}m {time_secs % 60}s";
            }
            else
            {
                return $"{time_secs / 60 / 60}h {time_secs / 60 % 60}m {time_secs % 60}s";
            }
        }

        private void Tick_timer(object sender, EventArgs e)
        {
            label_timer.Text = $"Time: {To_String(time_secs++)}...";
        }

        private void ResizeEnd_MainForm(object sender, EventArgs e)
        {
            this.panel_gameboard.Location = new Point(10, this.menu.Height + 5);
            this.panel_gameboard.Size
                = new Size(this.ClientSize.Width - 20, this.ClientSize.Width - 20);
            this.label_timer.Location
                = new Point(10, this.panel_gameboard.Location.Y + this.panel_gameboard.Height + 5);
            this.label_playtime.Location
                = new Point(10, this.label_timer.Location.Y + this.label_timer.Height);
            this.label_ranks.Location
                = new Point(this.ClientSize.Width / 2, this.label_timer.Location.Y);

            int button_size = this.panel_gameboard.Width / this.size_x;
            for (int x = 0; x < this.size_x; x++)
            {
                for (int y = 0; y < this.size_y; y++)
                {
                    this.map_buttons[x, y].Location = new Point(x * button_size, y * button_size);
                    this.map_buttons[x, y].Size = new Size(button_size, button_size);
                }
            }
        }

        private void Cheak_Clear()
        {
            bool clear = true;

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    if (!((!map_click[x, y] && map_data[x, y].Data == HIDE) || (map_click[x, y] && map_data[x, y].Data != HIDE)))
                    {
                        clear = false;
                        goto Next;
                    }
                }
            }

        Next:
            if (clear)
            {
                new DB().WriteLine(true, false, time_secs);

                timer.Stop();
                MessageBox.Show("You didn't touch bomb.\nTry again?", "Wow!", MessageBoxButtons.OK);

                Set_map_data();
            }
        }

        private void Erase_Empty(int x, int y)
        {
            const int CHANGE_EMPTY = 2, CHANGE_NUMBER = 4, POSSIBLE_EMPTY = 1, POSSIBLE_NUMBER = 3, DEFAULT = 0;
            int[,] field = new int[size_x, size_y];
            int[,] field_past = (int[,])field.Clone();

            for (int ix = 0; ix < size_x; ix++)
            {
                for (int iy = 0; iy < size_y; iy++)
                {
                    if (map_data[ix, iy].Data == 0)
                    {
                        field[ix, iy] = POSSIBLE_EMPTY;
                    }
                }
            }

            field[x, y] = POSSIBLE_EMPTY;

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == POSSIBLE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == POSSIBLE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == POSSIBLE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == POSSIBLE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == POSSIBLE_EMPTY))
                    {
                        if (field[w, h] == DEFAULT)
                        {
                            field[w, h] = POSSIBLE_NUMBER;
                        }
                    }
                }
            }

            field[x, y] = CHANGE_EMPTY;

        ReTry:
            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == CHANGE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == CHANGE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == CHANGE_EMPTY))
                    {
                        if (field[w, h] == POSSIBLE_EMPTY)
                        {
                            field[w, h] = CHANGE_EMPTY;
                        }
                    }
                }
            }

            if (!Is_EqualArray(field_past, field))
            {
                field_past = (int[,])field.Clone();
                goto ReTry;
            }

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == CHANGE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == CHANGE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == CHANGE_EMPTY))
                    {
                        if (field[w, h] == POSSIBLE_NUMBER)
                        {
                            field[w, h] = CHANGE_NUMBER;
                        }
                    }
                }
            }

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if (field[w, h] == CHANGE_EMPTY || field[w, h] == CHANGE_NUMBER)
                    {
                        if (map_data[w, h].Data != 0)
                        {
                            map_buttons[w, h].Text = map_data[w, h].Data.ToString();
                        }
                        map_buttons[w, h].BackColor = Color.YellowGreen;
                        map_click[w, h] = true;
                    }
                }
            }
        }
    }
}
