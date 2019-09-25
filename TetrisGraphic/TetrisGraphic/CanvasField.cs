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

        public Cube[,] CanvasFieldArray { get { return _canvasField; } }

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
        }
    }
}

