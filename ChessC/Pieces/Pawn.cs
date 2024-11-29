using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Pawn : Piece
    {
        private bool isFirstMove;
        public Pawn() : base() { }
        public Pawn(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = false) : base(location, pieceColor, pinned, isRecurringMovePiece) { }

        public override void calculateDirections()
        {
            MoveOffsets tempOffset = new MoveOffsets();
            if (this.pieceColor == color.white) tempOffset.moveDirection = directions.Up;
            else tempOffset.moveDirection = directions.Down;
            moveOffsets.Add(tempOffset);
        }
        public bool IsMyFirstMove() { return this.isFirstMove; }
    }
}
