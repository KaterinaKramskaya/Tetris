using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public static class FigureColors
    {
        public static uint RandomColor(Random rnd)
        {
            int colorNumber = rnd.Next(7);
            switch (colorNumber)
            {
                case 0:
                    return Constant.Red;
                case 1:
                    return Constant.Orange;
                case 2:
                    return Constant.Yellow;
                case 3:
                    return Constant.LightGreen;
                case 4:
                    return Constant.DarkGreen;
                case 5:
                    return Constant.Turquoise;
                case 6:
                default:
                    return Constant.Blue;
            }
        }
    }
}
