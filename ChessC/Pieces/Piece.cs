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
        protected bool pinned, isRecurringMovePiece;
        protected coordPair location;
        protected MoveOffsets[] moveOffsets;
        protected coordPair[] possibleMoveLocations;
        protected color pieceColor;
        protected Piece(coordPair location, color pieceColor, bool pinned = false)
        {
            this.pinned = pinned;
            this.location = location;
            this.pieceColor = pieceColor;
        }
        protected Piece()
        {
            this.pinned = false;
            coordPair tempPair;
            tempPair.collumn = 0;
            tempPair.row = 0;
            this.location = tempPair;
            this.pieceColor = color.white;
        }

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

        private bool isWithinBounds(coordPair input) {
            if (input.row > 7 || input.row < 0 || input.collumn > 7 || input.collumn < 0) return false;
            else return true;
        }

        public coordPair[] returnAllPossibleMoves() {
            List<coordPair> moveLocations = new List<coordPair>();
            if (isRecurringMovePiece) {
                bool[] rayDepletionArray = new bool[moveOffsets.Length];
                for (int i = 0; i < moveOffsets.Length; i++) rayDepletionArray[i] = false; //TO-DO : PROBABLY REDUNDANT
                int offset = 0, depletedRayCounter = 0;
                bool areAllRaysDepleted = false;
                coordPair rayLoc, tempDisp;
                while (!areAllRaysDepleted) {
                    for (int i = 0; i < moveOffsets.Length; i++) {
                        rayLoc = location;
                        if (!rayDepletionArray[i])
                        {
                            tempDisp = convertDirectionToOffset(moveOffsets[i].moveDirection);
                            rayLoc.row += tempDisp.row * offset;
                            rayLoc.collumn += tempDisp.collumn * offset;
                            if (isWithinBounds(rayLoc))
                            {
                                moveLocations.Add(rayLoc);
                            }
                            else rayDepletionArray[i] = true; // true means ray is depleted
                        }
                        else depletedRayCounter++;
                    }
                    if (depletedRayCounter == 8) areAllRaysDepleted = true;
                    else offset++;
                }
            }

            int finalArraySize = moveLocations.Count;
            coordPair[] finalArray = new coordPair[finalArraySize];
            finalArray = moveLocations.ToArray();
            return finalArray;
        }
        public coordPair getLocation() { return this.location; }
        public abstract void calculateDirections();
        public bool returnPin() { return this.pinned; }
        public void setPinned(bool pinned) { this.pinned = pinned; }
        public void setColor(color color) { this.pieceColor = color; }
        public color getColor() { return this.pieceColor; }
    }

}
