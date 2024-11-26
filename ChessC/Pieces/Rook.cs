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
            moveOffsets = new MoveOffsets[4];

            for (int i = 0; i < 4; i++) { moveOffsets[i].isRecurring = true; }

            moveOffsets[0].moveDirection = directions.Up;
            moveOffsets[1].moveDirection = directions.Right;
            moveOffsets[2].moveDirection = directions.Down;
            moveOffsets[3].moveDirection = directions.Left;
        }
    }
}
