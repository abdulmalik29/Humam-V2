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
using System.Diagnostics;

namespace myGame
{
    public partial class Form1 : Form
    {
        //bool game_is_paused;
        int score;
        Player player;

        public Form1()
        {
            InitializeComponent();
            player = new Player(player_img);
            reset_game();
        }


        private void mainGameTimer(object sender, EventArgs e) // the main loop for the game which have most of the ruls 
        {

            player.move_player();
            check_for_overlapping();
            check_if_player_left_boundries();

            void check_if_player_left_boundries()
            {
                if (player_img.Left <= (100 - player.playerVel) || (player_img.Left >= 650 + player.playerVel) || player_img.Top <= (100 - player.playerVel) || player_img.Top >= (650 - player.playerVel)) // to check if the player have left the initial game boundaries

                {
                    Debug.WriteLine("you lost");
                    gameOver();
                }
            }
        }


        private void key_is_down(object sender, KeyEventArgs e) // bind the key with a boolean variable
        {
            if(e.KeyCode == Keys.W)
            {
                player.goUp = true;
            }

            if (e.KeyCode == Keys.S)
            {
                player.goDown = true;
            }

            if (e.KeyCode == Keys.A)
            {
                player.goLeft = true;
            }

            if (e.KeyCode == Keys.D)
            {
                player.goRight = true;
            }

        }

        private void key_is_up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                player.goUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                player.goDown = false;
            }

            if (e.KeyCode == Keys.A)
            {
                player.goLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                player.goRight = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                start_game();
            }
        }

        private void check_for_overlapping()
        {
            foreach (Control block in this.Controls) // used to check for other tags ??????
            {
                if (block is PictureBox)
                {
                    if ((string)block.Tag == "blocks")
                    {
                        if (player_img.Bounds.IntersectsWith(block.Bounds) && !block.Visible)  // to check if the player is on a visible block or not
                        {                                                                  // if they are not they fall over and lose the game
                            Debug.WriteLine("you lost 0000");
                            gameOver();
                        }
                    }
                }
            }
        } // a method that always checks if the player is on block or not


        private void reset_game() // a fucnction that reset the game and most of its virables
        {
            score_txt.Text = "Score = 0";
            score = 0;
            player.playerVel = 10;

            reset_blocks();
            player_img.Location = new Point(200, 200);
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

            block_b1.Location = new Point(200, 100);
            block_b2.Location = new Point(200, 200);
            block_b3.Location = new Point(200, 300);
            block_b4.Location = new Point(200, 400);
            block_b5.Location = new Point(200, 500);
            block_b6.Location = new Point(200, 600);

            block_c1.Location = new Point(300, 100);
            block_c2.Location = new Point(300, 200);
            block_c3.Location = new Point(300, 300);
            block_c4.Location = new Point(300, 400);
            block_c5.Location = new Point(300, 500);
            block_c6.Location = new Point(300, 600);

            block_d1.Location = new Point(400, 100);
            block_d2.Location = new Point(400, 200);
            block_d3.Location = new Point(400, 300);
            block_d4.Location = new Point(400, 400);
            block_d5.Location = new Point(400, 500);
            block_d6.Location = new Point(400, 600);

            block_e1.Location = new Point(500, 100);
            block_e2.Location = new Point(500, 200);
            block_e3.Location = new Point(500, 300);
            block_e4.Location = new Point(500, 400);
            block_e5.Location = new Point(500, 500);
            block_e6.Location = new Point(500, 600);

            block_f1.Location = new Point(600, 100);
            block_f2.Location = new Point(600, 200);
            block_f3.Location = new Point(600, 300);
            block_f4.Location = new Point(600, 400);
            block_f5.Location = new Point(600, 500);
            block_f6.Location = new Point(600, 600);

            extra_block.Location = new Point(750,700);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
        }

        private void gameOver()
        {
            //game_timer.Stop();
        }

        //private void pause_game(){game_timer.();}

        private void start_game()
        {
            stage_2();
        }

        public void stage_0() // lll
        {
            Blocks.shake_6(block_b1, block_b2, block_b3, block_b4, block_b5, block_b6, 0, -100);
            Blocks.shake_6(block_d1, block_d2, block_d3, block_d4, block_d5, block_d6, 0, -100);
            Blocks.shake_6(block_f1, block_f2, block_f3, block_f4, block_f5, block_f6, 0, -100);
        }

        public void stage_1() // ≡
        {
            Blocks.shake_6(block_a2, block_b2, block_c2, block_d2, block_e2, block_f2, 0, 0);
            Blocks.shake_6(block_a4, block_b4, block_c4, block_d4, block_e4, block_f4, 0, 0);
            Blocks.shake_6(block_a6, block_b6, block_c6, block_d6, block_e6, block_f6, 0, 0);
        }

        public void stage_2()
        {
            Blocks.shake_6(block_d1, block_d2, block_d3, block_d4, block_d5, block_d6, 0, 0);
            Blocks.shake_6(block_e1, block_e2, block_e3, block_e4, block_e5, block_e6, 0, 0);
            Blocks.shake_6(block_f1, block_f2, block_f3, block_f4, block_f5, block_f6, 0, 0);

        }
    }

}
