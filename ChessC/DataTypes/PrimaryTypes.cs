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
    }

    struct MoveField
    {
        public coordPair coords;
        public OffsetType offsetType;
        //public delegate? funcPointer; TO-DO: figure out function pointer for OffsetType.Conditional fields! (en passant and castle in the stock context)
    }
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