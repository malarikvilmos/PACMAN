using System;
using System.Windows.Forms;

namespace PACMAN
{
    public partial class mainForm : Form
    {
        Player player;
        Blinky blinky;
        Pinky pinky;
        Inky inky;
        Clyde clyde;
        bool canMove = true;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player = new Player();
            blinky = new Blinky(161);
            pinky = new Pinky(155);
            inky = new Inky(245);
            clyde = new Clyde(239);
            

        }

        private void bttnLeft_Click(object sender, EventArgs e)
        {
            player.Move(Direction.Left);
        }

        private void bttnUp_Click(object sender, EventArgs e)
        {
            player.Move(Direction.Up);
        }

        private void bttnRight_Click(object sender, EventArgs e)
        {
            player.Move(Direction.Right);
        }

        private void bttnDown_Click(object sender, EventArgs e)
        {
            player.Move(Direction.Down);
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (canMove)
                switch (e.KeyCode)
                {
                    case Keys.W:
                        player.Move(Direction.Up);
                        canMove = false;
                        break;
                    case Keys.A:
                        player.Move(Direction.Left);
                        canMove = false;
                        break;
                    case Keys.S:
                        player.Move(Direction.Down);
                        canMove = false;
                        break;
                    case Keys.D:
                        player.Move(Direction.Right);
                        canMove = false;
                        break;
                }
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            canMove = true;
        }
    }
}
