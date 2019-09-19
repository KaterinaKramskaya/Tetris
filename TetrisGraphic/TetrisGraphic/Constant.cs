using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class Constant
    {
        public static int Size { get { return 30; } }
        public static int XOffset { get { return 20; } }
        public static int YOffset { get { return 20; } }

        public static int TableSize { get { return Size * 3; } }

        public static uint CanvasColor { get { return 0xFFdedede; } }
        public static uint GridColor { get { return 0xFFffffff; } }
        public static uint FontColor { get { return 0xFFffffff; } }

        public static uint BestScoreTableColor { get { return 0xFFf59d31; } }
        public static uint RoundScoreTableColor { get { return 0xFF54beff; } }
        public static uint LineCountTableColor { get { return 0xFF4bd927; } }
        public static uint GameOverTableColor { get { return 0xFFe63555; } }
        public static uint OptionsTableColor { get { return 0xFFe0ba3a; } }
        public static uint TetrisTableColor { get { return 0xFFa932d9; } }


        public static uint OptionsButtonColor { get { return 0xFFf0e13c; } }
        public static uint PauseButtonColor { get { return 0xFFe03d60; } }

        public static int FigurePoints { get { return 4; } }
        public static int LinePoints { get { return 20; } }

        public static string Font { get { return "Arial Narrow"; } }
    }
}
