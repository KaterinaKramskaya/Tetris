using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TestGraphic
{
    class Program
    {
        public const int size = 30;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleGraphics graphic = new ConsoleGraphics();

            int сlientHeight = 16 * size;
            int сlientWidth = 12 * size;

            Console.WindowWidth = 50;
            Console.WindowHeight = 25;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Clear();

            var color = new FigureColors();

            var canvas = new Canvas(0xFF2b2b2b, сlientHeight, сlientWidth, size);

            var grid = new Grid(0x50ffffff, сlientHeight, сlientWidth, size);
            Random random = new Random();


            Figure figure;
            figure = FigureCreator.RandomFigure(random, color, canvas);
            while (true)
            {
                // тут я добавлю все объекты, относящиеся к GraphicObject, в лист, и буду для каждого элемента листа вызывать и рендер, и апдейт
                canvas.Render(graphic);
                figure.Update();

                figure.Render(graphic);
                grid.Render(graphic);

                graphic.FlipPages();
                Thread.Sleep(500);
            }
        }


    }
}

