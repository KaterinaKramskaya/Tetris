using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    public static class LineDrawer
    {
        public static void DrawVerticalLine(uint color, ConsoleGraphics graphic, int x, int length)
        {
            graphic.DrawLine(color, x, 0, x, length);
        }

        public static void DrawHorizontalLine(uint color, ConsoleGraphics graphic, int y, int length)
        {
            graphic.DrawLine(color, 0, y, length, y);
        }
    }
}
