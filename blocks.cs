using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
// PictureBox block7, PictureBox block8, PictureBox block9, PictureBox block10, PictureBox block11, PictureBox block12
namespace myGame
{
    internal class Blocks
    {
        public static async void shake_6(PictureBox block1, PictureBox block2, PictureBox block3, PictureBox block4, PictureBox block5, PictureBox block6, int delay_time)
        {
            for  (int counter = 0;  counter < 2; counter++)
            {
                block1.Top++;
                block3.Top++;
                block5.Top++;
                await Task.Delay(200 - delay_time);

                block1.Top--;
                block3.Top--;
                block5.Top--;
                await Task.Delay(200 - delay_time);

                block2.Left++;
                block4.Left++;
                block6.Left++;
                await Task.Delay(200 - delay_time);

                block2.Left--;
                block4.Left--;
                block6.Left--;
                await Task.Delay(200 - delay_time);

                block1.Top++;
                block3.Top++;
                block5.Top++;
                block2.Left++;
                block4.Left++;
                block6.Left++;
                await Task.Delay(200 - delay_time);

                block1.Top--;
                block3.Top--;
                block5.Top--;
                block2.Left--;
                block4.Left--;
                block6.Left--;
                await Task.Delay(200 - delay_time);
            }

                //foreach (PictureBox box in )
                block1.Visible = false; block2.Visible = false; block3.Visible = false; block4.Visible = false; block5.Visible = false; block6.Visible = false;
                await Task.Delay(1000);
                block1.Visible = true; block2.Visible = true; block3.Visible = true; block4.Visible = true; block5.Visible = true; block6.Visible = true;

        }
    }
}
