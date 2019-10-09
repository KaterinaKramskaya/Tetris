using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGraphic
{
    public enum MovingType
    {
        Stop = 0,
        OnlyLeft = 1,
        OnlyRight = 2,
        WithoutLimits = 3,
        OnlyDown = 4
    }
}
