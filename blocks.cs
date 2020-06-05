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
        public static async void shake_7(PictureBox block1, PictureBox block2, PictureBox block3, PictureBox block4, PictureBox block5, PictureBox block6, PictureBox block7, int delay_time, int stages_delay)
        {
            shake_5(block1, block2, block3, block4, block5, delay_time, stages_delay);
            shake_2(block6, block7, delay_time, stages_delay);

        }
        public static async void shake_5(PictureBox block1, PictureBox block2, PictureBox block3, PictureBox block4, PictureBox block5, int delay_time, int stages_delay)
        {
            shake_2(block1, block2, delay_time, stages_delay);
            shake_2(block3, block4, delay_time, stages_delay);
            shake_1(block5, delay_time, stages_delay);
        }

        public static async void shake_3(PictureBox block1, PictureBox block2, PictureBox block3, int delay_time, int stages_delay)
        {
            shake_2(block1, block2, delay_time, stages_delay);
            shake_1(block3, delay_time, stages_delay);
        }
        

        public static async void shake_2(PictureBox block1, PictureBox block2, int delay_time, int stages_delay) // a method that shakes two block and hide them
        {
            for (int counter = 0; counter < 2; counter++)
            {
                block1.Top++;
                await Task.Delay(210 - delay_time); //  the amount of time that the blocks shake for

                block1.Top--;
                await Task.Delay(210 - delay_time);

                block2.Left++;
                await Task.Delay(210 - delay_time);

                block2.Left--;
                await Task.Delay(210 - delay_time);

                block1.Top++;
                block2.Left++;
                await Task.Delay(210 - delay_time);

                block1.Top--;
                block2.Left--;

                await Task.Delay(210 - delay_time);
            }

            block1.Visible = false; block2.Visible = false;
            await Task.Delay(1150 - stages_delay); // the amount of time that the blocks disappear for
            block1.Visible = true; block2.Visible = true;
        }

        public static async void shake_1(PictureBox block1, int delay_time, int stages_delay) // a method thats shake one block and hide it
        {
            for (int counter = 0; counter < 2; counter++)
            {
                block1.Top++;
                await Task.Delay(210 - delay_time); //  the amount of time that the blocks shake for

                block1.Top--;
                await Task.Delay(210 - delay_time);

                block1.Top++;
                await Task.Delay(630 - delay_time);
                
                block1.Top--;
                await Task.Delay(210 - delay_time);
            }

            block1.Visible = false;
            await Task.Delay(1150 - stages_delay);  // the amount of time that the blocks disappear for
            block1.Visible = true;
        }

    }

}
/*            
for  (int counter = 0;  counter< 2; counter++)
{
block1.Top++;
block3.Top++;
block5.Top++;
block7.Top++;
await Task.Delay(210 - delay_time);

block1.Top--;
block3.Top--;
block5.Top--;
block7.Top--;
await Task.Delay(210 - delay_time);

block2.Left++;
block4.Left++;
block6.Left++;
await Task.Delay(210 - delay_time);

block2.Left--;
block4.Left--;
block6.Left--;
await Task.Delay(210 - delay_time);

block1.Top++;
block3.Top++;
block5.Top++;
block7.Top++;
block2.Left++;
block4.Left++;
block6.Left++;
await Task.Delay(210 - delay_time);

block1.Top--;
block3.Top--;
block5.Top--;
block7.Top--;
block2.Left--;
block4.Left--;
block6.Left--;
await Task.Delay(210 - delay_time);
}


block1.Visible = false; block2.Visible = false; block3.Visible = false; block4.Visible = false; block5.Visible = false; block6.Visible = false; block7.Visible = false;
await Task.Delay(1150 - stages_delay);
block1.Visible = true; block2.Visible = true; block3.Visible = true; block4.Visible = true; block5.Visible = true; block6.Visible = true; block7.Visible = true;
*/