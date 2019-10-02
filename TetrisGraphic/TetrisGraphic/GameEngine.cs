using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;
using System.Threading;

namespace TetrisGraphic
{
    public class GameEngine
    {
        private readonly int _clientHeight;
        private readonly int _clientWidth;

        private readonly ConsoleGraphics graphic;
        private readonly Random rnd;

        private readonly int startCoordX;
        private readonly int startCoordY;

        private readonly int nextStartCoordX;
        private readonly int nextStartCoordY;

        private MyList gameObjects = new MyList();
        private MyList figures = new MyList();

        private int _bestResult = BestResultKeeper.BestResult;
        private int _clearedLines;
        private int _points;
        private int _lineToClear;

        private CanvasField canvasField;

        private Grid grid;
        private Grid gridForNextFigure;

        private Canvas canvas;
        private Canvas canvasForNextFigure;
        private Canvas background;
        private Canvas backgroundFill;

        private Figure figure;
        private Figure figureNext;

        private Button pauseButton;
        private Button optionsButton;

        private TitleTable NameTable;
        private TitleTable RoundScoreTable;
        private TitleTable BestScoreTable;
        private TitleTable ClearedLinesTable;

        private int _speed;
        private bool GameContinue;


        public GameEngine(ConsoleGraphics graphic, int сlientHeight, int сlientWidth)
        {
            _clientHeight = сlientHeight;
            _clientWidth = сlientWidth;

            rnd = new Random();
            this.graphic = graphic;

            startCoordX = 3 * Constant.Size + Constant.XOffset;
            startCoordY = 2 * Constant.Size + Constant.YOffset;

            nextStartCoordX = _clientWidth + Constant.XOffset + Constant.Size + 7;
            nextStartCoordY = _clientWidth / 2 + 2 * Constant.YOffset + 2 * Constant.Size;

            ResultTables();
            Buttons();

            InitNewGame();
        }

        public void InitNewGame()
        {
            _points = 0;
            _clearedLines = 0;

            _speed = 450;

            canvas = new Canvas(
                new GameObjectParametres()
                {
                    Color = Constant.CanvasColor,
                    CoordX = Constant.XOffset,
                    CoordY = Constant.YOffset + 2 * Constant.Size,
                    Height = _clientHeight - Constant.YOffset,
                    Width = _clientWidth - Constant.XOffset,
                });

            grid = new Grid(
                new GameObjectParametres()
                {
                    Color = Constant.GridColor,
                    CoordX = Constant.XOffset,
                    CoordY = Constant.YOffset + 2 * Constant.Size,
                    Height = _clientHeight + 2 * Constant.Size,
                    Width = _clientWidth,
                });

            canvasForNextFigure = new Canvas(
                new GameObjectParametres()
                {
                    Color = Constant.CanvasColor,
                    CoordX = _clientWidth + Constant.XOffset + 7,
                    CoordY = _clientWidth / 2 + 2 * Constant.YOffset,
                    Height = 6 * Constant.Size,
                    Width = 6 * Constant.Size
                });

            gridForNextFigure = new Grid(
                new GameObjectParametres()
                {
                    Color = Constant.GridColor,
                    CoordX = _clientWidth + Constant.XOffset + 7,
                    CoordY = _clientWidth / 2 + 2 * Constant.YOffset,
                    Height = 6 * Constant.Size + _clientWidth / 2 + 2 * Constant.YOffset,
                    Width = 6 * Constant.Size + _clientWidth + Constant.XOffset + 7,
                });

            background = new Canvas(
                new GameObjectParametres()
                {
                    Color = 0x98ffffff,
                    CoordX = 0,
                    CoordY = 0,
                    Height = _clientHeight + 4 * Constant.Size,
                    Width = 2 * _clientWidth
                });

            backgroundFill = new Canvas(new GameObjectParametres()
            {
                Color = 0xFFffffff,
                CoordX = 0,
                CoordY = 0,
                Height = _clientHeight + 4 * Constant.Size,
                Width = 2 * _clientWidth
            });

            canvasField = new CanvasField();

            AddObject(canvas);
            AddObject(canvasForNextFigure);

            AddObject(pauseButton);
            AddObject(optionsButton);

            AddObject(NameTable);
            AddObject(RoundScoreTable);
            AddObject(BestScoreTable);
            AddObject(ClearedLinesTable);

            AddObject(grid);
            AddObject(gridForNextFigure);

            figure = FigureCreator.RandomFigure(rnd, canvas, canvasField, startCoordX, startCoordY);
            AddObject(figure);

            GameContinue = true;
        }

        public void ClearPreviousGame()
        {
            gameObjects.Clear();
            figures.Clear();

            grid = null;
            gridForNextFigure = null;

            canvas = null;
            canvasForNextFigure = null;
            background = null;
            backgroundFill = null;

            figure = null;
            figureNext = null;
        }

        public void AddObject(GameObject obj)
        {
            if (obj is Figure)
            {
                gameObjects.Insert(gameObjects.Count - 2, obj); 
                figures.Add(obj);
            }
            else
            {
                gameObjects.Add(obj);
            }
        }

        public void AddFigurePoints()
        {
            _points += Constant.FigurePoints;
            if (_points > _bestResult)
            {
                _bestResult = _points;
            }
        }

        public void AddLinePoints()
        {
            _points += Constant.LinePoints;
            if (_points > _bestResult)
            {
                _bestResult = _points;
            }
        }

        public void Start()
        {
            while (GameContinue)
            {
                CreateNextFigure();
                while (true)
                {
                    ChangeSpeed();
                    RotateFigure();

                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if (gameObjects[i] is GameObject graphicObject)
                        {
                            if (graphicObject != figureNext)
                            {
                                graphicObject.Update();
                            }

                            graphicObject.Render(graphic);
                        }
                    }
                    DrawResults();
                    graphic.FlipPages();
                    Thread.Sleep(_speed);

                    if (figure.FigureStoppedMove)
                    {
                        figure.FillFigureFields();

                        while (NeedToClearLine())
                        {
                            _clearedLines += 1;
                            AddLinePoints();
                            ClearLine();
                        }

                        figureNext.ResetCoords();
                        figure = figureNext;

                        CreateNextFigure();

                        if (figure.FigureCantUpdate())
                        {
                            if (_bestResult > BestResultKeeper.BestResult)
                            {
                                BestResultKeeper.BestResult = _bestResult;
                            }

                            GameContinue = false;
                            GameOver();
                        }
                        AddFigurePoints();
                        Speed();
                    }
                }
            }
        }

        public void CreateNextFigure()
        {
            figureNext = FigureCreator.RandomFigure
                (rnd, canvas, canvasField, nextStartCoordX, nextStartCoordY);
            AddObject(figureNext);
        }

        public bool NeedToClearLine()
        {
            _lineToClear = canvasField.LineToClear();

            if (_lineToClear >= 0)
            {
                return true;
            }
            return false;
        }

        public void ClearLine()
        {
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i] is Figure figure)
                {
                    figure.ClearFigureLine(_lineToClear);
                }
            }
            canvasField.ClearLines(_lineToClear);

            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i] is Figure figure)
                {
                    figure.Update();
                }
            }

            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i] is Figure figure)
                {
                    figure.FillFigureFields();
                }
            }
        }

        public void Speed()
        {
            if (_clearedLines >= 20)
            {
                _speed = 250;
            }
            else if (_clearedLines >= 10)
            {
                _speed = 350;
            }
            else if (_clearedLines < 10)
            {
                _speed = 450;
            }
        }

        public void ChangeSpeed()
        {
            if (Input.IsKeyDown(Keys.DOWN) && _speed >= 150)
            {
                _speed -= 150;
            }
            if (Input.IsKeyDown(Keys.UP))
            {
                _speed += 150;
            }
        }

        public void RotateFigure()
        {
            if (Input.IsKeyDown(Keys.SPACE))
            {
                figure.Rotate();
            }
        }

        public void ResultTables()
        {
            int height = Constant.TableSize;
            int width = Constant.TableSize;

            NameTable = new TitleTable(
                new GameObjectParametres()
                {
                    Color = Constant.TetrisTableColor,
                    CoordX = Constant.XOffset,
                    CoordY = Constant.YOffset,
                    Height = height / 2,
                    Width = width,
                },
                "TETRIS",
                7);


            RoundScoreTable = new TitleTable(
                new GameObjectParametres()
                {
                    Color = Constant.RoundScoreTableColor,
                    CoordX = _clientWidth + Constant.XOffset,
                    CoordY = Constant.YOffset,
                    Height = height,
                    Width = width,
                },
                "SCORE\n",
                7);


            BestScoreTable = new TitleTable(
                new GameObjectParametres()
                {
                    Color = Constant.BestScoreTableColor,
                    CoordX = _clientWidth + width + 2 * Constant.XOffset,
                    CoordY = Constant.YOffset,
                    Height = height,
                    Width = width,
                },
                "BEST\n",
                15);


            ClearedLinesTable = new TitleTable(
                new GameObjectParametres()
                {
                    Color = Constant.LineCountTableColor,
                    CoordX = _clientWidth + Constant.XOffset,
                    CoordY = Constant.YOffset + height + Constant.YOffset,
                    Height = height / 2,
                    Width = 2 * width + Constant.XOffset,
                },
                "LINES\n",
                7);
        }

        public void Buttons()
        {
            int height = Constant.TableSize;
            int width = Constant.TableSize;

            pauseButton = new Button(
                new GameObjectParametres()
                {
                    Color = Constant.PauseButtonColor,
                    CoordX = 5 * Constant.XOffset + 20,
                    CoordY = Constant.YOffset,
                    Height = height / 2,
                    Width = width - 5
                },
                "PAUSE",
                8);
            pauseButton.Click += Pause;
           
            optionsButton = new Button(
                new GameObjectParametres()
                {
                    Color = Constant.OptionsButtonColor,
                    CoordX = width + 6 * Constant.XOffset + 5,
                    CoordY = Constant.YOffset,
                    Height = height / 2,
                    Width = width + Constant.XOffset - 5
                },
                "OPTIONS",
                7);
            optionsButton.Click += GameOptions;
        }

        public void DrawResults()
        {
            graphic.DrawString($"{_points}", Constant.Font, Constant.FontColor, _clientWidth + 50, 3 * Constant.YOffset + 5, 17);
            graphic.DrawString($"{_bestResult}", Constant.Font, Constant.FontColor, _clientWidth + _clientWidth / 4 + 75, 3 * Constant.YOffset + 5, 17);
            graphic.DrawString($"{_clearedLines}", Constant.Font, Constant.FontColor, _clientWidth + _clientWidth / 4 + 75, _clientWidth / 3 + 30, 17);
        }

        public void GameOver()
        {
            TitleTable GameOverResult = new TitleTable(
                new GameObjectParametres()
                {
                    Color = Constant.GameOverTableColor,
                    CoordX = Constant.XOffset + _clientWidth / 3,
                    CoordY = Constant.YOffset + _clientHeight / 3,
                    Height = _clientWidth / 4,
                    Width = _clientWidth - Constant.XOffset
                },
                "GAME OVER",
                15,
                38);

            background.Render(graphic);
            GameOverResult.Render(graphic);
            DrawContinueString();
            while (true)
            {
                graphic.FlipPages();
                if (Input.IsKeyDown(Keys.ESCAPE))
                {
                    backgroundFill.Render(graphic);
                    graphic.FlipPages();
                    NewGame();
                }
            }
        }

        public void GameOptions()
        {
            TitleTable Options = new TitleTableWithText(
                new GameObjectParametres()
                {
                    Color = Constant.OptionsTableColor,
                    CoordX = Constant.XOffset + _clientWidth / 3,
                    CoordY = Constant.YOffset + _clientHeight / 7,
                    Height = _clientWidth - 3 * Constant.XOffset,
                    Width = _clientWidth - 2 * Constant.XOffset
                },
                "\tOPTIONS",
                17,
                "\n\n MOVE RIGHT  -  RIGHT ARROW\n\n MOVE LEFT  -  LEFT ARROW\n\n ROTATE FIGURE  -  SPACE\n\n SOFT DROP  -  UP ARROW\n\n HARD DROP  -  DOWN ARROW"
                );

            background.Render(graphic);
            Options.Render(graphic);
            DrawContinueString();

            while (true)
            {
                graphic.FlipPages();
                if (Input.IsKeyDown(Keys.ESCAPE))
                {
                    backgroundFill.Render(graphic);
                    break;
                }
            }
        }

        public void Pause()
        {
            background.Render(graphic);
            graphic.DrawString("press ESCAPE to continue", Constant.Font, 0x50004223, Constant.XOffset, _clientHeight + 28, 16);
            while (true)
            {
                graphic.FlipPages();
                if (Input.IsKeyDown(Keys.ESCAPE))
                {
                    break;
                }
            }
        }

        public void NewGame()
        {
            ClearPreviousGame();
            InitNewGame();
            Start();
        }

        public void DrawContinueString()
        {
            graphic.DrawString("press ESCAPE to continue", Constant.Font, 0x50004223, Constant.XOffset, _clientHeight + 28, 16);
        }
    }
}