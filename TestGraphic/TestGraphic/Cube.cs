using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    public class Cube : GraphicObject
    {
        private readonly Canvas _canvas;

        public Cube(uint color, int size, int x, int y, Canvas canvas)
        {
            _color = color;

            _heigth = size;
            _width = size;

            _coordX = x;
            _coordY = y;

            _canvas = canvas;
        }

        public override void Update(int movingType)
        {
            _coordY += _heigth;

            if (movingType == 3) 
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    _coordX -= _width;
                }
                else if (Input.IsKeyDown(Keys.RIGHT))
                {
                    _coordX += _width;
                }
                else if (Input.IsKeyDown(Keys.DOWN))
                {
                    if(!(_coordY + 2* _heigth >= _canvas.ClientHeidth))
                    _coordY += _heigth;
                }
            }
            else if (movingType == 1)
            {
                if (Input.IsKeyDown(Keys.LEFT))
                {
                    _coordX -= _width;
                }
                else if (Input.IsKeyDown(Keys.DOWN))
                {
                    if (!(_coordY + 2 * _heigth >= _canvas.ClientHeidth))
                        _coordY += _heigth;
                }
            }
            else if (movingType == 2)
            {
                if (Input.IsKeyDown(Keys.RIGHT))
                {
                    _coordX += _width;
                }
                else if (Input.IsKeyDown(Keys.DOWN))
                {
                    if (!(_coordY + 2 * _heigth >= _canvas.ClientHeidth))
                        _coordY += _heigth;
                }
            }
        }

        public int UpdateResult()
        {
            if (_coordY + _heigth >= _canvas.ClientHeidth) // cube hit with bottom board and should to stop
            {
                return 0;
            }
            else if (_coordX + _width >= _canvas.ClientWidth) // cube hit with right board 
            {
                return 1;
            }
            else if (_coordX <= 0) // cube hit with left board
            {
                return 2;
            }
            return 3;
        }
    }
}
