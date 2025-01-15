using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;



namespace ChessC.Pieces
{
    class Queen : Piece
    {
        public Queen(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true) : base(location, pieceColor, pinned, isRecurringMovePiece) { }
        public Queen() : base() { }
        public override void calculateDirections()
        {
            MoveOffset tempOffset;
            tempOffset.moveOffset = location;
            tempOffset.isRecurring = true;
            tempOffset.offsetType = OffsetType.MoveAndAttackOffset;

            for (int i = 0; i < 8; i++)
            {
                tempOffset.moveDirection = tempDirectionsGlobalArray[i];
                moveOffsets.Add(tempOffset);
            }
        }
    }
}
