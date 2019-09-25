using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;

namespace TetrisGraphic
{
    public abstract class Figure : GameObject
    {
        protected int _startCoordX;
        protected int _startCoordY;

        protected Cube[,] _figure;

        public Cube[,] FigureArray
        {
            get
            {
                Cube[,] figure = _figure;
                return figure;
            }
        }

        protected Cube _cube1;
        protected Cube _cube2;
        protected Cube _cube3;
        protected Cube _cube4;

        protected Canvas _canvas;
        protected CanvasField _canvasField;

        public bool FigureStoppedMove { get; private set; }

        public Figure(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY)
        {
            _startCoordX = startCoordX;
            _startCoordY = startCoordY;

            _color = color;
            _canvasField = canvasField;
            _canvas = canvas;
        }

        public virtual void ResetCoords()
        {
            ClearCubesArray();

            _startCoordX = 3 * Constant.Size + Constant.XOffset;
            _startCoordY = Constant.Size + Constant.YOffset;

            SetCubesCoords();
            CreateCubesArray();
        }

        public abstract void SetCubesCoords();

        public void ClearCubesArray()
        {
            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    _figure[i, j] = null;
                }
            }
        }

        public abstract Cube[,] CreateCubesArray();

        public abstract void Rotate();

        public bool FigureCantUpdate()
        {
            return HitBottomBoard();
        }

        public override void Update()
        {
            MovingType movingType = MovingTypeResult();
            bool shouldToStop = HitBottomBoard();

            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    if (_figure[i, j] != null && !shouldToStop)
                    {
                        if (movingType == MovingType.OnlyDown)
                        {
                            _figure[i, j].Update();
                        }
                        else
                        {
                            _figure[i, j].Update(movingType);
                            FigureStoppedMove = false;
                        }
                    }
                    else if (shouldToStop)
                    {
                        FigureStoppedMove = true;
                    }
                }
            }
        }

        public MovingType MovingTypeResult()
        {
            if (HitBottomBoard())
            {
                return MovingType.Stop;
            }
            else if (BetweenBoardAndFigure())
            {
                return MovingType.OnlyDown;
            }
            else if (HitRightBoard())
            {
                return MovingType.OnlyLeft;
            }
            else if (HitLeftBoard())
            {
                return MovingType.OnlyRight;
            }
            else
            {
                return MovingType.WithoutLimits;
            }
        }

        public override void Render(ConsoleGraphics graphic)
        {
            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    if (_figure[i, j] != null)
                    {
                        _figure[i, j].Render(graphic);
                    }
                }
            }
        }

        public void FillFigureFields()
        {
            foreach (Cube cube in _figure)
            {
                _canvasField.FillCanvasField(cube);
            }
        }

        public void ClearFigureLine(int lineToClear)
        {
            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    if (_figure[i, j] != null && _figure[i, j].CoordY == lineToClear)
                    {
                        _figure[i, j] = null;
                    }
                }
            }
            FillFigureFields();
        }

        protected bool HitBottomBoard()
        {
            bool result = false;

            foreach (Cube cube in _figure)
            {
                if (cube != null)
                {
                    if (cube.HitWithCanvasResult() == MovingType.Stop ||
                        cube.HitWithFigureResult(_canvasField.CanvasFieldArray) == MovingType.Stop)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        protected bool HitLeftBoard()
        {
            bool result = false;
            foreach (Cube cube in _figure)
            {
                if (cube != null)
                {
                    if (cube.HitWithCanvasResult() == MovingType.OnlyRight
                        || cube.HitWithFigureResult(_canvasField.CanvasFieldArray) == MovingType.OnlyRight)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        protected bool HitRightBoard()
        {
            bool result = false;

            foreach (Cube cube in _figure)
            {
                if (cube != null)
                {
                    if (cube.HitWithCanvasResult() == MovingType.OnlyLeft
                        || cube.HitWithFigureResult(_canvasField.CanvasFieldArray) == MovingType.OnlyLeft)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        protected bool BetweenBoardAndFigure()
        {
            bool result = false;

            foreach (Cube cube in _figure)
            {
                if (cube != null)
                {
                    if ((HitRightBoard() && cube.HitWithFigureResult(_canvasField.CanvasFieldArray) == MovingType.OnlyRight)
                        || (HitLeftBoard() && cube.HitWithFigureResult(_canvasField.CanvasFieldArray) == MovingType.OnlyLeft))
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
