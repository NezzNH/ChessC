using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessC.DataTypes
{
    struct MoveOffset
    {
        public coordPair moveOffset;
        public directions moveDirection;
        public OffsetType offsetType;
        public bool isRecurring;
    }

    enum OffsetType
    {
        Null,
        Claim,
        MoveAndAttackOffset,
        AttackOffset,
        MoveOffset,
        ConditionalOffset
    }
}
