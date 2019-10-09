using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class TitleTable : GameObject
    {
        protected string _title;
        protected int _titleSize = 17;

        protected int _XOffset;
        protected int _YOffset = 7;

        public TitleTable(GameObjectParametres parametres, string title, int XOffset) 
            : base(parametres)
        {
            _title = title;
            _XOffset = XOffset;
        }

        public TitleTable(GameObjectParametres parametres, string title, int XOffset, int textSize)
            : base(parametres)
        {
            _title = title;
            _XOffset = XOffset;
            _titleSize = textSize;
        }

        public override void Render(ConsoleGraphics graphic)
        {
            base.Render(graphic);
            graphic.DrawString(_title, Constant.Font, Constant.FontColor, CoordX + _XOffset, CoordY + _YOffset, _titleSize);
        }
    }

    public class TitleTableWithText : TitleTable
    {
        private string _text;
        private int _textSize = 14;

        public TitleTableWithText(GameObjectParametres parametres, string title, int XOffset, string text) 
            : base(parametres, title, XOffset)
        {
            _text = text;
        }

        public override void Render(ConsoleGraphics graphic)
        {
            base.Render(graphic);
            graphic.DrawString(_text, Constant.Font, Constant.FontColor, CoordX + _XOffset, CoordY + _YOffset, _textSize);
        }
    }
}