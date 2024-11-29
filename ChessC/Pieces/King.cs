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
            MoveOffsets tempOffset = new MoveOffsets();
            tempOffset.isRecurring = false;

            for (int i = 0; i < 8; i++)
            {
                tempOffset.moveDirection = tempDirectionsGlobalArray[i];
                moveOffsets.Add(tempOffset);
            }
        }
    }
}
