using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class King : Piece
    {
        public King(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = false) : base(location, pieceColor, pinned, isRecurringMovePiece) { }
        public King() : base() { }
        public override void calculateDirections()
        {
            MoveOffset tempoffset;
            tempoffset.moveOffset = location;
            tempoffset.isRecurring = false;
            

            for (int i = 0; i < 8; i++)
            {
                tempoffset.moveDirection = tempDirectionsGlobalArray[i];
                moveOffsets.Add(tempoffset);
            }
        }
    }
}
