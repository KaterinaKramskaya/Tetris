using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;

namespace TetrisGraphic
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.CursorVisible = false;
            ConsoleGraphics graphic = new ConsoleGraphics();

            int сlientHeight = 10 * Constant.Size + Constant.YOffset;
            int сlientWidth = 10 * Constant.Size + Constant.XOffset;

            Console.WindowWidth = 60;
            Console.WindowHeight = 19;

            Console.BackgroundColor = ConsoleColor.White;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Clear();

            GameEngine gameEngine = new GameEngine(graphic, сlientHeight, сlientWidth);
            gameEngine.Start();
        }
    }
}

