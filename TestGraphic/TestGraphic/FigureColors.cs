using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraphic
{
    public class FigureColors
    {
        Random rnd = new Random();

        private uint _color0 = 0xFFe39f9f,
        _color1 = 0xFFebbe96,
        _color2 = 0xFFf7e6a8,
        _color3 = 0xFFe7f7a8,
        _color4 = 0xFFa8f7c0,
        _color5 = 0xFFa8f7e1,
        _color6 = 0xFFc4efff,
        _color7 = 0xFFc0bdf0,
        _color8 = 0xFFeac5fc,
        _color9 = 0xFFffd4f3;

        public uint RandomColor()
        {
            int colorNumber = rnd.Next(10);
            switch (colorNumber)
            {
                case 0:
                    return _color0;
                case 1:
                    return _color1;
                case 2:
                    return _color2;
                case 3:
                    return _color3;
                case 4:
                    return _color4;
                case 5:
                    return _color5;
                case 6:
                    return _color6;
                case 7:
                    return _color7;
                case 8:
                    return _color8;
                case 9:
                default:
                    return _color9;
            }
        }
    }
}
