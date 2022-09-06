using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Rank
    {
        private bool is_win;
        private string day;
        private int width, height, bombs;
        private int sec;

        public Rank(string[] data)
        {
            if (data[0] == "W") is_win = true;
            else if (data[0] == "L") is_win = false;
            day = data[1];
            width = int.Parse(data[2]);
            height = int.Parse(data[3]);
            bombs = int.Parse(data[4]);
            sec = int.Parse(data[5]);
        }

        public bool IsWin { get => is_win; set => is_win = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Bombs { get => bombs; set => bombs = value; }
        public int Sec { get => sec; set => sec = value; }
        public string Day { get => day; set => day = value; }
    }
}
