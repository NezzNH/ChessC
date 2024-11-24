﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    class Queen : Piece
    {
        public Queen(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
        public Queen() : base() { }
        public override void calculateDirections()
        {
            moveOffsets = new MoveOffsets[8];

            for (int i = 0; i < 8; i++)
            {
                moveOffsets[i].isRecurring = true;
                moveOffsets[i].moveDirection = tempDirectionsGlobalArray[i];
            }
        }
    }
}