using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    class Grid : GameObject
    {
        public Grid(uint color, int height, int width, int coordX, int coordY) : base(color, height, width, coordX, coordY)
        {
        }

        public override void Render(ConsoleGraphics graphic)
        {
            for (int i = _coordX; i <= _width; i += _size)
            { 
                graphic.DrawLine(_color, i, _coordY, i, _height);
            }
            for (int i = _coordY; i <= _height; i += _size)
            {
                graphic.DrawLine(_color, _coordX, i, _width, i);
            }
        }
    }
}
