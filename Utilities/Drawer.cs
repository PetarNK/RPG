using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Utilities
{
    public class Drawer
    {
        public static void DrawGameField()
        {
            char[,] field = new char[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    field[y, x] = '▒';
                }
            }


            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(field[y, x]);
                }
                Console.WriteLine();
            }
        }
    }
}
