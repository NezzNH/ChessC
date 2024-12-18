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
using ChessC.DataTypes;
using ChessC.Pieces;
using ChessC.Util;

namespace ChessC
{

    //TO-DO: Implement more rigorous move discrenment and a robust move handling pipeline
    //we must assume the user can input any number of clicks, and handling edge cases shouldnt be so
    //convoluted and hidden in small conditionals somewhere
    //it will also be useful for debugging purposes and the eventual implementation of AI into the mix

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Board board = new Board();
        coordPair clickLocation;

        public coordPair convertIndexToCoords(int input) {
            coordPair tempPair;
            tempPair.row = input / Constants.BOARD_ROW_COUNT; tempPair.collumn = input % Constants.BOARD_COLLUMN_COUNT;
            return tempPair;
        } //possibly belongs in Board
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

            
            board.setDisplayFieldMatrix(displayFieldMatrix);

            board.initBoard();
            board.renderFields();
        }

        private void a1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(0);
            board.receiveClick(clickLocation);
        }

        private void b1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(1);
            board.receiveClick(clickLocation);
        }

        private void c1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(2);
            board.receiveClick(clickLocation);
        }

        private void d1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(3);
            board.receiveClick(clickLocation);
        }

        private void e1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(4);
            board.receiveClick(clickLocation);
        }

        private void f1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(5);
            board.receiveClick(clickLocation);
        }

        private void g1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(6);
            board.receiveClick(clickLocation);
        }

        private void h1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(7);
            board.receiveClick(clickLocation);
        }

        //all this reorganization leaves us with only form related functions and init in this file. yay :3
    }
}