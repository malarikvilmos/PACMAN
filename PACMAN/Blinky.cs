using System;
using System.Timers;

namespace PACMAN
{
    class Blinky : Mob, IGhost
    {
        static Timer ScatterTimer;
        static Timer ChaseTimer;
        static Timer PhaseTimer;
        public Blinky(int position)
        {
            ScatterTimer = new Timer();
            ScatterTimer.Interval = 300;
            ScatterTimer.Start();
            ScatterTimer.Elapsed += Scatter;

            ChaseTimer = new Timer();
            ChaseTimer.Interval = 300;
            ChaseTimer.Enabled = false;
            ChaseTimer.Elapsed += Chase;

            PhaseTimer = new Timer();
            PhaseTimer.Interval = 4000;
            PhaseTimer.Start();
            PhaseTimer.Elapsed += SwitchModes;

            this.position = position;
            images = new int[] { 5, 9 };
            scatterTarget = 20;
            Move(position, 5);
            colID = 0;
            ghostList.Add(this);
        }

        public override void AddPosToArray()
        {
            Mob.positions[colID] = position;
        }

        public void Chase(Object source, ElapsedEventArgs e)
        {
            Move(FindNextStep(playerPos));
            blinkyPos = position;
        }

        public void Scatter(Object source, ElapsedEventArgs e)
        {
            Move(FindNextStep(scatterTarget));
            blinkyPos = position;
        }

        public void SwitchModes(Object source, ElapsedEventArgs e)
        {
            if (ScatterTimer.Enabled)
            {
                ScatterTimer.Enabled = false;
                ChaseTimer.Enabled = true;
                PhaseTimer.Interval = 20000;
            }
            else if (ChaseTimer.Enabled)
            {
                ScatterTimer.Enabled = true;
                ChaseTimer.Enabled = false;
                PhaseTimer.Interval = 4000;
            }
            if      (direction == Direction.Up)    direction = Direction.Down;
            else if (direction == Direction.Down)  direction = Direction.Up;
            else if (direction == Direction.Left)  direction = Direction.Right;
            else if (direction == Direction.Right) direction = Direction.Left;
        }
    }
}
