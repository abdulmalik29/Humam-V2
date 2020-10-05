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
using System.Threading;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Drawing.Drawing2D;
//using MySql.Data.MySqlClient;

namespace myGame
{
    public partial class Form1 : Form
    {
        //bool game_is_paused;
        public int score, death_counter;
        private Player player;
        private bool is_game_over;
        Enemy boss;

        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 900;
            death_counter = 0;
            player = new Player(player_img);
            boss = new Enemy(boss_img);
            //reset_game();
            game_start_menu();
        }

        private void game_start_menu()
        {

            BackColor = Color.WhiteSmoke;
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    control.Visible = true;
                }
                else
                {
                    control.Visible = false;
                }
            }
        }
        private void mainGameTimer(object sender, EventArgs e) // the main loop for the game which have most of the ruls 
        {
            player.move_player();
            check_for_overlapping();
            check_if_player_left_boundries();
            //location_debugger();

            void check_if_player_left_boundries()
            {
                if (player_img.Left <= (100 - player.playerVel) || (player_img.Left > 750 + player.playerVel) || player_img.Top <= (100 - player.playerVel) || player_img.Top >= (750 + player.playerVel)) // to check if the player have left the initial game boundaries
                {
                    Debug.WriteLine("you lost");
                    gameOver();
                }
            }
            void location_debugger() // a function to check if Is_player_... functions work
            {
                if (player.is_player_TopLeft() == true)
                {
                    Debug.WriteLine("top left");
                }
                else if (player.is_player_TopRight() == true)
                {
                    Debug.WriteLine("top right");
                }
                else if (player.is_player_BottomLeft() == true)
                {
                    Debug.WriteLine("bot left");
                }
                else if (player.is_player_BottomRight() == true)
                {
                    Debug.WriteLine("bot right");
                }
                else if (player.is_player_TopMiddle() == true)
                {
                    Debug.WriteLine("top midlle");
                }
                else if (player.is_player_BottomMiddle() == true)
                {
                    Debug.WriteLine("bot midlle");
                }
            }
        }


        private void key_is_down(object sender, KeyEventArgs e) // bind the key with a boolean variable
        {
            if (e.KeyCode == Keys.W)
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
        }

        private void check_for_overlapping() // a method that always checks if the player is on block or not
        {
            foreach (Control block in this.Controls) // used to check for other tags ??????
            {
                if (block is PictureBox)
                {
                    if ((string)block.Tag == "blocks")
                    {
                        if (player_img.Bounds.IntersectsWith(block.Bounds) && !block.Visible)  // to check if the player is on a visible block or not
                        {                                                                     // if they are not they fall over and lose the game
                            Debug.WriteLine("you lost 0000");
                            gameOver();
                        }
                        else if (player_img.Bounds.IntersectsWith(block.Bounds))
                        {
                            //Debug.WriteLine(block.Name);  // tool to name the block the the player standing on to make debuging easiers
                        }
                    }
                }
            }
        } 


        public async void reset_game() // a fucnction that reset the game or start the game
        {
            Focus();
            BackColor = Color.FromArgb(255, 153, 0);

            foreach (Control button in this.Controls)
            {
                if (button is Button)
                {
                    button.Visible = false;
                }
            }

            score_txt.Visible = true;
            is_game_over = false;

            score_txt.Text = "Score = 0";
            player.playerVel = 5;
            score = -10;

            int player_start_x = Random_Number.random_number_between(1, 7) * 100;
            int player_start_y = Random_Number.random_number_between(4, 7) * 100;
            player_img.Location = new Point(player_start_x, player_start_y);

            reset_blocks();
            extra_block.Visible = false;  // hide the extra block to be used latter

            game_timer.Start();
            await show_be_ready();
            start_gameAsync();

            async Task show_be_ready()
            {
                
                be_rady_panel.Visible = true;
                be_rady_panel.BringToFront();
                for (int counter = 3; counter > 0; counter--)
                {
                    be_rady_counter.Text = "" + counter;
                    await Task.Delay(1000);
                }
                be_rady_panel.Visible = false;
                be_rady_panel.SendToBack();
                be_rady_panel.Visible = false;
                be_rady_panel.SendToBack();
                Focus();

                //if

                boss_img.Location = new Point(200, 100);
                boss_label.Location = new Point(500, 130);

                boss_img.Visible = true;
                boss_label.Visible = true;

                boss_img.BringToFront();
                boss_label.BringToFront();
                

                if (death_counter == 0)
                {

                    for (int aCounter = 3; aCounter > 0; aCounter--)
                    {
                        if (aCounter == 3)
                        {
                            boss_label.Text = "Greeting Cube?!";

                        }
                        else if (aCounter == 2)
                        {
                            boss_label.Text = "or whatever you are...   (ಠ_ಠ)";
                        }
                        else if (aCounter == 1)
                        {
                            boss_label.Text = "just don't fall loser, HAHAHA";
                        }
                        await Task.Delay(2400);

                    }
                }
                else if (death_counter >= 1)
                {
                    int boss_random_text3 = Random_Number.random_number_between(1, 10);
                    int time_delay = 3500;


                    if (boss_random_text3 == 1)
                    {
                        boss_label.Text = "keep trying you WILL NOT WIN";
                    }

                    else if (boss_random_text3 == 2)
                    {
                        boss_label.Text = "So... , What are you? a sqare or a cube, something else?";
                    }

                    else if (boss_random_text3 == 3)
                    {
                        boss_label.Text = "Still trying to figure out what you are";
                    }

                    else if (boss_random_text3 == 4)
                    {
                        boss_label.Text = "If you keep playing you may get more intresting responses";
                    }

                    else if (boss_random_text3 == 5)
                    {
                        boss_label.Text = "Why you keep trying? Do you like the game?";
                    }

                    else if (boss_random_text3 == 6)
                    {
                        boss_label.Text = "Do you know how many attempts you have done? I lost count";
                    }

                    else if (boss_random_text3 == 7)
                    {
                        boss_label.Text = "well... if you are going to spend a lot of time here can you at least tell me your name";
                        time_delay = 4200;
                    }

                    else if (boss_random_text3 == 8)
                    {
                        boss_label.Text = "I think you have lost for " + death_counter + "  times btw";
                    }

                    else if (boss_random_text3 == 9)
                    {
                        boss_label.Text = "believe me you are not going to win";
                    }

                    else if (boss_random_text3 == 10)
                    {
                        boss_label.Text = "YOU ARE WORTHLESS";
                    }
                    await Task.Delay(time_delay);
                }


                boss_img.Visible = false;
                boss_label.Visible = false;
                await Task.Delay(500);

            }
        }

        private void reset_blocks()  // a method to returns all the blocks to its initial location and make them visible

        {
            block_a1.Location = new Point(100, 100);
            block_a2.Location = new Point(200, 100);
            block_a3.Location = new Point(300, 100);
            block_a4.Location = new Point(400, 100);
            block_a5.Location = new Point(500, 100);
            block_a6.Location = new Point(600, 100);
            block_a7.Location = new Point(700, 100);

            block_b1.Location = new Point(100, 200);
            block_b2.Location = new Point(200, 200);
            block_b3.Location = new Point(300, 200);
            block_b4.Location = new Point(400, 200);
            block_b5.Location = new Point(500, 200);
            block_b6.Location = new Point(600, 200);
            block_b7.Location = new Point(700, 200);

            block_c1.Location = new Point(100, 300);
            block_c2.Location = new Point(200, 300);
            block_c3.Location = new Point(300, 300);
            block_c4.Location = new Point(400, 300);
            block_c5.Location = new Point(500, 300);
            block_c6.Location = new Point(600, 300);
            block_c7.Location = new Point(700, 300);

            block_d1.Location = new Point(100, 400);
            block_d2.Location = new Point(200, 400);
            block_d3.Location = new Point(300, 400);
            block_d4.Location = new Point(400, 400);
            block_d5.Location = new Point(500, 400);
            block_d6.Location = new Point(600, 400);
            block_d7.Location = new Point(700, 400);

            block_e1.Location = new Point(100, 500);
            block_e2.Location = new Point(200, 500);
            block_e3.Location = new Point(300, 500);
            block_e4.Location = new Point(400, 500);
            block_e5.Location = new Point(500, 500);
            block_e6.Location = new Point(600, 500);
            block_e7.Location = new Point(700, 500);

            block_f1.Location = new Point(100, 600);
            block_f2.Location = new Point(200, 600);
            block_f3.Location = new Point(300, 600);
            block_f4.Location = new Point(400, 600);
            block_f5.Location = new Point(500, 600);
            block_f6.Location = new Point(600, 600);
            block_f7.Location = new Point(700, 600);

            block_g1.Location = new Point(100, 700);
            block_g2.Location = new Point(200, 700);
            block_g3.Location = new Point(300, 700);
            block_g4.Location = new Point(400, 700);
            block_g5.Location = new Point(500, 700);
            block_g6.Location = new Point(600, 700);
            block_g7.Location = new Point(700, 700);


            extra_block.Location = new Point(-50, 700);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
        }


        private void increase_score(int number)
        {
            score += number;
            score_txt.Text = "Score = " + score;
        }

        private async Task start_gameAsync()
        {
            int delay1 = Convert.ToInt32(score * .9);  // the amount of time that the blocks disappear for
            int delay2 = Convert.ToInt32(score * .3); //  the amount of time that the blocks shake for
            int delay_between_stages = 3900;
            int rand;
            stage_1(0, -100);
            await Task.Delay(delay_between_stages);
            while (score < 9000 && !is_game_over) // in this part, the game do two tasks, it generate a random number after every stage
            {                                           // the second task is to randomly choose the next stage depending on the player location
                rand = Random_Number.random_number_between(1, 4);
                delay_between_stages = 3900 - 150;

                if (player.is_player_TopLeft() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_20(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_13(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_7(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_0(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;

                    }
                }
                else if (player.is_player_TopMiddle() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_19(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_12(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_6(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_2(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                    }
                }
                else if (player.is_player_TopRight() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_21(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_14(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_9(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_1(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                    }
                }
                else if (player.is_player_BottomLeft() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_20(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_17(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_8(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_5(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                    }
                }
                else if (player.is_player_BottomMiddle() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_22(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_18(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_23(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_3(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                    }
                }
                else if (player.is_player_BottomRight() == true)
                {
                    switch (rand)
                    {
                        case 1:
                            stage_21(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 2:
                            stage_15(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 3:
                            stage_11(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                        case 4:
                            stage_4(delay2, delay1);
                            rand = await stages_func(delay_between_stages, rand);
                            break;
                    }
                }
                else
                {
                    stage_0(0, score);
                    await Task.Delay(delay_between_stages);
                }
                
            }
            //break;
            async Task<int> stages_func(int delay, int num)
            {
                await Task.Delay(delay);
                rand = Random_Number.random_number_between(1, 4);
                Debug.WriteLine(num);
                return rand;
            }
        }

        public void stage_0(int t1, int t2) // locks like l l l removes 2 4 6 ▥
        {
            Blocks.shake_7(block_a2, block_b1, block_c2, block_d1, block_e2, block_f1, block_g2, t1, t2);
            Blocks.shake_7(block_a4, block_b3, block_c4, block_d3, block_e4, block_f3, block_g4, t1, t2);
            Blocks.shake_7(block_a6, block_b5, block_c6, block_d5, block_e6, block_f5, block_g6, t1, t2);
            Blocks.shake_3(block_b7, block_d7, block_f7, t1, t2);
            increase_score(10);

        }
        public void stage_1(int t1, int t2) // locks like ≡ removes a c e g 
        {
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_7(block_c1, block_c2, block_c3, block_c4, block_c5, block_c6, block_c7, t1, t2);
            Blocks.shake_7(block_e1, block_e2, block_e3, block_e4, block_e5, block_e6, block_e7, t1, t2);
            Blocks.shake_7(block_g1, block_g2, block_g3, block_g4, block_g5, block_g6, block_g7, t1, t2);
            increase_score(10);

        }

        public void stage_2(int t1, int t2) //  removes the entire top side (a b c d)
        {
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_7(block_b1, block_b2, block_b3, block_b4, block_b5, block_b6, block_b7, t1, t2);
            Blocks.shake_7(block_c1, block_c2, block_c3, block_c4, block_c5, block_c6, block_c7, t1, t2);
            Blocks.shake_7(block_d1, block_d2, block_d3, block_d4, block_d5, block_d6, block_d7, t1, t2);
            Blocks.shake_3(block_e1, block_f1, block_g1, t1, t2);
            Blocks.shake_3(block_e2, block_f2, block_g2, t1, t2);
            Blocks.shake_3(block_e6, block_f6, block_g6, t1, t2);
            Blocks.shake_3(block_e7, block_f7, block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_3(int t1, int t2) //  removes the entire bottom side  (d e f g)
        {
            Blocks.shake_7(block_d1, block_d2, block_d3, block_d4, block_d5, block_d6, block_d7, t1, t2);
            Blocks.shake_7(block_e1, block_e2, block_e3, block_e4, block_e5, block_e6, block_e7, t1, t2);
            Blocks.shake_7(block_f1, block_f2, block_f3, block_f4, block_f5, block_f6, block_f7, t1, t2);
            Blocks.shake_7(block_g1, block_g2, block_g3, block_g4, block_g5, block_g6, block_g7, t1, t2);
            increase_score(10);
        }


        public void stage_4(int t1, int t2) // remove the entire right side ◧(4 5 6 7)
        {
            Blocks.shake_7(block_a4, block_b4, block_c4, block_d4, block_e4, block_f4, block_g4, t1, t2);
            Blocks.shake_7(block_a5, block_b5, block_c5, block_d5, block_e5, block_f5, block_g5, t1, t2);
            Blocks.shake_7(block_a6, block_b6, block_c6, block_d6, block_e6, block_f6, block_g6, t1, t2);
            Blocks.shake_7(block_a7, block_b7, block_c7, block_d7, block_e7, block_f7, block_g7, t1, t2);
            Blocks.shake_3(block_a2, block_a3, block_a4, t1, t2);
            Blocks.shake_3(block_g2, block_g3, block_g4, t1, t2);
            Blocks.shake_7(block_a1, block_b1, block_c1, block_d1, block_e1, block_f1, block_g1, t1, t2);
            increase_score(10);
        }
        public void stage_5(int t1, int t2) // remove the entire left side side  ◨  (1 2 3 4)
        {
            Blocks.shake_7(block_a1, block_b1, block_c1, block_d1, block_e1, block_f1, block_g1, t1, t2);
            Blocks.shake_7(block_a2, block_b2, block_c2, block_d2, block_e2, block_f2, block_g2, t1, t2);
            Blocks.shake_7(block_a3, block_b3, block_c3, block_d3, block_e3, block_f3, block_g3, t1, t2);
            Blocks.shake_7(block_a4, block_b4, block_c4, block_d4, block_e4, block_f4, block_g4, t1, t2);
            increase_score(10);
        }
        public void stage_6(int t1, int t2) // themove the middel part ( 3 4 5) 
        {
            Blocks.shake_7(block_a3, block_b3, block_c3, block_d3, block_e3, block_f3, block_g3, t1, t2);
            Blocks.shake_7(block_a4, block_b4, block_c4, block_d4, block_e4, block_f4, block_g4, t1, t2);
            Blocks.shake_7(block_a5, block_b5, block_c5, block_d5, block_e5, block_f5, block_g5, t1, t2);
            Blocks.shake_2(block_a2, block_a6, t1, t2);
            Blocks.shake_2(block_g2, block_g6, t1, t2);
            increase_score(10);
        }


        public void stage_7(int t1, int t2) //  remove top left and bottom right  □ ■
        {                                                                      // ■ □
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_b1, block_b2, block_b3, t1, t2);
            Blocks.shake_7(block_b4, block_c1, block_c2, block_c3, block_c4, block_d1, block_d2, t1, t2);
            Blocks.shake_7(block_d3, block_d5, block_d6, block_d7, block_e4, block_e5, block_e6, t1, t2);
            Blocks.shake_7(block_e7, block_f4, block_f5, block_f6, block_f7, block_g4, block_g5, t1, t2);
            Blocks.shake_3(block_g6, block_d4, block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_8(int t1, int t2) // 2 remove top right and bottom left ■ □
        {                                                                      // □ ■
            Blocks.shake_7(block_a4, block_a5, block_a6, block_a7, block_b4, block_b5, block_b6, t1, t2);
            Blocks.shake_7(block_b7, block_c4, block_c5, block_c6, block_c7, block_d1, block_d2, t1, t2);
            Blocks.shake_7(block_d3, block_d4, block_d5, block_d6, block_d7, block_e1, block_e2, t1, t2);
            Blocks.shake_7(block_e3, block_e4, block_f1, block_f2, block_f3, block_f4, block_g1, t1, t2);
            Blocks.shake_3(block_g2, block_g3, block_g4, t1, t2);
            increase_score(10);
        }


        public void stage_9(int t1, int t2) // locks like ▼ 
        {                                              // ▲
            Blocks.shake_7(block_a1, block_b1, block_c1, block_d1, block_e1, block_f1, block_g1, t1, t2);
            Blocks.shake_5(block_f2, block_e2, block_d2, block_c2, block_b2, t1, t2);
            Blocks.shake_3(block_c3, block_d3, block_e3, t1, t2);
            Blocks.shake_1(block_d4, t1, t2);
            Blocks.shake_3(block_e5, block_d5, block_c5, t1, t2);
            Blocks.shake_5(block_b6, block_c6, block_d6, block_e6, block_f6, t1, t2);
            Blocks.shake_7(block_a7, block_b7, block_c7, block_d7, block_e7, block_f7, block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_10(int t1, int t2) // locks like ▶◀
        { 
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_5(block_b2, block_b3, block_b4, block_b5, block_b6, t1, t2);
            Blocks.shake_3(block_c3, block_c4, block_c5, t1, t2);
            Blocks.shake_1(block_d4, t1, t2);
            Blocks.shake_3(block_e5, block_e4, block_e3, t1, t2);
            Blocks.shake_5(block_f2, block_f3, block_f4, block_f5, block_f6, t1, t2);
            Blocks.shake_7(block_g7, block_g6, block_g5, block_g4, block_g3, block_g2, block_g1, t1, t2);
            increase_score(10);
        }

        public void stage_11(int t1, int t2) // locks like ⯐ 
        {
            Blocks.shake_7(block_a1, block_a2, block_a3, block_b1, block_b2, block_b3, block_c1, t1, t2);
            Blocks.shake_7(block_c2, block_c3, block_e1, block_e2, block_e3, block_f1, block_f2, t1, t2);
            Blocks.shake_7(block_e5, block_e6, block_e7, block_f5, block_f6, block_f7, block_g5, t1, t2);
            Blocks.shake_7(block_g1, block_g2, block_g3, block_g6, block_f3, block_a5, block_a6, t1, t2);
            Blocks.shake_7(block_a7, block_b5, block_b6, block_b7, block_c5, block_c6, block_c7, t1, t2);
            Blocks.shake_1(block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_12(int t1, int t2) // removes switzerland flag 
        {
            Blocks.shake_7(block_a3, block_b3, block_c3, block_d3, block_e3, block_f3, block_g3, t1, t2);
            Blocks.shake_7(block_a4, block_b4, block_c4, block_d4, block_e4, block_f4, block_g4, t1, t2);
            Blocks.shake_7(block_a5, block_b5, block_c5, block_d5, block_e5, block_f5, block_g5, t1, t2);
            Blocks.shake_3(block_c1, block_d1, block_e1, t1, t2);
            Blocks.shake_3(block_d2, block_e2, block_c2, t1, t2);
            Blocks.shake_3(block_c6, block_d6, block_e6, t1, t2);
            Blocks.shake_3(block_d7, block_e7, block_c7, t1, t2);

            increase_score(10);
        }
        public void stage_13(int t1, int t2) // locks like a diamond in the middle ❖
        {
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_3(block_b1, block_b2, block_b3, t1, t2);
            Blocks.shake_3(block_b5, block_b6, block_b7, t1, t2);
            Blocks.shake_1(block_d4, t1, t2);
            Blocks.shake_3(block_c2, block_c1, block_d1, t1, t2);
            Blocks.shake_3(block_c6, block_c7, block_d7, t1, t2);
            Blocks.shake_3(block_f5, block_f6, block_f7, t1, t2);
            Blocks.shake_7(block_e1, block_e2, block_e6, block_e7, block_f1, block_f2, block_f3, t1, t2);
            Blocks.shake_7(block_g1, block_g2, block_g3, block_g4, block_g5, block_g6, block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_14(int t1, int t2) // locks like ◳
        {
            Blocks.shake_5(block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_5(block_b7, block_b6, block_b5, block_b4, block_b3, t1, t2);
            Blocks.shake_5(block_c3, block_c4, block_c5, block_c6, block_c7, t1, t2);
            Blocks.shake_5(block_d7, block_d6, block_d5, block_d4, block_d3, t1, t2);
            Blocks.shake_5(block_e3, block_e4, block_e5, block_e6, block_e7, t1, t2);
            increase_score(10);
        }
        
        public void stage_15(int t1, int t2) // locks like ◲
        {
            Blocks.shake_5(block_c3, block_c4, block_c5, block_c6, block_c7, t1, t2);
            Blocks.shake_5(block_d7, block_d6, block_d5, block_d4, block_d3, t1, t2);
            Blocks.shake_5(block_e3, block_e4, block_e5, block_e6, block_e7, t1, t2);
            Blocks.shake_5(block_f7, block_f6, block_f5, block_f4, block_f3, t1, t2);
            Blocks.shake_5(block_g3, block_g4, block_g5, block_g6, block_g7, t1, t2);
            increase_score(10);
        }
        public void stage_16(int t1, int t2) // locks like ◰
        {
            Blocks.shake_5(block_a1, block_a2, block_a3, block_a4, block_a5, t1, t2);
            Blocks.shake_5(block_b5, block_b4, block_b3, block_b2, block_b1, t1, t2);
            Blocks.shake_5(block_c1, block_c2, block_c3, block_c4, block_c5, t1, t2);
            Blocks.shake_5(block_d5, block_d4, block_d3, block_d2, block_d1, t1, t2);
            Blocks.shake_5(block_e1, block_e2, block_e3, block_e4, block_e5, t1, t2);
            increase_score(10);
        }
        public void stage_17(int t1, int t2) // locks like ◱
        {
            Blocks.shake_5(block_c1, block_c2, block_c3, block_c4, block_c5, t1, t2);
            Blocks.shake_5(block_d5, block_d4, block_d3, block_d2, block_d1, t1, t2);
            Blocks.shake_5(block_e1, block_e2, block_e3, block_e4, block_e5, t1, t2);
            Blocks.shake_5(block_f5, block_f4, block_f3, block_f2, block_f1, t1, t2);
            Blocks.shake_5(block_g1, block_g2, block_g3, block_g4, block_g5, t1, t2);
            increase_score(10);
        }

        public void stage_18(int t1, int t2) // locks like 🔲
        {
            Blocks.shake_3(block_b3, block_b4, block_b5, t1, t2);
            Blocks.shake_5(block_c2, block_c3, block_c4, block_c5, block_c6, t1, t2);
            Blocks.shake_5(block_d2, block_d3, block_d4, block_d5, block_d6, t1, t2);
            Blocks.shake_5(block_e2, block_e3, block_e4, block_e5, block_e6, t1, t2);
            Blocks.shake_3(block_f3, block_f4, block_f5,t1, t2);
            increase_score(10);
        }

        public void stage_19(int t1, int t2) // locks like / \
        //                                                 \ /
        {
            Blocks.shake_5(block_a1, block_a2, block_a4, block_a6, block_a7, t1, t2);
            Blocks.shake_5(block_b1, block_b3, block_b4, block_b5, block_b7, t1, t2);
            Blocks.shake_5(block_c2, block_c3, block_c4, block_c5, block_c6, t1, t2);
            Blocks.shake_7(block_d1, block_d2, block_d3, block_d4, block_d5, block_d6, block_d7, t1, t2);
            Blocks.shake_5(block_e2, block_e3, block_e4, block_e5, block_e6, t1, t2);
            Blocks.shake_5(block_f1, block_f3, block_f4, block_f5, block_f7, t1, t2);
            Blocks.shake_5(block_g1, block_g2, block_g4, block_g6, block_g7, t1, t2);
            increase_score(10);
        }

        public void stage_20(int t1, int t2) // random blocks
        {
            Blocks.shake_7(block_a1, block_a2, block_a3, block_a4, block_a5, block_a6, block_a7, t1, t2);
            Blocks.shake_5(block_b1, block_b3, block_b3, block_b4, block_b5, t1, t2);
            Blocks.shake_1(block_b7, t1, t2);

            Blocks.shake_5(block_c1, block_c2, block_c3, block_c4, block_c5, t1, t2);
            Blocks.shake_1(block_c6, t1, t2);

            Blocks.shake_7(block_d2, block_d2, block_d3, block_d4, block_d5, block_d6, block_d7, t1, t2);
            Blocks.shake_7(block_e1, block_e2, block_e3, block_e4, block_e5, block_e6, block_e7, t1, t2);

            Blocks.shake_5(block_f1, block_f2, block_f3, block_f5, block_f6, t1, t2);
            Blocks.shake_1(block_f7, t1, t2);

            Blocks.shake_5(block_g1, block_g3, block_g4, block_g5, block_g6, t1, t2);
            Blocks.shake_1(block_g7, t1, t2);

            increase_score(30);
        }

        public void stage_21(int t1, int t2) // locks like ◙
        {
            Blocks.shake_3(block_a1, block_a2, block_a6, t1, t2); Blocks.shake_1(block_a7, t1, t2);
            Blocks.shake_5(block_b1, block_b3, block_b4, block_b5, block_b7, t1, t2);
            Blocks.shake_5(block_c2, block_c3, block_c4, block_c5, block_c6, t1, t2);
            Blocks.shake_5(block_d2, block_d3, block_d4, block_d5, block_d6, t1, t2);
            Blocks.shake_5(block_e2, block_e3, block_e4, block_e5, block_e6, t1, t2);
            Blocks.shake_5(block_f1, block_f3, block_f4, block_f5, block_f7, t1, t2);
            Blocks.shake_3(block_g1, block_g2, block_g6, t1, t2); Blocks.shake_1(block_g7, t1, t2);
            increase_score(10);
        }

        public void stage_22(int t1, int t2) // locks like 🌀
        {
            Blocks.shake_1(block_a7,t1,t2);
            Blocks.shake_5(block_b1, block_b2, block_b3, block_b4, block_b5, t1, t2); Blocks.shake_1(block_b7, t1, t2);
            Blocks.shake_3(block_c1, block_c5, block_c7, t1, t2);
            Blocks.shake_3(block_d1, block_d3, block_d5, t1, t2); Blocks.shake_1(block_d7, t1, t2);
            Blocks.shake_5(block_e1, block_e3, block_e4, block_e5, block_e7, t1, t2);
            Blocks.shake_2(block_f1, block_f7, t1, t2);
            Blocks.shake_7(block_g1, block_g2, block_g3, block_g4, block_g5, block_g6, block_g7, t1, t2);
            increase_score(10);
        }

        public void stage_23(int t1, int t2) // locks like 8
        {
            Blocks.shake_3(block_a1, block_a2, block_a6, t1, t2); Blocks.shake_1(block_a7, t1, t2);
            Blocks.shake_5(block_b1, block_b3, block_b4, block_b5, block_b7, t1, t2);
            Blocks.shake_5(block_c1, block_c3, block_c4, block_c5, block_c7, t1, t2);
            Blocks.shake_3(block_d1, block_d2, block_d6, t1, t2); Blocks.shake_1(block_d7, t1, t2);
            Blocks.shake_5(block_e1, block_e3, block_e4, block_e5, block_e7, t1, t2);
            Blocks.shake_5(block_f1, block_f3, block_f4, block_f5, block_f7, t1, t2);
            Blocks.shake_3(block_g1, block_g2, block_g6, t1, t2); Blocks.shake_1(block_g7, t1, t2);
            increase_score(10);
        }

        public void level1_final(int t1, int t2) // locks like 
        {

        }

        private void start_game_button_Click(object sender, EventArgs e)
        {
            reset_game();
            this.Focus();

        }
        private async void gameOver()
        {
            is_game_over = true;
            death_counter++;
            game_timer.Stop();
            await Task.Delay(750);

            show_game_over_screenAsync();

            play_again_button.Enabled = false;
            home_play_again.Enabled = false;
            Cursor = Cursors.WaitCursor;
            await Task.Delay(1250);
            play_again_button.Enabled = true;
            home_play_again.Enabled = true;
            Cursor = Cursors.Default;

            async Task show_game_over_screenAsync()
            {

               
                game_over_panel.Location = new Point(200, 400);
                game_over_panel.Visible = true;
                game_over_panel.BringToFront();

                boss_img.Location = new Point(200, 100);
                boss_label.Location = new Point(500, 130);

                boss_img.Visible = true;
                boss_label.Visible = true;

                boss_img.BringToFront();
                boss_label.BringToFront();

                int boss_random_text = Random_Number.random_number_between(1, 10);

                if (boss_random_text == 1)
                {
                    boss_label.Text = "You got Only " + score + " HAHAHAHA";
                }
                else if (boss_random_text == 2) 
                {
                    boss_label.Text = "DON'T PLAY THIS GAME EVER AGAIN";
                }
                else if (boss_random_text == 3)
                {
                    boss_label.Text = "I could get better score than yours even if i was BLINDFOLD";
                }
                else if (boss_random_text == 4)
                {
                    boss_label.Text = "JUST QUIT THE GAME LOSER";
                }
                else if (boss_random_text == 5)
                {
                    boss_label.Text = "Did you lose INTENTIONALLY!!!";
                }
                else if (boss_random_text == 6)
                {
                    boss_label.Text = "YOU NEED THOUSANDS OF YEARS TO BEAT THIS";
                }
                else if (boss_random_text == 7)
                {
                    boss_label.Text = "even my CAT got a better score than yours !!!";
                }
                else if (boss_random_text == 8)
                {
                    boss_label.Text = "you know you have to use the keyboard to move????";
                }
                else if (boss_random_text == 9)
                {
                    boss_label.Text = score + ",  is your family proud of you?";
                }
                else if (boss_random_text == 10)
                {
                    boss_label.Text = score + " ONLY,  WHAT ARE YOU DOING?!!!";
                }


            }

        }
        private void play_again_button_Click(object sender, EventArgs e)
        {
            boss_img.Visible = false;
            boss_label.Visible = false;
            game_over_panel.Visible = false;
            BringToFront();
            reset_game();
            Focus();
        }

        private void home_play_again_Click(object sender, EventArgs e)
        {
            boss_img.Visible = false;
            boss_label.Visible = false;
            game_over_panel.Visible = false;
            BringToFront();
            game_start_menu();
            Focus();
        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void instructions_play_button_Click(object sender, EventArgs e)
        {
            instructions_panel.Visible = false;
            BringToFront();
            reset_game();
            Focus();
        }

        private void instructions_home_button_Click(object sender, EventArgs e)
        {
            instructions_panel.Visible = false;
            BringToFront();
            game_start_menu();
            Focus();
        }

        private void instructions_button_Click(object sender, EventArgs e)
        {
            instructions_panel.Location = new Point(0, 0);
            instructions_panel.Visible = true;
            instructions_panel.BringToFront();
        }
    }

}