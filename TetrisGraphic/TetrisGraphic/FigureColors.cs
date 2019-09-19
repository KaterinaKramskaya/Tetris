using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureColors
    {
        private uint _red = 0xFFf74853,
        _orange = 0xFFf28a2e,
        _yellow = 0xFFf0c92e,
        _lightGreen = 0xFF16de45,
        _darkGreen = 0xFF239600,
        _turquoise = 0xFF19d49f,
        _blue = 0xFF14d2e3;

        public uint RandomColor(Random rnd)
        {
            int colorNumber = rnd.Next(7);
            switch (colorNumber)
            {
                case 0:
                    return _red;
                case 1:
                    return _orange;
                case 2:
                    return _yellow;
                case 3:
                    return _lightGreen;
                case 4:
                    return _darkGreen;
                case 5:
                    return _turquoise;
                case 6:
                default:
                    return _blue;
            }
        }
    }
}
