using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public static class FigureCreator
    {
        public static Figure RandomFigure(Random random, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY)
        {
            int figureNumber = random.Next(1, 8);

            switch (figureNumber)
            {
                case 1:
                    return new FigureI(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY); 
                case 2:
                    return new FigureJLeft(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 3:
                    return new FigureJRight(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 4:
                    return new FigureO(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 5:
                    return new FigureS(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 6:
                    return new FigureT(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                default:
                    return new FigureZ(FigureColors.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
            }
        }
    }
}
