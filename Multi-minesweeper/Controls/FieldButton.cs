using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMinesweeper.Managers;

namespace MultiMinesweeper.Controls
{
    internal class FieldButton : Label
    {
        internal enum State 
        { 
            None = 0, 
            NonClicked = 0b00001, 
            Clicked = 0b00010, 
            Bomb = 0b00100, 
            Empty = 0b01000, 
            Protected = 0b10000 
        }
        internal readonly struct ButtonColor
        {
            public static Color NonClicked { get; } = Color.Green;
            public static Color ClickedEmpty { get; } = Color.GreenYellow;
            public static Color ClickedBomb { get; } = Color.Red;
            public static Color ClickedNearBomb { get; } = Color.YellowGreen;
            public static Color ProtectedButton { get; } = Color.DarkGray;
        }

        public int NearBombs { get; set; }
        public State ButtonState { get; set; }
        public Point Position { get; set; }
        public new FieldPanel Parent { get; set; } 

        public FieldButton(FieldPanel parent, int x, int y)
        {
            this.MouseClick += FieldButtonClick;

            this.BackColor = ButtonColor.NonClicked;
            this.NearBombs = 0;
            this.Parent = parent;
            this.Position = new Point(x, y);
            this.ButtonState = State.NonClicked | State.Empty;
            this.Font = new Font("Arial", this.Height / 2, FontStyle.Bold);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetBomb()
        {
            this.ButtonState = State.NonClicked | State.Bomb;

            int max_width = this.Parent.Buttons.GetLength(0);
            int max_height = this.Parent.Buttons.GetLength(1);

            for (int x = Math.Max(this.Position.X - 1, 0); x < Math.Min(this.Position.X + 1 + 1, max_width); x++)
            {
                for (int y = Math.Max(this.Position.Y - 1, 0); y < Math.Min(this.Position.Y + 1 + 1, max_height); y++)
                {
                    if (this.Parent.Buttons[x, y].ButtonState == (State.NonClicked | State.Empty))
                    {
                        this.Parent.Buttons[x, y].NearBombs++;
                    }
                }
            }
        }

        private void FieldButtonClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Parent.FirstClick)
                {
                    this.Parent.SetBombs(this.Position);
                }

                if (this.ButtonState == (State.NonClicked | State.Bomb))
                {
                    this.BackColor = ButtonColor.ClickedBomb;
                    this.ButtonState = State.Clicked | State.Bomb;

                    GameManager.LoseGame();
                }
                else if (this.ButtonState == (State.NonClicked | State.Empty) && this.NearBombs == 0)
                {
                    this.BackColor = ButtonColor.ClickedEmpty;
                    this.ButtonState = State.Clicked | State.Empty;
                    this.BorderStyle = BorderStyle.None;

                    int max_width = this.Parent.Buttons.GetLength(0);
                    int max_height = this.Parent.Buttons.GetLength(1);

                    for (int x = Math.Max(this.Position.X - 1, 0); x < Math.Min(this.Position.X + 1 + 1, max_width); x++)
                    {
                        for (int y = Math.Max(this.Position.Y - 1, 0); y < Math.Min(this.Position.Y + 1 + 1, max_height); y++)
                        {
                            if (this.Parent.Buttons[x, y].ButtonState == (State.NonClicked | State.Empty))
                            {
                                this.Parent.Buttons[x, y].FieldButtonClick(this.Parent.Buttons[x, y], e);
                            }
                        }
                    }
                }
                else if (this.ButtonState == (State.NonClicked | State.Empty))
                {
                    this.BackColor = ButtonColor.ClickedNearBomb;
                    this.ButtonState = State.Clicked | State.Empty;
                    this.BorderStyle = BorderStyle.None;

                    this.Text = $"{this.NearBombs}";
                }

                if (this.Parent.CheckWin())
                {
                    GameManager.EndGame();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if ((this.ButtonState & State.NonClicked) == State.NonClicked 
                    && (this.ButtonState & State.Protected) == State.None)
                {
                    this.ButtonState |= State.Protected;
                    this.BackColor = ButtonColor.ProtectedButton;
                } 
                else if ((this.ButtonState & State.Protected) == State.Protected)
                {
                    this.ButtonState &= ~State.Protected;
                    this.BackColor = ButtonColor.NonClicked;
                }
            }
        }
    }
}
