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
        const int COORDPAIR_SIZE = 50; //TEMPORARY, AUGMENT BASED ON POS USING SEARCH FUNC AND RAY COLL
        public Queen(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true) : base(location, pieceColor, pinned, isRecurringMovePiece) { }
        public Queen() : base() { }
        public override void calculateDirections()
        {
            MoveOffsets tempOffset = new MoveOffsets();
            tempOffset.isRecurring = true;

            for (int i = 0; i < 8; i++)
            {
                tempOffset.moveDirection = tempDirectionsGlobalArray[i];
                moveOffsets.Add(tempOffset);
            }
        }
    }
}
