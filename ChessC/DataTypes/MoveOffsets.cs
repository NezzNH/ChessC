using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessC.DataTypes
{
    class MoveOffsets
    {
        public bool isRecurring;
        public directions moveDirection;
        public coordPair moveOffset;

        public MoveOffsets() {
            this.moveDirection = directions.Up;
            this.isRecurring = false;
        }
        public MoveOffsets(directions moveDirection, coordPair moveOffset, bool isRecurring) {
            this.moveDirection = moveDirection;
            this.moveOffset = moveOffset;
            this.isRecurring = isRecurring;
        } //TO-DO: Change into struct for better recall speeds and memory management. (remember those 64 bytes mister)
    }
}
