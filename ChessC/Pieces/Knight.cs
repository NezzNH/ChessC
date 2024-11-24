using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Knight : Piece
    {
        public Knight(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
        public Knight() : base() { }
        public override void calculateDirections()
        {
            moveOffsets = new MoveOffsets[8];

            for (int i = 0; i < 8; i++) moveOffsets[i].isRecurring = false;

            coordPair tempPair;

            tempPair.collumn = 1; tempPair.row = 2;
            moveOffsets[0].moveOffset = tempPair;
            tempPair.collumn = -1; tempPair.row = 2;
            moveOffsets[1].moveOffset = tempPair;
            tempPair.collumn = 2; tempPair.row = 1;
            moveOffsets[2].moveOffset = tempPair;
            tempPair.collumn = -2; tempPair.row = 1;
            moveOffsets[3].moveOffset = tempPair;
            tempPair.collumn = 2; tempPair.row = -1;
            moveOffsets[4].moveOffset = tempPair;
            tempPair.collumn = -2; tempPair.row = -1;
            moveOffsets[5].moveOffset = tempPair;
            tempPair.collumn = -1; tempPair.row = -2;
            moveOffsets[6].moveOffset = tempPair;
            tempPair.collumn = 1; tempPair.row = -2;
            moveOffsets[7].moveOffset = tempPair;
        }
    }
}
