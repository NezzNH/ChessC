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
        public Pawn(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }

        public override void calculateDirections()
        {
            moveOffsets = new MoveOffsets[1];
            if (this.pieceColor == color.white) moveOffsets[0].moveDirection = directions.Up;
            else moveOffsets[0].moveDirection = directions.Down;
        }
        public bool IsMyFirstMove() { return this.isFirstMove; }
    }
}
