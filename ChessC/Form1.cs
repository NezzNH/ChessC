using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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

        public struct coordPair
        {
            public int xOffset, yOffset;
        };

        public enum directions
        {
            Up,
            UpRight,
            Right,
            RightDown,
            Down,
            DownLeft,
            Left,
            LeftUp
        }

        public enum color { 
            white,black
        }

        class Player {
            private color plyrcolor;
            Piece[] graveyard;
            public Player() {
                plyrcolor = color.white;
                graveyard = new Piece[16];
            }

            public color getColor() { return this.plyrcolor; }
            public void setColor(color plyrcolor) {  this.plyrcolor = plyrcolor; }
        }
        class Game
        {
            Player[] players;

        }

        class Match { 
            
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
        class MoveOffsets
        {
            public bool isRecurring;
            public directions moveDirection;
            public coordPair moveOffset;
        }

        class Board
        {
            private Label[,] DisplayFieldMatrix;
            private Field[,] FieldMatrix;
            private Piece[] PieceArray;
            private coordPair[] attackedFieldArray;
            public Board()
            {
                this.DisplayFieldMatrix = new Label[8, 8];
                PieceArray = new Piece[4];
            }
            public void setDisplayFieldMatrix(Label[,] DisplayFieldMatrix)
            {
                this.DisplayFieldMatrix = DisplayFieldMatrix;
            }
        }

        class Field
        {
            private bool isOccupied;
            private coordPair location;
            private bool isWhite;
            private bool isAttacked;
            public Field(int normedFieldIndex)
            {
                if (normedFieldIndex % 2 == 0) isWhite = false;
                else isWhite = true;
                isOccupied = false;
            }
            public void changeOccupancy(bool inputOccupancy) { this.isOccupied = inputOccupancy; }
            public bool returnOccupancy() { return this.isOccupied; }
        }

        abstract class Piece
        {
            protected directions[] tempDirectionsGlobalArray = {directions.Up, directions.UpRight, directions.Right, directions.RightDown,
                                                  directions.Down, directions.DownLeft, directions.Left, directions.LeftUp}; //temp for queen writing convention
            protected bool pinned;
            protected coordPair location;
            protected MoveOffsets[] moveOffsets;
            public abstract void calculateDirections();
            public bool returnPin() { return this.pinned; }
            public void setPinned(bool pinned) { this.pinned = pinned; }

        }
        class Pawn : Piece
        {
            private bool isFirstMove;
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[1];
                moveOffsets[0].moveDirection = directions.Up;
            }
            public bool IsMyFirstMove() { return this.isFirstMove; }
        }

        class Bishop : Piece {
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[4];

                for (int i = 0; i < 4; i++) moveOffsets[i].isRecurring = true;

                moveOffsets[0].moveDirection = directions.UpRight;
                moveOffsets[1].moveDirection = directions.RightDown;
                moveOffsets[2].moveDirection = directions.DownLeft;
                moveOffsets[3].moveDirection = directions.LeftUp;
            }
        }

        class Queen : Piece {
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[8];

                for (int i = 0; i < 8; i++) { 
                    moveOffsets[i].isRecurring = true;
                    moveOffsets[i].moveDirection = tempDirectionsGlobalArray[i];
                } 
            }
        }

        class Knight : Piece {
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[8];

                for (int i = 0; i < 8; i++) moveOffsets[i].isRecurring = false;

                coordPair tempPair;

                tempPair.xOffset = 1; tempPair.yOffset = 2;
                moveOffsets[0].moveOffset = tempPair;
                tempPair.xOffset = -1; tempPair.yOffset = 2;
                moveOffsets[1].moveOffset = tempPair;
                tempPair.xOffset = 2; tempPair.yOffset = 1;
                moveOffsets[2].moveOffset = tempPair;
                tempPair.xOffset = -2; tempPair.yOffset = 1;
                moveOffsets[3].moveOffset = tempPair;
                tempPair.xOffset = 2; tempPair.yOffset = -1;
                moveOffsets[4].moveOffset = tempPair;
                tempPair.xOffset = -2; tempPair.yOffset = -1;
                moveOffsets[5].moveOffset = tempPair;
                tempPair.xOffset = -1; tempPair.yOffset = -2;
                moveOffsets[6].moveOffset = tempPair;
                tempPair.xOffset = 1; tempPair.yOffset = -2;
                moveOffsets[7].moveOffset = tempPair;
            }
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

        class King : Piece {
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[8];

                for (int i = 0; i < 8; i++) {
                    moveOffsets[i].isRecurring = false;
                    moveOffsets[i].moveDirection = tempDirectionsGlobalArray[i];
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rook rook = new Rook();
            String rookType = Convert.ToString(rook.GetType()); //ChessC.Form1+Rook

            rookType = rookType.Substring(13, rookType.Length - 13); //differentiate types w/o additional memory req
            Debug.WriteLine(rookType);
        }
    }
}
