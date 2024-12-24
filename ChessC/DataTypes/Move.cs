using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessC.DataTypes
{
    enum moveType { 
        neutralMove,
        captureMove,
        check,
        longCastle,
        shortCastle,
        promotion,
        stalemate,
        checkmate,
        repetition
    }
    class Move
    {
        public int index;
        public moveType type;
    }
}
