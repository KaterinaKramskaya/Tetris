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
        private readonly ConsoleGraphics graphic;
        private readonly Random rnd;

        private MyList graphicObjects = new MyList();
        private MyList figures = new MyList();

        private int _bestResult = BestResultKeeper.BestResult;
        private int _clearedLines;
        private int _points;
        private int _lineToClear;

        private readonly int _clientHeight;
        private readonly int _clientWidth;

        private readonly CanvasField canvasField;

        private readonly Grid grid;
        private readonly Grid gridForNextFigure;

        private readonly Canvas canvas;
        private readonly Canvas canvasForNextFigure;
        private readonly Canvas background;
        private readonly Canvas backgroundFill;

        private Figure figure;
        private Figure figureNext;

        private readonly int startCoordX;
        private readonly int startCoordY;

        private readonly int nextStartCoordX;
        private readonly int nextStartCoordY;

        private Button pauseButton;
        private Button optionsButton;

        private int _speed;

        private bool GameContinue;

        public GameEngine(ConsoleGraphics graphic, int сlientHeight, int сlientWidth)
        {
            _points = 0;
            _clearedLines = 0;

            _clientHeight = сlientHeight;
            _clientWidth = сlientWidth;

            rnd = new Random();
            this.graphic = graphic;

            canvas = new Canvas
                (Constant.CanvasColor, _clientHeight, _clientWidth, Constant.XOffset, Constant.YOffset + 2 * Constant.Size);
            grid = new Grid
                (Constant.GridColor, _clientHeight + 2 * Constant.Size, _clientWidth, Constant.XOffset, Constant.YOffset + 2 * Constant.Size);

            canvasForNextFigure = new Canvas
                (Constant.CanvasColor, 6 * Constant.Size + Constant.YOffset, 6 * Constant.Size + Constant.XOffset, _clientWidth + Constant.XOffset + 7, _clientWidth / 2 + 2 * Constant.YOffset);
            gridForNextFigure = new Grid
                (Constant.GridColor, 6 * Constant.Size + _clientWidth / 2 + 2 * Constant.YOffset, 6 * Constant.Size + _clientWidth + Constant.XOffset + 7, _clientWidth + Constant.XOffset + 7, _clientWidth / 2 + 2 * Constant.YOffset);

            background = new Canvas(0x98ffffff, _clientHeight + 4 * Constant.Size, 2 * _clientWidth, 0, 0);
            backgroundFill = new Canvas(0xFFffffff, _clientHeight + 4 * Constant.Size, 2 * _clientWidth, 0, 0);

            canvasField = new CanvasField(_clientHeight + 2 * Constant.Size + 1, _clientWidth + 1);

            ResultTables();
            Buttons();

            AddObject(canvas);
            AddObject(canvasForNextFigure);

            AddObject(grid);
            AddObject(gridForNextFigure);

            _speed = 450;

            startCoordX = 3 * Constant.Size + Constant.XOffset;
            startCoordY = 2 * Constant.Size + Constant.YOffset;

            nextStartCoordX = _clientWidth + Constant.XOffset + Constant.Size + 7;
            nextStartCoordY = _clientWidth / 2 + 2 * Constant.YOffset + 2 * Constant.Size;

            figure = FigureCreator.RandomFigure(rnd, canvas, canvasField, startCoordX, startCoordY);
            AddObject(figure);

            GameContinue = true;
        }

        public void AddObject(GameObject obj)
        {
            if (obj is Figure)
            {
                graphicObjects.Insert(graphicObjects.Count - 2, obj);
                figures.Add(obj);
            }
            else
            {
                graphicObjects.Add(obj);
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

                    for (int i = 0; i < graphicObjects.Count; i++)
                    {
                        if (graphicObjects[i] is GameObject graphicObject)
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
            Cube[,] array = canvasField.CanvasFieldArray;

            for (int i = Constant.YOffset; i < array.GetLength(0); i += 30)
            {
                int result = Constant.YOffset;

                for (int j = Constant.XOffset; j < array.GetLength(1); j += 30)
                {
                    if (array[i, j] != null)
                    {
                        result += 30;
                    }
                }
                if (result == array.GetLength(1) - 1)
                {
                    _lineToClear = i;
                    return true;
                }
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
            canvasField.CreateCanvasField();

            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i] is Figure figure)
                {
                    figure.Update();
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

            TextTable NameTable = new TextTable
                (Constant.TetrisTableColor, width, height / 2, Constant.XOffset, Constant.YOffset, "TETRIS");

            TextTable RoundScoreTable = new RoundResultTable
                (Constant.RoundScoreTableColor, width, height, _clientWidth + Constant.XOffset, Constant.YOffset, $"SCORE\n");

            TextTable BestScoreTable = new BestResultTable
                (Constant.BestScoreTableColor, width, height, _clientWidth + width + 2 * Constant.XOffset, Constant.YOffset, $"BEST\n");

            TextTable ClearedLinesTable = new ClearedLinesTable
                (Constant.LineCountTableColor, 2 * width + Constant.XOffset, height / 2, _clientWidth + Constant.XOffset, Constant.YOffset + height + Constant.YOffset, $"LINES\n");

            AddObject(NameTable);
            AddObject(RoundScoreTable);
            AddObject(BestScoreTable);
            AddObject(ClearedLinesTable);
        }

        public void Buttons()
        {
            int height = Constant.TableSize;
            int width = Constant.TableSize;

            pauseButton = new Button(Constant.PauseButtonColor, width - 5, height / 2, 5 * Constant.XOffset + 20, Constant.YOffset, "PAUSE");
            pauseButton.Click += Pause;
            AddObject(pauseButton);

            optionsButton = new Button(Constant.OptionsButtonColor, width + Constant.XOffset - 5, height / 2, width + 6 * Constant.XOffset + 5, Constant.YOffset, "OPTIONS");
            optionsButton.Click += GameOptions;
            AddObject(optionsButton);
        }

        public void DrawResults()
        {
            graphic.DrawString($"{_points}", Constant.Font, Constant.FontColor, _clientWidth + 50, 3 * Constant.YOffset + 5, 17);
            graphic.DrawString($"{_bestResult}", Constant.Font, Constant.FontColor, _clientWidth + _clientWidth / 4 + 75, 3 * Constant.YOffset + 5, 17);
            graphic.DrawString($"{_clearedLines}", Constant.Font, Constant.FontColor, _clientWidth + _clientWidth / 4 + 75, _clientWidth / 3 + 30, 17);
        }

        public void GameOver()
        {
            TextTable GameOverResult = new GameOverTable
                (Constant.GameOverTableColor, _clientWidth - Constant.XOffset, _clientWidth / 4, Constant.XOffset + _clientWidth / 3, Constant.YOffset + _clientHeight / 3, "Game Over");

            background.Render(graphic);
            GameOverResult.Render(graphic);
            graphic.DrawString("press ESCAPE to continue", Constant.Font, 0x50004223, Constant.XOffset, _clientHeight + 28, 16);
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
            TextTable Options = new OptionsTable
                (Constant.OptionsTableColor, _clientWidth - Constant.XOffset, _clientWidth - 2 * Constant.XOffset, Constant.XOffset + _clientWidth / 3, Constant.YOffset + _clientHeight / 7, "");

            background.Render(graphic);
            Options.Render(graphic);
            graphic.DrawString("press ESCAPE to continue", Constant.Font, 0x50004223, Constant.XOffset, _clientHeight + 28, 16);
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
            GameEngine gameEngine = new GameEngine(graphic, _clientHeight, _clientWidth);
            gameEngine.Start();
        }
    }
}