using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.Pieces;

namespace ChessC.DataTypes
{
    enum moveType { //possible restructure of this enum too. its possible that if claimType were to be implemented, moveTYpe would be used there too
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
    struct Move
    {
        Piece[] participants = new Piece[2]; //possible issue with garbage collector holding a permanent reference here, i'll def see about
                                             //possibly translating piece objects into simpletons of struct property and like 2 bytes instead of 20
        public int index;
        public moveType type;
    }
}
