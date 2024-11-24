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
            public int collumn, row;
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
            private int score, deadPieces;
            Piece[] graveyard;
            public Player() {
                plyrcolor = color.white;
                graveyard = new Piece[16];
                deadPieces = 0;
            }

            public Player(color plyrcolor, int deadPieces = 0) {
                this.plyrcolor = plyrcolor;
                graveyard = new Piece[16];
                this.deadPieces = deadPieces;
            }

            public color getColor() { return this.plyrcolor; }
            public void setColor(color plyrcolor) {  this.plyrcolor = plyrcolor; }
            public int getScore() { return score; }
            public void setScore(int score) { this.score = score; }
        }

        public coordPair convertDirectionToOffset(directions direction)
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
        class MoveOffsets
        {
            public bool isRecurring;
            public directions moveDirection;
            public coordPair moveOffset;
        }

        class Board
        {
            private Label[,] DisplayFieldMatrix;
            private Field[,] Fields = new Field[8, 8];
            private Piece[] Pieces = new Piece[32];
            private coordPair[] attackedFields;
            public Board()
            {
                this.DisplayFieldMatrix = new Label[8, 8];
                Pieces = new Piece[32];
            }
            public void setDisplayFieldMatrix(Label[,] DisplayFieldMatrix) {
                this.DisplayFieldMatrix = DisplayFieldMatrix;
            }
            public void initBoard() {
                initPieces(); initFields();
            }

            private void initPieces()
            {
                coordPair tempPair;
                color tempColor = color.white;
                tempPair.collumn = 0;
                tempPair.row = 0;
                Pieces[0] = new Rook(tempPair, tempColor);
                tempPair.collumn = 1;
                Pieces[1] = new Knight(tempPair, tempColor);
                tempPair.collumn = 2;
                Pieces[2] = new Bishop(tempPair, tempColor);
                tempPair.collumn = 3;
                Pieces[3] = new Queen(tempPair, tempColor);
                tempPair.collumn = 4;
                Pieces[4] = new King(tempPair, tempColor);
                tempPair.collumn = 5;
                Pieces[5] = new Bishop(tempPair, tempColor);
                tempPair.collumn = 6;
                Pieces[6] = new Knight(tempPair, tempColor);
                tempPair.collumn = 7;
                Pieces[7] = new Rook(tempPair, tempColor);
                tempPair.row = 1;
                for (int i = 0; i < 8; i++)
                {
                    tempPair.collumn = i;
                    Pieces[i+8] = new Pawn(tempPair, tempColor);
                }
                tempPair.row = 6;
                tempColor = color.black;
                for (int i = 0; i < 8; i++)
                {
                    tempPair.collumn = i;
                    Pieces[i+16] = new Pawn(tempPair, tempColor);
                }
                tempPair.row = 7;
                tempPair.collumn = 0;
                Pieces[24] = new Rook(tempPair, tempColor);
                tempPair.collumn = 1;
                Pieces[25] = new Knight(tempPair, tempColor);
                tempPair.collumn = 2;
                Pieces[26] = new Bishop(tempPair, tempColor);
                tempPair.collumn = 3;
                Pieces[27] = new Queen(tempPair, tempColor);
                tempPair.collumn = 4;
                Pieces[28] = new King(tempPair, tempColor);
                tempPair.collumn = 5;
                Pieces[29] = new Bishop(tempPair, tempColor);
                tempPair.collumn = 6;
                Pieces[30] = new Knight(tempPair, tempColor);
                tempPair.collumn = 7;
                Pieces[31] = new Rook(tempPair, tempColor);
            }

            private void initFields() {
                int row, collumn;
                coordPair tempPair;
                color colorBuffer = color.black;
                for (int i = 0; i < 64; i++)
                {
                    collumn = i % 8; row = i / 8;
                    if (!hasRowChanged(i)) {
                        if (colorBuffer == color.white) colorBuffer = color.black;
                        else colorBuffer = color.white;
                    }
                    Fields[row, collumn] = new Field(i, colorBuffer);
                }
                for (int i = 0; i < Pieces.Length; i++)
                {
                    tempPair = Pieces[i].getLocation();
                    Fields[tempPair.row, tempPair.collumn].setPiece(Pieces[i]);
                }
            }

            private bool hasRowChanged(int i) {
                int currentRow, lastRow;
                currentRow = i / 8;
                lastRow = (i - 1) / 8;
                if (currentRow != lastRow) return true;
                else return false;
            }

            public void updateFieldStatus() {
                
            } //requires variable LastUpdatedPiece, or some other approach

            private String returnPieceSymbol(Piece inputPiece) {
                if (inputPiece == null) return "";
                String pieceType = inputPiece.GetType().Name;
                switch (pieceType) {
                    case "Knight":
                        return "k";
                    case "King":
                        return "K";
                    default:
                        return pieceType.Substring(0, 1);
                }
            }

            public void renderFields() {
                int collumn, row;
                Piece tempPiece;
                for (int i = 0; i < 64; i++) {
                    collumn = i % 8; row = i / 8;
                    tempPiece = Fields[row, collumn].getPiece();
                    if (Fields[row, collumn].getColor() == color.white) DisplayFieldMatrix[row, collumn].BackColor = Color.Wheat;
                    else DisplayFieldMatrix[row, collumn].BackColor = Color.SaddleBrown;
                    if (tempPiece != null) {
                        if (tempPiece.getColor() == color.white) DisplayFieldMatrix[row, collumn].ForeColor = Color.White;
                        else DisplayFieldMatrix[row, collumn].ForeColor = Color.Black;
                    }
                } //TO-DO: move this part to a new function in the future (after testing) that only gets run on Form1.Load
                for (int i = 0; i < 64; i++) {
                    collumn = i % 8; row = i / 8; //again, this would be consolidated in one for loop if this were to remain one function
                    DisplayFieldMatrix[row,collumn].Text = returnPieceSymbol(Fields[row, collumn].getPiece());
                }
            } 

        }

        class Field
        {
            private bool isOccupied;
            private coordPair location;
            private color fieldColor;
            private bool isAttacked;
            private Piece pieceOnField;
            public Field(int normedFieldIndex, color fieldColor)
            {
                location.collumn = normedFieldIndex % 8;
                location.row = normedFieldIndex / 8;
                this.fieldColor = fieldColor;
            }
            public void changeOccupancy(bool inputOccupancy) { this.isOccupied = inputOccupancy; }
            public bool returnOccupancy() { return this.isOccupied; }
            public Piece getPiece() { return this.pieceOnField; }
            public void setPiece(Piece pieceOnField) { this.pieceOnField = pieceOnField; }
            public color getColor() { return this.fieldColor; }
        }

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
            protected Piece() {
                this.pinned = false;
                coordPair tempPair;
                tempPair.collumn = 0;
                tempPair.row = 0;
                this.location = tempPair;
                this.pieceColor = color.white;
            }
            public String getDebugInfo() {
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
        class Pawn : Piece
        {
            private bool isFirstMove;
            public Pawn() : base() {}
            public Pawn(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) {}

            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[1];
                if (this.pieceColor == color.white) moveOffsets[0].moveDirection = directions.Up;
                else moveOffsets[0].moveDirection = directions.Down;
            }
            public bool IsMyFirstMove() { return this.isFirstMove; }
        }

        class Bishop : Piece {

            public Bishop(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
            public Bishop() : base() { }
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
            public Queen(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
            public Queen() : base() { }
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
            public Knight(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
            public Knight() : base() { }
            public override void calculateDirections()
            {
                moveOffsets = new MoveOffsets[8];

                for (int i = 0; i < 8; i++) moveOffsets[i].isRecurring = false;

                coordPair tempPair;

                tempPair.collumn = 1; tempPair.row = 2;
                moveOffsets[0].moveOffset = tempPair;
                tempPair.collumn = -1; tempPair.row = 2;
                moveOffsets[1].moveOffset = tempPair;
                tempPair.collumn = 2; tempPair.row = 1;
                moveOffsets[2].moveOffset = tempPair;
                tempPair.collumn = -2; tempPair.row = 1;
                moveOffsets[3].moveOffset = tempPair;
                tempPair.collumn = 2; tempPair.row = -1;
                moveOffsets[4].moveOffset = tempPair;
                tempPair.collumn = -2; tempPair.row = -1;
                moveOffsets[5].moveOffset = tempPair;
                tempPair.collumn = -1; tempPair.row = -2;
                moveOffsets[6].moveOffset = tempPair;
                tempPair.collumn = 1; tempPair.row = -2;
                moveOffsets[7].moveOffset = tempPair;
            }
        }

    class Rook : Piece {
            public Rook(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
            public Rook() : base() { }
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
            public King(coordPair location, color pieceColor, bool pinned = false) : base(location, pieceColor, pinned) { }
            public King() : base() { }
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
            Label[,] displayFieldMatrix = { {a1, b1, c1, d1, e1, f1, g1, h1 },
                                            {a2, b2, c2, d2, e2, f2, g2, h2 },
                                            {a3, b3, c3, d3, e3, f3, g3, h3 },
                                            {a4, b4, c4, d4, e4, f4, g4, h4 },
                                            {a5, b5, c5, d5, e5, f5, g5, h5 },
                                            {a6, b6, c6, d6, e6, f6, g6, h6 },
                                            {a7, b7, c7, d7, e7, f7, g7, h7 },
                                            {a8, b8, c8, d8, e8, f8, g8, h8 } }; //is flipped around the row axis

            Board board = new Board();
            board.setDisplayFieldMatrix(displayFieldMatrix);

            board.initBoard();
            board.renderFields();
        }

        private void h1_Click(object sender, EventArgs e)
        {

        }
    }
}
