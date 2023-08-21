using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using EU4_Parse_Lib.DataClasses;
using Timer = System.Windows.Forms.Timer;

namespace EU4_Parse_Lib
{
    public partial class MainWindow : Form
    {
        private const int MoveAmount = 50;
        public Rectangle DisplayRect;
        private int _maxXOffset;
        private int _maxYOffset;

        // Hover tooltip over Map
        private bool _isMouseOverPictureBox;
        private readonly Timer _cooldownTimer = new();
        private Point _lastMouseLocation;
        private Color _tooltipColor = Color.FromArgb(255, 0, 0, 0);

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
            _maxXOffset = Vars.Map.Width - Map.Width;
            _maxYOffset = Vars.Map.Height - Map.Height;
        }
        private void ResetDisplayRect()
        {
            if (Vars.Map == null)
                return;
            var centerX = (Vars.Map.Width - Map.Width + 400) / 2;
            var centerY = (Vars.Map.Height - Map.Height - 1000) / 2;

            DisplayRect = new Rectangle(centerX, centerY, Map.Width, Map.Height);
        }
        private void UpdateDisplayedImage()
        {
            if (Vars.Map == null)
                return;
            var displayedBitmap = Vars.Map.Clone(DisplayRect, Vars.Map.PixelFormat);
            Map.Image = displayedBitmap;
        }
        public void MoveBitmap(int dx, int dy)
        {
            var newX = DisplayRect.X + dx;
            var newY = DisplayRect.Y + dy;

            // Ensure the new position stays within bounds
            newX = Math.Max(0, Math.Min(_maxXOffset, newX));
            newY = Math.Max(0, Math.Min(_maxYOffset, newY));

            DisplayRect.X = newX;
            DisplayRect.Y = newY;

            UpdateDisplayedImage();
        }
        private void RightButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(MoveAmount * StepsizeMove.Value, 0);
        }
        private void DownButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(0, MoveAmount * StepsizeMove.Value);
        }
        private void LeftButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(-MoveAmount * StepsizeMove.Value, 0);
        }
        private void UpButton_Click(object sender, EventArgs e)
        {
            MoveBitmap(0, -MoveAmount * StepsizeMove.Value);
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
            var x = e.X + DisplayRect.X;
            var y = e.Y + DisplayRect.Y;

            if (x < 0 || x >= Vars.Map.Width || y < 0 || y >= Vars.Map.Height)
                return;
            var color = Vars.Map.GetPixel(x, y);

            //Debug.WriteLine($"Pixel Color: R = {color.R}, G = {color.G}, B = {color.B}");
            if (color.Equals(Color.FromArgb(255, 0, 0, 0)))
                return;

            if (e.Button == MouseButtons.Left)
            {
                var p = Vars.Provinces[Vars.ColorIds[color]];
                if (Vars.SelectedProvinces.Count == 1 && Vars.SelectedProvinces[0].Equals(p))
                {
                    Util.SetProvinceBorder(p, p.Color);
                    Vars.SelectedProvinces.Clear();
                    return;
                }
                foreach (var pro in Vars.SelectedProvinces)
                {
                    Util.SetProvinceBorder(pro, pro.Color);
                }
                Vars.SelectedProvinces.Clear();
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
                // I cant remember what I wanted to put here
            }
            */

            // Clear references to objects that are no longer needed
            GC.Collect(); // Explicitly trigger garbage collection
        }

        private void Map_MouseHover(object sender, EventArgs e)
        {
            if (sender is not PictureBox pictureBox) return;
            var clientMouseLocation = pictureBox.PointToClient(Cursor.Position);

            var mouseX = clientMouseLocation.X;
            var mouseY = clientMouseLocation.Y;

            GenerateMouseTooltip(new Point(mouseX, mouseY));
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
            if (!_isMouseOverPictureBox) return;
            if (_lastMouseLocation == new Point(e.X, e.Y)) return;

            _cooldownTimer.Interval = 400;
            _cooldownTimer.Tick += CooldownTimer_Tick!;
            _cooldownTimer.Start();

            _lastMouseLocation = e.Location;

            GenerateMouseTooltip(new Point(e.X, e.Y));
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            _cooldownTimer.Stop();
            _cooldownTimer.Tick -= CooldownTimer_Tick!;
        }

        private void GenerateMouseTooltip(Point p)
        {
            var color = Vars.Map!.GetPixel(p.X + DisplayRect.X, p.Y + DisplayRect.Y);
            if (color == _tooltipColor || color == Color.FromArgb(255, 0, 0, 0)) return;
            var id = Vars.ColorIds[color];
            var area = Vars.Provinces[id].Area;
            // TODO expand when more data is available.

            //Debug.WriteLine($"{name} [{id}]\nArea: {area}");

            _tt.SetToolTip(Vars.MainWindow!.Map, $"{Loading.GetLoc($"PROV{id}")} [{id}]\nArea: {area}");
            _tooltipColor = color;
        }

        private void Help_MenuItem_Click(object sender, EventArgs e)
        {
            Vars.WebBrowserForm = Gui.ShowForm<WebBrowserForm>();
            Util.ConvertAndDisplayInWebBrowser(Path.Combine(Vars.DataPath, "README.md"));
        }
    }
}
