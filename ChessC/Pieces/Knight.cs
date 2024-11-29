using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Knight : Piece
    {
        public Knight(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = false) : base(location, pieceColor, pinned, isRecurringMovePiece) {
        }
        public Knight() : base() { }
        public override void calculateDirections()
        {
            coordPair tempPair;

            tempPair.collumn = 1; tempPair.row = 2;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = -1; tempPair.row = 2;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = 2; tempPair.row = 1;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = -2; tempPair.row = 1;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = 2; tempPair.row = -1;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = -2; tempPair.row = -1;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = -1; tempPair.row = -2;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));

            tempPair.collumn = 1; tempPair.row = -2;
            moveOffsets.Add(new MoveOffsets(directions.Up, tempPair, false));
        }
    }
}
