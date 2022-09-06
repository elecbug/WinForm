using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class MapData
    {
        private int data;

        public MapData() 
        {
            data = 0;
        }

        public int Data { get => data; set => data = value; }
    }
}
