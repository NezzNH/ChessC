using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Bishop : Piece
    {
        public Bishop(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
        public Bishop() : base() { }
        public override void calculateDirections()
        {
            moveOffsets = new MoveOffsets[4];

            for (int i = 0; i < 4; i++) moveOffsets[i].isRecurring = true;

            moveOffsets[0].moveDirection = directions.UpRight;
            moveOffsets[1].moveDirection = directions.RightDown;
            moveOffsets[2].moveDirection = directions.DownLeft;
            moveOffsets[3].moveDirection = directions.LeftUp;
        }
    }
}
