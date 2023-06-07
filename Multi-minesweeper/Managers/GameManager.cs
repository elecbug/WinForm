using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMinesweeper.Managers
{
    internal static class GameManager
    {
        internal static void EndGame()
        {
            MessageBox.Show("Win");
        }

        internal static void LoseGame()
        {
            MessageBox.Show("Lose");
        }
    }
}
