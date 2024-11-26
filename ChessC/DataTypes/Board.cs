using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.Pieces;
using ChessC.DataTypes;
using System.Windows.Forms;
using System.Drawing;

namespace ChessC.DataTypes
{
    class Board
    {
        private Label[,] DisplayFieldMatrix;
        private Field[,] Fields = new Field[8, 8];
        private Piece[] PieceArray = new Piece[32];
        private Piece LastUpdatedPiece;
        private LinkedList<String> allMoves = new LinkedList<String>();
        private coordPair[] attackedFields;
        public Board()
        {
            this.DisplayFieldMatrix = new Label[8, 8];
            PieceArray = new Piece[32];
        }
        public void setDisplayFieldMatrix(Label[,] DisplayFieldMatrix)
        {
            this.DisplayFieldMatrix = DisplayFieldMatrix;
        }
        public void initBoard()
        {
            initPieceArray(); initFields(); setupFieldColors();
        }

        public bool getMoveLegality(coordPair requestedCoords, Piece inputPiece) { //TO-DO decide whether selected piece will be held, or it should be passed along with each function
            Field referencedField = Fields[requestedCoords.row, requestedCoords.collumn];
            Piece reqPiece = inputPiece;

            if (!referencedField.returnOccupancy() && (referencedField.getPiece().getColor() != inputPiece.getColor())) return true;
            else return false;
        }

        private void initPieceArray()
        {
            coordPair tempPair;
            color tempColor = color.white;
            tempPair.collumn = 0;
            tempPair.row = 0;
            PieceArray[0] = new Rook(tempPair, tempColor);
            tempPair.collumn = 1;
            PieceArray[1] = new Knight(tempPair, tempColor);
            tempPair.collumn = 2;
            PieceArray[2] = new Bishop(tempPair, tempColor);
            tempPair.collumn = 3;
            PieceArray[3] = new Queen(tempPair, tempColor);
            tempPair.collumn = 4;
            PieceArray[4] = new King(tempPair, tempColor);
            tempPair.collumn = 5;
            PieceArray[5] = new Bishop(tempPair, tempColor);
            tempPair.collumn = 6;
            PieceArray[6] = new Knight(tempPair, tempColor);
            tempPair.collumn = 7;
            PieceArray[7] = new Rook(tempPair, tempColor);
            tempPair.row = 1;
            for (int i = 0; i < 8; i++)
            {
                tempPair.collumn = i;
                PieceArray[i + 8] = new Pawn(tempPair, tempColor);
            }
            tempPair.row = 6;
            tempColor = color.black;
            for (int i = 0; i < 8; i++)
            {
                tempPair.collumn = i;
                PieceArray[i + 16] = new Pawn(tempPair, tempColor);
            }
            tempPair.row = 7;
            tempPair.collumn = 0;
            PieceArray[24] = new Rook(tempPair, tempColor);
            tempPair.collumn = 1;
            PieceArray[25] = new Knight(tempPair, tempColor);
            tempPair.collumn = 2;
            PieceArray[26] = new Bishop(tempPair, tempColor);
            tempPair.collumn = 3;
            PieceArray[27] = new Queen(tempPair, tempColor);
            tempPair.collumn = 4;
            PieceArray[28] = new King(tempPair, tempColor);
            tempPair.collumn = 5;
            PieceArray[29] = new Bishop(tempPair, tempColor);
            tempPair.collumn = 6;
            PieceArray[30] = new Knight(tempPair, tempColor);
            tempPair.collumn = 7;
            PieceArray[31] = new Rook(tempPair, tempColor);
        }

        private void initFields()
        {
            int row, collumn;
            coordPair tempPair;
            color colorBuffer = color.black;
            for (int i = 0; i < 64; i++)
            {
                collumn = i % 8; row = i / 8;
                if (!hasRowChanged(i))
                {
                    if (colorBuffer == color.white) colorBuffer = color.black;
                    else colorBuffer = color.white;
                }
                Fields[row, collumn] = new Field(i, colorBuffer);
            }
            for (int i = 0; i < PieceArray.Length; i++)
            {
                tempPair = PieceArray[i].getLocation();
                Fields[tempPair.row, tempPair.collumn].setPiece(PieceArray[i]);
            }
        }

        private bool hasRowChanged(int i)
        {
            int currentRow, lastRow;
            currentRow = i / 8;
            lastRow = (i - 1) / 8;
            if (currentRow != lastRow) return true;
            else return false;
        }

        public void updateFieldStatus()
        {

        } //requires variable LastUpdatedPiece, or some other approach

        private String returnPieceSymbol(Piece inputPiece)
        {
            if (inputPiece == null) return "";
            String pieceType = inputPiece.GetType().Name;
            if (pieceType == "Knight") return "k";
            else return pieceType.Substring(0, 1); //no need to use a switch here...
        }

        private void setupFieldColors() {
            int collumn, row;
            for (int i = 0; i < 64; i++)
            {
                collumn = i % 8; row = i / 8;   
                if (Fields[row, collumn].getColor() == color.white) DisplayFieldMatrix[row, collumn].BackColor = Color.Wheat;
                else DisplayFieldMatrix[row, collumn].BackColor = Color.SaddleBrown;

            }
        }
        public void renderFields()
        {
            int collumn, row;
            Piece tempPiece;
           
            for (int i = 0; i < 64; i++)
            {
                collumn = i % 8; row = i / 8;
                tempPiece = Fields[row, collumn].getPiece();
                if (tempPiece != null)
                {
                    if (tempPiece.getColor() == color.white) DisplayFieldMatrix[row, collumn].ForeColor = Color.White;
                    else DisplayFieldMatrix[row, collumn].ForeColor = Color.Black;
                }
                DisplayFieldMatrix[row, collumn].Text = returnPieceSymbol(Fields[row, collumn].getPiece());
            }
        }
    }
}
