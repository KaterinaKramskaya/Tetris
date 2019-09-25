using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class Canvas : GameObject
    {
        private int _clientHeight;
        private int _clientWidth;

        public int ClientHeight { get { return _clientHeight; } }
        public int ClientWidth { get { return _clientWidth; } }

        public Canvas(uint color, int height, int width, int coordX, int coordY) : base(color, height, width, coordX, coordY)
        {
            _height = height - Constant.YOffset;
            _width = width - Constant.XOffset;

            _clientHeight = _height + 2 * Constant.Size + Constant.XOffset;
            _clientWidth = _width + Constant.YOffset;
        }
    }
}
