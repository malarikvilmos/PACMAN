using System;
using System.Timers;

namespace PACMAN
{
    class Pinky : Mob, IGhost
    {
        static Timer ScatterTimer;
        static Timer ChaseTimer;
        static Timer PhaseTimer;
        public Pinky(int position)
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
            images = new int[] { 7, 9 };
            scatterTarget = 2;
            colID = 1;
            ghostList.Add(this);
        }

        public override void AddPosToArray()
        {
            Mob.positions[colID] = position;
        }

        public void Chase(Object source, ElapsedEventArgs e)
        {
            int target = 200;
            try {
                if (playerDir == Direction.Up)
                    target = playerPos - 21 * 4;
            }
            catch { target = playerPos - 21; }
            try{
                if (playerDir == Direction.Right)
                    target = playerPos + 4;
            }
            catch { target = playerPos; }
            try{
                if (playerDir == Direction.Down)
                    target = playerPos + 21 * 4;
            }
            catch { target = playerPos + 21; }
            try{
                if (playerDir == Direction.Left)
                    target = playerPos - 4;
            }
            catch { target = playerPos; }

            Move(FindNextStep(target));
        }

        public void Scatter(Object source, ElapsedEventArgs e)
        {
            Move(FindNextStep(scatterTarget));
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
            if (direction == Direction.Up) direction = Direction.Down;
            else if (direction == Direction.Down) direction = Direction.Up;
            else if (direction == Direction.Left) direction = Direction.Right;
            else if (direction == Direction.Right) direction = Direction.Left;
        }
    }
}
