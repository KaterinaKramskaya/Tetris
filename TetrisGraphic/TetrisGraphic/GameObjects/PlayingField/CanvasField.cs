using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;

namespace TetrisGraphic
{
    public class CanvasField
    {
        private FieldCell[,] _canvasField;
        private int _height;
        private int _width;

        private int X = Constant.XOffset;
        private int Y = Constant.YOffset + 2 * Constant.Size;


        public FieldCell[,] CanvasFieldArray { get { return _canvasField; } }

        public CanvasField()
        {
            _height = Constant.CellCount;
            _width = Constant.CellCount;

            CreateCanvasField();
        }

        public void FillCanvasField(Cube cube)
        {
            if (cube != null)
            {
                for (int i = 0; i < _height; i++, Y += Constant.Size)
                {
                    for (int j = 0; j < _width; j++, X += Constant.Size)
                    {
                        if (_canvasField[i, j].coordX == cube.CoordX && _canvasField[i, j].coordY == cube.CoordY)
                        {
                            _canvasField[i, j].value = 1;
                            break;
                        }
                    }
                    X = Constant.XOffset;
                }
            }
        }

        public void CreateCanvasField()
        {
            _canvasField = new FieldCell[_height, _width]; 

            for (int i = 0; i < _height; i++, Y += Constant.Size)
            {
                for (int j = 0; j < _width; j++, X += Constant.Size)
                {
                    _canvasField[i, j].coordX = X;
                    _canvasField[i, j].coordY = Y;
                }
                X = Constant.XOffset;
            }
        }

        public int FindValueByCoords(int coordX, int coordY)
        {
            int value = 0;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_canvasField[i, j].coordX == coordX && _canvasField[i, j].coordY == coordY)
                    {
                        value = _canvasField[i, j].value;
                        break;
                    }
                }
                X = Constant.XOffset;
            }

            return value;
        }

        public int LineToClear()
        {
            int lineToClear = -1;

            for (int i = 0; i < _height; i++)
            {
                int result = 0;
                for (int j = 0; j < _width; j++)
                {
                    if (_canvasField[i, j].value != 0)
                    {
                        result += 1;
                    }
                }
                if (result == Constant.CellCount)
                {
                    return lineToClear = _canvasField[i, 0].coordY;
                }
                X = Constant.XOffset;
            }

            return lineToClear;
        }

        public void ClearLines(int lineToClear)
        {
            for (int i = 0; i < _height; i++)
            {
                if (_canvasField[i, 0].coordY <= lineToClear)
                {
                    for (int j = 0; j < _width; j++)
                    {
                        _canvasField[i, j].value = 0;
                    }
                }
                else
                {
                    break;
                }
                X = Constant.XOffset;
            }
        }
    }
}