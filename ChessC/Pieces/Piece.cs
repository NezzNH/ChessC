using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessC.DataTypes;

namespace ChessC.Pieces
{
    abstract class Piece
    {
        protected directions[] tempDirectionsGlobalArray = {directions.Up, directions.UpRight, directions.Right, directions.RightDown,
                                                              directions.Down, directions.DownLeft, directions.Left, directions.LeftUp}; //temp for queen writing convention
        protected bool pinned, isRecurringMovePiece, moveCalcIsUpdated;
        protected coordPair location, dimensions;
        protected List<MoveOffsets> moveOffsets;
        protected coordPair[] possibleMoveLocations;
        protected color pieceColor;
        protected int positionInPiecesArray;
        protected Piece(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true, bool moveCalcIsUpdated = false)
        {
            this.pinned = pinned;
            this.location = location;
            this.pieceColor = pieceColor;
            this.dimensions.row = 8;
            this.dimensions.collumn = 8;
            this.isRecurringMovePiece = isRecurringMovePiece;
            this.moveOffsets = new List<MoveOffsets>();
            this.calculateDirections();
            this.moveCalcIsUpdated = moveCalcIsUpdated;
        }
        protected Piece()
        {
            this.moveOffsets = new List<MoveOffsets>();
            this.pinned = false;
            coordPair tempPair;
            tempPair.collumn = 0;
            tempPair.row = 0;
            this.location = tempPair;
            this.pieceColor = color.white;
            this.isRecurringMovePiece = true;
            tempPair.collumn = 8; tempPair.row = 8;
            this.dimensions = tempPair;
            this.calculateDirections();
            this.moveCalcIsUpdated = false;
        }

        public void setLocation(coordPair location) { this.location = location; }

        private coordPair convertDirectionToOffset(directions direction)
        {
            coordPair tempCoordPair;
            switch (direction)
            {
                case directions.Up:
                    tempCoordPair.collumn = 0;
                    tempCoordPair.row = 1;
                    break;
                case directions.UpRight:
                    tempCoordPair.collumn = 1;
                    tempCoordPair.row = 1;
                    break;
                case directions.Right:
                    tempCoordPair.collumn = 1;
                    tempCoordPair.row = 0;
                    break;
                case directions.RightDown:
                    tempCoordPair.collumn = 1;
                    tempCoordPair.row = -1;
                    break;
                case directions.Down:
                    tempCoordPair.collumn = 0;
                    tempCoordPair.row = -1;
                    break;
                case directions.DownLeft:
                    tempCoordPair.collumn = -1;
                    tempCoordPair.row = -1;
                    break;
                case directions.Left:
                    tempCoordPair.collumn = -1;
                    tempCoordPair.row = 0;
                    break;
                case directions.LeftUp:
                    tempCoordPair.collumn = -1;
                    tempCoordPair.row = 1;
                    break;
                default:
                    tempCoordPair.collumn = 0;
                    tempCoordPair.row = 0;
                    break;
            }
            return tempCoordPair;
        }
        public String getDebugInfo()
        {
            String debugString;

            debugString = $"Location:{location.collumn},{location.row}; moveOffsets:{moveOffsets}, pieceColor:{pieceColor}, pinned:{pinned}";

            return debugString;
        }

        protected bool isWithinBounds(coordPair input) {
            if (input.row > dimensions.row-1 || input.row < 0 || input.collumn > dimensions.collumn-1 || input.collumn < 0) return false;
            else return true;
        }

        public coordPair[] returnAllPossibleMoves() {
            if (possibleMoveLocations != null || moveCalcIsUpdated == false) return possibleMoveLocations;
            List<coordPair> moveLocations = new List<coordPair>();
            if (isRecurringMovePiece)
            {
                bool[] rayDepletionArray = new bool[moveOffsets.Count];
                int offset = 0, depletedRayCounter = 0;
                bool areAllRaysDepleted = false;
                coordPair rayLoc, tempDisp;

                while (!areAllRaysDepleted)
                {
                    for (int i = 0; i < moveOffsets.Count; i++)
                    {
                        rayLoc = location;
                        if (!rayDepletionArray[i])
                        {
                            tempDisp = convertDirectionToOffset(moveOffsets[i].moveDirection);
                            rayLoc.row += tempDisp.row * offset;
                            rayLoc.collumn += tempDisp.collumn * offset;

                            if (isWithinBounds(rayLoc)) moveLocations.Add(rayLoc);
                            else rayDepletionArray[i] = true; // true means ray is depleted
                        }
                        else depletedRayCounter++;
                    }
                    if (depletedRayCounter == moveOffsets.Count) areAllRaysDepleted = true;
                    else offset++;
                }
            }
            else {
                coordPair hitLoc;
                for (int i = 0; i < moveOffsets.Count; i++)
                {
                    hitLoc = location;
                    hitLoc.row += moveOffsets[i].moveOffset.row;
                    hitLoc.collumn += moveOffsets[i].moveOffset.collumn;
                    if (isWithinBounds(hitLoc)) moveLocations.Add(hitLoc);
                }
            }
            
            int finalArraySize = moveLocations.Count;
            coordPair[] finalArray = new coordPair[finalArraySize];
            finalArray = moveLocations.ToArray();
            moveCalcIsUpdated = false;
            possibleMoveLocations = finalArray; //hold a copy in case the user requests the moves again and the piece hasnt moved
            return finalArray;
        }

        public coordPair[] returnMoveAttemptDebug() {
            coordPair[] testArray = returnAllPossibleMoves();
            return testArray;
        }
        public coordPair getLocation() { return this.location; }
        public abstract void calculateDirections();
        public bool returnPin() { return this.pinned; }
        public void setPinned(bool pinned) { this.pinned = pinned; }
        public void setColor(color color) { this.pieceColor = color; }
        public color getColor() { return this.pieceColor; }
    }
}