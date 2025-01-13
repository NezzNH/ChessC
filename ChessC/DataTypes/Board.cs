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
        private directions[] tempDirectionsGlobalArray = {directions.Up, directions.UpRight, directions.Right, directions.RightDown,
                                                              directions.Down, directions.DownLeft, directions.Left, directions.LeftUp};
        private coordPair[] moveableFields, claimSpecificFields;
        private coordPair dimensions, selectedField;
        private color currentMoveColor;
        
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

        public enum inputType
        {
            selectedPiece,
            reselectedPiece,
            nullSelection,
            neutralMoveRequest,
            attackMoveRequest,
            longCastleRequest,
            shortCastleRequest,
            errorRequest
        }

        private void cycleMoveColors() {
            if (currentMoveColor == color.white) currentMoveColor = color.black;
            else currentMoveColor = color.white;
        }

        public inputType determineInputType(coordPair clickLocation) {
            Piece pieceOnLocation = getPieceAt(clickLocation);
            if (pieceOnLocation == null) {
                if (selectedPiece == null) return inputType.nullSelection;
                else if (fieldIsViableMove(clickLocation)) return inputType.neutralMoveRequest;
                else return inputType.nullSelection;
            }
            else
            {
                if (pieceOnLocation.getColor() == currentMoveColor)
                {
                    if (selectedPiece == null) return inputType.selectedPiece;
                    else return inputType.reselectedPiece;
                }
                else {
                    if (fieldIsViableMove(clickLocation) && selectedPiece != null) return inputType.attackMoveRequest;
                    else return inputType.nullSelection;
                }
            }
        }

        private bool isAlongSameDiagonal(coordPair previousField, coordPair currentField) {

            coordPair difference;
            difference.row = currentField.row - previousField.row;
            difference.collumn = currentField.collumn - previousField.collumn;

            if (difference.collumn > 1 || difference.collumn < -1 || difference.row < -1 ||  difference.row > 1) return false;
            else return true;
        }

        private coordPair[] filterMovesForOccupancy(coordPair[] inputFields) {
            List<coordPair> tempList = new List<coordPair> ();
            List<coordPair> claimSpecific = new List<coordPair>();
            if (selectedPiece.isRecurrPiece()) {
                bool encounteredPiece = false, newDiagonal = true;
                for (int i = 0; i < inputFields.Length; i++) {
                    if (!newDiagonal && !isAlongSameDiagonal(inputFields[i - 1], inputFields[i])) {
                        newDiagonal = true;
                        encounteredPiece = false;
                    } 
                    else newDiagonal = false;

                    if (encounteredPiece && !newDiagonal) continue;

                    Piece selectedPiece = Fields[inputFields[i].row, inputFields[i].collumn].getPiece();

                    if (selectedPiece != null)
                    {
                        encounteredPiece = true;
                        if (selectedPiece.getColor() == currentMoveColor) claimSpecific.Add(inputFields[i]);
                        else tempList.Add(inputFields[i]);
                    }
                    else tempList.Add(inputFields[i]);
                }
            }
            else
            {
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (Fields[inputFields[i].row, inputFields[i].collumn].getPiece() == null ||
                        Fields[inputFields[i].row, inputFields[i].collumn].getPiece().getColor() != selectedPiece.getColor()) tempList.Add(inputFields[i]);
                }
            }
            coordPair[] result = tempList.ToArray();
            claimSpecificFields = claimSpecific.ToArray();
            return result;
        }

        private void highlightMoveFields() {
            for (int i = 0; i < moveableFields.Length; i++) {
                DisplayFieldMatrix[moveableFields[i].row, moveableFields[i].collumn].BackColor = Color.Green;
            }
        }

        private void unhighlightMoveFields() {
            for (int i = 0; i < moveableFields.Length; i++) {
                DisplayFieldMatrix[moveableFields[i].row, moveableFields[i].collumn].BackColor = convertColor(Fields[moveableFields[i].row, moveableFields[i].collumn].getColor());            
            }
        }

        public String moveDebugInfo() {
            String outputString = "";

            outputString += $"moveType: {currentInputType}\n";
            switch (currentInputType) {
                case inputType.reselectedPiece:
                case inputType.selectedPiece:
                    outputString += $"{selectedPiece.GetType().Name}\n";
                    outputString += $"moveCount:{selectedPiece.getMoveCount()}\n";
                    if (currentInputType == inputType.selectedPiece || currentInputType == inputType.reselectedPiece)
                    {
                        if (moveableFields.Length > 0)
                        {
                            outputString += "moveableFields:\n";
                            for (int i = 0; i < moveableFields.Length; i++)
                            {
                                if (i > 0) outputString += ",";
                                outputString += $"[{moveableFields[i].row}, {moveableFields[i].collumn}]";
                            }
                        }
                    }
                    break;
                    
                default:
                    outputString = "inputType error";
                    break;
            }
            outputString += "\nClaims:";
                    outputString += $"Field:{selectedField.row}, {selectedField.collumn}\n";
                    Claim[] claimArray = Fields[selectedField.row, selectedField.collumn].getClaims();
                    if (claimArray != null) for (int i = 0; i < claimArray.Length; i++) outputString += $"{i}: claimant:{claimArray[i].claimant.GetType().Name}" +
                                $"loc:{claimArray[i].claimant.getLocation().row}, {claimArray[i].claimant.getLocation().collumn}\n";  
            return outputString;
        }

        public void receiveClick(coordPair clickLocation) {
            currentInputType = determineInputType(clickLocation);
            selectedField = clickLocation;
            switch (currentInputType) {
                case inputType.reselectedPiece:
                case inputType.selectedPiece:
                    selectedPiece = getPieceAt(clickLocation);
                    unhighlightMoveFields();
                    if (selectedPiece.GetType().Name == "Pawn") selectedPiece.calculateDirections();
                    moveableFields = selectedPiece.returnAllPossibleMoves();
                    moveableFields = filterMovesForOccupancy(moveableFields);
                    highlightMoveFields();
                    break;
                case inputType.attackMoveRequest:
                case inputType.neutralMoveRequest:
                    unhighlightMoveFields();
                    movePiece(selectedPiece, clickLocation);
                    renderFields();
                    break;
                default:
                    break;
            }
        }
        
        private void movePiece(Piece inputPiece, coordPair location) {
            if (currentInputType == inputType.attackMoveRequest) {
                Piece attackedPiece = getPieceAt(location);
                removeAllClaimsOf(attackedPiece);
                pieces.Remove(attackedPiece);
                Fields[location.row, location.collumn].setPiece(null);
            }
            coordPair selectedPieceCurLocation = selectedPiece.getLocation();
            removeAllClaimsOf(inputPiece);
            Fields[selectedPieceCurLocation.row, selectedPieceCurLocation.collumn].setPiece(null);
            selectedPiece.setLocation(location);
            selectedPiece.setMoveCalcUpdate(true);
            Fields[location.row, location.collumn].setPiece(selectedPiece);
            selectedPiece.incrementMoveCount();
            selectedPiece.returnAllPossibleMoves();
            addClaims(inputPiece);

            cycleMoveColors();
            selectedPiece = null;
            currentInputType = inputType.nullSelection;
        }

        private void removeAllClaimsOf(Piece inputPiece) {
            coordPair[] oldClaimFields = inputPiece.returnAllPossibleMoves();
            for (int i = 0; i < oldClaimFields.Length; i++) Fields[oldClaimFields[i].row, oldClaimFields[i].collumn].removeClaimOf(inputPiece);
        }

        private void addClaims(Piece inputPiece)
        {
            coordPair[] fieldsToClaim = inputPiece.returnAllPossibleMoves();
            for (int i = 0; i < fieldsToClaim.Length; i++) Fields[fieldsToClaim[i].row, fieldsToClaim[i].collumn].addClaim(inputPiece);
            //for (int i = 0; i < claimSpecificFields.Length; i++) Fields[claimSpecificFields[i].row, claimSpecificFields[i].collumn].addClaim(inputPiece);
            claimSpecificFields = new coordPair[0]; //make sure its empty!!!!
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