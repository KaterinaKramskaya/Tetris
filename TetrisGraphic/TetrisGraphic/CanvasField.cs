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
        private Cube[,] _canvasField;
        private int _clientHeight;
        private int _clientWidth;

        public Cube[,] CanvasFieldArray
        {
            get
            {
                Cube[,] canvasField = _canvasField;
                return canvasField;
            }
        }

        public CanvasField(int сlientHeight, int сlientWidth)
        {
            _clientHeight = сlientHeight;
            _clientWidth = сlientWidth;

            CreateCanvasField();
        }

        public void FillCanvasField(Cube cube)
        {
            if (cube != null && cube.CoordY >= Constant.YOffset && cube.CoordX <= _clientWidth)
            {
                _canvasField[cube.CoordY, cube.CoordX] = cube;
            }
        }

        public void CreateCanvasField()
        {
            _canvasField = new Cube[_clientHeight, _clientWidth];

            for (int i = 0; i < _canvasField.GetLength(0); i += Constant.Size)
            {
                for (int j = 0; j < _canvasField.GetLength(1); j += Constant.Size)
                {
                    _canvasField[i, j] = null;
                }
            }
        }
    }
}

