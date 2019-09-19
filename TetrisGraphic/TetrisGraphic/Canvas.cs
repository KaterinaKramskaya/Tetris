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
        public int ClientHeigth { get { return _height + 2 * Constant.Size + Constant.XOffset; } }
        public int ClientWidth { get { return _width + Constant.YOffset; } }

        public Canvas(uint color, int height, int width, int coordX, int coordY) : base(color, height, width, coordX, coordY)
        {
            _height = height - Constant.YOffset;
            _width = width - Constant.XOffset;
        }

        public override void Render(ConsoleGraphics graphic)
        {
            base.Render(graphic);
        }
    }
}
