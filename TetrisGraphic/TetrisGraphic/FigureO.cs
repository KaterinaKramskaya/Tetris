using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureO : Figure
    {
        public FigureO(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {_cube1, _cube2 },
                                  { _cube3, _cube4 },};
        }

        public override void Rotate()
        {
            // do nothing
        }
    }
}
