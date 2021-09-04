using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace PACMAN
{
    class Player : Mob
    {
        public static System.Timers.Timer collisionTimer;
        public Player()
        {
            TimerInit();
            playerPos = position = 326;
            images = new int[] { 1, 2, 3, 4 };
            Move(position, 4);
        }

        private void TimerInit()
        {
            collisionTimer = new System.Timers.Timer();
            collisionTimer.Interval = 1;
            collisionTimer.Elapsed += CheckCollision;
            collisionTimer.Start();
        }

        public override void Move(Direction direction)
        {

            switch (direction)
            {
                case Direction.Down:
                    if (pbList[position + 20].BackColor != Color.Blue)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[position + 20].Image = imageList.Images[1];
                        position += 21;
                    }
                    break;
                case Direction.Left:
                    if (position == 190)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[209].Image = imageList.Images[2];
                        position = 210;
                        break;
                    }
                    if (pbList[position - 2].BackColor != Color.Blue)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[position - 2].Image = imageList.Images[2];
                        position -= 1;
                    }
                    break;
                case Direction.Right:
                    if (position == 210)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[189].Image = imageList.Images[3];
                        position = 190;
                        break;
                    }
                    if (pbList[position].BackColor != Color.Blue)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[position].Image = imageList.Images[3];
                        position += 1;
                    }
                    break;
                case Direction.Up:
                    if (pbList[position - 22].BackColor != Color.Blue)
                    {
                        pbList[position - 1].Image = null;
                        pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                        pbList[position - 22].Image = imageList.Images[4];
                        position -= 21;
                    }
                    break;
                default:
                    break;
            }
            playerPos = position;
            playerDir = direction;
            CheckForPoints();
        }

        public void CheckCollision(Object source, ElapsedEventArgs e)
        {
            foreach (int ghostPos in Mob.positions)
                if (position == ghostPos)
                {
                    collisionTimer.Elapsed -= CheckCollision;
                    collisionTimer.Stop();
                    collisionTimer.Dispose();
                    DialogResult dialogResult = MessageBox.Show("Talán legközelebb?", "Ez most nem sikerült. :(", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    else
                    { 
                        Application.Exit(); 
                        Environment.Exit(0);
                    }
                }
        }

        public override void AddPosToArray()
        {
            //Itt nem történik semmi, pusztán öröklődés miatt van itt.
        }
    }
}