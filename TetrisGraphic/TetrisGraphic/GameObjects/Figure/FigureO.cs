using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public class FigureO : Figure
    {
        public FigureO(uint color, Canvas canvas, CanvasField canvasField, int startCoordX, int startCoordY) 
            : base(color, canvas, canvasField, startCoordX, startCoordY)
        {
            SetCubesCoords();
            CreateCubesArray();
        }

        protected override void SetCubesCoords()
        {
            _cube1 = new Cube(
               new GameObjectParametres()
               {
                   Color = this.Color,
                   CoordX = _startCoordX,
                   CoordY = _startCoordY,
                   Height = _size,
                   Width = _size
               },
               _canvas);

            _cube2 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + _size,
                    CoordY = _startCoordY,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube3 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);

            _cube4 = new Cube(
                new GameObjectParametres()
                {
                    Color = this.Color,
                    CoordX = _startCoordX + _size,
                    CoordY = _startCoordY + _size,
                    Height = _size,
                    Width = _size
                },
                _canvas);
        }

        public override void Rotate()
        {
            // do nothing
        }
    }
}
