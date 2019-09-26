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
            if (_coordY + _height >= _canvas.ClientHeight)
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

        public MovingType HitWithFigureResult(CanvasField canvasField)
        {
            if (_coordX >= _width && _coordX <= _canvas.ClientWidth - _width && _coordY >= 0
                && _coordY <= _canvas.ClientHeight - _height)
            {
                if (canvasField.FindValueByCoords(_coordX, _coordY + _height) != 0)
                {
                    return MovingType.Stop;
                }
                if (canvasField.FindValueByCoords(_coordX + _width, _coordY + _height) != 0)
                {
                    return MovingType.OnlyLeft;
                }
                if (canvasField.FindValueByCoords(_coordX - _width, _coordY + _height) != 0)
                {
                    return MovingType.OnlyRight;
                }
            }
            return MovingType.WithoutLimits;
        }
    }
}
