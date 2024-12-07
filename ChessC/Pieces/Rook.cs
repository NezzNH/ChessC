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
            moveOffsets.Add(new MoveOffsets(directions.Up, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Left, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Down, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Right, location, true));
        }
    }
}
