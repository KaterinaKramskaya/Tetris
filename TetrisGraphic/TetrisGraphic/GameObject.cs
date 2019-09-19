using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public abstract class GameObject
    {
        protected int _size = Constant.Size;

        protected uint _color;
        protected int _width;
        protected int _height;

        protected int _coordX;
        protected int _coordY;

        public GameObject()
        {

        }

        public GameObject(uint color, int height, int width, int coordX, int coordY)
        {
            _color = color;
            _height = height;
            _width = width;
            _coordX = coordX;
            _coordY = coordY;
        }

        public virtual void Update()
        {
        }
        public virtual void Update(MovingType movingType)
        {
        }

        public virtual void Render(ConsoleGraphics graphic)
        {
            graphic.FillRectangle(_color, _coordX, _coordY, _width, _height);
        }

        public bool IsInObject(int x, int y) => x >= _coordX && x <= _coordX + _width && y >= _coordY && y <= _coordY + _height;
    }
}
