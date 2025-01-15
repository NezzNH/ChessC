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
            MoveOffset tempoffset;
            tempoffset.moveDirection = directions.UpRight;
            tempoffset.moveOffset = location;
            tempoffset.isRecurring = true;
            tempoffset.offsetType = OffsetType.MoveAndAttackOffset;
            moveOffsets.Add(tempoffset);
            tempoffset.moveDirection = directions.RightDown;
            moveOffsets.Add(tempoffset);
            tempoffset.moveDirection = directions.DownLeft;
            moveOffsets.Add(tempoffset);
            tempoffset.moveDirection = directions.LeftUp;
            moveOffsets.Add(tempoffset);
        }
    }
}
