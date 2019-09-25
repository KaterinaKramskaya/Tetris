using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    class Button : TextTable
    {
        public event Action Click;

        public Button(uint color, int width, int height, int coordX, int coordY, string text) : base(color, width, height, coordX, coordY, text)
        {
        }

        public override void Update()
        {
            int x = Input.MouseX;
            int y = Input.MouseY;
            if (Input.IsMouseLeftButtonDown && (IsInObject(x, y)))
            {
                Click?.Invoke();
            }
        }
    }
}