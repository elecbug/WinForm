using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMinesweeper.Controls
{
    internal class FieldPanel : Panel
    {
        public FieldButton[,] Buttons { get; set; }
        public int WidthCount { get; private set; }
        public int HeightCount { get; private set; }
        public int BombsCount { get; private set; }
        public bool FirstClick { get; set; }

        public FieldPanel(int width_count, int height_count, int bombs_count)
        {
            this.Buttons = new FieldButton[width_count, height_count];
            this.WidthCount = width_count;
            this.HeightCount = height_count;
            this.BombsCount = bombs_count;
            this.FirstClick = true;
        }

        public void SetField()
        {
            Size button_size = new Size(this.Width / this.WidthCount, this.Height / this.HeightCount);

            for (int x = 0; x < this.WidthCount; x++)
            {
                for (int y = 0; y < this.HeightCount; y++)
                {
                    this.Buttons[x, y] = new FieldButton(this, x, y) 
                    {
                        Visible = true,
                        Location = new Point(x * button_size.Width, y * button_size.Height),
                        Size = button_size,
                    };
                    this.Controls.Add(this.Buttons[x, y]);
                }
            }
        }

        private enum Data { Possible, Impossibe, Bombs }
        public void SetBombs(Point start_position)
        {
            
            Data[,] data = new Data[this.WidthCount, this.HeightCount]; //Basicaly Possible

            for (int x = Math.Max(start_position.X - 1, 0); x < Math.Min(start_position.X + 1 + 1, this.WidthCount); x++)
            {
                for (int y = Math.Max(start_position.Y - 1, 0); y < Math.Min(start_position.Y + 1 + 1, this.HeightCount); y++)
                {
                    data[x, y] = Data.Impossibe;
                }
            }

            int now_bombs = 0;

            while (now_bombs != this.BombsCount)
            {
                Point position = new Point(new Random(DateTime.Now.Millisecond * 22).Next(0, this.WidthCount),
                    new Random(DateTime.Now.Millisecond * 33).Next(0, this.HeightCount));

                if (data[position.X, position.Y] == Data.Possible)
                {
                    data[position.X, position.Y] = Data.Bombs;
                    this.Buttons[position.X, position.Y].SetBomb();
                    
                    now_bombs++;
                }
            }

            this.FirstClick = false;
        }

        public bool CheckWin()
        {
            int non_click_count = 0;
            
            for (int x = 0; x < this.WidthCount; x++)
            {
                for (int y = 0; y < this.HeightCount; y++)
                {
                    if ((this.Buttons[x, y].ButtonState & FieldButton.State.NonClicked) == FieldButton.State.NonClicked)
                    {
                        non_click_count++;
                    }
                }
            }

            if (non_click_count == this.BombsCount)
                return true;
            else
                return false;
        }
    }
}
