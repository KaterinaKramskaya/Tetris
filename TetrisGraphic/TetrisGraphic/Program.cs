using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NConsoleGraphics;
using DataStructures.Lib;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TetrisGraphic
{
    class Program
    {      
        // 1. exception (out of memory) при InsertObj

        // 2. изменить начало игры (обнулять предыдущую игру перед началом следующей)

        // 3. переворот фигур в CanvasField

        // 4. проблема с очищением линий

        static void Main(string[] args)
        { 
            Random random = new Random();
            Console.CursorVisible = false;
            ConsoleGraphics graphic = new ConsoleGraphics();

            int сlientHeight = 10 * Constant.Size + Constant.YOffset;
            int сlientWidth = 10 * Constant.Size + Constant.XOffset;

            var scale = GetScalingFactor();

            int consoleWidth = 70;
            int consoleHeigth = 25;

            if(scale == 1)
            {
                Console.WindowWidth = 70;
                Console.WindowHeight = 25;
            }
            else if(scale == 1.25)
            {
                Console.WindowWidth = (int)(consoleWidth + 8 - (consoleWidth * scale - consoleWidth));
                Console.WindowHeight = (int)(consoleHeigth + 1 - (consoleHeigth * scale - consoleHeigth));
            }
            else if(scale == 1.5)
            {
                Console.WindowWidth = (int)(consoleWidth + 14 - (consoleWidth * scale - consoleWidth));
                Console.WindowHeight = (int)(consoleHeigth + 4 - (consoleHeigth * scale - consoleHeigth));
            }
            else if(scale == 1.75)
            {
                Console.WindowWidth = 41;
                Console.WindowHeight = 13;
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Clear();

            GameEngine gameEngine = new GameEngine(graphic, сlientHeight, сlientWidth);
            gameEngine.Start();
        }

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }

        private static double GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            double ScreenScalingFactor = (double)PhysicalScreenHeight / (double)LogicalScreenHeight;

            return Math.Round(ScreenScalingFactor, 2); // 1 = 100%, 1.25 = 125%, 1.5 = 150%, 1.75 = 175%
        }
    }
}

