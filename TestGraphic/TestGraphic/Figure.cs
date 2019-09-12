using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;

namespace TestGraphic
{
    public abstract class Figure : GraphicObject
    {
        protected const int size = 30;

        protected int startCoordX = 5 * size;
        protected int startCoordY = -2 * size;

        protected Cube[,,] _figure;

        protected Cube _cube1;
        protected Cube _cube2;
        protected Cube _cube3;
        protected Cube _cube4;

        protected MyList topCubes;
        protected MyList bottomCubes;
        protected MyList leftCubes;
        protected MyList rightCubes;

        protected Canvas _canvas;

        public override void Update()
        {
            int movingType = 3;
            bool shouldToStop = IsHitWithBottomBoard(_canvas);

            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                if (IsHitWithRightBoard(_canvas))
                {
                    movingType = 1;
                }
                else if (IsHitWithLeftBoard(_canvas))
                {
                    movingType = 2;
                }
                else
                {
                    movingType = 3;
                }

                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    for (int k = 0; k < _figure.GetLength(2); k++)
                    {
                        if (_figure[i, j, k] != null && !shouldToStop)
                        {
                            _figure[i, j, k].Update(movingType);
                        }
                    }
                }
            }
        }

        public override void Render(ConsoleGraphics graphic)
        {
            for (int i = 0; i < _figure.GetLength(0); i++)
            {
                for (int j = 0; j < _figure.GetLength(1); j++)
                {
                    for (int k = 0; k < _figure.GetLength(2); k++)
                    {
                        if (_figure[i, j, k] != null)
                            _figure[i, j, k].Render(graphic);
                        else
                        { }
                    }
                }
            }
        }

        protected bool IsHitWithBottomBoard(Canvas canvas)
        {
            bool result = false;

            for (int i = 0; i < bottomCubes.Count; i++)
            {
                if (bottomCubes[i] is Cube cube)
                {
                    if (cube.UpdateResult() == 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        protected bool IsHitWithLeftBoard(Canvas canvas)
        {
            bool result = false;

            for (int i = 0; i < leftCubes.Count; i++)
            {
                if (leftCubes[i] is Cube cube)
                {
                    if (cube.UpdateResult() == 2)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        protected bool IsHitWithRightBoard(Canvas canvas)
        {
            bool result = false;

            for (int i = 0; i < rightCubes.Count; i++)
            {
                if (rightCubes[i] is Cube cube)
                {
                    if (cube.UpdateResult() == 1)
                    {
                        result = true;
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
        public FigureI(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube3 = new Cube(color, size, startCoordX + 2 * size, startCoordY, canvas);
            _cube4 = new Cube(color, size, startCoordX + 3 * size, startCoordY, canvas);

            _figure = new Cube[,,]{{ {_cube1, _cube2, _cube3, _cube4},
                                     { null, null, null, null },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);
            topCubes.Add(_cube2);
            topCubes.Add(_cube3);
            topCubes.Add(_cube4);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube1);
            bottomCubes.Add(_cube2);
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube1);

            rightCubes = new MyList();
            rightCubes.Add(_cube4);
        }
    }

    public class FigureJLeft : Figure
    {
        public FigureJLeft(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube3 = new Cube(color, size, startCoordX + 2 * size, startCoordY, canvas);
            _cube4 = new Cube(color, size, startCoordX + 2 * size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ {_cube1, _cube2, _cube3, },
                                     { null, null, _cube4 },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);
            topCubes.Add(_cube2);
            topCubes.Add(_cube3);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube1);

            rightCubes = new MyList();
            rightCubes.Add(_cube3);
            rightCubes.Add(_cube4);
        }
    }

    public class FigureJRight : Figure
    {
        public FigureJRight(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX + 2 * size, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX, startCoordY + size, canvas);
            _cube3 = new Cube(color, size, startCoordX + size, startCoordY + size, canvas);
            _cube4 = new Cube(color, size, startCoordX + 2 * size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ {null, null, _cube1, },
                                     { _cube2, _cube3, _cube4 },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube2);
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube2);

            rightCubes = new MyList();
            rightCubes.Add(_cube1);
            rightCubes.Add(_cube4);
        }
    }

    public class FigureO : Figure
    {
        public FigureO(uint color, Canvas canvas)
        {
            _canvas = canvas;

            _cube1 = new Cube(color, size, startCoordX, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube3 = new Cube(color, size, startCoordX, startCoordY + size, canvas);
            _cube4 = new Cube(color, size, startCoordX + size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ {_cube1, _cube2 },
                                     { _cube3, _cube4 },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);
            topCubes.Add(_cube2);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube1);
            leftCubes.Add(_cube3);

            rightCubes = new MyList();
            rightCubes.Add(_cube2);
            rightCubes.Add(_cube4);
        }
    }

    public class FigureS : Figure
    {
        public FigureS(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX + 2 * size, startCoordY, canvas);
            _cube3 = new Cube(color, size, startCoordX, startCoordY + size, canvas);
            _cube4 = new Cube(color, size, startCoordX + size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ {null, _cube1, _cube2, },
                                     { _cube3, _cube4, null },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);
            topCubes.Add(_cube2);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube3);

            rightCubes = new MyList();
            rightCubes.Add(_cube2);
        }
    }

    public class FigureT : Figure
    {
        public FigureT(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX, startCoordY + size, canvas);
            _cube3 = new Cube(color, size, startCoordX + size, startCoordY + size, canvas);
            _cube4 = new Cube(color, size, startCoordX + 2 * size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ {null, _cube1, null, },
                                     { _cube2, _cube3, _cube4 },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube2);
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube2);

            rightCubes = new MyList();
            rightCubes.Add(_cube4);
        }
    }

    public class FigureZ : Figure
    {
        public FigureZ(uint color, Canvas canvas)
        {
            _cube1 = new Cube(color, size, startCoordX, startCoordY, canvas);
            _cube2 = new Cube(color, size, startCoordX + size, startCoordY, canvas);
            _cube3 = new Cube(color, size, startCoordX + size, startCoordY + size, canvas);
            _cube4 = new Cube(color, size, startCoordX + 2 * size, startCoordY + size, canvas);

            _figure = new Cube[,,]{{ { _cube1, _cube2, null },
                                     { null, _cube3, _cube4 },}
            };

            topCubes = new MyList();
            topCubes.Add(_cube1);
            topCubes.Add(_cube2);

            bottomCubes = new MyList();
            bottomCubes.Add(_cube3);
            bottomCubes.Add(_cube4);

            leftCubes = new MyList();
            leftCubes.Add(_cube1);

            rightCubes = new MyList();
            rightCubes.Add(_cube4);
        }
    }
}

