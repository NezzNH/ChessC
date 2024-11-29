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

            MoveOffsets tempOffset = new MoveOffsets();
            tempOffset.isRecurring = true;

            tempOffset.moveDirection = directions.UpRight;
            moveOffsets.Add(tempOffset);
            tempOffset.moveDirection = directions.RightDown;
            moveOffsets.Add(tempOffset);
            tempOffset.moveDirection = directions.DownLeft;
            moveOffsets.Add(tempOffset);
            tempOffset.moveDirection = directions.LeftUp;
            moveOffsets.Add(tempOffset);
        }
    }
}
