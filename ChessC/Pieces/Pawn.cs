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
            moveOffsets.Clear();
            int directionMultiplier;
            coordPair tempCoordOffset;
            tempCoordOffset.row = 0; tempCoordOffset.collumn = 0;
            if (this.pieceColor == color.white) directionMultiplier = 1;
            else directionMultiplier = -1;

            tempCoordOffset.row = directionMultiplier * 1;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempCoordOffset, false));
            if (moveCounter < 1)
            {
                tempCoordOffset.row = directionMultiplier * 2;
                moveOffsets.Add(new MoveOffsets(directions.Up, tempCoordOffset, false));
            }
        }
    }
}
