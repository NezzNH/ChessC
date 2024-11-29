using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Rook : Piece
    {
        public Rook(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true) : base(location, pieceColor, pinned, isRecurringMovePiece ) { }
        public Rook() : base() { }
        public override void calculateDirections()
        {
            MoveOffsets tempOffset = new MoveOffsets();
            tempOffset.isRecurring = true;

            for (int i = 0; i < 8; i += 2) {
                tempOffset.moveDirection = tempDirectionsGlobalArray[i];
                moveOffsets.Add(tempOffset);
            }
        }
    }
}
