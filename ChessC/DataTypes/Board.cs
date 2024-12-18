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
        private List<Piece> pieces = new List<Piece>(); //remember all that kerfuffel i made about lists... yeah...
        private Piece selectedPiece;
        private Piece[] Graveyard; //temp
        private LinkedList<String> allMoves = new LinkedList<String>(); //remember what I said about lists... yeah these are just strings, shouldnt take up too much
                                                                        //also, I like the Linked hierarchy thats constructed like this, and that's harder to do with an array, so i'll just use this
                                                                       
        private coordPair[] attackedFields, moveableFields;
        private coordPair dimensions;
        private color currentMoveColor;
        public enum inputType
        {
            selection,
            moveRequest,
        }
        public inputType currentInputType;
        public Board()
        {
            moveableFields = new coordPair[0];
            selectedPiece = null;
            pieces = new List<Piece>();
            dimensions.row = 8; dimensions.collumn = 8;
            this.DisplayFieldMatrix = new Label[dimensions.row, dimensions.collumn]; //we'll need a way to generate labels in the gui dynamically with board dimensions
        }
        public void setDisplayFieldMatrix(Label[,] DisplayFieldMatrix)
        {
            this.DisplayFieldMatrix = DisplayFieldMatrix;
        }
        public void setDimensions(coordPair dimensions) { this.dimensions = dimensions; }
        public coordPair getDimensions() { return this.dimensions; }
        public void initBoard()
        {
            initPieceArray(); initFields(); setupFieldColors();
        }

        public Piece getPieceAt(coordPair location) {
            if (location.row > dimensions.row - 1 || location.collumn > dimensions.collumn - 1) return null;
            else return Fields[location.row, location.collumn].getPiece();
        }

        private bool fieldIsViableMove(coordPair moveLocation)
        {
            for (int i = 0; i < moveableFields.Length; i++) {
                if (moveLocation.row == moveableFields[i].row && moveLocation.collumn == moveableFields[i].collumn) return true;
            }
            return false;
        }

        public inputType determineInputType(coordPair clickLocation) {
            if (moveableFields.Length > 0) {
                for (int i = 0; i < moveableFields.Length; i++)
                {
                    if (moveableFields[i].row == clickLocation.row && moveableFields[i].collumn == clickLocation.collumn) return inputType.moveRequest;
                }
            }
            
            return inputType.selection;
        }

        private coordPair[] filterMovesForOccupancy(coordPair[] inputFields) {
            List<coordPair> tempList = new List<coordPair> ();
            for (int i = 0; i < inputFields.Length; i++) {
                if (Fields[inputFields[i].row, inputFields[i].collumn].getPiece() == null) tempList.Add(inputFields[i]);
            }
            coordPair[] result = tempList.ToArray();
            return result;
        }

        public void receiveClick(coordPair clickLocation) {
            currentInputType = determineInputType(clickLocation);
            if (currentInputType == inputType.selection){
                if (Fields[clickLocation.row, clickLocation.collumn].getPiece().getColor() == currentMoveColor
                    && Fields[clickLocation.row, clickLocation.collumn].getPiece() != null) {
                    selectedPiece = Fields[clickLocation.row, clickLocation.collumn].getPiece();
                    moveableFields = selectedPiece.returnAllPossibleMoves();
                    moveableFields = filterMovesForOccupancy(moveableFields);
                    for (int i = 0; i < moveableFields.Length; i++)
                    {
                        DisplayFieldMatrix[moveableFields[i].row, moveableFields[i].collumn].BackColor = Color.Green;
                    }
                }
                else
                {
                    for (int i = 0; i < moveableFields.Length; i++) {
                        DisplayFieldMatrix[moveableFields[i].row, moveableFields[i].collumn].BackColor =
                        convertColor(Fields[moveableFields[i].row, moveableFields[i].collumn].getColor());
                    }
                }
            }
            else {
                if (selectedPiece != null) movePiece(selectedPiece, clickLocation);
            }
        }
        
        private bool movePiece(Piece inputPiece, coordPair location) {

            color inputPieceColor = inputPiece.getColor();
            coordPair inputPieceLocation = inputPiece.getLocation();
            Piece opposingPiece = Fields[location.row, location.collumn].getPiece();

            if (opposingPiece == null)
            {
                Fields[inputPieceLocation.row, inputPieceLocation.collumn].setPiece(null);
                inputPiece.setLocation(location);
                Fields[location.row, location.collumn].setPiece(inputPiece);
                return true;
            }
            else {
                if (opposingPiece.getColor() != inputPieceColor) {
                    Fields[inputPieceLocation.row, inputPieceLocation.collumn].setPiece(null);
                    inputPiece.setLocation(location);
                    pieces.Remove(opposingPiece);
                    Fields[location.row, location.collumn].setPiece(inputPiece);
                    return true;
                }
            }
            return false;
        }
        private void initPieceArray()
        {
            coordPair tempPair;
            color tempColor = color.white;
            tempPair.collumn = 0;
            tempPair.row = 0;
            pieces.Add(new Rook(tempPair, tempColor));
            tempPair.collumn = 1;
            pieces.Add(new Knight(tempPair, tempColor));
            tempPair.collumn = 2;
            pieces.Add(new Bishop(tempPair, tempColor));
            tempPair.collumn = 3;
            pieces.Add(new Queen(tempPair, tempColor));
            tempPair.collumn = 4;
            pieces.Add(new King(tempPair, tempColor));
            tempPair.collumn = 5;
            pieces.Add(new Bishop(tempPair, tempColor));
            tempPair.collumn = 6;
            pieces.Add(new Knight(tempPair, tempColor));
            tempPair.collumn = 7;
            pieces.Add(new Rook(tempPair, tempColor));
            tempPair.row = 1;
            for (int i = 0; i < 8; i++)
            {
                tempPair.collumn = i;
                pieces.Add(new Pawn(tempPair, tempColor));
            }
            tempPair.row = 6;
            tempColor = color.black;
            for (int i = 0; i < 8; i++)
            {
                tempPair.collumn = i;
                pieces.Add(new Pawn(tempPair, tempColor));
            }
            tempPair.row = 7;
            tempPair.collumn = 0;
            pieces.Add(new Rook(tempPair, tempColor));
            tempPair.collumn = 1;
            pieces.Add(new Knight(tempPair, tempColor));
            tempPair.collumn = 2;
            pieces.Add(new Bishop(tempPair, tempColor));
            tempPair.collumn = 3;
            pieces.Add(new Queen(tempPair, tempColor));
            tempPair.collumn = 4;
            pieces.Add(new King(tempPair, tempColor));
            tempPair.collumn = 5;
            pieces.Add(new Bishop(tempPair, tempColor));
            tempPair.collumn = 6;
            pieces.Add(new Knight(tempPair, tempColor));
            tempPair.collumn = 7;
            pieces.Add(new Rook(tempPair, tempColor));
            Graveyard = new Piece[32]; //when we do dynamic board setups for n*n boards, this will be determined
                                       //automatically...
        }
        private void initFields()
        {
            coordPair tempPair;
            bool changeRow = false;
            color colorBuffer = color.white;
            for (int i = 0; i < dimensions.row; i++)
            {
                changeRow = true;
                for (int j = 0; j < dimensions.collumn; j++) {
                    if (!changeRow)
                    {
                        if (colorBuffer == color.white) colorBuffer = color.black;
                        else colorBuffer = color.white;
                    }
                    else changeRow = false;
                  
                    
                   Fields[i, j] = new Field(i,j, colorBuffer);
                }
            }
            for (int i = 0; i < pieces.Count; i++)
            {
                tempPair = pieces[i].getLocation();
                Fields[tempPair.row, tempPair.collumn].setPiece(pieces[i]);
            }
        }
        private String returnPieceSymbol(Piece inputPiece)
        {
            if (inputPiece == null) return "";
            String pieceType = inputPiece.GetType().Name;
            if (pieceType == "Knight") return "k";
            else return pieceType.Substring(0, 1);
        }
        private void setupFieldColors() {
            for (int i = 0; i < dimensions.row; i++)
            {
                for (int j = 0; j < dimensions.collumn; j++) {
                    DisplayFieldMatrix[i, j].BackColor = convertColor(Fields[i,j].getColor());
                }
            }
        }

        private Color convertColor(color inputcolor) {
            if (inputcolor == color.white) return Color.Wheat;
            else return Color.SaddleBrown;
        }
        public void renderFields()
        {
            Piece tempPiece;
           
            for (int i = 0; i < dimensions.row; i++)
            {
                for (int j = 0; j < dimensions.collumn; j++)
                {
                    tempPiece = Fields[i, j].getPiece();
                    if (tempPiece != null)
                    {
                        if (tempPiece.getColor() == color.white) DisplayFieldMatrix[i, j].ForeColor = Color.White;
                        else DisplayFieldMatrix[i, j].ForeColor = Color.Black;
                    }
                    DisplayFieldMatrix[i, j].Text = returnPieceSymbol(Fields[i, j].getPiece());
                }
            }
        }
    }
}