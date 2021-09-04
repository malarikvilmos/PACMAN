using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace PACMAN
{
    public enum Direction { Up, Left, Down, Right };

    public abstract class Mob : Map
    {
        public static int playerPos;
        public static Direction playerDir;
        public static int blinkyPos;
        public static int[] positions = new int[4];
        public static List<Mob> ghostList = new List<Mob>();

        protected int position;
        protected Direction direction;
        protected int[] images;
        protected int scatterTarget;
        protected int colID;
        protected int target;
        Image tempImg = null;      

        public abstract void AddPosToArray();

        new public void Move(int coords, int imgNum)
        {
            position = coords;
            pbList[coords - 1].Image = imageList.Images[imgNum];
            direction = Direction.Up;
        }

        new public virtual void Move(Direction direction)
        {
            pbList[position - 1].Image = tempImg;
      
            switch (direction)
            {
                case Direction.Down:
                    if (pbList[position + 20].BackColor != Color.Blue && this.direction != Direction.Up)
                    {
                        tempImg = pbList[position + 20].Image;
                        this.direction = direction;
                        pbList[position + 20].Image = imageList.Images[images[0]];
                        position += 21;
                    } 
                    break;
                case Direction.Left:
                    if (position == 190)
                    {
                        tempImg = pbList[position].Image;
                        this.direction = direction;
                        pbList[209].Image = imageList.Images[images[0]];
                        position = 210;
                        break;
                    }
                    if (pbList[position - 2].BackColor != Color.Blue && this.direction != Direction.Right)
                    {
                        tempImg = pbList[position - 2].Image;
                        this.direction = direction;
                        pbList[position - 2].Image = imageList.Images[images[0]];
                        position -= 1;
                    }
                    break;
                case Direction.Right:
                    if (position == 210)
                    {
                        tempImg = pbList[position].Image;
                        this.direction = direction;
                        pbList[189].Image = imageList.Images[images[0]];
                        position = 190;
                        break;
                    }
                    if (pbList[position].BackColor != Color.Blue && this.direction != Direction.Left)
                    {
                        tempImg = pbList[position].Image;
                        this.direction = direction;
                        pbList[position].Image = imageList.Images[images[0]];
                        position += 1;
                    }
                    break;
                case Direction.Up:
                    if (pbList[position - 22].BackColor != Color.Blue && this.direction != Direction.Down)
                    {
                        tempImg = pbList[position - 22].Image;
                        this.direction = direction;
                        pbList[position - 22].Image = imageList.Images[images[0]];
                        position -= 21;
                    }
                    break;

            }
            AddPosToArray();

            //Ha 2 szellem egymásba menne, ez meggátolja azt, hogy "duplázódjon" a sprite-juk. Viszont ha pont volt a helyükön akkor az eltűnik.
            for (int i = 0; i < 4; ++i)
                if (position == positions[i] && i != colID)
                {
                    pbList[position - 1].BackColor = SystemColors.ActiveCaptionText;
                    tempImg = null;
                }
                    
        }

        public double CalculateDistance(int position, int target)
        {
            try
            {
                double num1 = Math.Pow(Math.Abs(pbList[position - 1].Location.X - pbList[target - 1].Location.X), 2);
                double num2 = Math.Pow(Math.Abs(pbList[position - 1].Location.Y - pbList[target - 1].Location.Y), 2);
                return Math.Sqrt(num1 + num2);
            }
            catch
            {
                return 0;
            }
        }

        public Direction FindNextStep(int target)
        {
            double upDist, downDist, leftDist, rightDist;
            List<double> steps = new List<double>();

            if (pbList[position - 22].BackColor != Color.Blue && direction != Direction.Down)
                upDist = CalculateDistance(position - 21, target);
            else upDist = 99999;
            steps.Add(upDist);

            if (pbList[position - 2].BackColor != Color.Blue && direction != Direction.Right)
                leftDist = CalculateDistance(position - 1, target);
            else leftDist = 99999;
            steps.Add(leftDist);

            if (pbList[position + 20].BackColor != Color.Blue && direction != Direction.Up)
                downDist = CalculateDistance(position + 21, target);
            else downDist = 99999;
            steps.Add(downDist);                 

            if (pbList[position].BackColor != Color.Blue && direction != Direction.Left)
                rightDist = CalculateDistance(position + 1, target);
            else rightDist = 99999;
            steps.Add(rightDist);

            Direction nextpos = (Direction)steps.Select((item, index) => (item, index)).Min().index;
            
            if (position == 210 && direction == Direction.Right) nextpos = Direction.Right;
            if (position == 190 && direction == Direction.Left) nextpos = Direction.Left;

            return nextpos;
        }
    }
}
