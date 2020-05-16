using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace myGame
{
    class Player
    {
        public bool game_is_over, game_is_paused, goUp, goDown, goLeft, goRight;
        public int playerVel;
        public int player_x, player_y;
        public PictureBox player_img;

        public Player(PictureBox player_img)
        {
            this.player_img = player_img;
        }

        public void move_player()
        {
            // these allow the player to be moved via the keyboard
            // also it does not allaw the player to go over the screen

            if (goUp == true && player_img.Top >= 10)
            {
                player_img.Top -= playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goDown == true && player_img.Top <= 700)
            {
                player_img.Top += playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goRight == true && player_img.Left < 730)
            {
                player_img.Left += playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }

            if (goLeft == true && player_img.Left > 0)
            {
                player_img.Left -= playerVel;
                Console.WriteLine("{0} , {1}", player_x, player_y);
            }
        }
    }
}
