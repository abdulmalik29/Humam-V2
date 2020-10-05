using System.Windows.Forms;
using System.Diagnostics;

namespace myGame
{
    class Player
    {
        public bool goUp, goDown, goLeft, goRight;
        private PictureBox player_img;
        public int playerVel;
        //public int score;

        public Player(PictureBox img)
        {
            this.player_img = img;
        }

        public void move_player() // this method allow the useer to move the player. also it does not allaw the player to go over the screen
        {
            int player_x = player_img.Left;
            int player_y = player_img.Top;
            if (goUp == true && player_img.Top >= 10)
            {
                player_img.Top -= playerVel;
                //Debug.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goDown == true && player_img.Top <= 800)
            {
                player_img.Top += playerVel;
                //player_img.Image = Properties.Resources.PikPng_com_blue_flame_png_840679;
                //Debug.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goRight == true && player_img.Left < 830)
            {
                player_img.Left += playerVel;
                //Debug.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goLeft == true && player_img.Left > 0)
            {
                player_img.Left -= playerVel;
                //Debug.WriteLine("{0} , {1}", player_x, player_y);
            }
        }

        public void change_player_img()
        {
         //player_img.Image = Properties.Resources.PikPng_com_blue_flame_png_840679;
        }

        public bool is_player_TopLeft()
        {
            if (player_img.Top < 450 && player_img.Left < 350)
            {
                return true;
            }
            return false;
        }
        public bool is_player_TopMiddle()
        {
            if (player_img.Top < 450 && player_img.Left >= 350 && player_img.Left < 550)
            {
                return true;
            }
            return false;
        }
        public bool is_player_TopRight()
        {
            if (player_img.Top < 450 && player_img.Left >= 550)
            {
                return true;
            }
            return false;
        }

        public bool is_player_BottomLeft()
        {
            if (player_img.Top >= 450 && player_img.Left < 350)
            {
                return true;
            }
            return false;
        }
        public bool is_player_BottomMiddle()
        {
            if (player_img.Top >= 450 && player_img.Left >= 350 && player_img.Left < 550)
            {
                return true;
            }
            return false;
        }
        public bool is_player_BottomRight()
        {
            if (player_img.Top >= 450 && player_img.Left >= 550)
            {
                return true;
            }
            return false;
        }
      
        


    }
}

