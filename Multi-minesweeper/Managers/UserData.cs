using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMinesweeper.Managers
{
    internal class UserData
    {
        public string Name { get; set; } = "";
        public int ID { get; set; } = -1;
        public Color Color { get; set; } = Color.White;

        public UserData(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
            this.ID = new Random(DateTime.Now.Millisecond).Next(0, int.MaxValue);
        }

        public override string ToString() => this.Name + "#" + this.ID;
    }
}
