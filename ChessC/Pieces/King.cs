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
            moveOffsets.Add(new MoveOffsets(directions.Up, location, false));
            moveOffsets.Add(new MoveOffsets(directions.UpRight, location, false));
            moveOffsets.Add(new MoveOffsets(directions.Right, location, false));
            moveOffsets.Add(new MoveOffsets(directions.RightDown, location, false));
            moveOffsets.Add(new MoveOffsets(directions.Down, location, false));
            moveOffsets.Add(new MoveOffsets(directions.DownLeft, location, false));
            moveOffsets.Add(new MoveOffsets(directions.Left, location, false));
            moveOffsets.Add(new MoveOffsets(directions.LeftUp, location, false));
        }
    }
}
