using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    class Grid : GameObject
    {
        public Grid(GameObjectParametres parametres) : base(parametres)
        {
        }

        public override void Render(ConsoleGraphics graphic)
        {
            for (int i = CoordX; i <= Width; i += _size)
            { 
                graphic.DrawLine(Color, i, CoordY, i, Height);
            }
            for (int i = CoordY; i <= Height; i += _size)
            {
                graphic.DrawLine(Color, CoordX, i, Width, i);
            }
        }
    }
}
