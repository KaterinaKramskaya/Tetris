using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class Cube : GameObject
    {
        private readonly Canvas _canvas;

        public uint Color => _color;

        public int CoordX { get { return _coordX; } set { _coordX = value; } }
        public int CoordY { get { return _coordY; } set { _coordY = value; } }

        public int Width => _width;
        public int Height => _height;

        public Cube(uint color, int coordX, int coordY, Canvas canvas) : base(color, Constant.Size, Constant.Size, coordX, coordY)
        {
            _canvas = canvas;
        }

        public override void Update()
        {
            _coordY += _height;
        }

        public override void Update(MovingType movingType)
        {
            if (movingType == MovingType.WithoutLimits)
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    _coordX -= _width;
                }

                else if (Input.IsKeyDown(Keys.RIGHT))
                {
                    _coordX += _width;
                }
            }
            else if (movingType == MovingType.OnlyLeft)
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    _coordX -= _width;
                }
            }
            else if (movingType == MovingType.OnlyRight)
            {
                if (Input.IsKeyDown(Keys.RIGHT))
                {
                    _coordX += _width;
                }
            }

            _coordY += _height;
        }

        public MovingType HitWithCanvasResult()
        {
            if (_coordY + _height >= _canvas.ClientHeigth)
            {
                return MovingType.Stop;
            }
            if (_coordX + _width >= _canvas.ClientWidth)
            {
                return MovingType.OnlyLeft;
            }
            if (_coordX <= Constant.XOffset)
            {
                return MovingType.OnlyRight;
            }
            return MovingType.WithoutLimits;
        }

        public MovingType HitWithFigureResult(Cube[,] canvasField)
        {
            if (_coordX >= _width && _coordX <= canvasField.GetLength(1) - _width && _coordY >= 0
                && _coordY <= canvasField.GetLength(0) - _height)
            {
                if (canvasField[_coordY + _height, _coordX] != null)
                {
                    return MovingType.Stop;
                }
                if (canvasField[_coordY + _height, _coordX + _width] != null)
                {
                    return MovingType.OnlyLeft;
                }
                if (canvasField[_coordY + _height, _coordX - _width] != null)
                {
                    return MovingType.OnlyRight;
                }
            }
            return MovingType.WithoutLimits;
        }
    }
}
