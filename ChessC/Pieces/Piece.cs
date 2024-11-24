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
        protected bool pinned;
        protected coordPair location;
        protected MoveOffsets[] moveOffsets;
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
        public String getDebugInfo()
        {
            String debugString;

            debugString = $"Location:{location.collumn},{location.row}; moveOffsets:{moveOffsets}, pieceColor:{pieceColor}, pinned:{pinned}";

            return debugString;
        }
        public coordPair getLocation() { return this.location; }
        public abstract void calculateDirections();
        public bool returnPin() { return this.pinned; }
        public void setPinned(bool pinned) { this.pinned = pinned; }
        public void setColor(color color) { this.pieceColor = color; }
        public color getColor() { return this.pieceColor; }
    }

}
