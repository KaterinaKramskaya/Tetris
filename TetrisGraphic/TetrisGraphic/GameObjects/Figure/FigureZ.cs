using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureZ : Figure
    {
        public FigureZ(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
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
                  CoordX = _startCoordX,
                  CoordY = _startCoordY,
                  Height = _size,
                  Width = _size
              },
              _canvas);

            _cube2 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + _size,
                    CoordY = _startCoordY,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube3 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + _size,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube4 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + 2 * _size,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX)
            {
                _baseX = _cube2.CoordX;
                _baseY = _cube2.CoordY;

                _newXCube1 = _baseX;
                _newYCube1 = _baseY - _size;

                _newXCube2 = _baseX;
                _newYCube2 = _baseY;

                _newXCube3 = _baseX - _size;
                _newYCube3 = _baseY;

                _newXCube4 = _baseX - _size;
                _newYCube4 = _baseY + _size;

                base.Rotate();
            }
            else if (_cube1.CoordY < _cube2.CoordY)
            {
                _baseX = _cube2.CoordX;
                _baseY = _cube2.CoordY;

                _newXCube1 = _baseX + _size;
                _newYCube1 = _baseY;

                _newXCube2 = _baseX;
                _newYCube2 = _baseY;

                _newXCube3 = _baseX;
                _newYCube3 = _baseY - _size;

                _newXCube4 = _baseX - _size;
                _newYCube4 = _baseY - _size;

                base.Rotate();
            }
            else if (_cube1.CoordX > _cube2.CoordX)
            {
                _baseX = _cube2.CoordX;
                _baseY = _cube2.CoordY;

                _newXCube1 = _baseX;
                _newYCube1 = _baseY + _size;

                _newXCube2 = _baseX;
                _newYCube2 = _baseY;

                _newXCube3 = _baseX + _size;
                _newYCube3 = _baseY;

                _newXCube4 = _baseX + _size;
                _newYCube4 = _baseY - _size;

                base.Rotate();
            }
            else if (_cube1.CoordY > _cube2.CoordY)
            {
                _baseX = _cube2.CoordX;
                _baseY = _cube2.CoordY;

                _newXCube1 = _baseX - _size;
                _newYCube1 = _baseY;

                _newXCube2 = _baseX;
                _newYCube2 = _baseY;

                _newXCube3 = _baseX;
                _newYCube3 = _baseY + _size;

                _newXCube4 = _baseX + _size;
                _newYCube4 = _baseY + _size;

                base.Rotate();
            }
        }
    }
}
