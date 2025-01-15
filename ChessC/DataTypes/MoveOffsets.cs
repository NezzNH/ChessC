using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessC.DataTypes
{
    struct MoveOffset
    {
        public bool isRecurring;
        public directions moveDirection;
        public coordPair moveOffset;
    }
}
