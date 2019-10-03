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

        public Cube(GameObjectParametres parametres, Canvas canvas) : base(parametres)
        {
            _canvas = canvas;
        }

        public override void Update()
        {
            CoordY += _size;
        }

        public override void Update(MovingType movingType)
        {
            if (movingType == MovingType.WithoutLimits)
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    CoordX -= _size;
                }

                else if (Input.IsKeyDown(Keys.RIGHT))
                {
                    CoordX += _size;
                }
            }
            else if (movingType == MovingType.OnlyLeft)
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    CoordX -= _size;
                }
            }
            else if (movingType == MovingType.OnlyRight)
            {
                if (Input.IsKeyDown(Keys.RIGHT))
                {
                    CoordX += _size;
                }
            }

             CoordY += _size;
        }

        public MovingType HitWithCanvasResult()
        {
            if (CoordY + _size >= _canvas.ClientHeight)
            {
                return MovingType.Stop;
            }
            if (CoordX + _size >= _canvas.ClientWidth)
            {
                return MovingType.OnlyLeft;
            }
            if (CoordX <= Constant.XOffset)
            {
                return MovingType.OnlyRight;
            }
            return MovingType.WithoutLimits;
        }

        public MovingType HitWithFigureResult(CanvasField canvasField)
        {
            if (CoordX >= 0 && CoordX <= _canvas.ClientWidth - _size && CoordY >= 0
                && CoordY <= _canvas.ClientHeight - _size)
            {
                if (canvasField.FindValueByCoords(CoordX, CoordY + _size) != 0)
                {
                    return MovingType.Stop;
                }
                if (canvasField.FindValueByCoords(CoordX + _size, CoordY + _size) != 0)
                {
                    return MovingType.OnlyLeft;
                }
                if (canvasField.FindValueByCoords(CoordX - _size, CoordY + _size) != 0)
                {
                    return MovingType.OnlyRight;
                }
            }
            return MovingType.WithoutLimits;
        }
    }
}
