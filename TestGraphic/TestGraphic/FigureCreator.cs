using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraphic
{
    public static class FigureCreator
    {
        public static Figure RandomFigure(Random random, FigureColors color, Canvas canvas)
        {
            int figureNumber = random.Next(1, 8);
            Figure figure;

            switch (figureNumber)
            {
                case 1:
                    return figure = new FigureI(color.RandomColor(), canvas); ;
                case 2:
                    return figure = new FigureJLeft(color.RandomColor(), canvas);
                case 3:
                    return figure = new FigureJRight(color.RandomColor(), canvas);
                case 4:
                    return figure = new FigureO(color.RandomColor(), canvas);
                case 5:
                    return figure = new FigureS(color.RandomColor(), canvas);
                case 6:
                    return figure = new FigureT(color.RandomColor(), canvas);
                default:
                    return figure = new FigureZ(color.RandomColor(), canvas);
            }
        }
    }
}
