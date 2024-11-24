﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.Pieces;

namespace ChessC.DataTypes
{
    class Field
    {
        private bool isOccupied;
        private coordPair location;
        private color fieldColor;
        private bool isAttacked;
        private Piece pieceOnField;
        public Field(int normedFieldIndex, color fieldColor)
        {
            location.collumn = normedFieldIndex % 8;
            location.row = normedFieldIndex / 8;
            this.fieldColor = fieldColor;
        }
        public void changeOccupancy(bool inputOccupancy) { this.isOccupied = inputOccupancy; }
        public bool returnOccupancy() { return this.isOccupied; }
        public Piece getPiece() { return this.pieceOnField; }
        public void setPiece(Piece pieceOnField) { this.pieceOnField = pieceOnField; }
        public color getColor() { return this.fieldColor; }
    }
}