using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public partial class MainWindow : Form
    {
        private int moveAmount = 50;
        private int currentOffset = 0;
        public Rectangle displayRect;
        private int maxXOffset;
        private int maxYOffset;

        public MainWindow()
        {
            InitializeComponent();
            var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
            var map = new Bitmap(combinedPath);
            Vars.Map = map;
            CalculateMaxOffsets();
            ResetDisplayRect();
            UpdateDisplayedImage();
        }

        private void CalculateMaxOffsets()
        {
            if (Vars.Map == null) 
                return;
            maxXOffset = Vars.Map.Width - Map.Width;
            maxYOffset = Vars.Map.Height - Map.Height;
        }
        private void ResetDisplayRect()
        {
            if (Vars.Map == null)
                return;
            var centerX = (Vars.Map.Width - Map.Width + 400) / 2;
            var centerY = (Vars.Map.Height - Map.Height - 1000) / 2;

            displayRect = new Rectangle(centerX, centerY, Map.Width, Map.Height);
        }
        private void UpdateDisplayedImage()
        {
            if (Vars.Map == null)
                return;
            var displayedBitmap = Vars.Map.Clone(displayRect, Vars.Map.PixelFormat);
            Map.Image = displayedBitmap;
        }
        public void MoveBitmap(int dx, int dy)
        {
            var newX = displayRect.X + dx;
            var newY = displayRect.Y + dy;

            // Ensure the new position stays within bounds
            newX = Math.Max(0, Math.Min(maxXOffset, newX));
            newY = Math.Max(0, Math.Min(maxYOffset, newY));

            displayRect.X = newX;
            displayRect.Y = newY;

            UpdateDisplayedImage();
        }
        private void RightButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(moveAmount * StepsizeMove.Value, 0);
        }
        private void DownButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(0, moveAmount * StepsizeMove.Value);
        }
        private void LeftButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(-moveAmount * StepsizeMove.Value, 0);
        }
        private void UpButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(0, -moveAmount * StepsizeMove.Value);
        }
        private void zoomTrackBar_ValueChanged(object sender, EventArgs e)
        {

        }
        public void SetPixel(int x, int y, Color newColor)
        {
            if (Vars.Map == null)
                return;
            if (x < 0 || x >= Vars.Map.Width || y < 0 || y >= Vars.Map.Height) 
                return;
            Vars.Map.SetPixel(x, y, newColor);
            UpdateDisplayedImage();
        }

        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right || Vars.Map == null)
                return;
            var x = e.X + displayRect.X;
            var y = e.Y + displayRect.Y;

            if (x < 0 || x >= Vars.Map.Width || y < 0 || y >= Vars.Map.Height)
                return;
            var color = Vars.Map.GetPixel(x, y);

            Debug.WriteLine($"Pixel Color: R = {color.R}, G = {color.G}, B = {color.B}");
            if (color.Equals(Color.FromArgb(255, 0, 0, 0)))
                return;

            if (e.Button == MouseButtons.Left)
            {
                foreach (var pro in Vars.SelecteProvinces)
                {
                    Util.SetProvinceBorder(pro, pro.color);
                }
                Vars.SelecteProvinces.Clear();
                var p = Vars.provinces[Vars.colorIds[color]];
                Util.NextProvince(p);
                Vars.SelecteProvinces.Add(p);
            }
            else
            {
                var p = Vars.provinces[Vars.colorIds[color]];
                Vars.SelecteProvinces.Add(p);
                Util.SetProvinceBorder(p, Color.Black);
            }

            // Clear references to objects that are no longer needed
            GC.Collect(); // Explicitly trigger garbage collection
        }

    }
}
