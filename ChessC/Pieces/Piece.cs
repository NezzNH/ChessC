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
        protected List<MoveOffset> moveOffsets;
        protected MoveField[] possibleMoveLocations; //just make sure this is always filtered for claim offset types. if it is ( i think so )
                                                     //then adding claims to each pieces internal data structure will SIGNIFICANTLY speed up a
                                                     //possibly n^3 algorithm that would kill performance
        protected color pieceColor;
        protected int moveCounter;
        protected Piece(coordPair location, color pieceColor, bool pinned = false, bool isRecurringMovePiece = true, bool moveCalcIsUpdated = true, int moveCount = 0)
        {
            this.moveCounter = moveCount;
            this.pinned = pinned;
            this.location = location;
            this.pieceColor = pieceColor;
            this.dimensions.row = 8;
            this.dimensions.collumn = 8;
            this.isRecurringMovePiece = isRecurringMovePiece;
            this.moveOffsets = new List<MoveOffset>();
            this.calculateDirections();
            this.moveCalcIsUpdated = moveCalcIsUpdated;
        }
        protected Piece()
        {
            this.moveCounter = 0;
            this.moveOffsets = new List<MoveOffset>();
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
            this.moveCalcIsUpdated = true;
        }

        public void setLocation(coordPair location) { this.location = location; }
        public coordPair getLocation() { return this.location; }
        public abstract void calculateDirections();
        public bool returnPin() { return this.pinned; }
        public void setPinned(bool pinned) { this.pinned = pinned; }
        public void setColor(color color) { this.pieceColor = color; }
        public color getColor() { return this.pieceColor; }
        public void setMoveCalcUpdate(bool input) { this.moveCalcIsUpdated = input; }
        public bool getMoveCalcUpdate() { return this.moveCalcIsUpdated; }
        public bool isRecurrPiece() { return this.isRecurringMovePiece; }
        public void incrementMoveCount() { this.moveCounter++; }
        public int getMoveCount() { return this.moveCounter; }

        public void setInternalMoveFields(MoveField[] input) { input = this.possibleMoveLocations; }

        protected coordPair convertDirectionToOffset(directions direction)
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
            if (input.row > dimensions.row - 1 || input.row < 0 || input.collumn > dimensions.collumn - 1 || input.collumn < 0) return false;
            else return true;
        }

        public MoveField[] returnAllPossibleMoves() {

            if (possibleMoveLocations != null && !moveCalcIsUpdated) return possibleMoveLocations;

            MoveField currentMoveField;
            List<MoveField> moveLocations = new List<MoveField>();

            if (isRecurringMovePiece)
            {
                currentMoveField.offsetType = OffsetType.MoveAndAttackOffset; // TO-DO: possible fix later to allow for combined move mode custom pieces, discrete and raycast
                bool[] rayDepletionArray = new bool[moveOffsets.Count];
                int offset = 0, depletedRayCounter = 0;
                bool areAllRaysDepleted = false;
                coordPair rayLoc, tempDisp;

                while (!areAllRaysDepleted)
                {
                    for (int i = 0; i < moveOffsets.Count; i++)
                    {
                        rayLoc = location;
                        do
                        {
                            tempDisp = convertDirectionToOffset(moveOffsets[i].moveDirection);
                            rayLoc.row += tempDisp.row;
                            rayLoc.collumn += tempDisp.collumn;

                            if (isWithinBounds(rayLoc)) {
                                currentMoveField.coords = rayLoc;
                                moveLocations.Add(currentMoveField);
                            }
                            else rayDepletionArray[i] = true; // true means ray is depleted
                        } while (!rayDepletionArray[i]);
                        depletedRayCounter++;
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
                    if (isWithinBounds(hitLoc)) {
                        currentMoveField.coords = hitLoc;
                        currentMoveField.offsetType = moveOffsets[i].offsetType;
                        moveLocations.Add(currentMoveField);
                    }
                    
                }
            }

            int finalArraySize = moveLocations.Count;
            MoveField[] finalArray = new MoveField[finalArraySize];
            finalArray = moveLocations.ToArray();
            moveCalcIsUpdated = false;
            possibleMoveLocations = finalArray; //hold a copy in case the user requests the moves again and the piece hasnt moved
            return finalArray;
        }
    }
}