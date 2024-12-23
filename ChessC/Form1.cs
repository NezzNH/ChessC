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
            debugDisplayLabel.Visible = false;
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

        private void dbgViewCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (dbgViewCheckbox.Checked) debugDisplayLabel.Visible = true;
            else debugDisplayLabel.Visible = false;
        }

        private void a1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(0);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(1);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(2);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(3);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(4);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(5);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(6);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h1_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(7);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(8);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(9);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(10);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(11);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(12);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(13);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(14);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h2_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(15);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(16);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(17);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(18);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(19);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(20);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(21);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(22);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h3_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(23);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(24);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(25);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(26);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(27);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(28);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(29);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(30);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h4_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(31);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(32);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(33);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(34);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(35);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(36);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(37);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(38);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h5_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(39);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(40);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(41);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(42);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(43);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(44);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(45);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(46);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h6_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(47);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(48);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(49);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(50);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(51);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(52);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(53);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(54);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h7_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(55);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void a8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(56);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void b8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(57);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void c8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(58);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void d8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(59);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void e8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(60);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void f8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(61);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void g8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(62);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }

        private void h8_Click(object sender, EventArgs e)
        {
            clickLocation = convertIndexToCoords(63);
            board.receiveClick(clickLocation);
            debugDisplayLabel.Text = board.moveDebugInfo();
        }



        //all this reorganization leaves us with only form related functions and init in this file. yay :3
    }
}