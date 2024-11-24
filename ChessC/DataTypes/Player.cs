using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;
using ChessC.Pieces;

namespace ChessC.DataTypes
{
    class Player
    {
        private color plyrcolor;
        private int score, deadPieceArray;
        Piece[] graveyard;
        public Player()
        {
            plyrcolor = color.white;
            graveyard = new Piece[16];
            deadPieceArray = 0;
        }

        public Player(color plyrcolor, int deadPieceArray = 0)
        {
            this.plyrcolor = plyrcolor;
            graveyard = new Piece[16];
            this.deadPieceArray = deadPieceArray;
        }

        public color getColor() { return this.plyrcolor; }
        public void setColor(color plyrcolor) { this.plyrcolor = plyrcolor; }
        public int getScore() { return score; }
        public void setScore(int score) { this.score = score; }
    }
}
