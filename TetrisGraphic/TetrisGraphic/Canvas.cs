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

        public Canvas(GameObjectParametres parametres) 
            : base(parametres)
        {
            int _height = Height - Constant.YOffset;
            int _width = Width - Constant.XOffset;

            _clientHeight = _height + 2 * Constant.Size + Constant.XOffset;
            _clientWidth = _width + Constant.YOffset;
        }
    }
}
