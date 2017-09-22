using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using Mode.Halo2;
using System;
using System.Drawing;

namespace Mode
{
    public partial class Mode : AbideTool
    {
        private Vector3 vec = Vector3.Zero;

        public Mode()
        {
            InitializeComponent();

            //Set
            alphaBox.Image = new Bitmap(150, 150);
            betaBox.Image = new Bitmap(150, 150);
            gammaBox.Image = new Bitmap(150, 150);
            Normal16 test = 0.8f;
            Console.WriteLine(test);
        }

        private void Vector_ValueChanged(object sender, EventArgs e)
        {
            //Setup Vector
            vec.Magnitude = magnitudeTrackBar.Value / 150f * 100f;
            vec.Alpha = alphaTrackBar.Value / 100f;
            vec.Beta = betaTrackBar.Value / 100f;
            vec.Gamma = gammaTrackBar.Value / 100f;
            vectorLabel.Text = vec.ToString();

            //Draw
            using (Pen vector = new Pen(Color.Black, 2f))
            {
                //Prepare
                PointF pt = PointF.Empty;

                //Alpha
                using (Graphics g = Graphics.FromImage(alphaBox.Image))
                {
                    pt = new PointF((float)(Math.Cos(vec.Z) * vec.Magnitude), -(float)(Math.Sin(vec.X) * vec.Magnitude));
                    g.Clear(Color.CornflowerBlue);
                    g.DrawLine(vector, PointF.Empty, pt);
                }

                //Beta
                using (Graphics g = Graphics.FromImage(betaBox.Image))
                {
                    pt = new PointF((float)(Math.Cos(vec.Z) * vec.Magnitude), -(float)(Math.Sin(vec.Y) * vec.Magnitude));
                    g.Clear(Color.CornflowerBlue);
                    g.DrawLine(vector, PointF.Empty, pt);
                }

                //Gamma
                using (Graphics g = Graphics.FromImage(gammaBox.Image))
                {
                    pt = new PointF((float)(Math.Cos(vec.X) * vec.Magnitude), -(float)(Math.Sin(vec.Y) * vec.Magnitude));
                    g.Clear(Color.CornflowerBlue);
                    g.DrawLine(vector, PointF.Empty, pt);
                }
            }

            //Refresh
            alphaBox.Refresh();
            betaBox.Refresh();
            gammaBox.Refresh();
        }

        private void Mode_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Check
            if(SelectedEntry.Root == HaloTags.mode)
            {
                ModelTagGroup tagGroup = new ModelTagGroup(SelectedEntry);
            }
        }
    }
}
