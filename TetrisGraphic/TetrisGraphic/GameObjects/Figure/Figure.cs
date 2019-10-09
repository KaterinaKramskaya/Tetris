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

        protected Cube[] _figure;


        public Cube[] FigureArray
        {
            get
            {
                Cube[] figure = _figure;
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

        // for Rotate()
        protected int[] _newXCoords;
        protected int[] _newYCoords;

        protected int _baseX;
        protected int _baseY;

        protected int _newXCube1;
        protected int _newYCube1;

        protected int _newXCube2;
        protected int _newYCube2;

        protected int _newXCube3;
        protected int _newYCube3;

        protected int _newXCube4;
        protected int _newYCube4;

        protected bool _allCellsEmpty;
        protected bool _hitWithCanvas;
        protected bool _figureCanRotate;


        public Figure(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY)
        {
            _startCoordX = startCoordX;
            _startCoordY = startCoordY;

            Color = color;

            _canvasField = canvasField;
            _canvas = canvas;
        }

        public virtual void ResetCoords()
        {
            ClearCubesArray();

            _startCoordX = 3 * Constant.Size + Constant.XOffset;
            _startCoordY = 2 * Constant.Size + Constant.YOffset;

            SetCubesCoords();
            CreateCubesArray();
        }

        public abstract void SetCubesCoords();

        public void ClearCubesArray()
        {
            for (int i = 0; i < _figure.Length; i++)
            {
                _figure[i] = null;

            }
        }

        public Cube[] CreateCubesArray()
        {
            return _figure = new Cube[] { _cube1, _cube2, _cube3, _cube4 };
        }

        public virtual void Rotate()
        {
            _newXCoords = new int[] { _newXCube1, _newXCube2, _newXCube3, _newXCube4 };
            _newYCoords = new int[] { _newYCube1, _newYCube2, _newYCube3, _newYCube4 };

            _allCellsEmpty = true;

            for (int i = 0; i < _newXCoords.Length; i++)
            {
                if (_canvasField.FindValueByCoords(_newXCoords[i], _newYCoords[i]) != 0)
                {
                    _allCellsEmpty = false;
                    break;
                }
            }

            _hitWithCanvas = true;
            if (_newXCoords.Min() >= Constant.XOffset
                && _newXCoords.Max() + _size <= _canvas.ClientWidth
                && _newYCoords.Min() >= Constant.XOffset + 3 * Constant.Size
                && _newYCoords.Max() + _size <= _canvas.ClientHeight)
            {
                _hitWithCanvas = false;
            }

            _figureCanRotate = _allCellsEmpty && !_hitWithCanvas;
            if (_figureCanRotate)
            {
                _cube1.CoordX = _newXCube1;
                _cube1.CoordY = _newYCube1;

                _cube2.CoordX = _newXCube2;
                _cube2.CoordY = _newYCube2;

                _cube3.CoordX = _newXCube3;
                _cube3.CoordY = _newYCube3;

                _cube4.CoordX = _newXCube4;
                _cube4.CoordY = _newYCube4;
            }
        }

        public bool FigureCantUpdate()
        {
            return HitBottomBoard();
        }

        public override void Update()
        {
            MovingType movingType = MovingTypeResult();
            bool shouldToStop = HitBottomBoard();

            for (int i = 0; i < _figure.Length; i++)
            {
                if (_figure[i] != null && !shouldToStop)
                {
                    if (movingType == MovingType.OnlyDown)
                    {
                        _figure[i].Update();
                    }
                    else
                    {
                        _figure[i].Update(movingType);
                        FigureStoppedMove = false;
                    }
                }
                else if (shouldToStop)
                {
                    FigureStoppedMove = true;
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
            for (int i = 0; i < _figure.Length; i++)
            {
                if (_figure[i] != null)
                {
                    _figure[i].Render(graphic);
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
            for (int i = 0; i < _figure.Length; i++)
            {
                if (_figure[i] != null && _figure[i].CoordY == lineToClear)
                {
                    _figure[i] = null;
                }
            }
        }

        protected bool HitBottomBoard()
        {
            bool result = false;

            foreach (Cube cube in _figure)
            {
                if (cube != null)
                {
                    if (cube.HitWithCanvasResult() == MovingType.Stop ||
                        cube.HitWithFigureResult(_canvasField) == MovingType.Stop)
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
                        || cube.HitWithFigureResult(_canvasField) == MovingType.OnlyRight)
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
                        || cube.HitWithFigureResult(_canvasField) == MovingType.OnlyLeft)
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
                    if ((HitRightBoard() && cube.HitWithFigureResult(_canvasField) == MovingType.OnlyRight)
                        || (HitLeftBoard() && cube.HitWithFigureResult(_canvasField) == MovingType.OnlyLeft))
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
