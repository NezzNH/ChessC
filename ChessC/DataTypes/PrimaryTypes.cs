using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessC.DataTypes
{
    public struct coordPair
    {
        public int collumn, row;
    };

    public enum directions
    {
        Up,
        UpRight,
        Right,
        RightDown,
        Down,
        DownLeft,
        Left,
        LeftUp
    }
    public enum color
    {
        white, black
    }
}