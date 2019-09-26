﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class FigureI : Figure
    {
        public FigureI(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 3 * _size, _startCoordY, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,] { { _cube1, _cube2, _cube3, _cube4 } };
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube1.CoordY + (3 * _size) + _size <= _canvas.ClientHeight
                && _cube1.CoordX + (3 * _size) <= _canvas.ClientWidth
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY + (2 * _size)) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY + (3 * _size)) == 0


                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY + _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY + 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY + 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY + 2 * _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY + 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY + 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY + 3 * _size) == 0)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + 2 * _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY + 3 * _size;
            }
            else if (_cube1.CoordY < _cube2.CoordY
                && _cube1.CoordX - 3 * _size >= Constant.XOffset
                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY) == 0


                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY + _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY + _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY + 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY + 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY + 2 * _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY + 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY + 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY + 3 * _size) == 0)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX - 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX - 3 * _size;
                _cube4.CoordY = coordY;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube1.CoordY - 3 * _size >= Constant.YOffset
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY - 1 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY - 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX, _cube1.CoordY - 3 * _size) == 0


                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY - _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY - 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY - 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY - 2 * _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX - _size, _cube1.CoordY - 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 2 * _size, _cube1.CoordY - 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX - 3 * _size, _cube1.CoordY - 3 * _size) == 0)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY - 1 * _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - 2 * _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY - 3 * _size;
            }
            else if (_cube1.CoordY > _cube2.CoordY
                && _cube1.CoordX + 3 * _size + _size <= _canvas.ClientWidth
                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY) == 0


                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY - _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY - _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY - 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY - 2 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY - 2 * _size) == 0

                && _canvasField.FindValueByCoords(_cube1.CoordX + _size, _cube1.CoordY - 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 2 * _size, _cube1.CoordY - 3 * _size) == 0
                && _canvasField.FindValueByCoords(_cube1.CoordX + 3 * _size, _cube1.CoordY - 3 * _size) == 0)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX + 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX + 3 * _size;
                _cube4.CoordY = coordY;
            }
        }
    }
}
