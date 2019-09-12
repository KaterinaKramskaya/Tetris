using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    public abstract class GraphicObject
    {
        protected uint _color;
        protected int _width;
        protected int _heigth;

        protected int _coordX;
        protected int _coordY;

        public virtual void Update()
        {
        }
        public virtual void Update(int movingType)
        {

        }

        public virtual void Render(ConsoleGraphics graphic)
        {
            graphic.FillRectangle(_color, _coordX, _coordY, _width, _heigth);
        }
    }
}
