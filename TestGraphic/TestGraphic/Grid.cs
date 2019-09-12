using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    class Grid : GraphicObject
    {
        private int _size;

        public Grid(uint color, int clientHeight, int clientWidth, int size)
        {
            _size = size;

            _color = color;
            _heigth = clientHeight;
            _width = clientWidth;

            _coordX = 0;
            _coordY = 0;
        }

        public override void Render(ConsoleGraphics graphic)
        {
            for (int i = _coordX; i <= _width; i += _size)
            {
                LineDrawer.DrawVerticalLine(_color, graphic, i, _heigth);

            }
            for (int i = _coordY; i <= _heigth; i += _size)
            {
                LineDrawer.DrawHorizontalLine(_color, graphic, i, _width);
            }
        }
    }
}
