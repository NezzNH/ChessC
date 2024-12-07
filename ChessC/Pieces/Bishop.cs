using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Bishop : Piece
    {
        public Bishop(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true) : base(location, pieceColor, pinned, isRecurringMovePiece) { }
        public Bishop() : base() { }
        public override void calculateDirections()
        {
            moveOffsets.Add(new MoveOffsets(directions.UpRight, location, true));
            moveOffsets.Add(new MoveOffsets(directions.RightDown, location, true));
            moveOffsets.Add(new MoveOffsets(directions.DownLeft, location, true));
            moveOffsets.Add(new MoveOffsets(directions.LeftUp, location, true));
        }
    }
}
