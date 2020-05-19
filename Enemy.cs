using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myGame
{
    class Enemy
    {
        private PictureBox boss_img;

        public Enemy(PictureBox boss_img)
        {
            this.boss_img = boss_img;
        }
    }
}
