using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EU4_Parse_Lib.DataClasses;
using Timer = System.Windows.Forms.Timer;

namespace EU4_Parse_Lib
{
    public partial class MainWindow : Form
    {
        private int moveAmount = 50;
        private int currentOffset = 0;
        public Rectangle displayRect;
        private int maxXOffset;
        private int maxYOffset;

        // Hover tooltip over Map
        private bool _isMouseOverPictureBox;
        private bool _isCooldownActive;
        private readonly Timer _cooldownTimer = new ();
        private Point _lastMouseLocation;

        public MainWindow()
        {
            InitializeComponent();
            var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
            var map = new Bitmap(combinedPath);
            Vars.Map = map;
            CalculateMaxOffsets();
            ResetDisplayRect();
            UpdateDisplayedImage();
            
            // Initialize the cooldown timer
            _cooldownTimer.Interval = 400; // Set the interval to 400 milliseconds
            _cooldownTimer.Tick += CooldownTimer_Tick!;
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
                foreach (var pro in Vars.SelectedProvinces)
                {
                    Util.SetProvinceBorder(pro, pro.Color);
                }
                Vars.SelectedProvinces.Clear();
                var p = Vars.Provinces[Vars.ColorIds[color]];
                Util.NextProvince(p);
                Vars.SelectedProvinces.Add(p);
            }
            else
            {
                var p = Vars.Provinces[Vars.ColorIds[color]];
                Vars.SelectedProvinces.Add(p);
                Util.SetProvinceBorder(p, Color.Black);
            }
            /*
            if (e.Button == MouseButtons.Middle)
            {
                
            }
            */

            // Clear references to objects that are no longer needed
            GC.Collect(); // Explicitly trigger garbage collection
        }

        private void Map_MouseHover(object sender, EventArgs e)
        {
            // Check if the event sender is a PictureBox (assuming "Map" is a PictureBox)
            if (sender is PictureBox pictureBox)
            {
                // Get the client coordinates of the mouse pointer within the PictureBox
                Point clientMouseLocation = pictureBox.PointToClient(Cursor.Position);

                // Extract the X and Y coordinates
                int mouseX = clientMouseLocation.X;
                int mouseY = clientMouseLocation.Y;

                Debug.WriteLine($"Mouse X: {mouseX} - Mouse Y: {mouseY}");

                // Now you can use mouseX and mouseY as needed
                // For example, display the coordinates in a label or perform some action
            }
        }


        private void Map_MouseEnter(object sender, EventArgs e)
        {
            _isMouseOverPictureBox = true;
        }

        private void Map_MouseLeave(object sender, EventArgs e)
        {
            _isMouseOverPictureBox = false;
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseOverPictureBox)
            {
                // Calculate the distance between the current and last mouse positions
                int distanceSquared = (_lastMouseLocation.X - e.X) * (_lastMouseLocation.X - e.X)
                                      + (_lastMouseLocation.Y - e.Y) * (_lastMouseLocation.Y - e.Y);

                // Check if the distance is significant and cooldown is not active
                if (distanceSquared > 4 && !_isCooldownActive)
                {
                    // Start the cooldown timer
                    _isCooldownActive = true;
                    _cooldownTimer.Interval = 400;
                    _cooldownTimer.Tick += CooldownTimer_Tick;
                    _cooldownTimer.Start();

                    // Update the last mouse location
                    _lastMouseLocation = e.Location;

                    // Your mouse hover behavior logic here
                    Debug.WriteLine($"Mouse X: {e.X} - Mouse Y: {e.Y}");
                }
            }
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            // Cooldown timer ticked, reset the cooldown
            _isCooldownActive = false;
            _cooldownTimer.Stop();
            _cooldownTimer.Tick -= CooldownTimer_Tick;
        }
    }
}
