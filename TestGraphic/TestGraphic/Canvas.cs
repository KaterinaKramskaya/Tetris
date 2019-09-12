using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    public class Canvas : GraphicObject
    {
        private int _size;

        public int ClientHeidth { get { return _heigth; } }
        public int ClientWidth { get { return _width; } }

        public Canvas(uint color, int clientHeight, int clientWidth, int size)
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
            base.Render(graphic);
            //boards

            LineDrawer.DrawVerticalLine(0xFFff0000, graphic, _coordX, _heigth+1);
            LineDrawer.DrawVerticalLine(0xFFff0000, graphic, _width, _heigth);

            LineDrawer.DrawHorizontalLine(0xFFff0000, graphic, _coordY, _width);
            LineDrawer.DrawHorizontalLine(0xFFff0000, graphic, _heigth-1, _width);
        }
    }
}
