using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public static class FigureCreator
    {
        public static Figure RandomFigure(Random random, FigureColors color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY)
        {
            int figureNumber = random.Next(1, 8);
            Figure figure;

            switch (figureNumber)
            {
                case 1:
                    return figure = new FigureI(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY); 
                case 2:
                    return figure = new FigureJLeft(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 3:
                    return figure = new FigureJRight(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 4:
                    return figure = new FigureO(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 5:
                    return figure = new FigureS(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                case 6:
                    return figure = new FigureT(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
                default:
                    return figure = new FigureZ(color.RandomColor(random), canvas, canvasField, startCoordX, startCoordY);
            }
        }
    }
}
