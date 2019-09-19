using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace TetrisGraphic
{
    public class TextTable : GameObject
    {
        protected string _text;
        protected int _textSize = 17;
        protected int _XOffset = 5;
        protected int _YOffset;

        public TextTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, height, width, coordX, coordY)
        {
            _text = text;
            _YOffset = _height / 6;
        }

        public override void Render(ConsoleGraphics graphic)
        {
            base.Render(graphic);
            graphic.DrawString(_text, Constant.Font, Constant.FontColor, _coordX + _XOffset, _coordY + _YOffset, _textSize);
        }
    }

    public class GameOverTable : TextTable
    {
        public GameOverTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, width, height, coordX, coordY, text)
        {
            _color = Constant.GameOverTableColor;
            _textSize = 32;
            _XOffset = _width / 6;
        }
    }

    public class RoundResultTable : TextTable
    {
        public RoundResultTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, width, height, coordX, coordY, text)
        {
            _text = text;
            _color = Constant.RoundScoreTableColor;
            _XOffset = _width / 25;
        }
    }

    public class BestResultTable : TextTable
    {
        public BestResultTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, width, height, coordX, coordY, text)
        {
            _XOffset = _width / 6;
            _YOffset = _height / 6;
        }
    }

    public class ClearedLinesTable : TextTable
    {
        public ClearedLinesTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, width, height, coordX, coordY, text)
        {
            _XOffset = _width / 30;
        }
    }

    public class OptionsTable : TextTable
    {
        private string _title;
        private int _titleSize;

        public OptionsTable(uint color, int width, int height, int coordX, int coordY, string text) 
            : base(color, width, height, coordX, coordY, text)
        {
            _title = "OPTIONS\n";
            _titleSize = 17;
            _text += "\n\n MOVE RIGHT  -  RIGHT ARROW\n\n MOVE LEFT  -  LEFT ARROW\n\n ROTATE FIGURE  -  SPACE\n\n SOFT DROP  -  UP ARROW\n\n HARD DROP  -  DOWN ARROW";

            _textSize = 14;
            _XOffset = _width / 12;
            _YOffset = _height / 12;
        }
        public override void Render(ConsoleGraphics graphic)
        {
            base.Render(graphic);
            graphic.DrawString(_title, Constant.Font, Constant.FontColor, _coordX + 5 * Constant.XOffset, _coordY + Constant.YOffset, _titleSize);
        }
    }
}
