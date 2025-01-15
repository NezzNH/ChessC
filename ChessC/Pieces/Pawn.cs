using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Pawn : Piece
    {
        public Pawn() : base() { }
        public Pawn(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = false) : base(location, pieceColor, pinned, isRecurringMovePiece) { }

        public override void calculateDirections()
        {
            this.moveCalcIsUpdated = true;

            MoveOffset tempOffset;
            tempOffset.moveDirection = directions.Up;
            tempOffset.isRecurring = false;
            tempOffset.offsetType = OffsetType.MoveOffset;

            moveOffsets.Clear();

            int directionMultiplier;
            coordPair tempCoordOffset;
            tempCoordOffset.row = 0; tempCoordOffset.collumn = 0;

            if (this.pieceColor == color.white) directionMultiplier = 1;
            else directionMultiplier = -1;

            tempCoordOffset.row = directionMultiplier * 1;
            tempOffset.moveOffset = tempCoordOffset;
            moveOffsets.Add(tempOffset);
            if (moveCounter < 1)
            {
                tempCoordOffset.row = directionMultiplier * 2;
                tempOffset.moveOffset = tempCoordOffset;
                moveOffsets.Add(tempOffset);
            }

            //attack offset portion

            tempOffset.offsetType = OffsetType.AttackOffset;

            tempCoordOffset.row = 1;
            tempCoordOffset.collumn = 1;

            tempCoordOffset.row *= directionMultiplier;

            tempOffset.moveOffset = tempCoordOffset;
            moveOffsets.Add(tempOffset);

            tempCoordOffset.collumn = -1;
            moveOffsets.Add(tempOffset);
        }
    }
}
