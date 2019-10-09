using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    class Button : TitleTable
    {
        public event Action Click;

        public Button(GameObjectParametres parametres, string text, int YOffset) 
            : base(parametres, text, YOffset)
        {
        }

        public override void Update()
        {
            int x = Input.MouseX;
            int y = Input.MouseY;
            if (Input.IsMouseLeftButtonDown && IsInObject(x, y))
            {
                Click?.Invoke();
            }
        }
    }
}