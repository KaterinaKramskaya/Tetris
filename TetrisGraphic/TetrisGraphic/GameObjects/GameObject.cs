using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class GameObject
    {
        protected int _size = Constant.Size;

        protected GameObjectParametres _parametres;

        public int CoordX { get { return _parametres.CoordX; } set { _parametres.CoordX = value; } }
        public int CoordY { get { return _parametres.CoordY; } set { _parametres.CoordY = value; } }

        public uint Color { get => _parametres.Color; set => _parametres.Color = value; }

        public int Width => _parametres.Width;
        public int Height => _parametres.Height;

        protected GameObject()
        {
        }

        protected GameObject(GameObjectParametres parametres)
        {
            _parametres.Color = parametres.Color;
            _parametres.Height = parametres.Height;
            _parametres.Width = parametres.Width;
            _parametres.CoordX = parametres.CoordX;
            _parametres.CoordY = parametres.CoordY;
        }

        public virtual void Update()
        {
        }

        public virtual void Update(MovingType movingType)
        {
        }

        public virtual void Render(ConsoleGraphics graphic)
        {
            graphic.FillRectangle(Color, CoordX, CoordY, Width, Height);
        }

        public bool IsInObject(int x, int y) =>
            x >= _parametres.CoordX
            && x <= _parametres.CoordX + _parametres.Width
            && y >= _parametres.CoordY && y <= _parametres.CoordY + _parametres.Height;
    }
}
