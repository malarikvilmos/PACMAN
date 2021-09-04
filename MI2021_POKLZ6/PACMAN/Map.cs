using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PACMAN
{
    public partial class Map : UserControl
    {
        public static List<PictureBox> pbList;
        public int activePointCount;

        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            pbList = new List<PictureBox>();

            foreach (dynamic control in mapPanel.Controls)
                if (control.GetType() == typeof(PictureBox))
                    pbList.Add(control);
            pbList.Reverse();         

            foreach (var item in pbList)
                if (item.BackColor == Color.Black && item.Enabled)
                    item.Image = imageList.Images[0];
        }

        public void CheckForPoints()
        {
            activePointCount = 0;
            foreach (var item in pbList)
                if (item.BackColor == Color.Black)
                    ++activePointCount;

            if (activePointCount <= 10)
            {
                Player.collisionTimer.Stop();
                DialogResult dialogResult = MessageBox.Show("Újra?", "Siker!", MessageBoxButtons.YesNo);
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
    }
}
