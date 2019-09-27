using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureS : Figure
    {
        public FigureS(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(
               new GameObjectParametres()
               {
                   Color = this.Color,
                   CoordX = _startCoordX + _size,
                   CoordY = _startCoordY,
                   Height = _size,
                   Width = _size
               },
               _canvas);

            _cube2 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + 2 * _size,
                    CoordY = _startCoordY,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube3 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube4 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + _size,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube4.CoordY + _size + _size <= _canvas.ClientHeight
                && _canvasField.FindValueByCoords(_cube4.CoordX + _size, _cube4.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX + _size, _cube4.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX, _cube4.CoordY - _size) == 0)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX + _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - _size;
            }
            else if (_cube3.CoordY < _cube4.CoordY
                && _cube4.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.FindValueByCoords(_cube4.CoordX + _size, _cube4.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX - _size, _cube4.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX + _size, _cube4.CoordY) == 0)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY + _size;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX + _size;
                _cube3.CoordY = coordY;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube4.CoordY - _size >= Constant.YOffset
                && _canvasField.FindValueByCoords(_cube4.CoordX - _size, _cube4.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX - _size, _cube4.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX, _cube4.CoordY + _size) == 0)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX - _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + _size;
            }
            else if (_cube3.CoordY > _cube4.CoordY
                && _cube4.CoordY + _size + _size <= _canvas.ClientWidth
                && _canvasField.FindValueByCoords(_cube4.CoordX, _cube4.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX + _size, _cube4.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube4.CoordX - _size, _cube4.CoordY) == 0)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY - _size;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX - _size;
                _cube3.CoordY = coordY;
            }
        }
    }
}
