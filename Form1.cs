using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myGame
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, game_is_over, game_is_paused;
        int score, playerVel, player_x, player_y;
 

        //int test = 0;
        //const int topOfTheScreen = 0, bottomOfTheScreen = 700;



        public Form1()
        {
            InitializeComponent();
            reset_game();
        }


        private void mainGameTimer(object sender, EventArgs e) // the main loop for the game which have most of the ruls 
        {

            move_player();
            check_for_overlapping();
            //shake_12(block_a1);


            if (player.Left <= (100 - playerVel) || (player.Left >= 650 + playerVel) || player.Top <= (100 - playerVel) || player.Top >= (650 - playerVel)) // to check if the player have left the initial game boundaries

            {
                Console.WriteLine("you lost");
                gameOver();
            }


            if (is_player_TopLeft())
            {
                Console.WriteLine("player is in the top left");

            }else if (is_player_TopRight())
            {
                Console.WriteLine("player is in the top right");

            }else if (is_player_BottomLeft())
            {
                Console.WriteLine("player is in the bottom left");

            }else if (is_player_BottomRight())
            {
                Console.WriteLine("player is in the bottom right");
            }

        }


        private void key_is_down(object sender, KeyEventArgs e) // bind the key with a boolean variable
        {
            if(e.KeyCode == Keys.W)
            {
                goUp = true;
            }

            if (e.KeyCode == Keys.S)
            {
                goDown = true;
            }

            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = true;
            }

        }
        private void key_is_up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                goDown = false;
            }

            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
        }

        private void check_for_overlapping()
        {
            foreach (Control Block in this.Controls) // used to check for other tags ??????
            {
                if (Block is PictureBox)
                {
                    if ((string)Block.Tag == "blocks")
                    {
                        if (player.Bounds.IntersectsWith(Block.Bounds) && !Block.Visible)  // to check if the player is on a visible block or not
                        {                                                                  // if they are not they fall over and lose the game
                            Console.WriteLine("you lost 0000");
                        }
                    }
                }
            }
        }

        private void move_player()
        {
            // this method allow the player to be moved via the keyboard
            // also it does not allaw the player to go over the screen

            int player_x = player.Left;
            int player_y = player.Top;
            if (goUp == true && player.Top >= 10)
            {
                player.Top -= playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goDown == true && player.Top <= 700)
            {
                player.Top += playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goRight == true && player.Left < 730)
            {
                player.Left += playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }
        }

        private void reset_game() // a fucnction that reset the game and most of its virables
        {
            game_is_over = false;
            score_txt.Text = "Score = 0";
            score = 0;
            playerVel = 10;

            reset_blocks();
            player.Location = new Point(200, 200);
            extra_block.Visible = false;  // hide the extra block to be used latter
            game_timer.Start();
         }

        private void reset_blocks()  // a method to returns all the blocks to its initial location and make them visible

        {
            block_a1.Location = new Point(100, 100);
            block_a2.Location = new Point(100, 200);
            block_a3.Location = new Point(100, 300);
            block_a4.Location = new Point(100, 400);
            block_a5.Location = new Point(100, 500);
            block_a6.Location = new Point(100, 600);

            block_b6.Location = new Point(200, 600);
            block_b5.Location = new Point(200, 500);
            block_b4.Location = new Point(200, 400);
            block_b3.Location = new Point(200, 300);
            block_b2.Location = new Point(200, 200);
            block_b1.Location = new Point(200, 100);

            block_c6.Location = new Point(300, 600);
            block_c5.Location = new Point(300, 500);
            block_c4.Location = new Point(300, 400);
            block_c3.Location = new Point(300, 300);
            block_c2.Location = new Point(300, 200);
            block_c1.Location = new Point(300, 100);

            block_d6.Location = new Point(400, 600);
            block_d5.Location = new Point(400, 500);
            block_d4.Location = new Point(400, 400);
            block_d3.Location = new Point(400, 300);
            block_d2.Location = new Point(400, 200);
            block_d1.Location = new Point(400, 100);

            block_e6.Location = new Point(500, 600);
            block_e5.Location = new Point(500, 500);
            block_e4.Location = new Point(500, 400);
            block_e3.Location = new Point(500, 300);
            block_e2.Location = new Point(500, 200);
            block_e1.Location = new Point(500, 100);

            block_f6.Location = new Point(600, 600);
            block_f5.Location = new Point(600, 500);
            block_f4.Location = new Point(600, 400);
            block_f3.Location = new Point(600, 300);
            block_f2.Location = new Point(600, 200);
            block_f1.Location = new Point(600, 100);

            extra_block.Location = new Point(750,700);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
        }

        private bool is_player_TopLeft()
        {
            if (player.Top < 400 && player.Left < 400)
            {
                return true;
            }
            return false;
        }
        private bool is_player_TopRight()
        {
            if (player.Top < 400 && player.Left > 400)
            {
                return true;
            }
            return false;
        }
        private bool is_player_BottomLeft()
        {
            if (player.Top >= 400 && player.Left <= 400)
            {
                return true;
            }
            return false;
        }
        private bool is_player_BottomRight()
        {
            if (player.Top >= 400 && player.Left > 400)
            {
                return true;
            }
             return false;
        }

        
        // a method used to shake the 12 blocks and remove them
        private void shake_12(Control block1, Control block2, Control block3, Control block4, Control block5, Control block6,
                              Control block7, Control block8, Control block9, Control block10, Control block11, Control block12)
        {
            block1.Top += 1; 
        }


        private void gameOver()
        {

        }

        private void pause_game()
        {
         //   game_timer.Stop();
        }
    }
}
