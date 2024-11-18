using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct coordPair {
            public int xOffset, yOffset;
        };

        public enum directions { 
            Up,
            UpRight,
            Right,
            RightDown,
            Down,
            DownLeft,
            Left,
            LeftUp
        }
            public coordPair convertDirectionToOffset(directions direction)
            {
                coordPair tempCoordPair;
                switch (direction)
                {
                    case directions.Up:
                        tempCoordPair.xOffset = 0;
                        tempCoordPair.yOffset = 1;
                        break;
                    case directions.UpRight:
                        tempCoordPair.xOffset = 1;
                        tempCoordPair.yOffset = 1;
                        break;
                    case directions.Right:
                        tempCoordPair.xOffset = 1;
                        tempCoordPair.yOffset = 0;
                        break;
                    case directions.RightDown:
                        tempCoordPair.xOffset = 1;
                        tempCoordPair.yOffset = -1;
                        break;
                    case directions.Down:
                        tempCoordPair.xOffset = 0;
                        tempCoordPair.yOffset = -1;
                        break;
                    case directions.DownLeft:
                        tempCoordPair.xOffset = -1;
                        tempCoordPair.yOffset = -1;
                        break;
                    case directions.Left:
                        tempCoordPair.xOffset = -1;
                        tempCoordPair.yOffset = 0;
                        break;
                    case directions.LeftUp:
                        tempCoordPair.xOffset = -1;
                        tempCoordPair.yOffset = 1;
                        break;
                    default:
                        tempCoordPair.xOffset = 0;
                        tempCoordPair.yOffset = 0;
                        break;
                }
                return tempCoordPair;
            }
        class MoveOffsets {
            public bool isRecurring;
            public directions moveDirection;
            public coordPair moveOffset;
        }

        class Board {
            private Label[,] DisplayFieldMatrix;
            private Field[,] FieldMatrix;
            public Board()
            {
                this.DisplayFieldMatrix = new Label[8, 8];
            }
            public void setDisplayFieldMatrix(Label[,] DisplayFieldMatrix ) {
                this.DisplayFieldMatrix = DisplayFieldMatrix;
            }
        }

        class Field {
            private bool isOccupied;
            private bool isWhite;
            public Field(int normedFieldIndex) {
                if (normedFieldIndex % 2 == 0) isWhite = false;
                else isWhite = true;
                isOccupied = false;
            }
            public void changeOccupancy(bool inputOccupancy) { this.isOccupied = inputOccupancy; }
            public bool returnOccupancy() { return this.isOccupied; }
        }

        abstract class Piece {
            protected bool pinned;
            protected coordPair location;
            protected MoveOffsets[] moveOffsets;
            public abstract void calculateDirections();
            public bool returnPin() { return this.pinned;}
            public void setPinned(bool pinned) { this.pinned = pinned; }

        }
        class Pawn : Piece {
            private bool isFirstMove;
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[1];
                moveOffsets[0].moveDirection = directions.Up;
                //maybe itll be smarter to initiate an array of objects moveOffset with each individual parameter
                //rather than one object containing arrays, which is quite cumsy
            }
            public bool IsMyFirstMove() { return this.isFirstMove;}
        }

        class Rook : Piece {
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[4];

                for (int i = 0; i < 4; i++) { moveOffsets[i].isRecurring = true; }

                moveOffsets[0].moveDirection = directions.Up;
                moveOffsets[1].moveDirection = directions.Right;
                moveOffsets[2].moveDirection = directions.Down;
                moveOffsets[3].moveDirection = directions.Left;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
