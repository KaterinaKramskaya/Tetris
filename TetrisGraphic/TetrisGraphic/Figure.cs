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

        public int StartCoordX
        {
            get
            {
                return _startCoordX;
            }
            set
            {
                _startCoordX = value;
            }
        }
        public int StartCoordY
        {
            get
            {
                return _startCoordY;
            }
            set
            {
                _startCoordY = value;
            }
        }

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
            StartCoordX = 3 * Constant.Size + Constant.XOffset;
            StartCoordY = Constant.Size + Constant.YOffset;
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

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube1.CoordY + (3 * _size) + _size <= _canvas.ClientHeigth
                && _cube1.CoordX + (3 * _size) <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (2 * _size), _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (3 * _size), _cube1.CoordX] == null


                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX + 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX + 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX + 3 * _size] == null)

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
                && (_canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - 3 * _size] == null


                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX - 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 2 * _size, _cube1.CoordX - 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + 3 * _size, _cube1.CoordX - 3 * _size] == null))
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
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 1 * _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX] == null


                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX - 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX - 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX - 3 * _size] == null)
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
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + 3 * _size] == null


                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX + 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX + 3 * _size] == null

                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 3 * _size, _cube1.CoordX + 3 * _size] == null)
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

    public class FigureJLeft : Figure
    {
        public FigureJLeft(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {_cube1, _cube2, _cube3, },
                                      { null, null, _cube4 },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube1.CoordY + (2 * _size) + _size <= _canvas.ClientHeigth
                && _cube1.CoordX - (2 * _size) >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (2 * _size), _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + (2 * _size), _cube1.CoordX - _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + 2 * _size;

                _cube4.CoordX = coordX - _size;
                _cube4.CoordY = coordY + 2 * _size;
            }
            else if (_cube3.CoordX > _cube4.CoordX
                && _cube1.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX - 2 * _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX - 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX - 2 * _size;
                _cube4.CoordY = coordY - _size;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube1.CoordY - 2 * _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube1.CoordY - _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY - 2 * _size, _cube1.CoordX + _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - 2 * _size;

                _cube4.CoordX = coordX + _size;
                _cube4.CoordY = coordY - 2 * _size;
            }
            else if (_cube3.CoordX < _cube4.CoordX
                && _cube1.CoordY + (2 * _size) + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY, _cube1.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube1.CoordY + _size, _cube1.CoordX + 2 * _size] == null)
            {
                int coordX = _cube1.CoordX;
                int coordY = _cube1.CoordY;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY;

                _cube3.CoordX = coordX + 2 * _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX + 2 * _size;
                _cube4.CoordY = coordY + _size;
            }
        }
    }

    public class FigureJRight : Figure
    {
        public FigureJRight(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX, _startCoordY + _size, _canvas);
            _cube3 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {null, null, _cube1, },
                                    { _cube2, _cube3, _cube4 },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube2.CoordX < _cube3.CoordX
                && _cube2.CoordY + (2 * _size) + _size <= _canvas.ClientHeigth
                && _cube1.CoordX - (2 * _size) >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY + 2 * _size, _cube2.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY + (2 * _size), _cube2.CoordX] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX + _size;
                _cube1.CoordY = coordY + 2 * _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY + 2 * _size;
            }
            else if (_cube1.CoordX > _cube4.CoordX
                && _cube2.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX - 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX - 2 * _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX - 2 * _size;
                _cube1.CoordY = coordY + _size;

                _cube3.CoordX = coordX - _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX - 2 * _size;
                _cube4.CoordY = coordY;
            }
            else if (_cube2.CoordX > _cube3.CoordX
                && _cube2.CoordY - 2 * _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY - 2 * _size, _cube2.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY - 2 * _size, _cube2.CoordX] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX - _size;
                _cube1.CoordY = coordY - 2 * _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY - 2 * _size;
            }
            else if (_cube1.CoordX < _cube4.CoordX
                && _cube2.CoordX + (2 * _size) + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX + 2 * _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX + 2 * _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX + 2 * _size;
                _cube1.CoordY = coordY - _size;

                _cube3.CoordX = coordX + _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX + 2 * _size;
                _cube4.CoordY = coordY;
            }
        }
    }

    public class FigureO : Figure
    {
        public FigureO(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {_cube1, _cube2 },
                                  { _cube3, _cube4 },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            // do nothing
        }
    }

    public class FigureS : Figure
    {
        public FigureS(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {null, _cube1, _cube2, },
                                { _cube3, _cube4, null },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube4.CoordY + _size + _size <= _canvas.ClientHeigth
                && _canvasField.CanvasFieldArray[_cube4.CoordY, _cube4.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY + _size, _cube4.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY - _size, _cube4.CoordX] == null)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX + _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - _size;
            }
            else if (_cube3.CoordY < _cube4.CoordY
                && _cube4.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube4.CoordY + _size, _cube4.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY + _size, _cube4.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY, _cube4.CoordX + _size] == null)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY + _size;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY + _size;

                _cube3.CoordX = coordX + _size;
                _cube3.CoordY = coordY;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube4.CoordY - _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube4.CoordY, _cube4.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY - _size, _cube4.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY + _size, _cube4.CoordX] == null)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX - _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + _size;
            }
            else if (_cube3.CoordY > _cube4.CoordY
                && _cube4.CoordY + _size + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube4.CoordY - _size, _cube4.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY - _size, _cube4.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube4.CoordY, _cube4.CoordX - _size] == null)
            {
                int coordX = _cube4.CoordX;
                int coordY = _cube4.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY - _size;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY - _size;

                _cube3.CoordX = coordX - _size;
                _cube3.CoordY = coordY;
            }
        }
    }

    public class FigureT : Figure
    {
        public FigureT(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX, _startCoordY + _size, _canvas);
            _cube3 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ {null, _cube1, null, },
                                { _cube2, _cube3, _cube4 },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube2.CoordX < _cube3.CoordX
                && _cube3.CoordY + _size + _size <= _canvas.ClientHeigth
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY - _size, _cube3.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY + _size, _cube3.CoordX] == null)
            {
                int coordX = _cube3.CoordX;
                int coordY = _cube3.CoordY;

                _cube1.CoordX = coordX + _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY - _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY + _size;
            }
            else if (_cube2.CoordY < _cube3.CoordY
                && _cube3.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube3.CoordY + _size, _cube3.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX - _size] == null)
            {
                int coordX = _cube3.CoordX;
                int coordY = _cube3.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY + _size;

                _cube2.CoordX = coordX + _size;
                _cube2.CoordY = coordY;

                _cube4.CoordX = coordX - _size;
                _cube4.CoordY = coordY;
            }
            else if (_cube2.CoordX > _cube3.CoordX
                && _cube3.CoordY - _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY + _size, _cube3.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY - _size, _cube3.CoordX] == null)
            {
                int coordX = _cube3.CoordX;
                int coordY = _cube3.CoordY;

                _cube1.CoordX = coordX - _size;
                _cube1.CoordY = coordY;

                _cube2.CoordX = coordX;
                _cube2.CoordY = coordY + _size;

                _cube4.CoordX = coordX;
                _cube4.CoordY = coordY - _size;
            }
            else if (_cube2.CoordY > _cube3.CoordY
                && _cube3.CoordY + _size + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube3.CoordY - _size, _cube3.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube3.CoordY, _cube3.CoordX + _size] == null)
            {
                int coordX = _cube3.CoordX;
                int coordY = _cube3.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY - _size;

                _cube2.CoordX = coordX - _size;
                _cube2.CoordY = coordY;

                _cube4.CoordX = coordX + _size;
                _cube4.CoordY = coordY;
            }
        }
    }

    public class FigureZ : Figure
    {
        public FigureZ(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        public override void SetCubesCoords()
        {
            _cube1 = new Cube(_color, _startCoordX, _startCoordY, _canvas);
            _cube2 = new Cube(_color, _startCoordX + _size, _startCoordY, _canvas);
            _cube3 = new Cube(_color, _startCoordX + _size, _startCoordY + _size, _canvas);
            _cube4 = new Cube(_color, _startCoordX + 2 * _size, _startCoordY + _size, _canvas);
        }

        public override Cube[,] CreateCubesArray()
        {
            return _figure = new Cube[,]{ { _cube1, _cube2, null },
                                     { null, _cube3, _cube4 },};
        }

        public override void ResetCoords()
        {
            ClearCubesArray();
            base.ResetCoords();
            SetCubesCoords();

            CreateCubesArray();
        }

        public override void Rotate()
        {
            if (_cube1.CoordX < _cube2.CoordX
                && _cube2.CoordY + _size + _size <= _canvas.ClientHeigth
                && _cube2.CoordY - _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX - _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY - _size;

                _cube3.CoordX = coordX - _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX - _size;
                _cube4.CoordY = coordY + _size;
            }
            else if (_cube1.CoordY < _cube2.CoordY
                && _cube2.CoordX - 2 * _size >= Constant.XOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX - _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX + _size;
                _cube1.CoordY = coordY;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY - _size;

                _cube4.CoordX = coordX - _size;
                _cube4.CoordY = coordY - _size;
            }
            else if (_cube1.CoordX > _cube2.CoordX
                && _cube2.CoordY - _size >= Constant.YOffset
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX + _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY - _size, _cube2.CoordX + _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX;
                _cube1.CoordY = coordY + _size;

                _cube3.CoordX = coordX + _size;
                _cube3.CoordY = coordY;

                _cube4.CoordX = coordX + _size;
                _cube4.CoordY = coordY - _size;
            }
            else if (_cube1.CoordY > _cube2.CoordY
                && _cube2.CoordY + _size + _size <= _canvas.ClientWidth
                && _canvasField.CanvasFieldArray[_cube2.CoordY, _cube2.CoordX - _size] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX] == null
                && _canvasField.CanvasFieldArray[_cube2.CoordY + _size, _cube2.CoordX + _size] == null)
            {
                int coordX = _cube2.CoordX;
                int coordY = _cube2.CoordY;

                _cube1.CoordX = coordX - _size;
                _cube1.CoordY = coordY;

                _cube3.CoordX = coordX;
                _cube3.CoordY = coordY + _size;

                _cube4.CoordX = coordX + _size;
                _cube4.CoordY = coordY + _size;
            }
        }
    }
}
