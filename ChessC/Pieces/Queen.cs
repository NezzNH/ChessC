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
            moveOffsets.Add(new MoveOffsets(directions.Up, location, true));
            moveOffsets.Add(new MoveOffsets(directions.UpRight, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Right, location, true));
            moveOffsets.Add(new MoveOffsets(directions.RightDown, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Down, location, true));
            moveOffsets.Add(new MoveOffsets(directions.DownLeft, location, true));
            moveOffsets.Add(new MoveOffsets(directions.Left, location, true));
            moveOffsets.Add(new MoveOffsets(directions.LeftUp, location, true));
        }
    }
}
