using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureJLeft : Figure
    {
        public FigureJLeft(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {_cube1, _cube2, _cube3, },
                                      { null, null, _cube4 },};
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube1.CoordY + (2 * _size) + _size <= _canvas.ClientHeight
                && _cube1.CoordX - (2 * _size) >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (2 * _size), _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (2 * _size), _cube1.CoordX - _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + 2 * _size;

                _cube4.CoordX = coordX - _size;
                _cube4.CoordY = coordY + 2 * _size;
            }
            else if (_cube3.CoordX > _cube4.CoordX
                && _cube1.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX - 2 * _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX - 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX - 2 * _size;
                _cube4.CoordY = coordY - _size;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube1.CoordY - 2 * _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX + _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - 2 * _size;

                _cube4.CoordX = coordX + _size;
                _cube4.CoordY = coordY - 2 * _size;
            }
            else if (_cube3.CoordX < _cube4.CoordX
                && _cube1.CoordY + (2 * _size) + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX + 2 * _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX + 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX + 2 * _size;
                _cube4.CoordY = coordY + _size;
            }
        }
    }
}
